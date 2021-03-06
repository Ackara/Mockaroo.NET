﻿using Acklann.Diffa;
using Acklann.Mockaroo.Fakes;
using Acklann.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Acklann.Mockaroo.Tests
{
    [TestClass]
    public class MockarooClientTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            MockarooClient.SetApiKey(Config.GetApikey());
        }

        [TestMethod]
        public void Can_fetch_raw_data_from_server()
        {
            // Arrange
            var limit = 2;
            var sut = new MockarooClient();
            var fields = GetAllFieldTypes().ToArray();

            var schema = new Schema();
            schema.AddRange(fields);

            // Act
            var data = sut.FetchDataAsync(schema, limit, Format.JSON).Result;
            var result = JArray.Parse(Encoding.UTF8.GetString(data));

            // Assert
            result.Count.ShouldBe(limit);
            Console.WriteLine(result.ToString());
        }

        [TestMethod]
        public void Can_return_data_from_server_as_objects()
        {
            // Arrange
            var limit = 2;
            var sut = new MockarooClient();

            // Act
            var results = sut.FetchDataAsync<CompositeObject>(limit).Result;
            Console.WriteLine(JsonConvert.SerializeObject(results));

            // Assert
            results.Length.ShouldBe(limit);
            results.ShouldAllBe(x => x.Id != 0);
        }

        [TestMethod]
        public void Should_throw_exception_when_schema_is_not_well_formed()
        {
            Should.Throw<Exception>(() =>
            {
                var schema = new Schema
                {
                    new RowNumberField("Id"),
                    new RowNumberField("Id")
                };

                var sut = new MockarooClient(Config.GetApikey());

                var result = sut.FetchDataAsync(schema).Result;
            });
        }

        [TestMethod]
        public void Can_handle_speacial_data_types()
        {
            // Arrange
            var schema = new Schema();
            schema.AddRange(new IField[]
            {
                new AvatarField("avatar"),
                new TemplateField("tempate"){ Value = "{avatar}"},
                new DummyImageURLField("dummy image"),
                new DigitSequenceField("digit sequence") { Format = "###-@@@"},
                new StateField("state"),
                new ParagraphsField("paragraph"),
                new NatoPhoneticField("nato"),
                new FormulaField("formula", "2+2")
            });

            var sut = new MockarooClient();

            // Act
            var data = sut.FetchDataAsync(schema, 1).Result;
            var results = JArray.Parse(Encoding.UTF8.GetString(data)).ToString(Formatting.Indented);

            // Assert
            results.ShouldNotBeNullOrEmpty();
            Diff.Approve(schema, ".json");
            //Diff.Approve(results, ".json", "result");
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