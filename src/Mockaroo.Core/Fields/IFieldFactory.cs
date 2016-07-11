namespace Gigobyte.Mockaroo.Fields
{
    public interface IFieldFactory<T>
    {
        IField CreateInstance(T value);
    }
}