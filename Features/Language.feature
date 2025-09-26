Feature: Language Management
As a user, I want to add languages that I know to my profile
So that my skills can be visible to others.

Scenario: Add a language with proficiency level
  Given I am on the Language section page
  When I enter a language and its proficiency level
  Then I should see the language saved in my user profile

Scenario: Add multiple languages up to the maximum limit
  Given I am on the Language section page
  When I enter up to 4 languages and their proficiency levels
  Then I should see all languages saved in my user profile

Scenario: Attempt to add more than the maximum allowed languages
  Given I am on the Language section page
  When I enter more than 4 languages
  Then the Add button should be disabled or invisible

Scenario: Prevent adding a duplicate language
  Given I am on the Language section page
  When I enter a language that already exists in my profile
  Then I should see an error message "Duplicate data"

Scenario: Attempt to add a language with empty fields
  Given I am on the Language section page
  When I leave the language or proficiency level field empty
  Then I should see an error message "Please enter a language and its level"

Scenario: Cancel adding a language
  Given I am on the Language section page
  When I click the Cancel button on the language form
  Then the language form should be closed without saving any data
