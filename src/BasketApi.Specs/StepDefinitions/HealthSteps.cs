using System.Threading.Tasks;
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
        public async Task WhenIRequestGETHealth()
        {
            apiContext.StringResponse = await apiContext.Client.GetAsync();
        }
    }
}