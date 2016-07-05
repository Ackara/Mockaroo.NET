using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [DeploymentItem(SampleData.DirectoryName)]
    public class SerializerTest
    {
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Assert <see cref="Serializer.Deserialize(string, Type)"/> can convert json to a primitive type.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        [DataSource(DDT.CSV, DDT.Connection.DataTypes, TestFile.DataTypeMap, DataAccessMethod.Sequential)]
        public void DeserializeJsonToPrimitiveType()
        {
            // Arrange
            var sut = new Serializer();
            var value = TestContext.DataRow[DDT.Column.Value];
            var dataType = Type.GetType($"System.{Convert.ToString(TestContext.DataRow[DDT.Column.Type])}");

            string json = $"{{\"{dataType.Name}\": \"{value}\"}}";
            TestContext.WriteLine("input: {0}={1}", dataType, value);

            // Act
            var result = sut.Deserialize(json, dataType);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, dataType);
            Assert.AreEqual(Convert.ChangeType(value, dataType), result);
        }

        /// <summary>
        /// Assert <see cref="Serializer.Deserialize(JObject, Type)"/> can deserialize a full
        /// response from the Mockaroo server.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        [DataSource(DDT.CSV, TestFile.ResponseBodyList, TestFile.ResponseBodyList, DataAccessMethod.Sequential)]
        public void DeserializeSampleResponsesFromMockarooServer()
        {
            // Arrange
            var sut = new Serializer();
            var fileName = Convert.ToString(TestContext.DataRow["File"]);
            var json = SampleData.GetFileContent(fileName);
            var errors = new System.Text.StringBuilder();

            TestContext.WriteLine("input: {0}", fileName);

            // Act
            foreach (JObject obj in JArray.Parse(json))
            {
                if (sut.Deserialize(obj, typeof(MockarooSchemaSpec)) == null)
                {
                    errors.AppendLine($"unable to parse => {obj.ToString()}");
                }
            }

            // Assert
            Assert.IsTrue(errors.Length == 0, errors.ToString());
        }

        /// <summary>
        /// Assert <see cref="Serializer.Deserialize(string, Type)"/> can convert json to an object.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void DeserializeJsonToSpecifiedObject()
        {
            // Arrange
            var sut = new Serializer();
            var sample = FakeObject.GetSample();

            // Act
            var result = (FakeObject)sut.Deserialize(sample.ToJson(), sample.GetType());

            // Assert
            Assert.AreEqual(sample.DateValue, result.DateValue);
            Assert.AreEqual(sample.Int32Value, result.Int32Value);
            Assert.AreEqual(sample.StringValue, result.StringValue);
            Assert.AreEqual(sample.DecimalValue, result.DecimalValue);
        }
    }
}