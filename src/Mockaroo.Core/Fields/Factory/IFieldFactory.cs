namespace Gigobyte.Mockaroo.Fields.Factory
{
    public interface IFieldFactory<T>
    {
        IField CreateInstance(T value);
    }
}