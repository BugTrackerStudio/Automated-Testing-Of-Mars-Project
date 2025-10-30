Feature: Manage Education on Profile
  As a user
  I want to add, edit, delete, and view my education records
  So that my profile accurately reflects my academic achievements

  Background:
    Given I am logged in with valid credentials
    And I am on the Profile page
    And I clicked on the Education tab

@AddEducation
Scenario Outline: 1-Add Education Successfully
    When I add my education including '<Country>', '<University>', '<Title>', '<Degree>', '<Graduation Year>'
    Then I am able to see my education details including '<Country>', '<University>', '<Title>', '<Degree>', '<Graduation Year>'
    And I should see a success message confirming education added

    Examples:
      | Country   | University | Title   | Degree | Graduation Year |
      | Australia | APIC       | M.Tech  | MIT    | 2021            |
      | India     | GEC Patan  | B.Tech  | BE     | 2011            |

@AddEducationValidation
Scenario Outline: 2-Add Education with Missing Mandatory Fields
    When I try to add an education record with missing details '<Country>', '<University>', '<Title>', '<Degree>', '<Graduation Year>'
    Then I should see an error message for missing fields

    Examples:
      | Country   | University | Title   | Degree | Graduation Year |
      | Australia |             | M.Tech  | MIT    | 2021            |
      | India     | GEC Patan  | B.Tech  |        | 2011            |

@DuplicateEducation
Scenario Outline: 3-Add Duplicate Education
    Given I have an existing education '<Country>', '<University>', '<Title>', '<Degree>', '<Graduation Year>'
    When I add the same education again
    Then I should see a message "Duplicate data is not allowed"

    Examples:
      | Country   | University | Title   | Degree | Graduation Year |
      | Australia | APIC       | M.Tech  | MIT    | 2021            |

@AllowSameUniTitleDifferentDegree
Scenario Outline: 4-Allow Same University and Title but Different Degree
    Given I have an existing education '<Country>', '<University>', '<Title>', '<Degree>', '<Graduation Year>'
    When I add the same university and title '<University>', '<Title>' but with a different degree '<New Degree>'
    Then the system should allow adding the record successfully

    Examples:
      | Country   | University | Title   | Degree | Graduation Year | New Degree |
      | India     | GEC Patan  | B.Tech  | BE     | 2011            | M.Tech     |

@AddMultipleEducation
Scenario Outline: 5-Add Multiple Education Records
    When I add multiple education records:
      | Country   | University | Title   | Degree | Graduation Year |
      | Australia | APIC       | M.Tech  | MIT    | 2021            |
      | India     | GEC Patan  | B.Tech  | BE     | 2011            |
      | USA       | MIT        | B.Sc    | Degree | 2020            |
    Then all added education records should appear correctly in the list

@CancelEducation
Scenario: 6-Cancel Adding Education
    When I start adding a new education record and click cancel
    Then the system should discard my input and close the add form

@EditEducation
Scenario Outline: 7-Edit Education Successfully
    Given I have an existing education '<Old Country>', '<Old University>', '<Old Title>', '<Old Degree>', '<Old Graduation Year>'
    When I edit my education to '<New Country>', '<New University>', '<New Title>', '<New Degree>', '<New Graduation Year>'
    Then I should see my updated education details including '<New Country>', '<New University>', '<New Title>', '<New Degree>', '<New Graduation Year>'
    And I should see a success message confirming update

    Examples:
      | Old Country | Old University | Old Title | Old Degree | Old Graduation Year | New Country | New University | New Title | New Degree | New Graduation Year |
      | Australia   | APIC           | M.Tech    | MIT        | 2021                | Australia   | QUT            | M.Tech    | MIT        | 2022                |

@EditEducationDuplicate
Scenario Outline: 8-Edit Education Duplicate Validation
    Given I have two education records:
      | Country   | University | Title   | Degree | Graduation Year |
      | Australia | APIC       | M.Tech  | MIT    | 2021            |
      | India     | GEC Patan  | B.Tech  | BE     | 2011            |
    When I try to edit 'India / GEC Patan / B.Tech / BE' to 'Australia / APIC / M.Tech / MIT'
    Then I should see a message "Duplicate data is not allowed"

@DeleteEducation
Scenario Outline: 9-Delete Education Successfully
    Given I have an existing education '<Country>', '<University>', '<Title>', '<Degree>', '<Graduation Year>'
    When I delete my education record
    Then I should see a success message confirming delete
    And the education record should no longer appear in the list

    Examples:
      | Country | University | Title | Degree | Graduation Year |
      | USA     | MIT        | B.Sc  | Degree | 2020            |

@ViewEducation
Scenario Outline: 10-View All Education Records
    Given I have multiple education records added:
      | Country   | University | Title   | Degree | Graduation Year |
      | Australia | APIC       | M.Tech  | MIT    | 2021            |
      | India     | GEC Patan  | B.Tech  | BE     | 2011            |
      | USA       | MIT        | B.Sc    | Degree | 2020            |
    When I view the education list
    Then I should see all education records displayed correctly

@InvalidCharacterEducation
# Not working in the current website
Scenario Outline: 11-Validation of Invalid Characters
    When I try to add an education record with invalid characters '<Country>', '<University>', '<Title>', '<Degree>', '<Graduation Year>'
    Then I should see a proper error message for invalid input

    Examples:
      | Country | University | Title   | Degree | Graduation Year |
      | USA     | MIT!@#     | B.Sc    | Degree | 2020            |

@LongInputEducation
Scenario Outline: 12-Validation for Long Text Input
    When I try to add an education record with long input '<Country>', '<University>', '<Title>', '<Degree>', '<Graduation Year>'
    Then the system should save and display the long input correctly without errors

    Examples:
      | Country | University                                            | Title   | Degree | Graduation Year |
      | USA     | AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA | B.Sc    | Degree | 2020            |
