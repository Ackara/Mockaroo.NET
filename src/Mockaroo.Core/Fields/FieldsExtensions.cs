namespace Gigobyte.Mockaroo.Fields
{
    public static class FieldsExtensions
    {
        public static int Between(this int value, int minInclusive, int maxInclusive)
        {
            if (value >= maxInclusive) return maxInclusive;
            else if (value <= minInclusive) return minInclusive;
            else return value;
        }
    }
}