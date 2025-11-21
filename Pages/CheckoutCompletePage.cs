using OpenQA.Selenium;

namespace SauceDemo.BddFramework.Pages
{
    public class CheckoutCompletePage : BasePage
    {
        private By CompleteHeader => By.ClassName("complete-header");
        private By CompleteText => By.ClassName("complete-text");

        public CheckoutCompletePage(IWebDriver driver) : base(driver) { }

        public string GetCompleteHeader()
        {
            return Driver.FindElement(CompleteHeader).Text;
        }

        public string GetCompleteText()
        {
            return Driver.FindElement(CompleteText).Text;
        }
    }
}