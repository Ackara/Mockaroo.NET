namespace Acklann.Mockaroo.Fields
{
    /// <summary>
    /// Base class for <see cref="IField"/>.
    /// </summary>
    /// <seealso cref="Acklann.Mockaroo.Fields.IField"/>
    [System.Diagnostics.DebuggerDisplay("{GetDebuggerDisplay()}")]
    public abstract class FieldBase : IField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldBase"/> class.
        /// </summary>
        protected FieldBase() : this(string.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldBase"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        protected FieldBase(string name)
        {
            Name = name;
        }

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
            set { _blankPercentage = Between(value, minInclusive: 0, maxInclusive: 99); }
        }

        /// <summary>
        /// Gets or sets the Ruby script to generate data based on custom logic. see more at https://mockaroo.com/help/formulas
        /// </summary>
        /// <value>The formula.</value>
        public string Formula { get; set; } = string.Empty;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Concat(BaseJson(), "}");
        }

        internal static int Between(int value, int minInclusive, int maxInclusive)
        {
            if (value >= maxInclusive) return maxInclusive;
            else if (value <= minInclusive) return minInclusive;
            else return value;
        }

        /// <summary>
        /// Bases the json.
        /// </summary>
        /// <returns></returns>
        protected internal string BaseJson()
        {
            return $"{{\"name\":\"{Name}\",\"type\":\"{FieldFactory.ToString(Type)}\",\"percentageBlank\":\"{_blankPercentage}\",\"formula\":\"{Formula}\"";
        }

        /// <summary>
        /// Get the string value that will represent this instance in the debugger window.
        /// </summary>
        /// <returns></returns>
        private string GetDebuggerDisplay()
        {
            string name = (string.IsNullOrEmpty(Name) ? "[Empty]" : Name);
            return $"{name}: <{Type}>";
        }

        #region Private Members

        private int _blankPercentage;

        #endregion Private Members
    }
}