namespace Gigobyte.Mockaroo.Fields.Factory
{
    /// <summary>
    /// Defines a method to create an <see cref="IField"/> from a specified value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFieldFactory<T>
    {
        /// <summary>
        /// Creates a <see cref="IField"/> instance from the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        IField CreateInstance(T value);
    }
}