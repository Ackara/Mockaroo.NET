using Gigobyte.Mockaroo.Fields;
using System;
using System.Reflection;

namespace Gigobyte.Mockaroo.Serialization
{
    public class ClrSchemaSerializer : ISchemaSerializer
    {
        public ClrSchemaSerializer() : this(new FieldFactory())
        {
        }

        public ClrSchemaSerializer(IFieldFactory factory)
        {
            _factory = factory;
        }

        public Schema ConvertToSchema(object value)
        {
            Type type = value as Type;
            if (type != null)
            {
                var schema = new Schema();
                foreach (var propertyInfo in type.GetRuntimeProperties())
                {
                    BuildSchema(schema, propertyInfo);
                }

                return schema;
            }
            else throw new ArgumentException($"This object can only convert objects of type '{nameof(System)}.{nameof(Type)}'.", nameof(value));
        }

        public object Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        

        #region Private Members

        private readonly IFieldFactory _factory;

        private void BuildSchema(Schema schema, PropertyInfo type)
        {
            
        }

        #endregion Private Members
    }
}