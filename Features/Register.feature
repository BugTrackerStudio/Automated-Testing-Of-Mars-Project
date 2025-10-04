Feature: User Registration
As a new user, I want to register an account, so I can access the application features.

Scenario: Register with valid credentials
  Given I am on the registration page
  When I enter all required valid details
  Then I should see a registration confirmation message

Scenario: Register with an already registered email
  Given I am on the registration page
  When I enter an already registered email
  Then I should see an error message "This email is already registered"

Scenario: Register with an invalid email format
  Given I am on the registration page
  When I enter an invalid email format
  Then I should see an error message "Please enter a valid email address"

Scenario Outline: Inline validation shows error when a required field is cleared
  Given I am on the registration page
  And I have filled all the required fields
  When I remove the value from the <Field> field
  Then I should see the error message "This field is required" below the <Field> field

 Examples:
   | Field      |
   | First Name |
   | Last Name  |
   | Email Address |
   | Password   |
   | Confirm Password  |

Scenario: User submits the form with only partial details
  Given I am on the registration page
  When I enter only the First Name
  And I check the Terms and Conditions box
  And I click on the Join button
  Then I should not be registered
  And I should not see any error message
