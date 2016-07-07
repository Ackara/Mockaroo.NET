using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mockaroo.IntegrationTest
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    [UseApprovalSubdirectory(nameof(ApprovalTests))]
    public class MockarooClientTest
    {
        public TestContext TestContext { get; set; }

        [ClassCleanup]
        public static void Cleanup()
        {
            ApprovalTests.Maintenance.ApprovalMaintenance.CleanUpAbandonedFiles();
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        [TestProperty("records", "2")]
        [TestCategory(Trait.Integration)]
        public async Task FetchDataAsync_should_export_data_from_the_mockaroo_restful_service()
        {
            // Arrange
            var apiKey = ApiKey.GetValue();
            var schema = SampleData.CreateSchema();
            int records = Convert.ToInt32(TestContext.Properties["records"] ?? 2);
            var endpoint = Gigobyte.Mockaroo.Mockaroo.Endpoint(apiKey, records, Format.JSON);

            // Act
            var data = await MockarooClient.FetchDataAsync(endpoint, schema);
            var json = JArray.Parse(Encoding.Default.GetString(data));

            // Assert
            Assert.AreEqual(records, json.Count);
        }
    }
}