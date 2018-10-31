using DotNetCoreAwsDockerSwaggerWebpi.Client;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace BasketApi.Specs.StepDefinitions
{
    [Binding]
    public class CommonSteps
    {
        private readonly ApiContext apiContext;

        public CommonSteps(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        [Given(@"the API is running")]
        public void GivenTheAPIIsRunning()
        {
            apiContext.Client = new ApiClient("https://localhost:44354/");
        }

        [Then(@"the response should contain ""(.*)""")]
        public void ThenTheResponseShouldContain(string expectedResponse)
        {
            apiContext.Response.Should().Contain(expectedResponse);
        }
    }
}