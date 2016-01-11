namespace Gigobyte.Mockaroo.Fields
{
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToDebuggerView) + "()}")]
    public abstract class FieldBase : IField
    {
        public string Name { get; set; }

        public abstract DataType Type { get; }

        public int BlankPercentage
        {
            get { return _blankPercentage; }
            set { value.Between(min: 0, max: 99); }
        }

        public virtual string ToJson()
        {
            return BaseJson() + "}";
        }

        internal string BaseJson()
        {
            return $"{{\"name\": \"{Name}\", \"type\": \"{Type.ToMockarooTypeName()}\", \"percentBlank\": \"{BlankPercentage}\"";
        }

        protected virtual string ToDebuggerView()
        {
            string name = (string.IsNullOrEmpty(Name) ? "<Empty>" : Name);
            return $"{{{name}: {Type}}}";
        }

        #region Private Members

        private int _blankPercentage;

        #endregion Private Members
    }
}