Feature: User Login
As a user, I want to log in to access restricted content.

Scenario: Perform a successful login
  Given I am on the Login page
  When I enter valid credentials
  Then I should see the secure area

Scenario: Attempt to log in with invalid credentials
   Given I am on the Login page
   When I enter invalid credentials
   Then I should see an error message

Scenario: Leave the email field empty
   Given I am on the Login page
   When I leave the email field empty
   Then I should see a validation message for invalid email

Scenario: Leave the password field empty
   Given I am on the Login page
   When I leave the password field empty
   Then I should see a validation message for password length

Scenario: Log in with an invalid email format
   Given I am on the Login page
   When I enter an invalid email format in the username field
   Then I should see a validation message for invalid email format