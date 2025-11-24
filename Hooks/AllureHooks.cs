using System;
using System.IO;
using Allure.Commons;
using TechTalk.SpecFlow;

namespace SauceDemo.BddFramework.Hooks
{
    [Binding]
    public class AllureHooks
    {
        [BeforeTestRun(Order = -2000)]
        public static void EnsureAllureDirectories()
        {
            var configPath = Path.Combine(AppContext.BaseDirectory, "allureConfig.json");
            if (!File.Exists(configPath))
            {
                var fallbackConfig = Path.Combine(Directory.GetCurrentDirectory(), "allureConfig.json");
                if (File.Exists(fallbackConfig))
                {
                    configPath = fallbackConfig;
                }
            }

            Environment.SetEnvironmentVariable("ALLURE_CONFIG", configPath);

            var resultsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestReports", "allure-results");
            Directory.CreateDirectory(resultsDirectory);

            // Force lifecycle initialization so SpecFlow hooks can use it without hitting null
            var _ = AllureLifecycle.Instance;
        }
    }
}
