using System;
using System.Globalization;

namespace Acklann.Mockaroo.Fields
{
    public partial class DateField
    {
        private static readonly string DateFormat = "MM/dd/yyyy";

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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{BaseJson()},\"min\":\"{Min.Date.ToString(DateFormat, CultureInfo.InvariantCulture)}\",\"max\":\"{Max.ToString(DateFormat, CultureInfo.InvariantCulture)}\"}}";
        }
    }
}