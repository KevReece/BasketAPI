using System.Net;
using System.Net.Http;
using System.Text;
using BasketApi.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BasketApi.Tests.IntegrationTests
{
    [TestClass]
    public class BasketTests
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
        public void PostBasketReturnsId()
        {
            var response = testClientFactory.Create().PostAsync("Basket", null).Result;

            var stringResponse = response.Content.ReadAsStringAsync().Result;
            response.IsSuccessStatusCode.Should().BeTrue();
            stringResponse.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public void GetsBasket()
        {
            var basketId = SetupBasketId();

            var response = testClientFactory.Create().GetAsync($"Basket/{basketId}").Result;

            var stringResponse = response.Content.ReadAsStringAsync().Result;
            response.IsSuccessStatusCode.Should().BeTrue();
            stringResponse.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public void GetsUnknownBasketAsNotFound()
        {
            var response = testClientFactory.Create().GetAsync("Basket/unknown").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void PutsBasketItems()
        {
            var basketId = SetupBasketId();
            var item = new Item{Id = "AnItemId", Quantity = 1};
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            var response = testClientFactory.Create().PutAsync($"Basket/{basketId}/Item/{item.Id}", content).Result;

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [TestMethod]
        public void PutFailsAsBadRequest()
        {
            var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

            var response = testClientFactory.Create().PutAsync($"Basket/unknown/Item/AnItemId/", content).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private static string SetupBasketId()
        {
            return testClientFactory.Create().PostAsync("Basket", null).Result.Content.ReadAsStringAsync().Result;
        }
    }
}