using System.Linq;

namespace Acklann.Mockaroo.Fields
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
        public Arrangement Sequence { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"selectionStyle\":\"{Sequence.ToString().ToLower()}\",\"values\":[{string.Join(",", Values.Select(x => $"\"{x}\""))}]}}";
        }
    }
}