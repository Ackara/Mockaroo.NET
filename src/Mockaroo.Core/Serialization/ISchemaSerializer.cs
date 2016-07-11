using System;

namespace Gigobyte.Mockaroo.Serialization
{
    public interface ISchemaSerializer
    {
        Schema ConvertToSchema(object value);

        object ReadObject(Type type, byte[] data);

        T ReadObject<T>(byte[] data);
    }
}