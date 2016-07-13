using Gigobyte.Mockaroo.Fields;
using Gigobyte.Mockaroo.Fields.Factory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Gigobyte.Mockaroo.Serialization
{
    public class ClrSchemaSerializer : ISchemaSerializer
    {
        public ClrSchemaSerializer() : this(new ClrFactory())
        {
        }

        public ClrSchemaSerializer(IFieldFactory<Type> factory)
        {
            _factory = factory;
        }

        public Schema ConvertToSchema(object value)
        {
            Type type = (value as Type);
            if (type != null)
            {
                var schema = new Schema();
                BuildSchema(schema, type, type);

                return schema;
            }
            else throw new ArgumentException($"This object can only convert objects of type '{nameof(System)}.{nameof(Type)}'.", nameof(value));
        }

        public object[] ReadObject(Type type, byte[] bytes)
        {
            using (var reader = new JsonTextReader(new StreamReader(new MemoryStream(bytes))))
            {
                var array = JArray.Load(reader);
                var data = new object[array.Count];
                int index = 0;

                foreach (JObject obj in array)
                {
                    object instance = Activator.CreateInstance(type);
                    BuildObject(type, obj, instance, type.Name);
                    data[index++] = instance;
                }

                return data;
            }
        }

        public T[] ReadObject<T>(byte[] data)
        {
            return ReadObject(typeof(T), data).Cast<T>().ToArray();
        }

        #region Private Members

        private IFieldFactory<Type> _factory;

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

        private void BuildSchema(Schema schema, Type type, Type rootType, string parentPropertyName = "")
        {
            IField field;
            foreach (var property in type.GetRuntimeProperties())
            {
                if (rootType == property.PropertyType || type == property.PropertyType) { continue; }  /* A guard against an infinite recursive loop */

                if (property.CanRead && property.CanWrite)
                {
                    Type elementType;

                    switch (GetBuildPath(property.PropertyType, out elementType))
                    {
                        default:
                        case BuildPath.Basic:
                            field = _factory.CreateInstance(property.PropertyType);
                            field.Name = (parentPropertyName + property.Name);
                            schema.Add(field);
                            break;

                        case BuildPath.Complex:
                            parentPropertyName = $"{parentPropertyName}{property.Name}.";
                            BuildSchema(schema, property.PropertyType, rootType, parentPropertyName);
                            parentPropertyName = Backtrack(parentPropertyName);
                            break;

                        case BuildPath.BasicCollection:
                            field = _factory.CreateInstance(property.PropertyType);
                            field.Name = (parentPropertyName + property.Name);
                            schema.Add(field);

                            field = _factory.CreateInstance(elementType);
                            field.Name = $"{parentPropertyName}{property.Name}.{elementType.Name}";
                            schema.Add(field);
                            break;

                        case BuildPath.ComplexCollection:
                            field = _factory.CreateInstance(property.PropertyType);
                            field.Name = (parentPropertyName + property.Name);
                            schema.Add(field);

                            parentPropertyName = $"{parentPropertyName}{property.Name}.";
                            BuildSchema(schema, elementType, rootType, parentPropertyName);
                            parentPropertyName = Backtrack(parentPropertyName);
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

        private string Backtrack(string value)
        {
            string[] parts = value.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            int index = parts.Length - 2;
            return ((index < 0) ? string.Empty : (parts[index] + '.'));
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