using Acklann.Diffa;
using Acklann.Diffa.Reporters;
using Acklann.Mockaroo.Fakes;
using Acklann.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.IO;
using System.Linq;

namespace Acklann.Mockaroo.Tests
{
    [TestClass]
    //[Use(typeof(NotepadPlusPlusReporter), doNotPauseIfTestFails: true)]
    public class SerializationTest
    {
        //  Type  ::>  Schema
        // ==================================================

        [TestMethod]
        public void Can_convert_a_basic_type_to_a_schema()
        {
            RunTypeToSchemaTest<BasicObject>();
        }

        [TestMethod]
        public void Can_convert_a_nested_type_to_a_schema()
        {
            RunTypeToSchemaTest<NestedObject>(3);
        }

        [TestMethod]
        public void Can_convert_a_collections_to_a_schema()
        {
            RunTypeToSchemaTest<VariousCollections>(2);
        }

        [TestMethod]
        public void Can_convert_a_composite_type_to_a_schema()
        {
            RunTypeToSchemaTest<CompositeObject>(3);
        }

        [TestMethod]
        [Use(typeof(NullReporter))]
        public void Should_throw_when_type_has_no_parameterless_constructor()
        {
            Should.Throw<ArgumentException>(() =>
            {
                var schema = MockarooConvert.ToSchema<BadObject>(1);
            });
        }

        [TestMethod]
        public void Should_ignore_immutable_types()
        {
            Should.Throw<ArgumentOutOfRangeException>(() =>
            {
                var schema = MockarooConvert.ToSchema<ImmutableObject>(0);
                var json = JArray.Parse(schema.ToString()).ToString(Newtonsoft.Json.Formatting.Indented);
            });
        }

        // Server-Response  ::>  Object
        // ==================================================

        [TestMethod]
        public void Can_deserialize_a_response_to_basic_object()
        {
            // Arrange
            var response = TestData.GetBasicResponse();

            // Act
            BasicObject result;
            using (var stream = response.OpenRead())
            {
                result = MockarooConvert.FromJson<BasicObject>(stream).First();
            }

            // Assert
            result.CharValue.ShouldBe('T');
            result.NumericValue.ShouldNotBe(0);
            result.NumericArray.ShouldNotBeEmpty();
            result.StringValue.ShouldNotBeNullOrEmpty();
            result.DateValue.Ticks.ShouldBeGreaterThan(0);
            result.TimeValue.Ticks.ShouldBeGreaterThan(0);
            result.StringCollection.ShouldNotBeEmpty();
        }

        [TestMethod]
        public void Can_deserialize_a_response_to_a_recursive_object()
        {
            // Arrange
            var response = TestData.GetNestedResponse();

            // Act
            var result = (NestedObject)MockarooConvert.FromJson(File.ReadAllText(response.FullName), typeof(NestedObject)).First();

            // Assert
            result.NumericValue.ShouldBeGreaterThan(0);
            result.Recursive.NumericValue.ShouldBeGreaterThan(0);
            result.Recursive.Recursive.NumericValue.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Can_deserialize_a_response_to_composite_object()
        {
            // Act
            CompositeObject result;
            using (var stream = TestData.GetCompositeResponse().OpenRead())
            {
                result = MockarooConvert.FromJson<CompositeObject>(stream).First();
            }

            // Assert
            result.Id.ShouldNotBe(0);
            result.Basic.StringValue.ShouldNotBeNullOrEmpty();
            result.Nested.Recursive.NumericValue.ShouldNotBe(0);

            result.Collection1.ShouldNotBeEmpty();
            result.Collection1[0].NumericArray.ShouldNotBeEmpty();
        }

        [TestMethod]
        public void Can_deserialize_a_response_to_an_array_objects()
        {
            // Act
            VariousCollections results;
            using (var stream = TestData.GetCollectionResponse().OpenRead())
            {
                results = MockarooConvert.FromJson<VariousCollections>(stream).First();
            }

            // Assert
            results.Capacity.ShouldNotBe(0);
            results.BasicObjectList.Count().ShouldBeGreaterThan(0);
            results.BasicObjectList.ShouldAllBe(x => x.NumericArray.Length > 0);
            results.BasicObjectList.ShouldAllBe(x => string.IsNullOrEmpty(x.StringValue) == false);
        }

        private static void RunTypeToSchemaTest<T>(int depth = 1)
        {
            var schema = MockarooConvert.ToSchema<T>(depth);
            var json = JArray.Parse(schema.ToString()).ToString(Newtonsoft.Json.Formatting.Indented);
            Diff.Approve(json, ".json");
        }
    }
}