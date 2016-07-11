using Gigobyte.Mockaroo.Fields;
using System;
using System.Collections;
using System.Reflection;

namespace Gigobyte.Mockaroo.Serialization
{
    public class ClrSchemaSerializer : ISchemaSerializer
    {
        public ClrSchemaSerializer() : this(null)
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
                BuildSchema(schema, type);

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

        private void BuildSchema(Schema schema, Type type, string parentPropertyName = "")
        {
            IField field;
            foreach (var property in type.GetRuntimeProperties())
            {
                if (type == property.PropertyType) { continue; }  /* A guard against an infinite recursive loop */

                if (property.CanRead && property.CanWrite)
                {
                    parentPropertyName = $"{parentPropertyName}{property.Name}.";

                    switch (GetBuildPath(property.PropertyType))
                    {
                        default:
                        case BuildPath.Basic:
                            field = property.PropertyType.AsField();
                            schema.Add(field);
                            break;

                        case BuildPath.Complex:
                            BuildSchema(schema, property.PropertyType, parentPropertyName);
                            parentPropertyName = string.Empty;
                            break;

                        case BuildPath.BasicCollection:

                            break;

                        case BuildPath.ComplexCollection:

                            parentPropertyName = string.Empty;
                            break;
                    }
                }
            }
        }

        private Type GetCollectionElementType(Type collectionType)
        {
            if (collectionType.IsArray)
            {
                return Type.GetType(collectionType.FullName.TrimEnd('[', ']'));
            }
            else
            {
                return collectionType.GenericTypeArguments[0];
            }
        }

        private BuildPath GetBuildPath(Type propertyType)
        {
            bool isCollection = false;
            TypeInfo typeInfo = propertyType.GetTypeInfo();

            if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(typeInfo))
            {
                isCollection = true;
                typeInfo = GetCollectionElementType(propertyType).GetTypeInfo();
            }

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