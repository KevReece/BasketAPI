using System.Net;
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
        public void GetsHealth()
        {
            var response = testClientFactory.Create().GetAsync("").Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = response.Content.ReadAsStringAsync().Result;
            stringResponse.Should().Contain("Healthy");
        }
    }
}