using System;

namespace Tests.Mockaroo.Fakes
{
    public class SimpleObject
    {
        public int IntegerValue { get; set; }

        public float DecimalValue { get; set; }

        public char CharValue { get; set; }

        public string StringValue { get; set; }

        public DateTime DateValue { get; set; }

        public DayOfWeek Day { get; set; }
    }
}