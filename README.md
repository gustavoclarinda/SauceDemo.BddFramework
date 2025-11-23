# SauceDemo BDD Automation Framework

This repository contains a BDD automation framework for the [SauceDemo](https://www.saucedemo.com/) application, built in **C#/.NET 8**, using **SpecFlow (Cucumber for .NET)**, **NUnit**, and **Selenium WebDriver**.

The goal is to provide a professional, interview-ready example of a QA Automation framework that:

- Uses **Gherkin BDD** for business-readable scenarios  
- Implements **Page Object Model (POM)**  
- Runs locally and in **GitHub Actions CI**  
- Generates **test result reports** (TRX artifacts in the pipeline)  

---

## Quickstart
    
A short, step-by-step guide to get the project running locally.

1. Clone the repository:

   git clone https://github.com/gustavoclarinda/SauceDemo.BddFramework.git
   cd SauceDemo.BddFramework

2. Restore dependencies and build:

   dotnet restore
   dotnet build --configuration Release

3. Run tests (see "Running tests" section below for category filters and report options).

---

## Prerequisites

- .NET 8 SDK
- Chrome browser (for UI tests)
- ChromeDriver compatible with your Chrome version (if not using a driver manager)
- Optional: GitHub CLI or access to GitHub Actions for CI runs

---

## Running tests

Keep the existing category-filter commands for reproducible test runs.

API Testing with Report:

dotnet test --filter "TestCategory=api" --logger "trx;LogFileName=TestResults_api.trx"

UI Testing with Report:

dotnet test --filter "TestCategory=ui" --logger "trx;LogFileName=TestResults_ui.trx"

Additional notes:

- To run all tests: `dotnet test`.
- Use `--verbosity normal` for more test output if troubleshooting.
- Test result TRX files are produced in the test runner output folder and can be consumed by CI and reporting tools.

### Gerar relatório HTML local (LivingDoc)

1. Restaure a ferramenta de linha de comando do LivingDoc (manifesto local em `.config/dotnet-tools.json`):

   ```bash
   dotnet tool restore
   ```

2. Execute o script de geração para rodar os testes e converter o `TestExecution.json` em um relatório HTML:

   ```bash
   ./generate-livingdoc-report.sh
   ```

   - Por padrão os artefatos ficam em `TestReports/` (`TestReports/TestResults` para TRX e `TestReports/LivingDoc.html` para o relatório).
   - Use `-c Release` para alterar a configuração de build ou `-o <pasta>` para escolher um diretório de saída diferente.
   - O script executa `dotnet test` com o plugin `SpecFlow.Plus.LivingDocPlugin` já configurado no `csproj` e falhará se o `TestExecution.json` não for gerado.

### Executar testes e gerar o LivingDoc pelo Visual Studio

Se preferir rodar os testes no **Visual Studio** e gerar o mesmo relatório HTML do LivingDoc, siga estes passos:

1. **Rodar os testes no Test Explorer**
   - Abra a solução `SauceDemo.BddFramework.sln` no Visual Studio.
   - Acesse **Test > Test Explorer** e clique em **Run All** (ou filtre por categoria nas configurações do Test Explorer).
   - Certifique-se de que as configurações de Resultados de Teste do Visual Studio apontam para um diretório conhecido (por exemplo, `TestReports/TestResults`). Caso queira alterar, vá em **Tools > Options > Test > General** e ajuste o caminho de saída.

2. **Localizar o `TestExecution.json` gerado pelo SpecFlow**
   - Após a execução, o Visual Studio gravará arquivos TRX e o `TestExecution.json` gerado pelo plugin `SpecFlow.Plus.LivingDocPlugin` dentro do diretório de resultados (por padrão fica em `TestResults` sob a pasta da solução, a menos que você configure um caminho customizado).

3. **Gerar o relatório LivingDoc a partir do resultado do Visual Studio**
   - Em um terminal dentro da solução, restaure a ferramenta local e gere o HTML usando o arquivo `TestExecution.json` criado pelo Visual Studio:

     ```bash
     dotnet tool restore
     livingdoc test-assembly SauceDemo.BddFramework/bin/Debug/net8.0/SauceDemo.BddFramework.dll \
       -t "<CAMINHO_PARA>/TestExecution.json" \
       -o TestReports/LivingDoc.html
     ```

   - Ajuste o caminho do DLL e do `TestExecution.json` se você usou outra configuração de build (por exemplo, `Release`).
   - Como alternativa, você pode reutilizar o script `./generate-livingdoc-report.sh` apontando o parâmetro `-o` para o mesmo diretório de resultados configurado no Visual Studio, garantindo que o `TestExecution.json` seja encontrado automaticamente.

---

## Tech Stack

- **Language:** C# (.NET 8)
- **Test Runner:** NUnit
- **BDD Framework:** SpecFlow (Gherkin)
- **UI Automation:** Selenium WebDriver (Chrome)
- **CI/CD:** GitHub Actions

---

## Project Structure

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

---

## Troubleshooting

- If ChromeDriver version mismatches, update the driver or Chrome to matching versions.
- If tests hang, run with `--logger "trx;LogFileName=debug.trx" --verbosity d` and inspect the TRX.
- Ensure environment variables used by tests (if any) are set before running.

---

## What changed

This addition reorganizes the README to include a Quickstart, Prerequisites, and Troubleshooting sections and preserves the original test commands and project structure references.

This revised `README.md` maintains the original content while enhancing the document's usability and clarity by adding structured sections for quick start, prerequisites, and troubleshooting.

## Run:

UI Testing with Report:
dotnet test --filter "TestCategory=ui" --logger "trx;LogFileName=TestResults_ui.trx"

API Testing with Report:
dotnet test --filter "TestCategory=api" --logger "trx;LogFileName=TestResults_api.trx"