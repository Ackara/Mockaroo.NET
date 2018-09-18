using Acklann.Mockaroo.Fakes;
using Acklann.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Acklann.Mockaroo.Tests
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void Can_serialize_a_schema_to_json()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public void Can_convert_a_class_to_schema()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public void Can_convert_a_nested_class_to_a_schema()
        {
            throw new System.NotImplementedException();
        }

        [TestMethod]
        public void Can_deserialize_mockaroo_data_to_an_object()
        {
            // Arrange
            var sut = new MockarooConverter();
            var sampleFile = SampleFile.GetBasic_Server_Response();

            // Act
            MutableObject result;
            using (var stream = sampleFile.OpenRead())
            {
                result = sut.Deserialize<MutableObject>(stream);
            }

            // Assert
            result.NumericValue.ShouldBeGreaterThan(0);
            result.DecimalValue.ShouldBeGreaterThan(0);
            result.StringValue.ShouldNotBeNullOrEmpty();
            result.DateValue.Ticks.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Can_deserialize_mockaroo_data_to_a_nested_object()
        {
            // Arrange
            var sut = new MockarooConverter();
            

            // Act

            // Assert
            throw new System.NotImplementedException();
        }
    }
}