Feature: Manage Certificates on Profile

  As a user
  I want to add, edit, and delete my certificates
  So that my profile accurately reflects the certificates I know

  Background:
	Given I am logged in with valid credentials
	And I am on the Profile page
	And I clicked on the Certificate tab

@AddCertificate
Scenario Outline: 1-Add Certificate 
	When I add my certificate including '<Certificate/Award>', '<Certificate From>', '<Year>'
	Then I am able to see my certificate details including '<Certificate/Award>', '<Certificate From>', '<Year>'
	And I should see a success message confirming certificate added

	 Examples:
      | Certificate/Award | Certificate From | Year |
      | Word Expert       | Microsoft        | 2023 |
      | Excel Master      | Microsoft        | 2024 |

@AddCertificateValidation
  Scenario Outline: 2-Add Certificate with Missing Mandatory Fields
    When I try to add a certificate with missing details '<Certificate/Award>', '<Certificate From>', '<Year>'
    Then I should see an error message for missing fields

    Examples:
      | Certificate/Award | Certificate From | Year |
      |                   | Microsoft        | 2023 |
      | Excel Master      |                  | 2024 |

@DuplicateCertificate
  Scenario Outline: 3-Add Duplicate Certificate
    Given I have an existing certificate '<Certificate/Award>' from '<Certificate From>' in year '<Year>'
    When I add the same certificate again
    Then I should see a message "Duplicate data is not allowed"

    Examples:
      | Certificate/Award | Certificate From | Year |
      | Word Expert       | Microsoft        | 2023 |

@AllowSameNameDifferentFrom
  Scenario Outline: 4-Allow Same Certificate Name but Different Certificate From Field
    Given I have an existing certificate '<Certificate/Award>' from '<Certificate From>' in year '<Year>'
    When I add the same certificate '<Certificate/Award>' but from '<New Certificate From>'
    Then the system should allow adding the record successfully

    Examples:
      | Certificate/Award | Certificate From | Year | New Certificate From |
      | Excel Master      | Microsoft        | 2024 | Udemy                |

@AddMultipleCertificates
  Scenario Outline: 5-Add Multiple Certificates
    When I add multiple certificates:
      | Certificate/Award | Certificate From | Year |
      | Adobe Expert       | Adobe            | 2023 |
      | Excel Master       | Microsoft        | 2024 |
      | Python Master      | Udemy            | 2022 |
    Then all added certificates should appear correctly in the list

@CancelCertificate
  Scenario: 6-Cancel Adding Certificate
    When I start adding a new certificate and click cancel
    Then the system should discard my input and close the add form

@EditCertificate
  Scenario Outline: 7-Edit Certificate Successfully
    Given I have an existing certificate '<Old Certificate/Award>' from '<Old Certificate From>' in year '<Old Year>'
    When I edit my certificate to '<New Certificate/Award>', '<New Certificate From>', '<New Year>'
    Then I should see my updated certificate details including '<New Certificate/Award>', '<New Certificate From>', '<New Year>'
    And I should see a success message confirming update

    Examples:
      | Old Certificate/Award | Old Certificate From | Old Year | New Certificate/Award | New Certificate From | New Year |
      | Adobe Expert          | Adobe                | 2023     | Adobe Designer        | Adobe                | 2024     |

@EditCertificateDuplicate
  Scenario Outline: 9-Edit Certificate Duplicate Validation
    Given I have two certificates:
      | Certificate/Award | Certificate From | Year |
      | Adobe Expert      | Adobe            | 2023 |
      | Web Designer      | Coursera         | 2024 |
    When I try to edit 'Web Designer' to 'Adobe Expert' from 'Adobe'
    Then I should see a message "Duplicate data is not allowed"

@DeleteCertificate
  Scenario Outline: 10-Delete Certificate Successfully
    Given I have an existing certificate '<Certificate/Award>' from '<Certificate From>' in year '<Year>'
    When I delete my certificate
    Then I should see a success message confirming delete
    And the certificate should no longer appear in the list

    Examples:
      | Certificate/Award | Certificate From | Year |
      | Python Master     | Udemy            | 2022 |

 @ViewCertificates
  Scenario Outline: 11-View All Certificates
    Given I have multiple certificates added:
      | Certificate/Award | Certificate From | Year |
      | Adobe Expert      | Adobe            | 2023 |
      | Excel Master      | Microsoft        | 2024 |
      | Python Master     | Udemy            | 2022 |
    When I view the certificate list
    Then I should see all certificate details displayed correctly

 #Not working in the current website
 @InvalidCharacterValidation
  Scenario Outline: 12-Validation of Invalid Characters
    When I try to add a certificate with invalid characters '<Certificate/Award>', '<Certificate From>', '<Year>'
    Then I should see a proper error message for invalid input

    Examples:
      | Certificate/Award | Certificate From | Year |
      | $$$$$$             | Adobe!@#        | 2024 |

@LongInputValidation
  Scenario Outline: 13-Validation for Long Text Input
    When I try to add a certificate with long input '<Certificate/Award>', '<Certificate From>', '<Year>'
    Then the system should save and display the long input correctly without errors

    Examples:
      | Certificate/Award | Certificate From | Year |
      | AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA | Adobe | 2023 |
