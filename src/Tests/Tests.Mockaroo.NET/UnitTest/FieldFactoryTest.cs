using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gigobyte.Mockaroo;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    public class FieldFactoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [Owner(Dev.Ackara)]
        [TestProperty("arg", "")]
        public void CreateIFieldInstance()
        {
            // Arrange
            var dataType = (DataType)Enum.Parse(typeof(DataType), TestContext.Properties["arg"].ToString());


            // Act


            // Assert

        }
    }
}
