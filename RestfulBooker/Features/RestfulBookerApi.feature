Feature: Restful Booker API
  API tests for the Restful Booker service

  @api @ping
  Scenario: Validate API healthcheck
    When I send a ping request to the Restful Booker API
    Then the response status code should be 201

  @api @createBooking
  Scenario: Create and retrieve a booking
    Given I have a valid booking payload
    When I create a booking in the Restful Booker API
    Then the booking should be successfully created
    And when I retrieve the created booking the returned data should be correct
