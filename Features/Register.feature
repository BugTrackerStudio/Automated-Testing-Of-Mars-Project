Feature: User Registration
As a new user, I want to register an account so that I can access the application.

Scenario: Register with valid details
  Given I am on the Registration page
  When I enter valid user details
  Then I should see a registration confirmation message

Scenario: Register with an already registered email
  Given I am on the Registration page
  When I enter an email that is already registered
  Then I should see an error message "This email is already registered"

Scenario: Register with invalid email format
  Given I am on the Registration page
  When I enter an invalid email format
  Then I should see an error message "Please enter a valid email address"

Scenario: Register with empty mandatory fields
  Given I am on the Registration page
  When I leave required fields empty
  Then I should see validation error messages "This field is required"
