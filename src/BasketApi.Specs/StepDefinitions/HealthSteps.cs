using TechTalk.SpecFlow;

namespace BasketApi.Specs.StepDefinitions
{
    [Binding]
    public class HealthSteps
    {
        private readonly ApiContext apiContext;

        public HealthSteps(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }
        
        [When(@"I request GET health")]
        public void WhenIRequestGETHealth()
        {
            apiContext.StringResponse = apiContext.Client.GetAsync().Result;
        }
    }
}