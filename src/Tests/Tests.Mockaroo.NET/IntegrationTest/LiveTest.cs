using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApprovalTests.Reporters;
using ApprovalTests.Namers;
using Gigobyte.Mockaroo;
using System.Threading.Tasks;

namespace Tests.Mockaroo.NET.IntegrationTest
{
    [TestClass]
    [UseApprovalSubdirectory("Approvals")]
    [UseReporter(typeof(FileLauncherReporter), typeof(ClipboardReporter))]
    public class LiveTest
    {
        [TestMethod]
        [Owner(Dev.Ackara)]
        public async Task FetchBitCoinAddressSample()
        {
            string url = "http://www.mockaroo.com/api/generate.csv?key=ddc59470&count=2";
            string body = "[{\"name\": \"name\", \"type\": \"Full Name\" }]";

            var sut = new MockarooClient(ApiKey.GetValue());

            var res = await sut.FetchDataAsync<string>(2);
        }
    }
}