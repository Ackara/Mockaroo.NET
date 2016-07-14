namespace Gigobyte.Mockaroo
{
    /// <summary>
    /// Extension methods for <see cref="Gigobyte.Mockaroo"/> name space.
    /// </summary>
    public static partial class Extensions
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

        public static T Item<T>(this System.Collections.Generic.IEnumerable<T> collection)
        {
            return System.Activator.CreateInstance<T>();
        }
    }
}