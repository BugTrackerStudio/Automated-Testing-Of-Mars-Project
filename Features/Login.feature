Feature: User Login
As a user, I want to log in to the application so that I can access restricted content.

Scenario: Attempt to log in with an unregistered email
  Given I am on the Login page
  When I enter an unregistered email and password
  Then I should see a "Confirm your email" notification

Scenario: Log in with valid credentials
  Given I am on the Login page
  When I enter valid credentials
  Then I should be redirected to the secure area

Scenario: Log in with an invalid password
  Given I am on the Login page
  When I enter a valid username and an invalid password
  Then I should see an error message

Scenario: Log in with an invalid username
  Given I am on the Login page
  When I enter an invalid username and a valid password
  Then I should see an error message 

Scenario: Attempt to log in with empty fields
  Given I am on the Login page
  When I leave the username and password fields empty
  Then I should see an error message
