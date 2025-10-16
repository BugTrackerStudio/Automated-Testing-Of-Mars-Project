Feature: Manage Skills on Profile

  As a user
  I want to add, edit, and delete my skills
  So that my profile accurately reflects my abilities

  Background:
    Given I am logged in with valid credentials
    And I am on the Profile page
    And I navigate to the Skills tab

  # ----------------------
  # Adding Skills
  # ----------------------

  Scenario: Add a new skill with a level
    When I click Add New in the Skills section
    And I enter "Project Management" as the skill
    And I select "Expert" as the skill level
    And I save the skill
    Then I should see a notification "Project Management has been added to your skills"
    And "Project Management - Expert" should appear in the Skills list

  Scenario: Prevent duplicate skills
    Given I already have "Communication" with level "Intermediate" in my Skills list
    When I try to add "Communication" skill with level "Intermediate" again
    Then I should see a notification "This skill is already exist in your skill list"
    And "Communication" with level "Intermediate" should not be duplicated in the Skill list

    Scenario: Add same skill with different level
    Given I already have "Communication" with level "Intermediate" in my Skills list
    When I try to add "Communication" skill with level "Expert"
    Then "Communication" with level "Expert" should be added successfully
    And no duplicate exists for "Communication" with level "Intermediate"

  # ----------------------
  # Editing Skills
  # ----------------------

  Scenario: Edit a skill level
    Given I have "Time Management - Intermediate" in my Skills list
    When I click the edit icon for "Time Management" skill
    And I change the skill level to "Expert"
    And I save the skill changes
    Then I should see a notification "Time Management has been updated to your skills"
    And "Time Management - Expert" should appear in the Skills list

  Scenario: Prevent duplicate skill on edit
    Given I have "Leadership - Intermediate" in my Skills list
    When I try to change "Leadership - Intermediate" to "Leadership - Intermediate" on skill list
    Then I should see a notification "This skill is already added to your skill list."
    And "Leadership - Intermediate" should remain unchanged in the Skills list

  # ----------------------
  # Deleting Skills
  # ----------------------

  Scenario: Delete a skill
    Given I have "Negotiation" in my Skills list
    When I click the delete icon for skill "Negotiation"
    Then I should see a notification "Negotiation has been deleted to your skills"
    And "Negotiation" should not appear in my Skills list
