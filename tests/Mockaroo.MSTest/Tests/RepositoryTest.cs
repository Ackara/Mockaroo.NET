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
            string folder = Path.Combine(Path.GetTempPath(), "basic");
            var sut = new MockarooRepository<BasicObject>(Config.GetApikey(), 10, folder, true);

            // Act
            var result1 = sut.First().StringValue;
            var result2 = sut.First().StringValue;

            // Assert
            Directory.Exists(folder).ShouldBeTrue();
            result1.ShouldNotBeNullOrEmpty();
            result2.ShouldNotBeNullOrEmpty();
            result1.ShouldBe(result2);
        }

        [TestMethod]
        public void Can_fetch_new_data()
        {
            // Arrange
            string folder = Path.Combine(Path.GetTempPath(), "freshdata");
            var sut = new MockarooRepository<BasicObject>(Config.GetApikey(), 10, folder, true);

            // Act
            var result1 = sut.First().StringValue;
            sut.Refresh();
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
            var folder = Path.Combine(Path.GetTempPath(), "autochanges");
            var sut = new MockarooRepository<BasicObject>(Config.GetApikey(), 10, folder, true);

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