@SauceDemo
Feature: SauceDemo basic flow
  As a QA Engineer
  I want to automate critical flows on SauceDemo
  So that I can validate login, cart, and checkout behavior

  @Login
  Scenario: Login and validate main inventory page elements
    Given I am on the SauceDemo login page
    When I login with username "standard_user" and password "secret_sauce"
    Then I should see the inventory page
    And the inventory title should be "Products"
    And the shopping cart icon should be visible
    And at least one product should be displayed

  @Checkout
  Scenario: Add first 3 items to cart and complete checkout
    Given I am logged in on SauceDemo as "standard_user" with password "secret_sauce"
    When I add the first 3 products to the cart
    And I open the shopping cart
    Then I should see 3 items in the cart
    When I proceed to checkout
    And I fill the checkout information with random data
    And I click on Continue
    And I click on Finish
    Then I should see the order confirmation page
    And the order confirmation title should be "Thank you for your order!"
    And the confirmation message should contain "Your order has been dispatched"