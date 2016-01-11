namespace Gigobyte.Mockaroo.Fields
{
    public static class FieldsExtensions
    {
        public static int Between(this int value, int min, int max)
        {
            if (value >= max) return max;
            else if (value <= min) return min;
            else return value;
        }
    }
}