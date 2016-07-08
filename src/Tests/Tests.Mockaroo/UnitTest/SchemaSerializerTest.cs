using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Tests.Mockaroo.Fakes;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [DeploymentItem(Test.Data.DirectoryName)]
    [UseApprovalSubdirectory(nameof(ApprovalTests))]
    [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
    public class SchemaSerializerTest
    {
        [ClassCleanup]
        public static void Cleanup()
        {
            ApprovalTests.Maintenance.ApprovalMaintenance.CleanUpAbandonedFiles();
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void WriteObject_should_serialize_a_schema_object_into_json()
        {
            // Arrange
            string serializedData;
            var sut = CreateSchema();

            // Act
            var data = sut.Serialize();
            using (var reader = new StreamReader(data))
            { serializedData = reader.ReadToEnd(); }

            // Assert
            Approvals.VerifyJson(serializedData);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ReadObject_should_create_a_schema_object_from_a_stream_of_json()
        {
            // Arrange
            var sut = new Schema();
            var sampleFile = Test.Data.GetFile(Test.File.SchemaJson);

            // Act
            sut.Deserialize(sampleFile.OpenRead());

            // Assert
            Assert.AreEqual(3, sut.Count);
            Assert.AreEqual(DataType.Words, sut[0].Type);
            Assert.IsInstanceOfType(sut[1], typeof(NumberField));
        }

        #region Samples

        public static Schema CreateSchema()
        {
            return new Schema(
                 new NumberField()
                 {
                     Name = nameof(SimpleObject.IntegerValue),
                     Min = 3,
                     Max = 1000
                 },
                new NumberField()
                {
                    Name = nameof(SimpleObject.DecimalValue),
                    Min = 10,
                    Max = 100
                },
                new WordsField()
                {
                    Name = nameof(SimpleObject.StringValue),
                    Min = 3,
                    Max = 5
                },

                new CustomListField()
                {
                    Name = nameof(SimpleObject.CharValue),
                    Values = new string[] { "a", "b", "c" }
                },
                new DateField()
                {
                    Name = nameof(SimpleObject.DateValue),
                    Min = new DateTime(2000, 01, 01),
                    Max = new DateTime(2010, 01, 01)
                });
        }

        #endregion Samples
    }
}