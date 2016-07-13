namespace Gigobyte.Mockaroo.Fields
{
    public partial class JSONArrayField
    {
        public int Min
        {
            get { return _min; }
            set { _min = value.Between(0, 100); }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value.Between(0, 100); }
        }

        public override string ToJson()
        {
            return $"{base.BaseJson()},\"minItems\":{Min},\"maxItems\":{Max}}}";
        }

        #region Private Members

        private int _min = 1, _max = 5;

        #endregion Private Members
    }
}