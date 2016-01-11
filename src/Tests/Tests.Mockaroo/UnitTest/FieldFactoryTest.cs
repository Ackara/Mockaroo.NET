using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    public class FieldFactoryTest
    {
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void CreateMockarooFieldFromDataType()
        {
            // Arrange
            var sut = new FieldFactory();
            var dataTypes = Enum.GetValues(typeof(DataType));

            var errors = new System.Text.StringBuilder();

            // Act
            foreach (var type in dataTypes)
            {
                try
                {
                    var instance = sut.Create((DataType)type);
                    if (instance == null)
                    {
                        errors.AppendLine($"{nameof(sut)} returned an null {nameof(IField)} when {nameof(DataType)}.{type} was passed.");
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"{ex.GetType().Name} was thrown when {nameof(DataType)}.{type} was passed.");
                    errors.AppendLine(ex.Message);
                }
            }

            // Assert
            Assert.IsTrue(errors.Length == 0, errors.ToString());
        }
    }
}