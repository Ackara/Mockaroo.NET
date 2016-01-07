namespace Gigobyte.Mockaroo.Annotation
{
    public sealed class NumberFieldAttribute : FieldInfoAttribute
    {
        public NumberFieldAttribute(string name) : base(name)
        {
        }

        public int Min { get; set; } = 1;

        public int Max { get; set; } = 100;

        public int Decimal { get; set; } = 2;
    }
}