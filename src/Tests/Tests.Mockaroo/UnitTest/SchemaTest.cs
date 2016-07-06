using ApprovalTests;
using ApprovalTests.Reporters;
using Gigobyte.Mockaroo;
using Gigobyte.Mockaroo.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mockaroo.UnitTest
{
    [TestClass]
    [UseReporter(typeof(FileLauncherReporter))]
    public class SchemaTest
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var sut = new Schema(new WordsField() { Name = "Fullname" } );



            var json = JsonConvert.SerializeObject(sut, Formatting.Indented);

            Approvals.VerifyJson(json);
        }
    }
}
