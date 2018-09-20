namespace Acklann.Mockaroo
{
    /// <summary>
    /// Provides helper methods for the <see cref="Acklann.Mockaroo"/> namespace.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Get an instance of the item specified in the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>T.</returns>
        /// <remarks>
        /// This method was intended to be used when denoting the property name of a item in a
        /// collection, when using the <see cref="Schema.Assign(string, DataType)"/> method.
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