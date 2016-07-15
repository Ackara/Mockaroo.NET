namespace Gigobyte.Mockaroo.Serialization
{
    /// <summary>
    /// Defines a method to create a <see cref="Schema"/> instance from a resource.
    /// </summary>
    public interface ISchemaConverter
    {
        /// <summary>
        /// Creates a new <see cref="Schema"/> instance using the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Schema"/>.</returns>
        Schema Convert(object value);
    }
}