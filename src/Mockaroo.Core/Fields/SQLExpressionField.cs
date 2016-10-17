namespace Gigobyte.Mockaroo.Fields
{
    public partial class SQLExpressionField
    {
        /// <summary>
        /// Gets or sets the sql expression.
        /// </summary>
        /// <value>The sql expression.</value>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Converts this instance into its equivalent json representation.
        /// </summary>
        /// <returns>A json representation of the instance.</returns>
        public override string ToJson()
        {
            return $"{BaseJson()},\"value\":\"{Value}\"}}";
        }
    }
}