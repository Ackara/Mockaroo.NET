namespace Gigobyte.Mockaroo.Fields
{
    /// <summary>
    /// Base class for <see cref="IField"/>.
    /// </summary>
    /// <seealso cref="Gigobyte.Mockaroo.Fields.IField" />
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToDebuggerView) + "()}")]
    public abstract class FieldBase : IField
    {
        /// <summary>
        /// Gets or sets the identifier of this field.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the Mockaroo data type.
        /// </summary>
        /// <value>
        /// The data type.
        /// </value>
        public abstract DataType Type { get; }

        /// <summary>
        /// Gets or sets the rate blank values should be generated for this field.
        /// </summary>
        /// <value>
        /// The blank percentage.
        /// </value>
        public int BlankPercentage
        {
            get { return _blankPercentage; }
            set { value.Between(minInclusive: 0, maxInclusive: 99); }
        }

        /// <summary>
        /// Converts the value of this instance to its JSON representation.
        /// </summary>
        /// <returns>This instance JSON representation.</returns>
        public virtual string ToJson()
        {
            return BaseJson() + "}";
        }

        internal string BaseJson()
        {
            return $"{{\"name\": \"{Name}\", \"type\": \"{Type.ToMockarooTypeName()}\", \"percentBlank\": \"{BlankPercentage}\"";
        }

        /// <summary>
        /// Get the string value that will represent this instance in the debugger window.
        /// </summary>
        /// <returns></returns>
        protected virtual string ToDebuggerView()
        {
            string name = (string.IsNullOrEmpty(Name) ? "<Empty>" : Name);
            return $"{{{name}: {Type.ToMockarooTypeName()}}}";
        }

        #region Private Members

        private int _blankPercentage;

        #endregion Private Members
    }
}