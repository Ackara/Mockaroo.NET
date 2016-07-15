using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
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
        public void Assign_should_override_a_field_instance_within_a_schema_when_invoked()
        {
            // Arrange
            var sut = new Schema<Message>();

            // Act

            /// case 1: The property is one level down the member tree.
            sut.Assign(x => x.Text, DataType.FullName);
            var newTextType = sut[0].Type;

            /// case 2: The property is two levels down the member tree.
            sut.Assign(x => x.Writer.Name, DataType.FullName);
            var newNameType = sut[2].Type;

            /// case 3: The property is a list of objects.
            sut.Assign(x => x.Tags, DataType.AppName);
            var newTagType = sut[5].Type;

            /// case 4: The property is a list of objects.
            sut.Assign(x => x.Writer.Reviews.Item().Rating, DataType.RowNumber);
            var newRatingType = sut[4].Type;

            // Assert
            newTextType.ShouldBe(DataType.FullName);
            newNameType.ShouldBe(DataType.FullName);
            newTagType.ShouldBe(DataType.AppName);
            newRatingType.ShouldBe(DataType.RowNumber);
        }


        [TestMethod]
        [Owner(Dev.Ackara)]
        public void Remove_should_return_true_when_a_field_instance_is_delete_from_the_schema()
        {
            // Arrange
            var sut = new Schema<Message>();

            // Act
            var fieldWasRemoved = sut.Remove(x => x.Writer.Id);

            // Assert
            fieldWasRemoved.ShouldBeTrue();
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

        public class Message
        {
            public string Text { get; set; }

            public Author Writer { get; set; }

            public string[] Tags { get; set; }
        }

        public class Author
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public Review[] Reviews { get; set; }
        }

        public class Review
        {
            public int Rating { get; set; }
        }

        #endregion Samples
    }
}