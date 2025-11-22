@SauceDemo
Feature: SauceDemo basic flow
  As a QA Engineer
  I want to automate critical flows on SauceDemo
  So that I can validate login, cart, and checkout behavior

  @Login @ui
  Scenario: Login and validate main inventory page elements
    Given I am logged in on SauceDemo as "standard_user" with password "secret_sauce"
    Then I should see the inventory page
    And the inventory title should be "Products"
    And the shopping cart icon should be visible
    And at least one product should be displayed

@ui
Scenario: Login with invalid credentials
    Given I am on the SauceDemo login page
    When I attempt to log in with username "invalid_user" and password "wrong_password"
	Then I should see the error message "Epic sadface: Username and password do not match any user in this service"

  @Checkout @ui
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

@ProductDetails @ui
Scenario: Access the product details page of the Sauce Labs Fleece Jacket
	Given I am logged in on SauceDemo as "standard_user" with password "secret_sauce"
    When I click on the "Sauce Labs Fleece Jacket" product
	Then I should see the product details page
    And I add the product in the card
    When I open the shopping cart
	Then I should see 1 item in the cart