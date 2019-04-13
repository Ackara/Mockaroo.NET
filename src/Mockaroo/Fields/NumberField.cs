namespace Acklann.Mockaroo.Fields
{
    public partial class NumberField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="decimals">The number of decimal points.</param>
        public NumberField(string name, int min, int max, int decimals = 0) : base(name)
        {
            Min = min;
            Max = max;
            Decimals = decimals;
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public int Min { get; set; } = 0;

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        public int Max { get; set; } = ushort.MaxValue;

        /// <summary>
        /// Gets or sets the number of decimals.
        /// </summary>
        /// <value>The number of decimals.</value>
        public int Decimals { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"min\":\"{Min}\",\"max\":\"{Max}\",\"decimals\":\"{Decimals}\"}}";
        }
    }
}