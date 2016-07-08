namespace Gigobyte.Mockaroo.Fields
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

        public override string ToJson()
        {
            string firstHalf = base.ToJson().TrimEnd('}');
            return $"{firstHalf},\"min\":\"{Min}\",\"max\":\"{Max}\"}}";
        }
    }
}