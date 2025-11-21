using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemo.BddFramework.Pages;
using TechTalk.SpecFlow;

namespace SauceDemo.BddFramework.Steps
{
    [Binding]
    public class SauceDemoSteps
    {
        private readonly ScenarioContext _scenarioContext;

        private IWebDriver Driver => _scenarioContext.Get<IWebDriver>("WebDriver");

        private LoginPage LoginPage => new LoginPage(Driver);
        private InventoryPage InventoryPage => new InventoryPage(Driver);
        private CartPage CartPage => new CartPage(Driver);
        private CheckoutInformationPage CheckoutInformationPage => new CheckoutInformationPage(Driver);
        private CheckoutOverviewPage CheckoutOverviewPage => new CheckoutOverviewPage(Driver);
        private CheckoutCompletePage CheckoutCompletePage => new CheckoutCompletePage(Driver);

        public SauceDemoSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        // -------- Scenario 1: Login --------

        [Given(@"I am on the SauceDemo login page")]
        public void GivenIAmOnTheSauceDemoLoginPage()
        {
            LoginPage.Navigate();
        }

        [When(@"I login with username ""(.*)"" and password ""(.*)""")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            LoginPage.Login(username, password);
        }

        [Then(@"I should see the inventory page")]
        public void ThenIShouldSeeTheInventoryPage()
        {
            Assert.That(InventoryPage.GetTitle(), Is.EqualTo("Products"));
        }

        [Then(@"the inventory title should be ""(.*)""")]
        public void ThenTheInventoryTitleShouldBe(string expectedTitle)
        {
            Assert.That(InventoryPage.GetTitle(), Is.EqualTo(expectedTitle));
        }

        [Then(@"the shopping cart icon should be visible")]
        public void ThenTheShoppingCartIconShouldBeVisible()
        {
            Assert.That(InventoryPage.IsCartIconVisible(), Is.True);
        }

        [Then(@"at least one product should be displayed")]
        public void ThenAtLeastOneProductShouldBeDisplayed()
        {
            Assert.That(InventoryPage.GetProductCount(), Is.GreaterThan(0));
        }

        // -------- Scenario 2: Checkout flow --------

        [Given(@"I am logged in on SauceDemo as ""(.*)"" with password ""(.*)""")]
        public void GivenIAmLoggedInOnSauceDemoAsWithPassword(string username, string password)
        {
            LoginPage.Navigate();
            LoginPage.Login(username, password);
            Assert.That(InventoryPage.GetTitle(), Is.EqualTo("Products"));
        }

        [When(@"I add the first 3 products to the cart")]
        public void WhenIAddTheFirst3ProductsToTheCart()
        {
            InventoryPage.AddFirstNProductsToCart(3);
        }

        [When(@"I open the shopping cart")]
        public void WhenIOpenTheShoppingCart()
        {
            InventoryPage.OpenCart();
        }

        [Then(@"I should see 3 items in the cart")]
        public void ThenIShouldSee3ItemsInTheCart()
        {
            Assert.That(CartPage.GetCartItemsCount(), Is.EqualTo(3));
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            CartPage.ClickCheckout();
        }

        [When(@"I fill the checkout information with random data")]
        public void WhenIFillTheCheckoutInformationWithRandomData()
        {
            CheckoutInformationPage.FillRandomInformation();
        }

        [When(@"I click on Continue")]
        public void WhenIClickOnContinue()
        {
            CheckoutInformationPage.ClickContinue();
        }

        [When(@"I click on Finish")]
        public void WhenIClickOnFinish()
        {
            CheckoutOverviewPage.ClickFinish();
        }

        [Then(@"I should see the order confirmation page")]
        public void ThenIShouldSeeTheOrderConfirmationPage()
        {
            Assert.That(CheckoutCompletePage.GetCompleteHeader(), Is.Not.Null.And.Not.Empty);
        }

        [Then(@"the order confirmation title should be ""(.*)""")]
        public void ThenTheOrderConfirmationTitleShouldBe(string expectedTitle)
        {
            Assert.That(CheckoutCompletePage.GetCompleteHeader(), Is.EqualTo(expectedTitle));
        }

        [Then(@"the confirmation message should contain ""(.*)""")]
        public void ThenTheConfirmationMessageShouldContain(string expectedText)
        {
            Assert.That(CheckoutCompletePage.GetCompleteText(), Does.Contain(expectedText));
        }
    }
}