using Acklann.Mockaroo;
using Acklann.Mockaroo.Exceptions;
using Acklann.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Acklann.Mockaroo.Constants;

namespace Acklann.Mockaroo.IntegrationTest
{
    [TestClass]
    //[DeploymentItem(KnownFile.ApiKey)]
    //[DeploymentItem(Data.DirectoryName)]
    public class MockarooClientTest
    {
        private const string RecordsProperty = "records";

        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestProperty(RecordsProperty, "2")]
        public async Task FetchDataAsync_should_export_data_from_the_mockaroo_api()
        {
            // Arrange
            var apiKey = ApiKey.GetValue();
            var schema = CreateSimpleSchema();
            int records = Convert.ToInt32(TestContext.Properties[RecordsProperty] ?? 1);
            var endpoint = Acklann.Mockaroo.Mockaroo.Endpoint(apiKey, records, Format.JSON);

            // Act
            var data = await MockarooClient.FetchDataAsync(endpoint, schema);
            var json = JArray.Parse(Encoding.Default.GetString(data));

            // Assert
            Assert.AreEqual(records, json.Count);
        }

        [Ignore()]
        [TestMethod]
        [TestProperty(RecordsProperty, "1")]
        [DataSource(DDTSettings.MockarooTypes)]
        /* This test will consume 117/200 of your daily request. Use with caution */
        public void FetchDataAsync_should_export_a_record_for_each_of_the_mockaroo_data_types()
        {
            //// Arrange
            //var records = Convert.ToInt32(TestContext.Properties[RecordsProperty] ?? 1);
            //var endpoint = Gigobyte.Mockaroo.Mockaroo.Endpoint(ApiKey.GetValue(), records, Format.JSON);

            //var dataType = (DataType)Enum.Parse(typeof(DataType), Convert.ToString(TestContext.DataRow[0]));
            //var field = new FieldFactory().CreateInstance(dataType);
            //field.Name = "name";

            //// Act
            //TestContext.WriteLine("Context: {0}", dataType);
            //var data = MockarooClient.FetchDataAsync(endpoint, new Schema(field)).Result;
            //var json = JArray.Parse(Encoding.Default.GetString(data));

            //// Assert
            //json.Count.ShouldBeGreaterThanOrEqualTo(1);
        }

        [TestMethod]
        [TestProperty(RecordsProperty, "1")]
        public void FetchDataAsync_should_export_a_record_containing_all_mockaroo_data_types()
        {
            // Arrange
            var records = Convert.ToInt32(TestContext.Properties[RecordsProperty] ?? 1);
            var endpoint = Acklann.Mockaroo.Mockaroo.Endpoint(ApiKey.GetValue(), records, Format.JSON);

            var schema = new Schema(GetAllFieldTypes());

            // Act

            var data = MockarooClient.FetchDataAsync(endpoint, schema).Result;
            var json = JArray.Parse(Encoding.Default.GetString(data));

            // Assert
            json.Count.ShouldBeGreaterThanOrEqualTo(1);
        }

        [TestMethod]
        [TestProperty(RecordsProperty, "2")]
        public void FetchData_should_export_data_from_mockaroo_and_deserialize_it_into_a_object()
        {
            // Arrange
            var sut = new MockarooClient(ApiKey.GetValue());
            var records = Convert.ToInt32(TestContext.Properties[RecordsProperty] ?? 1);

            // Act
            var results = sut.FetchData<SimpleObject>(records);

            // Assert
            results.ShouldNotBeEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(MockarooException))]
        public async Task FetchDataAsync_should_thrown_an_exception_when_an_error_occurs()
        {
            // Arrange
            var schema = CreateSimpleSchema();
            var endpoint = Acklann.Mockaroo.Mockaroo.Endpoint(ApiKey.GetValue(), 1, Format.JSON);

            // Act
            schema[0].Name = null;
            try
            {
                var data = await MockarooClient.FetchDataAsync(endpoint, schema);
                Assert.Fail("'{0}' was not thrown.", nameof(MockarooException));
            }
            catch (MockarooException ex) { TestContext.WriteLine("message: {0}", ex.Message); throw; }
        }

        #region Private Members

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
            public int IntegerValue { get; set; }

            public float DecimalValue { get; set; }

            public char CharValue { get; set; }

            public string StringValue { get; set; }

            public DateTime DateValue { get; set; }

            public DayOfWeek Day { get; set; }
        }

        #endregion Private Members
    }
}