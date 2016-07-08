using System;

namespace Tests.Mockaroo.Fakes
{
    public class ComplexObject
    {
        public ComplexObject()
        {
            Problem = new ComplexObject();
        }

        public int IntegerValue { get; set; }

        public float DecimalValue { get; set; }

        public char CharValue { get; set; }

        public string StringValue { get; set; }

        public DateTime DateValue { get; set; }

        public SimpleObject Embedded { get; set; }

        public ComplexObject Problem { get; set; }

        public int[] Numbers { get; set; }
    }
}