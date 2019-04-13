using Acklann.Mockaroo.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.IO;
using System.Linq;

namespace Acklann.Mockaroo.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Can_fetch_and_save_data_to_disk()
        {
            // Arrange
            string file = Path.Combine(Path.GetTempPath(), "basic.json");
            var sut = new MockarooRepository<BasicObject>(Config.GetApikey(), 100, file);

            // Act
            var result1 = sut.First().StringValue;
            var result2 = sut.First().StringValue;

            // Assert
            File.Exists(file).ShouldBeTrue();
            result1.ShouldNotBeNullOrEmpty();
            result2.ShouldNotBeNullOrEmpty();
            result1.ShouldBe(result2);
        }

        [TestMethod]
        public void Can_fetch_new_data()
        {
            // Arrange
            string file = Path.Combine(Path.GetTempPath(), "freshdata.json");
            var sut = new MockarooRepository<BasicObject>(Config.GetApikey(), 10, file);

            // Act
            var result1 = sut.First().StringValue;
            var dead = sut.RefreshAsync().Result;
            var result2 = sut.First().StringValue;

            // Assert
            result1.ShouldNotBeNullOrEmpty();
            result2.ShouldNotBeNullOrEmpty();
            result1.ShouldNotBe(result2);
        }

        [TestMethod]
        public void Should_load_new_data_when_the_schema_changes()
        {
            // Arrange
            var filePath = Path.Combine(Path.GetTempPath(), "autochanges.json");
            var sut = new MockarooRepository<BasicObject>(Config.GetApikey(), 10, filePath);

            // Act
            var result1 = sut.Sync().First().StringValue;
            sut.Schema.Replace(x => x.StringValue, DataType.UserAgent);
            var result2 = sut.Sync().First().StringValue;

            // Assert
            sut.InSync.ShouldBeTrue();
            result1.ShouldNotBe(result2);
            result1.ShouldNotBeNullOrEmpty();
            result2.ShouldNotBeNullOrEmpty();
        }

        [TestMethod]
        public void Should_still_return_data_when_not_insync()
        {
            // Arrange
            var sut = new MockarooRepository<BasicObject>(Config.GetApikey(), 10);

            // Act
            var result1 = sut.First().StringValue;
            sut.Schema.Replace(x => x.NumericValue, DataType.RowNumber);
            var result2 = sut.First().StringValue;

            // Assert
            result1.ShouldNotBeNullOrEmpty();
            result2.ShouldNotBeNullOrEmpty();
        }
    }
}