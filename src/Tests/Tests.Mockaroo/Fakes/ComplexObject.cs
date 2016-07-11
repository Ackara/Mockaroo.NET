using System;
using System.Collections.Generic;

namespace Tests.Mockaroo.Fakes
{
    public class ComplexObject
    {
        public ComplexObject()
        {
            //RecursionProblem1 = new ComplexObject();
            //Nested1 = new SimpleObject();
            //Nested2 = new SimpleObject();
        }

        //public int IntegerValue { get; set; }

        //public float DecimalValue { get; set; }

        //public char CharValue { get; set; }

        //public string StringValue { get; set; }

        public int[] IntArray { get; set; }

        //        public DateTime DateValue { get; set; }

        //        public SimpleObject Nested1 { get; set; }

        //        public SimpleObject Nested2 { get; set; }

        //        public ComplexObject RecursionProblem1 { get; set; }
    }
}