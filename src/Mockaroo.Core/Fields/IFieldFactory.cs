namespace Gigobyte.Mockaroo.Fields
{
    public interface IFieldFactory
    {
        IField CreateInstance(DataType dataType);
    }
}