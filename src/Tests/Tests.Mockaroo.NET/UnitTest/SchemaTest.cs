using Gigobyte.Mockaroo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    public class SchemaTest
    {
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void CreateSchemaFieldsFromObject()
        {
            // Arrange
            Func<IEnumerable<IFieldInfo>, string, bool> contains = (list, target) =>
            {
                foreach (var field in list)
                    if (field.Name == target)
                    {
                        return true;
                    }

                return false;
            };

            var expectedFields = new string[]
            {
                nameof(Person.FullName), nameof(Person.Age), nameof(Person.Height),
                nameof(Person.NetWorth), nameof(Person.Dob)
            };

            // Act
            var fields = Schema<Person>.GetFields();

            // Assert
            foreach (var fieldName in expectedFields)
            {
                Assert.IsTrue(contains(fields, fieldName), $"Expected \"{fieldName}\" but was not found.");
            }
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void CreateFieldFromString()
        {
            // Act
            RuntPrimitiveTypeTest<string>(typeof(DateTime));
        }

        internal void RuntPrimitiveTypeTest<T>(Type expectedFieldType)
        {
            // Act
            var fields = Schema<T>.GetFields();
            var firstField = fields.First();
            var count = fields.Count();

            // Assert
            Assert.IsTrue(count == 1, $"The <Schema> object should generate 1 field not {count}.");
            Assert.IsInstanceOfType(firstField, expectedFieldType);
            Assert.AreEqual(typeof(T).Name, firstField.Name);
        }
    }
}