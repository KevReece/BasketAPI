﻿using DotNetCoreAwsDockerSwaggerWebpi.Client;
using TechTalk.SpecFlow;

namespace BasketApi.Specs.StepDefinitions
{
    [Binding]
    public class BasketActionSteps
    {
        private readonly ApiContext apiContext;

        public BasketActionSteps(ApiContext apiContext)
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

        [Given(@"I have a basket with items")]
        public async void GivenIHaveABasketWithItems()
        {
            apiContext.BasketId = apiContext.Client.BasketPostAsync().Result;
            await apiContext.Client.BasketByIdItemByItemIdPutAsync(apiContext.BasketId, "Apple", new Item { Id = "Apple", Quantity = 1 });
            await apiContext.Client.BasketByIdItemByItemIdPutAsync(apiContext.BasketId, "Banana", new Item { Id = "Banana", Quantity = 2 });
            await apiContext.Client.BasketByIdItemByItemIdPutAsync(apiContext.BasketId, "Cheese", new Item { Id = "Cheese", Quantity = 3 });
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

        [When(@"I DELETE a ""(.*)"" item from my basket")]
        public async void WhenIDELETEAItemFromMyBasket(string itemId)
        {
            await apiContext.Client.BasketByIdItemByItemIdDeleteAsync(apiContext.BasketId, itemId);
        }

        [When(@"I DELETE all items in my basket")]
        public async void WhenIDELETEAllItemsInMyBasket()
        {
            await apiContext.Client.BasketByIdItemDeleteAsync(apiContext.BasketId);
        }
    }
}