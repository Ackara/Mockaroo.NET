using System;

namespace Gigobyte.Mockaroo.Fields
{
    public partial class DateField
    {
        internal const string MockarooFormat = "MM/dd/yyyy";

        /// <summary>
        /// Gets or sets the minimum date in mm/dd/yyyy format.
        /// </summary>
        /// <value>The minimum date in mm/dd/yyyy format.</value>
        public DateTime Min { get; set; } = new DateTime(1975, 04, 04);

        /// <summary>
        /// Gets or sets the maximum date in mm/dd/yyyy format.
        /// </summary>
        /// <value>The maximum date in mm/dd/yyyy format.</value>
        public DateTime Max { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets The format to output. This can be any format directive supported by ruby see
        /// more at http://ruby-doc.org/core-1.9.3/Time.html#method-i-strftime. Defaults to ISO 8601 format.
        /// </summary>
        /// <value>The format to output.</value>
        public string Format { get; set; } = "%F %T";
    }
}