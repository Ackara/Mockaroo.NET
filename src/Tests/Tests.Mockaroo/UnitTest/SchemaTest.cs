using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [DeploymentItem(Artifact.DataXLSX)]
    [UseApprovalSubdirectory(Artifact.ApprovalsDir)]
    [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
    public class SchemaTest
    {
        public TestContext TestContext { get; set; }

        [ClassCleanup]
        public static void Cleanup()
        {
            ApprovalTests.Maintenance.ApprovalMaintenance.CleanUpAbandonedFiles();
        }

        /// <summary>
        /// Assert <see cref="Schema.Replace(string, IField)"/> replaces existing <see cref="IField"/>.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void EditMockarooSchemaField()
        {
            var sut = new Schema<FakeObject>();

            sut.Replace(x => x.StringValue, DataType.FullName);
            sut.Replace(x => x.DateValue, new DateField() { Max = new DateTime(2016, 01, 01) });
            var json = sut.ToJson();

            Approvals.VerifyJson(json);
        }

        /// <summary>
        /// Removes the field in mockaroo schema.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void RemoveFieldFromMockarooSchema()
        {
            var sut = new Schema<FakeObject>();

            sut.Remove(x => x.DateValue);
            var json = sut.ToJson();

            Approvals.VerifyJson(json);
        }

        /// <summary>
        /// Asserts that all of the .NET built-in/primitive types are mapped to the appropriate
        /// Mockaroo field type. SUT is <see cref="Schema.GetFields(Type)"/>
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        [DataSource(Data.ODBC, Data.ExcelConnectiongString, Data.BuiltInDataSheet, DataAccessMethod.Sequential)]
        public void CreateMockarooFieldFromPrimitiveTypes()
        {
            // Arrange
            var count = 0;
            var dotNetType = $"{TestContext.DataRow[Data.TypeColumn]}";
            var expectedFieldType = Assembly.GetAssembly(typeof(IField)).GetType($"{typeof(IField).Namespace}.{TestContext.DataRow[Data.FieldColumn]}");

            TestContext.WriteLine("input: {0}", dotNetType);
            TestContext.WriteLine("expected: {0}", expectedFieldType);

            // Act
            var fields = Schema.GetFields(Type.GetType("System." + dotNetType));
            var firstField = fields.First();
            count = fields.Count();

            // Assert
            Assert.IsTrue(count == 1, $"{nameof(Schema.GetFields)}() is expected to generate 1 <{nameof(IField)}> not {count}.");
            Assert.IsInstanceOfType(firstField, expectedFieldType);
            Assert.AreEqual(dotNetType, firstField.Name);
        }

        /// <summary>
        /// Assert that all of the child properties that have a getter, a setter, and is also a .NET
        /// built-in/primitive type of the specified type, are mapped to the appropriate Mockaroo
        /// field type. SUT is <see cref="Schema.GetFields(Type)"/>
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void GenerateMockarooFieldsFromObjectType()
        {
            // Arrange
            Func<IEnumerable<IField>, string, bool> contains = (list, target) =>
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
                nameof(FakeObject.StringValue), nameof(FakeObject.Int32Value), nameof(FakeObject.DoubleValue),
                nameof(FakeObject.DecimalValue), nameof(FakeObject.DateValue), nameof(FakeObject.ByteValue),
                nameof(FakeObject.SByteValue), nameof(FakeObject.Int16Value), nameof(FakeObject.UInt16Value),
                nameof(FakeObject.UInt32Value), nameof(FakeObject.Int64Value), nameof(FakeObject.CharValue),
                nameof(FakeObject.UInt64Value), nameof(FakeObject.SingleValue)
            };

            // Act
            var fields = Schema<FakeObject>.GetFields();
            var unexpectedFields = string.Join(", ", fields.Select(x => x.Name).Except(expectedFields));

            // Assert
            Assert.AreEqual(expectedFields.Length, fields.Count(), $"Field(s) ({unexpectedFields}) should not be included in the collection because they do not map to a .NET built-in type or do not have a getter or setter.");
            foreach (var fieldName in expectedFields)
            {
                Assert.IsTrue(contains(fields, fieldName), $"Expected \"{fieldName}\" but was not found.");
            }
        }

        /// <summary>
        /// Assert <see cref="Schema.ToJson"/> returns a valid Mockaroo json request body for the
        /// specified object.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void SerializeMockarooSchemaToJson()
        {
            // Arrange
            var sut = new Schema<FakeObject>();
            sut.Replace(x => x.DateValue, new DateField() { Max = new DateTime(2016, 01, 01) });
            var pattern = @"(?i)\[\s*({(""[_a-z 0-9]+"": (""[^"",{}\[\]]+""+|\[(""[^"",{}\[\]]+"",? ?)+\]),? ?)+},?\s*)+\]";

            // Act
            var json = sut.ToJson();
            TestContext.WriteLine("{0}", json);

            // Assert
            Approvals.VerifyJson(json);
            Assert.IsTrue(new Regex(pattern).IsMatch(json));
        }

        /// <summary>
        /// Assert <see cref="IField.ToJson"/> returns a valid Mockaroo json field object.
        /// </summary>
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ValidateTheMockarooFieldsJsonValue()
        {
            // Arrange
            var errors = new System.Text.StringBuilder();
            var mockarooFields = new FieldFactory().GetAllFields();
            var pattern = new Regex(@"(?i){(""[_a-z 0-9]+"": (""[^"",{}\[\]]+""+|\[(""[^"",{}\[\]]+"",? ?)*\]),? ?)+}");

            // Act
            foreach (var field in mockarooFields)
            {
                var json = field.ToJson();

                if (!pattern.IsMatch(json))
                {
                    errors.AppendLine($"<{field.GetType().Name}> json was invalid.");
                    System.Diagnostics.Debug.WriteLine($"{field.GetType().Name} = {json}");
                }
            }

            // Assert
            Assert.IsTrue(errors.Length == 0, errors.ToString());
        }
    }
}