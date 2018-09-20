﻿using System;
using System.Globalization;

namespace Acklann.Mockaroo.Fields
{
    public partial class DateField
    {
        public static readonly string DateFormat = "MM/dd/yyyy";

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
            return $"{firstHalf},\"min\":\"{Min.Date.ToString(DateFormat, CultureInfo.InvariantCulture)}\",\"max\":\"{Max.ToString(DateFormat, CultureInfo.InvariantCulture)}\"}}";
        }
    }
}