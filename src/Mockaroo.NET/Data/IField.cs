namespace Gigobyte.Mockaroo
{
    public interface IFieldInfo
    {
        string Name { get; set; }

        DataType Type { get; }

        string GetJson();
    }
}