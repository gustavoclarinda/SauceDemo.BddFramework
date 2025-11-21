using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SauceDemo.BddFramework.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create()
        {
            var options = new ChromeOptions();

            // Detect if running in CI (GitHub Actions sets CI=true)
            var isCi = string.Equals(
                Environment.GetEnvironmentVariable("CI"),
                "true",
                StringComparison.OrdinalIgnoreCase);

            if (isCi)
            {
                // Headless mode for CI
                options.AddArgument("--headless=new");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
            }
            else
            {
                // Local dev: open normal window
                options.AddArgument("--start-maximized");
            }

            // Use a clean session (no saved passwords / Google account)
            options.AddArgument("--incognito");

            // Disable Chrome password manager
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            return new ChromeDriver(options);
        }
    }
}
