using System.Linq;

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
        public Arrangement Sequence { get; set; }

        /// <summary>
        /// Converts this instance into its equivalent json representation.
        /// </summary>
        /// <returns>
        /// A json representation of the instance.
        /// </returns>
        public override string ToJson()
        {
            string part1 = base.ToJson().TrimEnd('}');
            return $"{part1},\"selectionStyle\":\"{Sequence.ToString().ToLower()}\",\"values\":[{string.Join(",", Values.Select(x => $"\"{x}\""))}]}}";
        }
    }
}