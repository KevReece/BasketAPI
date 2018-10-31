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

        [When(@"I request GET basket for my basket ID")]
        public void WhenIRequestGETBasketForMyBasketID()
        {
            apiContext.Response = apiContext.Client.BasketByIdGetAsync("MyBasketId").Result;
        }
    }
}