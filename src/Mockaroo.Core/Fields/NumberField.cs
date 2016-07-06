namespace Gigobyte.Mockaroo.Fields
{
    public partial class NumberField
    {
        public virtual int Min { get; set; } = 1;

        public virtual int Max { get; set; } = 100;

        public virtual int Decimals { get; set; }
        
    }
}