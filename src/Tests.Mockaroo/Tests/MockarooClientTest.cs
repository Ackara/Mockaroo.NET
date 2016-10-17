using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Exceptions;
using Gigobyte.Mockaroo.Fields;
using Gigobyte.Mockaroo.Fields.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mockaroo.IntegrationTest
{
    [TestClass]
    [DeploymentItem(Test.File.ApiKey)]
    [DeploymentItem(Test.Data.DirectoryName)]
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
        [TestCategory(Test.Trait.Integration)]
        [TestProperty(Test.Property.Records, "2")]
        public async Task FetchDataAsync_should_export_data_from_the_mockaroo_api()
        {
            // Arrange
            var apiKey = ApiKey.GetValue();
            var schema = CreateSimpleSchema();
            int records = Convert.ToInt32(TestContext.Properties[Test.Property.Records] ?? 1);
            var endpoint = Gigobyte.Mockaroo.Mockaroo.Endpoint(apiKey, records, Format.JSON);

            // Act
            var data = await MockarooClient.FetchDataAsync(endpoint, schema);
            var json = JArray.Parse(Encoding.Default.GetString(data));

            // Assert
            Assert.AreEqual(records, json.Count);
        }

        //[TestMethod]
        [Owner(Dev.Ackara)]
        [TestCategory(Test.Trait.Integration)]
        [TestProperty(Test.Property.Records, "1")]
        [DataSource(Test.Data.CsvProvider, "mockaroo_type_list.csv", "mockaroo_type_list#csv", DataAccessMethod.Sequential)]
        public void FetchDataAsync_should_export_a_record_for_each_of_the_mockaroo_data_types()
        {
            // Arrange
            var records = Convert.ToInt32(TestContext.Properties[Test.Property.Records] ?? 1);
            var endpoint = Gigobyte.Mockaroo.Mockaroo.Endpoint(ApiKey.GetValue(), records, Format.JSON);

            var dataType = (DataType)Enum.Parse(typeof(DataType), Convert.ToString(TestContext.DataRow[0]));
            var field = new FieldFactory().CreateInstance(dataType);
            field.Name = "name";

            // Act
            TestContext.WriteLine("Context: {0}", dataType);
            var data = MockarooClient.FetchDataAsync(endpoint, new Schema(field)).Result;
            var json = JArray.Parse(Encoding.Default.GetString(data));

            // Assert
            json.Count.ShouldBeGreaterThanOrEqualTo(1);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        [TestCategory(Test.Trait.Integration)]
        [TestProperty(Test.Property.Records, "2")]
        public void FetchData_should_export_data_from_mockaroo_and_deserialize_it_into_a_object()
        {
            // Arrange
            var sut = new MockarooClient(ApiKey.GetValue());
            var records = Convert.ToInt32(TestContext.Properties[Test.Property.Records] ?? 1);

            // Act
            var results = sut.FetchData<SimpleObject>(records);

            // Assert
            results.ShouldNotBeEmpty();
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        [TestCategory(Test.Trait.Integration)]
        [ExpectedException(typeof(MockarooException))]
        public async Task FetchDataAsync_should_thrown_an_exception_when_an_error_occurs()
        {
            // Arrange
            var schema = CreateSimpleSchema();
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

        #region Samples

        public static Schema CreateSimpleSchema()
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

        public class SimpleObject
        {
            public SimpleObject()
            {
            }

            public int IntegerValue { get; set; }

            public float DecimalValue { get; set; }

            public char CharValue { get; set; }

            public string StringValue { get; set; }

            public DateTime DateValue { get; set; }

            public DayOfWeek Day { get; set; }
        }

        #endregion Samples
    }
}