namespace Gigobyte.Mockaroo.Fields
{
    public partial class JSONArrayField
    {
        public int Min { get; set; } = 1;

        public int Max { get; set; } = 5;

        public override string ToJson()
        {
            return $"{base.BaseJson()},\"minItems\":{Min},\"maxItems\":{Max}}}";
        }
    }
}