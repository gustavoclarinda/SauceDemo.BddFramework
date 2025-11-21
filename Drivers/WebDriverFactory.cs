using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemo.BddFramework.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create()
        {
            var options = new ChromeOptions();

            options.AddArgument("--start-maximized");

            // Use a clean session (no saved passwords / Google account)
            options.AddArgument("--incognito");

            // Disable Chrome password manager & credential service
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            // In CI you can also add headless:
            // options.AddArgument("--headless=new");

            return new ChromeDriver(options);
        }
    }
}
