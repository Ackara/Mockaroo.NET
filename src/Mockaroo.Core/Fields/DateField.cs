using System;
using System.Globalization;

namespace Gigobyte.Mockaroo.Fields
{
    public partial class DateField
    {
        /// <summary>
        /// Gets or sets the minimum date.
        /// </summary>
        /// <value>The minimum date.</value>
        public DateTime Min { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the maximum date.
        /// </summary>
        /// <value>The maximum date.</value>
        public DateTime Max { get; set; } = DateTime.MaxValue;

        /// <summary>
        /// Converts this instance into its equivalent json representation.
        /// </summary>
        /// <returns>A json representation of the instance.</returns>
        public override string ToJson()
        {
            string firstHalf = base.ToJson().TrimEnd('}');
            return $"{firstHalf},\"min\":\"{Min.Date.ToString(Mockaroo.DateFormat, CultureInfo.InvariantCulture)}\",\"max\":\"{Max.ToString(Mockaroo.DateFormat, CultureInfo.InvariantCulture)}\"}}";
        }
    }
}