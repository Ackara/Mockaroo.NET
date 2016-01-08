using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gigobyte.Mockaroo;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace Tests.Mockaroo.IntegrationTest
{
    [TestClass]
    public class LiveTest
    {
        internal static string MockarooApiKey;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            MockarooApiKey = ApiKey.GetValue();
        }

        [TestMethod]
        [Owner(Dev.Ackara)]
        public async Task RunDataRetrievalTest()
        {
            // Arrange
            var sut = new MockarooClient(MockarooApiKey);

            // Act
            var data = await sut.FetchDataAsync<string>(2);

            System.Diagnostics.Debug.WriteLine(data);
        }
    }
}