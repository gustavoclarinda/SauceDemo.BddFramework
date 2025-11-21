using OpenQA.Selenium;

namespace SauceDemo.BddFramework.Pages
{
    public class LoginPage : BasePage
    {
        private const string LoginUrl = "https://www.saucedemo.com/";

        private By UsernameInput => By.Id("user-name");
        private By PasswordInput => By.Id("password");
        private By LoginButton => By.Id("login-button");

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
    }
}