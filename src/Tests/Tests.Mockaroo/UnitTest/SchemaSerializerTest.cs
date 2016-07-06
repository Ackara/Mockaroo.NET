using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using Gigobyte.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [DeploymentItem(SampleData.DirectoryName)]
    [UseApprovalSubdirectory(nameof(ApprovalTests))]
    [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
    public class SchemaSerializerTest
    {
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void WriteObject_should_serialize_a_schema_object_into_json()
        {
            // Arrange
            string serializedData;
            var sut = new SchemaSerializer();
            var sample = SampleData.CreateSchema();

            // Act

            using (var memory = new MemoryStream())
            {
                sut.WriteObject(sample, memory);
                using (var reader = new StreamReader(memory))
                { serializedData = reader.ReadToEnd(); }
            }

            // Assert
            Approvals.VerifyJson(serializedData);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ReadObject_should_create_a_schema_object_from_a_stream_of_json()
        {
            // Arrange
            Schema deserializedSchema;
            var sut = new SchemaSerializer();
            var sampleFile = SampleData.GetFile(Asset.SchemaJson);

            // Act
            using (var serializedData = sampleFile.OpenRead())
            {
                deserializedSchema = sut.ReadObject<Schema>(serializedData);
            }

            // Assert
            Assert.AreEqual(3, deserializedSchema.Count);
            Assert.AreEqual(DataType.AppName, deserializedSchema[0].Type);
            Assert.IsInstanceOfType(deserializedSchema[1], typeof(WordsField));
        }
    }
}