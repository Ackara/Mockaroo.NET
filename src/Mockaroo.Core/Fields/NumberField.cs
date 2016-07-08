namespace Gigobyte.Mockaroo.Fields
{
    public partial class NumberField
    {
        public int Min { get; set; } = int.MinValue;

        public int Max { get; set; } = int.MaxValue;

        public int Decimals { get; set; }

        public override string ToJson()
        {
            string firstHalf = base.ToJson().TrimEnd('}');
            return $"{firstHalf},\"min\":\"{Min}\",\"max\":\"{Max}\",\"decimals\":\"{Decimals}\"}}";
        }
    }
}