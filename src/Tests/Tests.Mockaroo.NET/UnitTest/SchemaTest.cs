using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Annotation;
using Telerik.JustMock.Helpers;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    public class SchemaTest
    {
        [TestMethod]
        [Owner(Dev.Ackara)]
        public void TestMethod1()
        {
            var schema = new Schema<Person>();
            string[] f = new string[1];
            var ord = f.OrderBy(x => x.Length);
            
            schema.Set(x => x.FullName, new GenericFieldAttribute(""));

            
        }
    }
}
