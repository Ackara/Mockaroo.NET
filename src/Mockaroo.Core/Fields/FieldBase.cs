namespace Gigobyte.Mockaroo.Fields
{
    /// <summary>
    /// Base class for <see cref="IField"/>.
    /// </summary>
    /// <seealso cref="Gigobyte.Mockaroo.Fields.IField"/>
    [System.Runtime.Serialization.DataContract]
    [System.Diagnostics.DebuggerDisplay("{ToDebuggerView()}")]
    public abstract class FieldBase : IField
    {
        /// <summary>
        /// Gets or sets the identifier of this field.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the data type.
        /// </summary>
        /// <value>The data type.</value>
        public abstract DataType Type { get; }

        /// <summary>
        /// Gets or sets the rate blank values should be generated for this field.
        /// </summary>
        /// <value>The blank percentage.</value>
        public int BlankPercentage
        {
            get { return _blankPercentage; }
            set { _blankPercentage = value.Between(minInclusive: 0, maxInclusive: 99); }
        }

        /// <summary>
        /// Gets or sets the Ruby script to generate data based on custom logic. see more at https://mockaroo.com/help/formulas
        /// </summary>
        /// <value>The formula.</value>
        public string Formula { get; set; }

        /// <summary>
        /// Get the string value that will represent this instance in the debugger window.
        /// </summary>
        /// <returns></returns>
        internal string ToDebuggerView()
        {
            string name = (string.IsNullOrEmpty(Name) ? "<Empty>" : Name);
            return $"{{{name}: {Type.ToMockarooTypeName()}}}";
        }

        #region Private Members

        private int _blankPercentage;
        private string _dataType;

        #endregion Private Members
    }
}