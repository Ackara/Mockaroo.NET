namespace Acklann.Mockaroo.Fields
{
    public partial class TemplateField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public TemplateField(string name, string value) : base(name)
        {
            Value = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <remarks>
        /// Templates allow you to incorporate values from other fields by surrounding field names with {braces}.
        /// For example, let's say you have a "first_name" field and a "last_name" field. The following template will generate a formatted name where last name comes first: {last_name}, {first_name}
        /// </remarks>
        public string Value { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"value\":\"{Value}\"}}";
        }
    }
}