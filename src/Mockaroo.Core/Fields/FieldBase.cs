﻿namespace Gigobyte.Mockaroo.Fields
{
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToDebuggerView) + "()}")]
    public abstract class FieldBase : IField
    {
        public string Name { get; set; }

        public abstract DataType Type { get; }

        public int BlankPercentage
        {
            get { return _blankPercentage; }
            set { value.Between(minInclusive: 0, maxInclusive: 99); }
        }

        /// <summary>
        /// Converts the value of this instance to its JSON representation.
        /// </summary>
        /// <returns>This instance JSON representation.</returns>
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
            return $"{{{name}: {Type.ToMockarooTypeName()}}}";
        }

        #region Private Members

        private int _blankPercentage;

        #endregion Private Members
    }
}