namespace Gigobyte.Mockaroo.Data
{
    public abstract class FieldBase : IFieldInfo
    {
        public string Name { get; set; }

        public abstract DataType Type { get; }

        public virtual string GetJson()
        {
            throw new System.NotImplementedException();
        }

        protected int AdjustByRange(int value, int min, int max)
        {
            if (value >= max) return max;
            else if (value <= min) return min;
            else return value;
        }
    }
}