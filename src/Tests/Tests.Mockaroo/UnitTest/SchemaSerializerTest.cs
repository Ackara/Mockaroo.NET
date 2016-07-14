using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
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
        public void ReadObject_should_create_a_schema_object_from_a_serialized_schema_object()
        {
            // Arrange
            var sut = CreateSchema();
            var actual = new Schema();
            string serializedData1, serializedData2;

            // Act
            using (var reader = new StreamReader(sut.Serialize()))
            { serializedData1 = reader.ReadToEnd(); }

            actual.Deserialize(Encoding.UTF8.GetBytes(serializedData1));

            using (var reader = new StreamReader(actual.Serialize()))
            { serializedData2 = reader.ReadToEnd(); }

            // Assert
            Assert.AreEqual(serializedData1, serializedData2);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void Assign_should_override_a_field_instance_of_the_schema_when_invoked()
        {
            // Arrange
            var sut = new Schema<Article>();
            DataType oldType, newType;

            // Act
            oldType = sut[0].Type;
            sut.Assign(x => x.Title, DataType.FullName);
            newType = sut[0].Type;

            // Assert
            Assert.AreNotEqual(oldType, newType);
            Assert.AreEqual(DataType.FullName, newType);
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

        public class Article
        {
            public string Title { get; set; }
        }

        #endregion Samples
    }
}