namespace Gigobyte.Mockaroo.Serialization
{
    public interface ISchemaSerializer
    {
        Schema ConvertToSchema(object value);

        object Deserialize(byte[] data);
    }
}