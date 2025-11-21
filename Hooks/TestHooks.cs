using OpenQA.Selenium;
using SauceDemo.BddFramework.Drivers;
using TechTalk.SpecFlow;

namespace SauceDemo.BddFramework.Hooks
{
    [Binding]
    public sealed class TestHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var driver = WebDriverFactory.Create();
            _scenarioContext["WebDriver"] = driver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Get the WebDriver using the same key used in BeforeScenario
            if (_scenarioContext.TryGetValue("WebDriver", out IWebDriver driver) && driver != null)
            {
                try
                {
                    // Close all browser windows and end the WebDriver session
                    driver.Quit();
                }
                catch
                {
                    // As a fallback, try to dispose the driver
                    try
                    {
                        driver.Dispose();
                    }
                    catch
                    {
                        // Ignore any cleanup exceptions
                    }
                }
            }
        }
    }
}
