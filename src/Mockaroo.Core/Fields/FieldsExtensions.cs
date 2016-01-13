namespace Gigobyte.Mockaroo.Fields
{
    /// <summary>
    /// Extension methods for <see cref="Gigobyte.Mockaroo.Fields"/> name space.
    /// </summary>
    public static class FieldsExtensions
    {
        /// <summary>
        /// Checks the value is between the specified minimum and maximum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minInclusive">The minimum inclusive.</param>
        /// <param name="maxInclusive">The maximum inclusive.</param>
        /// <returns></returns>
        public static int Between(this int value, int minInclusive, int maxInclusive)
        {
            if (value >= maxInclusive) return maxInclusive;
            else if (value <= minInclusive) return minInclusive;
            else return value;
        }
    }
}