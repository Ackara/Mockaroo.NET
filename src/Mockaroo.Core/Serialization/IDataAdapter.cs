namespace Gigobyte.Mockaroo.Serialization
{
    /// <summary>
    /// Defines methods to transform the data exported from http://mockaroo.com into a specified type.
    /// </summary>
    public interface IDataAdapter
    {
        /// <summary>
        /// Transforms the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="arg">The argument.</param>
        object Transform(byte[] data, object arg);
    }
}