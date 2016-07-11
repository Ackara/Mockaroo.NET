using Gigobyte.Mockaroo.Fields;
using System;
using System.Collections;
using System.Reflection;

namespace Gigobyte.Mockaroo.Serialization
{
    public class ClrSchemaSerializer : ISchemaSerializer
    {
        public ClrSchemaSerializer() : this(new Fields.Factory.ClrFactory())
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
            throw new NotImplementedException();
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
                if (rootType == property.PropertyType) { continue; }  /* A guard against an infinite recursive loop */

                if (property.CanRead && property.CanWrite)
                {
                    Type elementType;

                    switch (GetBuildPath(property.PropertyType, out elementType))
                    {
                        default:
                        case BuildPath.Basic:
                            //field = property.PropertyType.AsField();
                            field = _factory.CreateInstance(property.PropertyType);
                            field.Name = (parentPropertyName + property.Name);
                            schema.Add(field);
                            break;

                        case BuildPath.Complex:
                            parentPropertyName = $"{parentPropertyName}{property.Name}.";
                            BuildSchema(schema, property.PropertyType, rootType, parentPropertyName);
                            parentPropertyName = string.Empty;
                            break;

                        case BuildPath.BasicCollection:
                            //field = property.PropertyType.AsField();
                            field = _factory.CreateInstance(property.PropertyType);
                            field.Name = (parentPropertyName + property.Name);
                            schema.Add(field);

                            field = elementType.AsField();
                            field.Name = $"{parentPropertyName}{property.Name}.{elementType.Name}";
                            schema.Add(field);
                            break;

                        case BuildPath.ComplexCollection:
                            //field = property.PropertyType.AsField();
                            field = _factory.CreateInstance(property.PropertyType);
                            field.Name = (parentPropertyName + property.Name);
                            schema.Add(field);

                            parentPropertyName = $"{parentPropertyName}{property.Name}.";
                            BuildSchema(schema, elementType, rootType, parentPropertyName);
                            parentPropertyName = string.Empty;
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