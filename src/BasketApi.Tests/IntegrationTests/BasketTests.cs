using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public async Task PostBasketReturnsId()
        {
            var response = await testClientFactory.Create().PostAsync("Basket", null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            response.IsSuccessStatusCode.Should().BeTrue();
            stringResponse.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public async Task GetsBasket()
        {
            var basketId = await SetupBasketId();

            var response = await testClientFactory.Create().GetAsync($"Basket/{basketId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            response.IsSuccessStatusCode.Should().BeTrue();
            stringResponse.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public async Task GetsUnknownBasketAsNotFound()
        {
            var response = await testClientFactory.Create().GetAsync("Basket/unknown");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task PutsBasketItems()
        {
            var basketId = await SetupBasketId();
            var item = new Item{Id = "AnItemId", Quantity = 1};

            var response = await testClientFactory.Create().PutAsync($"Basket/{basketId}/Item/{item.Id}", BuildJsonContent(item));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task PutFailsAsBadRequest()
        {
            var response = await testClientFactory.Create().PutAsync("Basket/unknown/Item/AnItemId/", BuildJsonContent(null));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task DeletesBasketItem()
        {
            var basketId = await SetupBasketId();
            var item = new Item { Id = "AnItemId", Quantity = 1 };
            await testClientFactory.Create().PutAsync($"Basket/{basketId}/Item/{item.Id}", BuildJsonContent(item));

            var response = await testClientFactory.Create().DeleteAsync($"Basket/{basketId}/Item/{item.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task DeletesUnknownBasketItemsAsNotFound()
        {
            var response = await testClientFactory.Create().DeleteAsync("Basket/unknown/Item/unknownItem");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task DeletesAllBasketItems()
        {
            var basketId = await SetupBasketId();

            var response = await testClientFactory.Create().DeleteAsync($"Basket/{basketId}/Item/");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static async Task<string> SetupBasketId()
        {
            return await (await testClientFactory.Create().PostAsync("Basket", null)).Content.ReadAsStringAsync();
        }

        private static StringContent BuildJsonContent(Item item)
        {
            return new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
        }
    }
}