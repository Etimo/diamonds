using Microsoft.AspNetCore.Mvc;
using Diamonds.Api.Controllers;
using TechTalk.SpecFlow;
using Xunit;

namespace Diamonds.Api.Tests
{
    [Binding]
    public class StepDefinitions
    {
        [Given(@"I am curious")]
        public void GivenIAmCurious()
        {
            //ScenarioContext.Current.Pending();
        }

        [When(@"I request the version")]
        public void WhenIRequestTheVersion()
        {
            var controller = new BotsController();

            var result = controller.Get();
            ScenarioContext.Current.Add("versionResult", result);
        }

        [Then(@"the result is content")]
        public void ThenTheResultIsContent()
        {
            var versionResult = ScenarioContext.Current["versionResult"];
            Assert.True(versionResult is OkObjectResult);
        }
        
        [Then(@"the result is constant")]
        public void ThenTheResultIsConstant()
        {
            var versionResult = (OkObjectResult)ScenarioContext.Current["versionResult"];
            Assert.Equal(versionResult.Value, BotsController.AppVersion);
        }

    }
}