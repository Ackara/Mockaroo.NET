namespace Acklann.Mockaroo.Fields
{
    public partial class NatoPhoneticField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NatoPhoneticField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="numberOfWords">The number of words.</param>
        public NatoPhoneticField(string name, int numberOfWords) : this(name, numberOfWords, numberOfWords)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NatoPhoneticField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="min">The minimum number of words.</param>
        /// <param name="max">The maximum number of words.</param>
        public NatoPhoneticField(string name, int min, int max) : base(name)
        {
            Min = min;
            Max = max;
        }

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