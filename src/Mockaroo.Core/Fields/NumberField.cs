namespace Gigobyte.Mockaroo.Fields
{
    public partial class NumberField
    {
        public virtual int Min { get; set; } = 1;

        public virtual int Max { get; set; } = 100;

        public virtual int Decimals { get; set; }

        /// <summary>
        /// Converts the value of this instance to its JSON representation.
        /// </summary>
        /// <returns>
        /// This instance JSON representation.
        /// </returns>
        public override string ToJson()
        {
            return $"{BaseJson()}, \"min\": \"{Min}\", \"max\": \"{Max}\", \"decimals\": \"{Decimals}\"}}";
        }
    }
}