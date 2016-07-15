namespace Gigobyte.Mockaroo.Fields
{
    public partial class NumberField
    {
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public int Min { get; set; } = 1;

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        public int Max { get; set; } = 100;

        /// <summary>
        /// Gets or sets the number of decimals.
        /// </summary>
        /// <value>The number of decimals.</value>
        public int Decimals { get; set; }

        /// <summary>
        /// Converts this instance into its equivalent json representation.
        /// </summary>
        /// <returns>A json representation of the instance.</returns>
        public override string ToJson()
        {
            return $"{BaseJson()},\"min\":\"{Min}\",\"max\":\"{Max}\",\"decimals\":\"{Decimals}\"}}";
        }
    }
}