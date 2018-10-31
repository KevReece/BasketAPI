using System.Threading.Tasks;
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
        public async Task ThenMyNewBasketShouldBeSaved()
        {
            var savedBasket = await apiContext.Client.BasketByIdGetAsync(apiContext.StringResponse);
            savedBasket.Id.Should().Be(apiContext.StringResponse);
        }

        [Then(@"the response should contain my basket")]
        public void ThenTheResponseShouldContainMyBasket()
        {
            apiContext.BasketResponse.Id.Should().Be(apiContext.BasketId);
        }

        [Then(@"my basket should have the ""(.*)"" item")]
        public async Task ThenMyBasketShouldHaveTheItem(string itemId)
        {
            (await apiContext.Client.BasketByIdGetAsync(apiContext.BasketId)).Items.Keys.Should().Contain(itemId);
        }

        [Then(@"my basket should have the new ""(.*)"" quantity")]
        public async Task ThenMyBasketShouldHaveTheNewQuantity(string itemId)
        {
            var item = (await apiContext.Client.BasketByIdGetAsync(apiContext.BasketId)).Items[itemId];
            item.Quantity.Should().Be(100);
        }

        [Then(@"my basket should not have the ""(.*)"" item")]
        public async Task ThenMyBasketShouldNotHaveTheItem(string itemId)
        {
            (await apiContext.Client.BasketByIdGetAsync(apiContext.BasketId)).Items.Keys.Should().NotContain(itemId);
        }

        [Then(@"my basket should be empty")]
        public async Task ThenMyBasketShouldBeEmpty()
        {
            (await apiContext.Client.BasketByIdGetAsync(apiContext.BasketId)).Items.Should().BeEmpty();
        }
    }
}