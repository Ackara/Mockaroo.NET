namespace Acklann.Mockaroo.Fields
{
    public partial class ParagraphsField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParagraphsField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="numberOfParagraphs">The number of paragraphs.</param>
        public ParagraphsField(string name, int numberOfParagraphs) : this(name, numberOfParagraphs, numberOfParagraphs)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParagraphsField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="min">The minimum number of words.</param>
        /// <param name="max">The maximum number of words.</param>
        public ParagraphsField(string name, int min, int max) : base(name)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Gets or sets the minimum number of paragraphs to generate.
        /// </summary>
        /// <value>The minimum.</value>
        public int Min { get; set; } = 1;

        /// <summary>
        /// Gets or sets the maximum number of paragraphs to generate.
        /// </summary>
        /// <value>The maximum.</value>
        public int Max { get; set; } = 3;

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