# SauceDemo BDD Automation Framework

This repository contains a BDD automation framework for the [SauceDemo](https://www.saucedemo.com/) application, built in **C#/.NET 8**, using **SpecFlow (Cucumber for .NET)**, **NUnit**, and **Selenium WebDriver**.

The goal is to provide a professional, interview-ready example of a QA Automation framework that:

- Uses **Gherkin BDD** for business-readable scenarios  
- Implements **Page Object Model (POM)**  
- Runs locally and in **GitHub Actions CI**  
- Generates **test result reports** (TRX artifacts in the pipeline)  

---

## Tech Stack

- **Language:** C# (.NET 8)
- **Test Runner:** NUnit
- **BDD Framework:** SpecFlow (Gherkin)
- **UI Automation:** Selenium WebDriver (Chrome)
- **CI/CD:** GitHub Actions

---

## Project Structure

```text
SauceDemo.BddFramework/
  Features/
    SauceDemo.feature          # BDD scenarios (Gherkin)
  Steps/
    SauceDemoSteps.cs          # Step definitions
  Pages/
    BasePage.cs
    LoginPage.cs
    InventoryPage.cs
    CartPage.cs
    CheckoutInformationPage.cs
    CheckoutOverviewPage.cs
    CheckoutCompletePage.cs
  Hooks/
    TestHooks.cs               # WebDriver setup/teardown per scenario
  Drivers/
    WebDriverFactory.cs        # Centralized WebDriver configuration
  specflow.json                # SpecFlow configuration
  SauceDemo.BddFramework.csproj

Run:
API Testing with Report:
dotnet test --filter "TestCategory=api" --logger "trx;LogFileName=TestResults_api.trx"

UI Testing with Report:
dotnet test --filter "TestCategory=ui" --logger "trx;LogFileName=TestResults_ui.trx"
