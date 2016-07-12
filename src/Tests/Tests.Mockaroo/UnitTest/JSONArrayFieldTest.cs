using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [UseApprovalSubdirectory(nameof(ApprovalTests))]
    [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
    public class JSONArrayFieldTest
    {
        [ClassCleanup]
        public static void Cleanup()
        {
            ApprovalTests.Maintenance.ApprovalMaintenance.CleanUpAbandonedFiles();
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void Add_should_insert_a_new_item_when_capacity_has_been_reached()
        {
            // Arrange
            var sut = new JSONArrayField();

            // Act
            for (int i = 0; i < 10; i++)
            {
                sut.Add(new NumberField());
            }

            // Assert
            Assert.AreEqual(10, sut.Count);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void Contains_should_return_true_when_a_specified_item_is_in_the_collection()
        {
            // Arrange
            var item = new NumberField();
            var sut = new JSONArrayField();

            // Act
            sut.Add(null);
            sut.Add(item);
            var results = sut.Contains(item);

            // Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void Contains_should_return_true_when_a_null_item_is_in_the_collection()
        {
            // Arrange
            var item = new NumberField();
            var sut = new JSONArrayField();

            // Act
            sut.Add(item);
            sut.Add(null);
            var results = sut.Contains(null);

            // Assert
            Assert.IsTrue(results);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void Remove_should_delete_a_specified_element_from_the_collection()
        {
            // Arrange
            var item = new NumberField();
            var sut = new JSONArrayField();

            // Act
            sut.Add(null);
            var case1 = sut.Remove(null);

            sut.Add(new NumberField());
            sut.Add(item);
            var case2 = sut.Remove(item);

            // Assert
            Assert.IsTrue(case1);
            Assert.IsTrue(case2);
            Assert.AreEqual(1, sut.Count);
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ToJson_should_return_a_valid_json_field_object()
        {
            // Arrange
            var sut = new JSONArrayField() { Name = "Contacts" };

            // Act
            var json = sut.ToJson();

            // Asssert
            Approvals.VerifyJson(json);
        }
    }
}