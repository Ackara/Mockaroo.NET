namespace Gigobyte.Mockaroo.Fields
{
    public interface IField
    {
        /// <summary>
        /// Gets or sets the identifier of this field.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets the Mockaroo data type.
        /// </summary>
        /// <value>
        /// The data type.
        /// </value>
        DataType Type { get; }

        /// <summary>
        /// Converts the value of this instance to its JSON representation.
        /// </summary>
        /// <returns>This instance JSON representation.</returns>
        string ToJson();
    }
}