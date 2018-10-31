using DotNetCoreAwsDockerSwaggerWebpi.Client;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace BasketApi.Specs.StepDefinitions
{
    [Binding]
    public class BasketSteps
    {
        private readonly ApiContext apiContext;

        public BasketSteps(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        [Given(@"I have a basket")]
        public void GivenIHaveABasket()
        {
            apiContext.BasketId = apiContext.Client.BasketPostAsync().Result;
        }

        [Given(@"I have a basket with a ""(.*)"" item")]
        public async void GivenIHaveABasketWithAItem(string itemId)
        {
            apiContext.BasketId = apiContext.Client.BasketPostAsync().Result;
            await apiContext.Client.BasketByIdItemByItemIdPutAsync(apiContext.BasketId, itemId, new Item { Id = itemId, Quantity = 1 });
        }

        [When(@"I GET my basket")]
        public void WhenIGETMyBasket()
        {
            apiContext.BasketResponse = apiContext.Client.BasketByIdGetAsync(apiContext.BasketId).Result;
        }
        
        [When(@"I POST a basket without basket ID")]
        public void WhenIPOSTABasketWithoutBasketID()
        {
            apiContext.StringResponse = apiContext.Client.BasketPostAsync().Result;
        }

        [When(@"I PUT a ""(.*)"" item in to my basket")]
        public async void WhenIPUTAItemInToMyBasket(string itemId)
        {
            await apiContext.Client.BasketByIdItemByItemIdPutAsync(apiContext.BasketId, itemId, new Item { Id = itemId, Quantity = 1 });
        }

        [When(@"I PUT my ""(.*)"" item as a new quantity in my basket")]
        public async void WhenIPUTMyItemAsANewQuantityInMyBasket(string itemId)
        {
            await apiContext.Client.BasketByIdItemByItemIdPutAsync(apiContext.BasketId, itemId, new Item { Id = itemId, Quantity = 100 });
        }

        [Then(@"the response should contain my new basket ID")]
        public void ThenTheResponseShouldContainMyNewBasketID()
        {
            apiContext.StringResponse.Should().NotBeNullOrWhiteSpace();
        }

        [Then(@"my new basket should be saved")]
        public void ThenMyNewBasketShouldBeSaved()
        {
            var savedBasket = apiContext.Client.BasketByIdGetAsync(apiContext.StringResponse).Result;
            savedBasket.Id.Should().Be(apiContext.StringResponse);
        }

        [Then(@"the response should contain my basket")]
        public void ThenTheResponseShouldContainMyBasket()
        {
            apiContext.BasketResponse.Id.Should().Be(apiContext.BasketId);
        }

        [Then(@"my basket should have the ""(.*)"" item")]
        public void ThenMyBasketShouldHaveTheItem(string itemId)
        {
            apiContext.Client.BasketByIdGetAsync(apiContext.BasketId).Result.Items.Keys.Should().Contain(itemId);
        }

        [Then(@"my basket should have the new ""(.*)"" quantity")]
        public void ThenMyBasketShouldHaveTheNewQuantity(string itemId)
        {
            var item = apiContext.Client.BasketByIdGetAsync(apiContext.BasketId).Result.Items[itemId];
            item.Quantity.Should().Be(100);
        }
    }
}