using FluentAssertions;
using TechTalk.SpecFlow;

namespace BasketApi.Specs.StepDefinitions
{
    [Binding]
    public class BasketAssertionSteps
    {
        private readonly ApiContext apiContext;

        public BasketAssertionSteps(ApiContext apiContext)
        {
            this.apiContext = apiContext;
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

        [Then(@"my basket should not have the ""(.*)"" item")]
        public void ThenMyBasketShouldNotHaveTheItem(string itemId)
        {
            apiContext.Client.BasketByIdGetAsync(apiContext.BasketId).Result.Items.Keys.Should().NotContain(itemId);
        }

        [Then(@"my basket should be empty")]
        public void ThenMyBasketShouldBeEmpty()
        {
            apiContext.Client.BasketByIdGetAsync(apiContext.BasketId).Result.Items.Should().BeEmpty();
        }
    }
}