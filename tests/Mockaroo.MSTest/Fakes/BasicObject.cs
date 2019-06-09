using System;
using System.Collections.Generic;

namespace Acklann.Mockaroo.Fakes
{
    public class BasicObject
    {
        public int[] NumericArray;

        public char CharValue;

        public float NumericValue { get; set; }

        public string StringValue { get; set; }

        public bool BooleanValue { get; set; }

        public DayOfWeek DayValue { get; set; }

        public TimeSpan TimeValue { get; set; }

        public DateTime DateValue { get; set; }

        public List<string> StringCollection { get; set; }

        public string GeneratedValue => "ReadOnly";

        public const string Fixed = "Barry";
    }
}