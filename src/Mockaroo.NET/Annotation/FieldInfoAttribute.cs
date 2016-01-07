using System;

namespace Gigobyte.Mockaroo.Annotation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class FieldInfoAttribute : Attribute, IFieldInfo
    {
        public FieldInfoAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public DataType Type { get; set; }

        public int Blanks
        {
            get { return _blanks; }
            set { _blanks = AdjustByRange(value, 0, 99); }
        }

        public virtual string GetJson()
        {
            return $"\"name\": \"{Name}\", \"type\": \"{Type.GetName()}\", \"percentBlank\": \"{Blanks}\"";
        }

        protected int AdjustByRange(int value, int min, int max)
        {
            if (value >= max) return max;
            else if (value <= min) return min;
            else return value;
        }

        #region Private Members

        private int _blanks;

        #endregion Private Members
    }
}