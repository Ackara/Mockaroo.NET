using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Tests.Mockaroo.Fakes;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [DeploymentItem(Test.Data.DirectoryName)]
    [UseApprovalSubdirectory(nameof(ApprovalTests))]
    [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
    public class ClrSchemaSerializerTest
    {
        public TestContext TestContext { get; set; }

        [ClassCleanup]
        public static void Cleanup()
        {
            ApprovalTests.Maintenance.ApprovalMaintenance.CleanUpAbandonedFiles();
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ConvertToSchema_should_convert_a_basic_clr_type_into_a_schema_object()
        {
            var sut = new ClrSchemaSerializer();
            var schema = sut.ConvertToSchema(typeof(SimpleObject));

            Approvals.VerifyJson(schema.ToJson());
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ConvertToSchema_should_convert_a_complex_clr_type_into_a_schema_object()
        {
            var sut = new ClrSchemaSerializer();
            var schema = sut.ConvertToSchema(typeof(ComplexObject));

            Approvals.VerifyJson(schema.ToJson());
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ReadObject_should_deserialize_a_bytes_array_into_a_basic_clr_object()
        {
            // Arrange
            var sut = new ClrSchemaSerializer();
            var data = File.ReadAllBytes(Test.Data.GetFile(Test.File.BasicResponse).FullName);

            // Act
            var result = sut.ReadObject<SimpleObject>(data).First();

            // Assert
            Assert.AreEqual(8184, result.DateValue.Year);
            Assert.AreEqual(DayOfWeek.Tuesday, result.Day);
            Assert.AreEqual("velit vivamus vel nulla eget eros elementum pellentesque quisque porta volutpat", result.StringValue);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ReadObject_should_deserialize_a_bytes_array_into_a_complex_clr_object()
        {
            // Arrange
            var sut = new ClrSchemaSerializer();
            var data = File.ReadAllBytes(Test.Data.GetFile(Test.File.VeryComplexResponse).FullName);

            // Act
            var result = sut.ReadObject<ComplexObject>(data).First();

            // Assert
            Assert.AreEqual(676558325, result.IntegerValue);
            Assert.AreEqual(5, result.IntArray.Length);
            Assert.IsNotNull(result.MultiLvl);
            Assert.IsNotNull(result.Nested1);
            Assert.IsNotNull(result.Points);
        }
    }
}