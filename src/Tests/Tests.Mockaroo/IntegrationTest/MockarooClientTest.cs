using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Exceptions;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mockaroo.IntegrationTest
{
    [TestClass]
    [DeploymentItem(Test.File.ApiKey)]
    [DeploymentItem(SampleData.DirectoryName)]
    [UseApprovalSubdirectory(nameof(ApprovalTests))]
    [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
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
        [TestCategory(Test.Trait.Integration)]
        public async Task FetchDataAsync_should_export_data_from_the_mockaroo_restful_service()
        {
            // Arrange
            var apiKey = ApiKey.GetValue();
            var schema = SampleData.CreateSchema();
            int records = Convert.ToInt32(TestContext.Properties["records"] ?? 1);
            var endpoint = Gigobyte.Mockaroo.Mockaroo.Endpoint(apiKey, records, Format.JSON);

            // Act
            var data = await MockarooClient.FetchDataAsync(endpoint, schema);
            var json = JArray.Parse(Encoding.Default.GetString(data));

            // Assert
            Assert.AreEqual(records, json.Count);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        [TestProperty("records", "2")]
        [TestCategory(Test.Trait.Integration)]
        public async Task FetchDataAsync_should_export_data_contain_all_known_data_types_from_the_mockaroo_restful_service()
        {
            // Arrange
            var records = Convert.ToInt32(TestContext.Properties["records"] ?? 1);
            var endpoint = Gigobyte.Mockaroo.Mockaroo.Endpoint(ApiKey.GetValue(), records, Format.JSON);

            var schema = new Schema(GetAllFieldTypes());

            // Act
            var data = await MockarooClient.FetchDataAsync(endpoint, schema);
            var json = JArray.Parse(Encoding.Default.GetString(data));
            System.Diagnostics.Debug.WriteLine(json.ToString());

            // Assert
            Assert.AreEqual(records, json.Count);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        [TestCategory(Test.Trait.Integration)]
        [ExpectedException(typeof(MockarooException))]
        public async Task FetchDataAsync_should_thrown_an_exception_when_an_error_occurs()
        {
            // Arrange
            var schema = SampleData.CreateSchema();
            var endpoint = Gigobyte.Mockaroo.Mockaroo.Endpoint(ApiKey.GetValue(), 1, Format.JSON);

            // Act
            schema[0].Name = null;
            try
            {
                var data = await MockarooClient.FetchDataAsync(endpoint, schema);
                Assert.Fail("'{0}' was not thrown.", nameof(MockarooException));
            }
            catch (MockarooException ex) { TestContext.WriteLine("message: {0}", ex.Message); throw; }
        }

        internal static IEnumerable<IField> GetAllFieldTypes()
        {
            foreach (var type in Assembly.GetAssembly(typeof(IField)).GetTypes())
            {
                if (type.GetInterface(typeof(IField).Name) != null && !type.IsAbstract && !type.IsInterface)
                {
                    var field = (IField)Activator.CreateInstance(type);
                    field.Name = type.Name;

                    yield return field;
                }
            }
        }
    }
}