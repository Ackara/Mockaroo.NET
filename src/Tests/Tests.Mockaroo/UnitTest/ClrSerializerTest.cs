using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gigobyte.Mockaroo.Serialization;
using System.IO;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [DeploymentItem(SampleData.DirectoryName)]
    public class ClrSerializerTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [Owner(Dev.Ackara)]
        [DataSource(DDT.CSV, "file.csv", "file#csv", DataAccessMethod.Sequential)]
        public void WriteObject_should_serialize_a_primitive_type_into_a_stream_object()
        {
            // Arrange
            var sut = new ClrSerializer();
            var value = TestContext.DataRow[DDT.Column.Value];
            var dataType = Type.GetType($"System.{Convert.ToString(TestContext.DataRow[DDT.Column.Type])}");
            
            // Act
            

            // Assert
        }

        [Owner(Dev.Ackara)]
        public void WriteObject_should_serialize_a_simple_class_into_a_stream_object()
        {
        }

        [Owner(Dev.Ackara)]
        public void WriteObject_should_serialize_a_complex_class_into_a_stream_object()
        {
        }

    }
}
