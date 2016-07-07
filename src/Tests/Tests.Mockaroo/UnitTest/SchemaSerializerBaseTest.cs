using Gigobyte.Mockaroo.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Mockaroo.UnitTest
{
    public abstract class SchemaSerializerBaseTest
    {
        protected virtual void RunTest(ISchemaSerializer sut, object obj)
        {
            var expected = SampleData.CreateSchemaWithDefaults();
            var actual = sut.ConvertToSchema(obj);

            Assert.IsTrue(new SchemaEqualityComparer().Equals(expected, actual));
        }
    }
}