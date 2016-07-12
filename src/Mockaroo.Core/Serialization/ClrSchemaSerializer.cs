using Gigobyte.Mockaroo.Fields;
using Gigobyte.Mockaroo.Fields.Factory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

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

        public object ReadObject(Type type, byte[] data)
        {
            using (var reader = new JsonTextReader(new StreamReader(new MemoryStream(data))))
            {
                var instance = Activator.CreateInstance(type);
                var array = JArray.Load(reader);

                foreach (var obj in array)
                {
                   return JsonConvert.DeserializeObject(obj.ToString(), type);
                }

                throw new System.NotImplementedException();
                //return instance;
            }
        }

        public T ReadObject<T>(byte[] data)
        {
            return (T)ReadObject(typeof(T), data);
        }

        #region Private Members

        private IFieldFactory<Type> _factory;

        private void BuildSchema(Schema schema, Type type, Type rootType, string parentPropertyName = "")
        {
            IField field;
            foreach (var property in type.GetRuntimeProperties())
            {
                if (rootType == property.PropertyType || type == property.PropertyType ) { continue; }  /* A guard against an infinite recursive loop */

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
            int index = parts.Length -2;
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