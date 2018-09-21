namespace Acklann.Mockaroo.Fields
{
    public partial class WordsField
    {
        /// <summary>
        /// Gets or sets the minimum number of words to generate.
        /// </summary>
        /// <value>The minimum.</value>
        public int Min { get; set; } = 10;

        /// <summary>
        /// Gets or sets the maximum number of words to generate.
        /// </summary>
        /// <value>The maximum.</value>
        public int Max { get; set; } = 20;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"min\":\"{Min}\",\"max\":\"{Max}\"}}";
        }
    }
}