using Gigobyte.Mockaroo.Fields;
using System;
using System.Reflection;

namespace Gigobyte.Mockaroo.Serialization
{
    public class ClrSchemaSerializer : ISchemaAdapter
    {
        public ClrSchemaSerializer()
        {
        }

        public Schema ConvertToSchema(object value)
        {
            Type type = value as Type;
            if (type != null)
            {
                var schema = new Schema();
                BuildSchema(schema, type);

                return schema;
            }
            else throw new ArgumentException($"This object can only convert objects of type '{nameof(System)}.{nameof(Type)}'.", nameof(value));
        }

        public object Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        #region Private Members

        private void BuildSchema(Schema schema, Type type, string parentPropertyName = "")
        {
            IField field;
            foreach (var propertyInfo in type.GetRuntimeProperties())
            {
                if (type == propertyInfo.PropertyType) { continue; }  /* A guard against an infinite recursive loop */

                if (propertyInfo.CanWrite && propertyInfo.CanWrite)
                {
                    if (propertyInfo.PropertyType.IsBasicType())
                    {
                        field = propertyInfo.PropertyType.AsField();
                        field.Name = parentPropertyName + propertyInfo.Name;

                        schema.Add(field);
                    }
                    else
                    {
                        parentPropertyName = $"{parentPropertyName}{propertyInfo.Name}.";
                        BuildSchema(schema, propertyInfo.PropertyType, parentPropertyName);
                    }
                }
            }
        }

        #endregion Private Members
    }
}