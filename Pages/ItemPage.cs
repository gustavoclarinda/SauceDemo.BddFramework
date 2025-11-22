using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace SauceDemo.BddFramework.Pages
{
    public class ItemPage : BasePage
    {
        public By ProductName => By.CssSelector(".inventory_details_name.large_size");
        private By Fleecejacket => By.XPath("//div[@data-test='inventory-item-name' and text()='Sauce Labs Fleece Jacket']");
        public By ProductDescription => By.CssSelector("div.inventory_details_desc.large_size");
        public By ProductPrice => By.CssSelector("div.inventory_details_price");
        public By AddProducttoCartButton => By.Id("add-to-cart");


        public ItemPage(IWebDriver driver) : base(driver) { }
        public void OpenFleecejacket()
        {
            Driver.FindElement(Fleecejacket).Click();
        }
        public string GetItemTitle()
        {
            Wait.Until(d => d.FindElement(ProductName).Displayed);
            return Driver.FindElement(ProductName).Text;
        }


        public bool IsProductDescriptionVisible()
        {
            return Driver.FindElement(ProductDescription).Displayed;
        }

        public bool IsProductPriceVisible()
        {
            return Driver.FindElement(ProductPrice).Displayed;
        }
        public void AddProductToCart()
        {
            Driver.FindElement(AddProducttoCartButton).Click();
        }
    }
}





