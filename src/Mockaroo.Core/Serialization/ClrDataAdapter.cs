using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Gigobyte.Mockaroo.Serialization
{
    /// <summary>
    /// Provides methods to transform the data exported from http://mockaroo.com into a CLR type.
    /// </summary>
    /// <seealso cref="IDataAdapter" />
    public class ClrDataAdapter : IDataAdapter
    {
        /// <summary>
        /// Transforms the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>A collection of the specified type.</returns>
        public IEnumerable<T> Transform<T>(byte[] data)
        {
            return (Transform(data, typeof(T))).Cast<T>();
        }

        /// <summary>
        /// Transforms the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="arg">The argument.</param>
        /// <returns>A collection of the specified type.</returns>
        /// <exception cref="ArgumentException"></exception>
        public object Transform(byte[] data, object arg)
        {
            if (typeof(Type) == arg.GetType())
            {
                return Transform(data, (Type)arg);
            }
            else throw new ArgumentException($"'{nameof(arg)}' must be a System.Type.", nameof(arg));
        }

        /// <summary>
        /// Transforms the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="type">The .NET type.</param>
        /// <returns>A collection of the specified type.</returns>
        public object[] Transform(byte[] data, Type type)
        {
            using (var reader = new JsonTextReader(new StreamReader(new MemoryStream(data))))
            {
                var array = JArray.Load(reader);
                var collection = new object[array.Count];
                int index = 0;

                foreach (JObject obj in array)
                {
                    object instance = Activator.CreateInstance(type);
                    BuildObject(type, obj, instance, type.Name);
                    collection[index++] = instance;
                }

                return collection;
            }
        }

        #region Private Members

        private void BuildObject(Type type, JToken json, object instance, string classHierarchy = "")
        {
            foreach (var property in type.GetRuntimeProperties())
            {
                if (classHierarchy.Contains(property.PropertyType.Name)) continue; /* A guard against an infinite recursive loop */

                if (property.CanRead && property.CanWrite)
                {
                    Type collectionElementType;
                    object propertyValue, childInstance;

                    switch (GetBuildPath(property.PropertyType, out collectionElementType))
                    {
                        default:
                        case BuildPath.Basic:
                            propertyValue = property.PropertyType.GetTypeInfo().IsEnum ?
                                propertyValue = Enum.Parse(property.PropertyType, json[property.Name].Value<string>()) :
                                propertyValue = Convert.ChangeType(json[property.Name].Value<string>(), property.PropertyType);

                            property.SetValue(instance, propertyValue);
                            break;

                        case BuildPath.Complex:
                            childInstance = Activator.CreateInstance(property.PropertyType);
                            BuildObject(property.PropertyType, json[property.Name], childInstance, $"{classHierarchy}.{property.PropertyType.Name}");
                            property.SetValue(instance, childInstance);
                            break;

                        case BuildPath.BasicCollection:
                            propertyValue = string.Join(",", json[property.Name].Select(x => x[collectionElementType.Name].Value<string>()));
                            propertyValue = JsonConvert.DeserializeObject($"[{propertyValue}]", property.PropertyType);
                            property.SetValue(instance, propertyValue);
                            break;

                        case BuildPath.ComplexCollection:
                            int index = 0;
                            var collection = new object[json[property.Name].Count()];

                            foreach (var item in json[property.Name])
                            {
                                childInstance = Activator.CreateInstance(collectionElementType);
                                BuildObject(collectionElementType, item, childInstance, $"{classHierarchy}.{collectionElementType.Name}");
                                collection[index++] = childInstance;
                            }

                            propertyValue = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(collection), property.PropertyType);
                            property.SetValue(instance, propertyValue);
                            break;
                    }
                }
            }
        }

        private BuildPath GetBuildPath(Type propertyType, out Type elementType)
        {
            bool isCollection = false;
            TypeInfo typeInfo = propertyType.GetTypeInfo();

            if (propertyType != typeof(string) && typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(typeInfo))
            {
                isCollection = true;
                elementType = GetCollectionElementType(propertyType);
                typeInfo = elementType.GetTypeInfo();
            }
            else elementType = propertyType;

            if (typeInfo.IsEnum)
            {
                return isCollection ? BuildPath.BasicCollection : BuildPath.Basic;
            }
            else switch (typeInfo.Name)
                {
                    default:
                        return isCollection ? BuildPath.ComplexCollection : BuildPath.Complex;

                    case nameof(Boolean):

                    case nameof(Byte):
                    case nameof(SByte):

                    case nameof(Int16):
                    case nameof(UInt16):

                    case nameof(Int32):
                    case nameof(UInt32):

                    case nameof(Int64):
                    case nameof(UInt64):

                    case nameof(Single):
                    case nameof(Double):
                    case nameof(Decimal):

                    case nameof(Char):
                    case nameof(String):

                    case nameof(TimeSpan):
                    case nameof(DateTime):
                        return isCollection ? BuildPath.BasicCollection : BuildPath.Basic;
                }
        }

        private Type GetCollectionElementType(Type collectionType)
        {
            if (collectionType.IsArray)
            {
                TypeInfo info = collectionType.GetTypeInfo();
                return Type.GetType(string.Concat(collectionType.FullName.TrimEnd('[', ']'), ",", info.Assembly.FullName));
            }
            else
            {
                return collectionType.GenericTypeArguments[0];
            }
        }

        private enum BuildPath
        {
            Basic,
            Complex,
            BasicCollection,
            ComplexCollection
        }

        #endregion Private Members
    }
}