using Gigobyte.Mockaroo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [DeploymentItem(Artifact.SampleDataDir)]
    public class SerializerTest
    {
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Assert <see cref="Serializer.Deserialize(string, Type)"/> can convert json to a
        /// primitive type.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        [DataSource(Data.ODBC, Data.ExcelConnectiongString, Data.BuiltInDataSheet, DataAccessMethod.Sequential)]
        public void DeserializeJsonToPrimitiveType()
        {
            // Arrange
            var sut = new Serializer();
            var value = TestContext.DataRow[Data.ValueColumn];
            var dataType = Type.GetType($"System.{Convert.ToString(TestContext.DataRow[Data.TypeColumn])}");

            string json = $"{{\"{dataType.Name}\": \"{value}\"}}";
            TestContext.WriteLine("input: {0}={1}", dataType, value);
            if (dataType == typeof(char)) System.Diagnostics.Debugger.Break();
            // Act
            var result = sut.Deserialize(json, dataType);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, dataType);
            Assert.AreEqual(Convert.ChangeType(value, dataType), result);
        }

        /// <summary>
        /// Assert <see cref="Serializer.Deserialize(string, Type)"/> can convert json to an object.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void DeserializeJsonToObject()
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

        /// <summary>
        /// Assert <see cref="Serializer.Deserialize(JObject, Type)"/> can deserialize a full
        /// response from the Mockaroo server.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        [DataSource(Data.CSV, "response_body_list.csv", "response_body_list#csv", DataAccessMethod.Sequential)]
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
    }
}