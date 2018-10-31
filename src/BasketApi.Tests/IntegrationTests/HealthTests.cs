using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasketApi.Tests.IntegrationTests
{
    [TestClass]
    public class HealthTests
    {
        private static TestClientFactory testClientFactory;

        [ClassInitialize]
        public static void BeforeAllTests(TestContext testContext)
        {
            testClientFactory = new TestClientFactory();
        }

        [ClassCleanup]
        public static void AfterAllTests()
        {
            testClientFactory.Dispose();
        }

        [TestMethod]
        public async Task GetsHealth()
        {
            var response = await testClientFactory.Create().GetAsync("");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().Contain("Healthy");
        }
    }
}