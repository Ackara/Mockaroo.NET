namespace Gigobyte.Mockaroo
{
    /// <summary>
    /// Provides helper methods for the <see cref="Gigobyte.Mockaroo"/> namespace.
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

        /// <summary>
        /// Get an instance of the item specified in the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>T.</returns>
        /// <remarks>
        /// This method was intended to be used when denoting the property name of a item in a
        /// collection, when using the <see cref="Schema.Assign(string, DataType)/&gt;"/> method.
        /// </remarks>
        /// <example>
        /// <code>
        /// sut.Assign(x =&gt; x.Writer.Reviews.Item().Rating, DataType.RowNumber);
        /// </code>
        /// </example>
        public static T Item<T>(this System.Collections.Generic.IEnumerable<T> collection)
        {
            return System.Activator.CreateInstance<T>();
        }
    }
}