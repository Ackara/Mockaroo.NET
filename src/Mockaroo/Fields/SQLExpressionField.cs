namespace Acklann.Mockaroo.Fields
{
    public partial class SQLExpressionField
    {
        /// <summary>
        /// Gets or sets the sql expression.
        /// </summary>
        /// <value>The sql expression.</value>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"value\":\"{Value}\"}}";
        }
    }
}