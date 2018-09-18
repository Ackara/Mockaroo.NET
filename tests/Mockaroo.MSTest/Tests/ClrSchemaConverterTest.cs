﻿using Acklann.Diffa;
using Acklann.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Acklann.Mockaroo.Constants;

namespace Acklann.Mockaroo.Tests
{
    [TestClass]
    [DeploymentItem(Data.DirectoryName)]
    
    
    public class ClrSchemaConverterTest
    {
        [TestMethod]
        public void Convert_should_create_a_schema_object_from_a_simple_type()
        {
            var sut = new ClrSchemaConverter();
            var schema = sut.Convert(typeof(SimpleObject));

            Diff.Approve(schema.ToJson());
        }

        [TestMethod]
        public void Convert_should_create_a_schema_object_from_a_complex_type()
        {
            var sut = new ClrSchemaConverter();
            var schema = sut.Convert(typeof(ComplexObject));

            Diff.Approve(schema.ToJson());
        }

        #region Samples

        public class SimpleObject
        {
            public int IntegerValue { get; set; }

            public float DecimalValue { get; set; }

            public char CharValue { get; set; }

            public string StringValue { get; set; }

            public DateTime DateValue { get; set; }

            public DayOfWeek Day { get; set; }
        }

        public class ComplexObject
        {
            public int IntegerValue { get; set; }

            public float DecimalValue { get; set; }

            public char CharValue { get; set; }

            public string StringValue { get; set; }

            public int[] IntArray { get; set; }

            public DateTime DateValue { get; set; }

            public SimpleObject Nested1 { get; set; }

            public SimpleObject Nested2 { get; set; }

            public List<Point> Points { get; set; }

            public ComplexObject RecursionProblem1 { get; set; }

            public MultiLevelObject MultiLvl { get; set; }
        }

        public class MultiLevelObject
        {
            public int Id { get; set; }

            public Point Coordinate { get; set; }

            public SimpleObject Simple { get; set; }

            public ComplexObject Problem { get; set; }
        }

        public struct Point
        {
            public int X { get; set; }

            public int Y { get; set; }

            public override string ToString()
            {
                return $"X:{X} Y:{Y}";
            }
        }

        #endregion Samples
    }
}