Feature: Skills Management
As a user, I want to add skills that I know to my profile
So that my abilities can be visible to others.

Scenario: Add a single skill successfully
  Given I am on the Skills section page
  When I enter a skill "Communication"
  Then I should see a message "Communication is added to your skills"

Scenario: Prevent adding a duplicate skill
  Given I am on the Skills section page
  When I enter a skill that already exists in my profile
  Then I should see an error message "Skill already exists"

Scenario: Attempt to add a skill with empty input
  Given I am on the Skills section page
  When I leave the skill or proficiency level field empty
  Then I should see an error message "Please enter a skill and its level"

Scenario: Cancel adding a skill 
  Given I am on the Skills section page
  When I click the Cancel button on the add skill form
  Then the skill form should be closed without saving any data
