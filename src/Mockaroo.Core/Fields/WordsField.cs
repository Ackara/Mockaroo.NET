namespace Gigobyte.Mockaroo.Fields
{
    public partial class WordsField
    {
        public virtual int Min { get; set; } = 10;

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