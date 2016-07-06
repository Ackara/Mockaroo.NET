using System;

namespace Gigobyte.Mockaroo.Converters
{
    public interface ISchemaAdapter<T>
    {
        Schema ConvertToSchema(T obj);
    }
}