Feature: Manage Languages on Profile

  As a user
  I want to add, edit, and delete my languages
  So that my profile accurately reflects the languages I know

Background:
	Given I am logged in with valid credentials
	And I am on the Profile page

  # ----------------------
  # Adding Languages
  # ----------------------

Scenario: Add a new language with a level
	Given I have less than 4 languages in my Languages list
	When I click Add New in the Languages section
	And I enter "Hindi" as the language
	And I select "Basic" as the level
	And I save the language
	Then I should see a notification "Hindi has been added to your languages"
	And "Hindi - Basic" should appear in the Languages list

Scenario: Prevent duplicate languages
	Given I already have "English" in my Languages list
	When I try to add "English" again
	Then I should see a notification "Duplicate data"
	And "English" should not be duplicated in the list

Scenario: Restrict maximum to 4 languages
	Given I already have 4 languages in my Languages list
	Then the Add New button should not be visible

  # ----------------------
  # Editing Languages
  # ----------------------

Scenario: Edit a language level
	Given I have "Gujarati - Native/Bilingual" in my Languages list
	When I click the edit icon for "Gujarati"
	And I change the level to "Basic"
	And I save the changes
	Then I should see a notification "Gujarati has been updated to your languages"
	And "Gujarati - Basic" should appear in the Languages list

Scenario: Prevent duplicate language on edit
	Given I have "Marathi" in my Languages list
	And I have "English" in my Languages list
	When I try to change "Marathi" to "English"
	Then I should see a notification "Duplicate data"
	And "Marathi" should remain unchanged in the Languages list
	And there should still be only one "English" in the Languages list


  # ----------------------
  # Deleting Languages
  # ----------------------

Scenario: Delete a language
	Given I have "Marathi" in my Languages list
	When I click the delete icon for "Marathi"
	Then I should see a notification "Marathi has been deleted from your languages"
	And "Marathi" should not appear in my Languages list
