using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SauceDemo.BddFramework.Pages
{
    public class LoginPage : BasePage
    {
        private const string LoginUrl = "https://www.saucedemo.com/";

        private By UsernameInput => By.Id("user-name");
        private By PasswordInput => By.Id("password");
        private By LoginButton => By.Id("login-button");
        private By ErrorMessage => By.CssSelector("h3[data-test='error']");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void Navigate()
        {
            NavigateTo(LoginUrl);
        }

        public void Login(string username, string password)
        {
            Driver.FindElement(UsernameInput).Clear();
            Driver.FindElement(UsernameInput).SendKeys(username);
            Driver.FindElement(PasswordInput).Clear();
            Driver.FindElement(PasswordInput).SendKeys(password);
            Driver.FindElement(LoginButton).Click();
        }
        public string GetErrorMessage()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(ErrorMessage).Displayed);

            return Driver.FindElement(ErrorMessage).Text;
        }

    }
}