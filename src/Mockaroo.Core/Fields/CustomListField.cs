namespace Gigobyte.Mockaroo.Fields
{
    public partial class CustomListField
    {
        /// <summary>
        /// Gets or sets an array of values to pick from.
        /// </summary>
        /// <value>The values.</value>
        public string[] Values { get; set; } = new string[0];

        /// <summary>
        /// Gets or sets the whether the <see cref="Values"/> provided should be returned randomly or sequentially.
        /// </summary>
        /// <value>The selection.</value>
        public SelectionStyle Sequence { get; set; }
    }
}