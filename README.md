# SauceDemo BDD Automation Framework

This repository contains a BDD automation framework for the [SauceDemo](https://www.saucedemo.com/) application, built in **C#/.NET 8**, using **SpecFlow (Cucumber for .NET)**, **NUnit**, and **Selenium WebDriver**.

The goal is to provide a professional, interview-ready example of a QA Automation framework that:

- Uses **Gherkin BDD** for business-readable scenarios
- Implements **Page Object Model (POM)**
- Runs locally and in **GitHub Actions CI**
- Generates **test result reports** (TRX artifacts in the pipeline) and **Allure HTML reports** locally and in GitHub Actions

---

## Quickstart

A short, step-by-step guide to get the project running locally.

1. Clone the repository:

   ```bash
   git clone https://github.com/gustavoclarinda/SauceDemo.BddFramework.git
   cd SauceDemo.BddFramework
   ```

2. Restore dependencies and build:

   ```bash
   dotnet restore
   dotnet build --configuration Release
   ```

3. Run tests (see "Running tests" section below for category filters and report options).

---

## Prerequisites

- .NET 8 SDK
- Chrome browser (for UI tests)
- ChromeDriver compatible with your Chrome version (if not using a driver manager)
- Optional: GitHub CLI or access to GitHub Actions for CI runs

---

## Running tests

Use the category-filter commands below for reproducible test runs.

**API Testing with TRX output:**

```bash
dotnet test --filter "TestCategory=api" --logger "trx;LogFileName=TestResults_api.trx"
```

**UI Testing with TRX output:**

```bash
dotnet test --filter "TestCategory=ui" --logger "trx;LogFileName=TestResults_ui.trx"
```

Additional notes:

- To run all tests: `dotnet test`.
- Use `--verbosity normal` for more test output if troubleshooting.
- Test result TRX files are produced in the test runner output folder and can be consumed by CI and reporting tools.

### Generate a local Allure HTML report

1. Install the Allure CLI (pick the option for your OS):
   - **Windows**: `choco install allure.commandline -y`
   - **macOS**: `brew install allure`
   - **Any OS with Node.js**: `npm install -g allure-commandline`
   - If Chocolatey is unavailable on your Windows environment, use the Node.js option instead.

2. Execute the helper script to run tests and build the Allure report:

   ```bash
   ./generate-allure-report.sh
   ```

   - Artifacts are written to `TestReports/` (`TestReports/allure-results` for raw JSON/XML results, `TestReports/allure-report` for the HTML).
   - Use `-c Release` to change the build configuration or `-o <folder>` to choose a different output directory.
   - Open `TestReports/allure-report/index.html` in your browser to explore the results.

### Generate the Allure report when running from Visual Studio

1. Open the solution and run your scenarios from **Test Explorer** as usual.
2. After the run, ensure the `allure-results` folder exists in the repo root (created automatically by the Allure SpecFlow plugin).
3. Generate the report using the Allure CLI:

   ```powershell
   allure generate TestReports/allure-results --clean -o TestReports/allure-report
   ```

   Adjust the paths if you used a different output directory or build configuration.

### Allure reporting in GitHub Actions

The `CI - SauceDemo BDD Automation` workflow now builds and publishes an Allure report on every push or pull request to `master`:

1. Tests run in **Release** with the Allure SpecFlow plugin enabled, producing `TestReports/allure-results` and TRX files.
2. The action installs the Allure CLI globally via `npm install -g allure-commandline` and generates `TestReports/allure-report` (HTML output).
3. Artifacts published in the workflow run:
   - `test-results-trx`: TRX test result files.
   - `allure-results`: raw Allure result files.
   - `allure-report`: folder with the generated HTML.
4. When the Allure report exists, it is pushed to **GitHub Pages** (environment `github-pages`), and the page URL appears in the deployment step.

---

## Tech Stack

- **Language:** C# (.NET 8)
- **Test Runner:** NUnit
- **BDD Framework:** SpecFlow (Gherkin)
- **UI Automation:** Selenium WebDriver (Chrome)
- **CI/CD:** GitHub Actions

---

## Project Structure

```
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
```

---

## Troubleshooting

- If ChromeDriver version mismatches, update the driver or Chrome to matching versions.
- If tests hang, run with `--logger "trx;LogFileName=debug.trx" --verbosity d` and inspect the TRX.
- Ensure any environment variables used by tests (if any) are set before running.

---

## What changed

This version of the README restructures the document with Quickstart, Prerequisites, Running tests, Allure reporting, and Troubleshooting sections to improve clarity while preserving the original test commands and project structure references.
