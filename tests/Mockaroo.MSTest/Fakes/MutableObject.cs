using System;

namespace Acklann.Mockaroo.Fakes
{
    public class MutableObject
    {
        public int[] NumericArray;

        public char CharValue { get; set; }

        public int NumericValue { get; set; }

        public float DecimalValue { get; set; }

        public string StringValue { get; set; }

        public bool BooleanValue { get; set; }

        public DayOfWeek DayValue { get; set; }

        public TimeSpan TimeValue { get; set; }

        public DateTime DateValue { get; set; }

        

        public string GeneratedValue => "ReadOnly";
    }
}