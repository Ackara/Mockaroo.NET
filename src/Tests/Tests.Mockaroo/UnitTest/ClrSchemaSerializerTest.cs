using Gigobyte.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    public class ClrSchemaSerializerTest : SchemaSerializerBaseTest
    {
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void ConvertToSchema_should_convert_a_clr_type_into_a_schema_object()
        {
            var sut = new ClrSchemaSerializer();
            base.RunTest(sut, typeof(SimpleObject));
        }
    }
}