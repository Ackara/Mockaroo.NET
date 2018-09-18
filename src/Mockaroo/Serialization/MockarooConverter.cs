using System;
using System.IO;

namespace Acklann.Mockaroo.Serialization
{
    public class MockarooConverter
    {
        public MockarooConverter()
        {

        }

        public string ToJson(Schema schema)
        {
            throw new System.NotImplementedException();
        }

        public Schema ToSchema<T>()
        {
            throw new System.NotImplementedException();
        }

        public Schema ToSchema(Type type)
        {
            throw new System.NotImplementedException();
        }

        public T Deserialize<T>(Stream stream)
        {
            return (T)Deserialize(stream, typeof(T));
        }

        public object Deserialize(Stream stream, Type type)
        {
            throw new System.NotImplementedException();
        }

        public object Deserialize(string json, Type type)
        {
            throw new System.NotImplementedException();
        }

        #region Private Members

        // Type => Schema
        // ====================================================================================================

        // Json => Object
        // ====================================================================================================

        #endregion Private Members
    }
}