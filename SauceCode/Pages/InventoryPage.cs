using OpenQA.Selenium;
using System.Linq;

namespace SauceDemo.BddFramework.SauceCode.Pages
{
    public class InventoryPage : BasePage
    {
        private By TitleLabel => By.ClassName("title");
        private By CartIcon => By.Id("shopping_cart_container");
        private By InventoryItems => By.ClassName("inventory_item");
        private By AddToCartButtons => By.CssSelector(".inventory_item button.btn_inventory");
        private By CartLink => By.Id("shopping_cart_container");

        public InventoryPage(IWebDriver driver) : base(driver) { }

        public string GetTitle()
        {
            return Driver.FindElement(TitleLabel).Text;
        }

        public bool IsCartIconVisible()
        {
            return Driver.FindElement(CartIcon).Displayed;
        }

        public int GetProductCount()
        {
            return Driver.FindElements(InventoryItems).Count;
        }

        public void AddFirstNProductsToCart(int count)
        {
            var buttons = Driver.FindElements(AddToCartButtons).Take(count);
            foreach (var btn in buttons)
            {
                btn.Click();
            }
        }

        public void OpenCart()
        {
            Driver.FindElement(CartLink).Click();
        }
    }
}