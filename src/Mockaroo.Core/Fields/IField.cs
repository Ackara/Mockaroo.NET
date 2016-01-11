namespace Gigobyte.Mockaroo.Fields
{
    public interface IField
    {
        string Name { get; set; }
        
        DataType Type { get; }

        string ToJson();
    }
}