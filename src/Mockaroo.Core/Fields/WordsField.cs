namespace Gigobyte.Mockaroo.Fields
{
    public partial class WordsField
    {
        public virtual int Min { get; set; } = 10;

        public virtual int Max { get; set; } = 20;

        public override string ToJson()
        {
            return $"{BaseJson()}, \"min\": \"{Min}\", \"max\": \"{Max}\"}}";
        }
    }
}