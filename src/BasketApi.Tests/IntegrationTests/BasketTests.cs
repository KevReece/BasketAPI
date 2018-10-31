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

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = response.Content.ReadAsStringAsync().Result;
            response.IsSuccessStatusCode.Should().BeTrue();
            stringResponse.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public void GetsBasket()
        {
            var basketId = SetupBasketId();

            var response = testClientFactory.Create().GetAsync($"Basket/{basketId}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
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

            var response = testClientFactory.Create().PutAsync($"Basket/{basketId}/Item/{item.Id}", BuildJsonContent(item)).Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void PutFailsAsBadRequest()
        {
            var response = testClientFactory.Create().PutAsync("Basket/unknown/Item/AnItemId/", BuildJsonContent(null)).Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void DeletesBasketItem()
        {
            var basketId = SetupBasketId();
            var item = new Item { Id = "AnItemId", Quantity = 1 };
            var putResponse = testClientFactory.Create().PutAsync($"Basket/{basketId}/Item/{item.Id}", BuildJsonContent(item)).Result;

            var response = testClientFactory.Create().DeleteAsync($"Basket/{basketId}/Item/{item.Id}").Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void DeletesUnknownBasketItemsAsNotFound()
        {
            var response = testClientFactory.Create().DeleteAsync("Basket/unknown/Item/unknownItem").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void DeletesAllBasketItems()
        {
            var basketId = SetupBasketId();

            var response = testClientFactory.Create().DeleteAsync($"Basket/{basketId}/Item/").Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static string SetupBasketId()
        {
            return testClientFactory.Create().PostAsync("Basket", null).Result.Content.ReadAsStringAsync().Result;
        }

        private static StringContent BuildJsonContent(Item item)
        {
            return new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
        }
    }
}