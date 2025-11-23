using OpenQA.Selenium;
using System;

namespace SauceDemo.BddFramework.SauceCode.Pages
{
    public class CheckoutInformationPage : BasePage
    {
        private By FirstNameInput => By.Id("first-name");
        private By LastNameInput => By.Id("last-name");
        private By PostalCodeInput => By.Id("postal-code");
        private By ContinueButton => By.Id("continue");

        public CheckoutInformationPage(IWebDriver driver) : base(driver) { }

        public void FillRandomInformation()
        {
            var random = Guid.NewGuid().ToString("N").Substring(0, 6);

            Driver.FindElement(FirstNameInput).Clear();
            Driver.FindElement(FirstNameInput).SendKeys("First" + random);

            Driver.FindElement(LastNameInput).Clear();
            Driver.FindElement(LastNameInput).SendKeys("Last" + random);

            Driver.FindElement(PostalCodeInput).Clear();
            Driver.FindElement(PostalCodeInput).SendKeys("ZIP" + random);
        }

        public void ClickContinue()
        {
            Driver.FindElement(ContinueButton).Click();
        }
    }
}