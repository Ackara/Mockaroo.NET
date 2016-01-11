using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tests.Mockaroo.IntegrationTest
{
    [TestClass]
    [DeploymentItem(Artifact.DataXLSX)]
    //[Ignore(/* Assign the "MockarooApiKey" variable with your Mockaroo API key to run these test. */)]
    public class CallsToMockarooServerTest
    {
        public static string MockarooApiKey = ApiKey.GetValue();

        public TestContext TestContext { get; set; }

        [ClassCleanup]
        public static void Cleanup()
        {
            ApprovalTests.Maintenance.ApprovalMaintenance.CleanUpAbandonedFiles();
        }

        /// <summary>
        /// Assert <see cref="MockarooClient.FetchDataAsync( Type,Schema, int)"/> can retrieve
        /// primitive data from mockaroo server.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        [DataSource(Data.ODBC, Data.ExcelConnectiongString, Data.BuiltInDataSheet, DataAccessMethod.Sequential)]
        public async Task FetchPrimitiveDataFromMockarooServer()
        {
            // Arrange
            int rows = 2, count = 0;
            var sut = new MockarooClient(MockarooApiKey);
            var dataType = Type.GetType($"System.{Convert.ToString(TestContext.DataRow[Data.TypeColumn])}");

            // Act
            var data = await sut.FetchDataAsync(dataType, rows);

            // Assert
            foreach (var record in data)
            {
                count++;
                Assert.IsNotNull(record);
                Assert.IsInstanceOfType(record, dataType);
            }

            Assert.AreEqual(rows, count);
        }

        /// <summary>
        /// Assert <see cref="MockarooClient.FetchDataAsync( Type,Schema, int)"/> can retrieve data
        /// from mockaroo server based on the specified <see cref="Schema"/>.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public async Task FetchDataFromMockarooServerUsingObjectSchema()
        {
            // Arrange
            int rows = 2, count = 0;
            var sut = new MockarooClient(MockarooApiKey);

            // Act
            var data = await sut.FetchDataAsync<FakeObject>(rows);

            // Assert
            foreach (var record in data)
            {
                count++;
                Assert.IsNotNull(record);
                Assert.IsFalse(string.IsNullOrEmpty(record.StringValue));
            }

            Assert.AreEqual(rows, count);
        }

        /// <summary>
        /// Assert <see cref="MockarooClient.FetchDataAsync( Type,Schema, int)"/> can retrieve a
        /// large data set from mockaroo server.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public async Task FetchLargeDataSetFromMockarooServer()
        {
            // Arrange
            int rows = 300, count = 0;
            var sut = new MockarooClient(MockarooApiKey);

            // Act
            var data = await sut.FetchDataAsync<string>(rows);

            // Assert
            foreach (var record in data)
            {
                count++;
                Assert.IsNotNull(record);
            }

            Assert.AreEqual(rows, count);
        }

        /// <summary>
        /// Assert <see cref="MockarooClient.FetchDataAsync( Type,Schema, int)"/> can retrieve a
        /// data sample from all Mockaroo data types.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public async Task FetchSampleOfAllMockarooDataTypes()
        {
            // Arrange
            int rows = 1, count = 0;
            var client = new MockarooClient(MockarooApiKey);
            var schema = new Schema(new FieldFactory().GetAllFields());

            TestContext.WriteLine("request body:\r\n{0}", schema.ToJson());

            // Act
            var data = await client.FetchDataAsync(typeof(MockarooSchemaSpec), schema, rows);

            // Assert
            foreach (var item in data)
            {
                count++;
                var record = item as MockarooSchemaSpec;

                Assert.IsNotNull(record);
                Assert.IsFalse(string.IsNullOrEmpty(record.BitcoinAddressField));
                Assert.IsFalse(string.IsNullOrEmpty(record.MIMETypeField));
                Assert.IsFalse(string.IsNullOrEmpty(record.URLField));
            }

            Assert.AreEqual(rows, count);
        }

        /// <summary>
        /// Assert <see cref="MockarooClient.FetchDataAsync(Schema, int, ResponseFormat)"/> can
        /// retrieve data from mockaroo server in csv format.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public async Task FetchDataFromMockarooServerInCSV()
        {
            // Arrange
            var schema = new Schema<FakeObject>();
            schema.Replace(x => x.DateValue, new DateField() { Max = new DateTime(2016, 01, 01) });
            var client = new MockarooClient(MockarooApiKey);

            // Act
            string csv;
            var stream = await client.FetchDataAsync(schema, 1, ResponseFormat.CSV);
            using (var reader = new StreamReader(stream))
            {
                csv = reader.ReadToEnd();
                TestContext.WriteLine("{0}", csv);
            }

            // Assert
            Assert.IsTrue(new Regex(@"(?i)^([-\w: \.]+,?)+").IsMatch(csv), "The data was not in csv format.");
        }
    }
}