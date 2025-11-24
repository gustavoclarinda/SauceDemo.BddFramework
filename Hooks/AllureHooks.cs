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
            try
            {
                var outputConfigPath = Path.Combine(AppContext.BaseDirectory, "allureConfig.json");
                var repoConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "allureConfig.json");

                if (!File.Exists(outputConfigPath) && File.Exists(repoConfigPath))
                {
                    // Ensure the configuration is available even if the build output missed it
                    File.Copy(repoConfigPath, outputConfigPath, overwrite: true);
                }

                if (File.Exists(outputConfigPath))
                {
                    Environment.SetEnvironmentVariable("ALLURE_CONFIG", outputConfigPath);
                }

                var resultsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestReports", "allure-results");
                Directory.CreateDirectory(resultsDirectory);

                // Force lifecycle initialization so SpecFlow hooks can use it without hitting null
                var _ = AllureLifecycle.Instance;
            }
            catch (Exception ex)
            {
                // Avoid failing the entire test run if Allure initialization hits an unexpected issue
                Console.Error.WriteLine($"[Allure] Initialization failed: {ex}");
            }
        }
    }
}
