namespace Gigobyte.Mockaroo.Fields
{
    public partial class WordsField
    {
        /// <summary>
        /// Gets or sets the minimum number of words to generate.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public virtual int Min { get; set; } = 10;

        /// <summary>
        /// Gets or sets the maximum number of words to generate.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public virtual int Max { get; set; } = 20;

        /// <summary>
        /// Converts the value of this instance to its JSON representation.
        /// </summary>
        /// <returns>
        /// This instance JSON representation.
        /// </returns>
        public override string ToJson()
        {
            return $"{BaseJson()}, \"min\": \"{Min}\", \"max\": \"{Max}\"}}";
        }
    }
}