namespace Gigobyte.Mockaroo.Fields
{
    public partial class JSONArrayField
    {
        /// <summary>
        /// Gets or sets the minimum number of items to generate (0 to 100).
        /// </summary>
        /// <value>The minimum.</value>
        public int Min
        {
            get { return _min; }
            set { _min = value.Between(0, 100); }
        }

        /// <summary>
        /// Gets or sets the maximum number of items to generate (0 to 100).
        /// </summary>
        /// <value>The maximum.</value>
        public int Max
        {
            get { return _max; }
            set { _max = value.Between(0, 100); }
        }

        /// <summary>
        /// Converts this instance into its equivalent json representation.
        /// </summary>
        /// <returns>A json representation of the instance.</returns>
        public override string ToJson()
        {
            return $"{base.BaseJson()},\"minItems\":{Min},\"maxItems\":{Max}}}";
        }

        #region Private Members

        private int _min = 1, _max = 5;

        #endregion Private Members
    }
}