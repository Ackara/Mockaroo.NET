namespace Gigobyte.Mockaroo.Fields
{
    public interface IFieldFactory
    {
        IField CreateInstance(System.Type type);

        IField CreateInstance(DataType dataType);
    }
}