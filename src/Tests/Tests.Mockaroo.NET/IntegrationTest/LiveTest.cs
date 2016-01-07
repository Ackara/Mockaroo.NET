using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gigobyte.Mockaroo;
using System.Threading.Tasks;

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

        //[TestMethod]
        [Owner(Dev.Ackara)]
        public async Task RunDataRetrievalTest()
        {
            // Arrange
            var sut = new MockarooClient(MockarooApiKey);
            
            // Act
            
        }
    }
}