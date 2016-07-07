using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
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
            var sut = SampleData.CreateSchema();

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
            var sampleFile = SampleData.GetFile(Asset.SchemaJson);

            // Act
            sut.Deserialize(sampleFile.OpenRead());

            // Assert
            Assert.AreEqual(3, sut.Count);
            Assert.AreEqual(DataType.Words, sut[0].Type);
            Assert.IsInstanceOfType(sut[1], typeof(NumberField));
        }
    }
}