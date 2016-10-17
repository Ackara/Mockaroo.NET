using Gigobyte.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tests.Mockaroo.Tests
{
    [TestClass]
    public class ClrDataAdapterTest
    {
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void Transform_should_deserialize_mockaroo_data_into_a_simple_object()
        {
            // Arrange
            var sut = new ClrDataAdapter();
            var data = File.ReadAllBytes(Test.Data.GetFile(Test.File.BasicResponse).FullName);

            // Act
            var result = sut.Transform<SimpleObject>(data).First();

            // Assert
            Assert.AreEqual(8184, result.DateValue.Year);
            Assert.AreEqual(DayOfWeek.Tuesday, result.Day);
            Assert.AreEqual("velit vivamus vel nulla eget eros elementum pellentesque quisque porta volutpat", result.StringValue);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void Transform_should_deserialize_mockaroo_data_into_a_complex_object()
        {
            // Arrange
            var sut = new ClrDataAdapter();
            var data = File.ReadAllBytes(Test.Data.GetFile(Test.File.VeryComplexResponse).FullName);

            // Act
            var result = sut.Transform<ComplexObject>(data).First();

            // Assert
            Assert.AreEqual(676558325, result.IntegerValue);
            Assert.AreEqual(5, result.IntArray.Length);
            Assert.IsNotNull(result.MultiLvl);
            Assert.IsNotNull(result.Nested1);
            Assert.IsNotNull(result.Points);
        }

        #region Samples

        public struct Point
        {
            public int X { get; set; }

            public int Y { get; set; }

            public override string ToString()
            {
                return $"X:{X} Y:{Y}";
            }
        }

        public class SimpleObject
        {
            public SimpleObject()
            {
            }

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

        #endregion Samples
    }
}