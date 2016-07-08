namespace Gigobyte.Mockaroo.Serialization
{
    public interface ISchemaAdapter
    {
        Schema ConvertToSchema(object value);

        object Deserialize(byte[] data);
    }
}