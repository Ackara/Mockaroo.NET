using System.Xml.Serialization;

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
        /// Converts this instance into its equivalent json representation.
        /// </summary>
        /// <returns>A json representation of the instance.</returns>
        public override string ToJson()
        {
            return $"{BaseJson()},\"min\":\"{Min}\",\"max\":\"{Max}\"}}";
        }
    }
}