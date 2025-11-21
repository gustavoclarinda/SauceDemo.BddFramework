using OpenQA.Selenium;

namespace SauceDemo.BddFramework.Pages
{
    public class CartPage : BasePage
    {
        private By CartItems => By.ClassName("cart_item");
        private By CheckoutButton => By.Id("checkout");

        public CartPage(IWebDriver driver) : base(driver) { }

        public int GetCartItemsCount()
        {
            return Driver.FindElements(CartItems).Count;
        }

        public void ClickCheckout()
        {
            Driver.FindElement(CheckoutButton).Click();
        }
    }
}