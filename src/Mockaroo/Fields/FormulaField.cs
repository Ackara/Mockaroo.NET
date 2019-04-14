namespace Acklann.Mockaroo.Fields
{
    public partial class FormulaField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormulaField"/> class. see &quot;https://mockaroo.com/api/docs#type_formula&quot;
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The Ruby code.</param>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public FormulaField(string name, string value) : base(name)
        {
            Value = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the Ruby code to generate data based on custom logic
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
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