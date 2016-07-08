using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Mockaroo.Fakes;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [UseApprovalSubdirectory(nameof(ApprovalTests))]
    [UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
    public class ClrSchemaSerializerTest
    {
        public TestContext TestContext { get; set; }

        [ClassCleanup]
        public static void Cleanup()
        {
            ApprovalTests.Maintenance.ApprovalMaintenance.CleanUpAbandonedFiles();
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ConvertToSchema_should_convert_a_basic_clr_type_into_a_schema_object()
        {
            var sut = new ClrSchemaSerializer();
            var schema = sut.ConvertToSchema(typeof(SimpleObject));

            Approvals.VerifyJson(schema.ToJson());
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ConvertToSchema_should_convert_a_complex_clr_type_into_a_schema_object()
        {
            var sut = new ClrSchemaSerializer();
            var schema = sut.ConvertToSchema(typeof(ComplexObject));

            Approvals.VerifyJson(schema.ToJson());
        }
    }
}