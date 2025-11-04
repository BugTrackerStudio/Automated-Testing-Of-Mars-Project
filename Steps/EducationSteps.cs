using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using qa_dotnet_cucumber.Pages;
using Reqnroll;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using qa_dotnet_cucumber.Helpers;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class EducationSteps
    {
        public readonly EducationPage _educationPage;
        private readonly WebDriverWait _wait;
        private readonly CommonSteps _commonSteps;

        public EducationSteps(EducationPage educationPage, CommonSteps commonSteps, IWebDriver driver)
        {
            _educationPage = educationPage;
            _commonSteps = commonSteps;
            _wait = new WebDriverWait(_educationPage.Driver, TimeSpan.FromSeconds(10));
        }

        //AddEducation
        [When(@"I add my education including '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenIAddMyEducation()
        {
            string country = JsonReader.GetValue("EducationData", "AddEducation", "Country");
            string university = JsonReader.GetValue("EducationData", "AddEducation", "University");
            string title = JsonReader.GetValue("EducationData", "AddEducation", "Title");
            string degree = JsonReader.GetValue("EducationData", "AddEducation", "Degree");
            string graduationYear = JsonReader.GetValue("EducationData", "AddEducation", "GraduationYear");
        }

        [Then(@"I am able to see my education details including '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void ThenIAmAbleToSeeMyEducationDetails(string country, string university, string title, string degree, string graduationYear)
        {
        }

        //AddEducationValidation
        [When(@"I try to add an education record with missing details '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenITryToAddAnEducationRecordWithMissingDetails(string country, string university, string title, string degree, string graduationYear)
        {
        }

        [Then(@"I should see an error message for missing fields")]
        public void ThenIShouldSeeAnErrorMessageForMissingFields()
        {
        }

        //DuplicateEducation
        [Given(@"I have an existing education '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void GivenIHaveAnExistingEducation(string country, string university, string title, string degree, string graduationYear)
        {
        }

        [When(@"I add the same education again")]
        public void WhenIAddTheSameEducationAgain()
        {
        }

        //AllowSameUniTitleDifferentDegree
        [When(@"I add the same university and title '([^']*)', '([^']*)' but with a different degree '([^']*)'")]
        public void WhenIAddTheSameUniversityAndTitleButWithADifferentDegree(string university, string title, string newDegree)
        {
        }

        [Then(@"the system should allow adding the record successfully")]
        public void ThenTheSystemShouldAllowAddingTheRecordSuccessfully()
        {
        }

        //AddMultipleEducation
        [When(@"I add multiple education records:")]
        public void WhenIAddMultipleEducationRecords(Table table)
        {
        }

        [Then(@"all added education records should appear correctly in the list")]
        public void ThenAllAddedEducationRecordsShouldAppearCorrectlyInTheList()
        {
        }

        //CancelEducation
        [When(@"I start adding a new education record and click cancel")]
        public void WhenIStartAddingANewEducationRecordAndClickCancel()
        {
        }

        [Then(@"the system should discard my input and close the add form")]
        public void ThenTheSystemShouldDiscardMyInputAndCloseTheAddForm()
        {
        }

        //EditEducation
        [Given(@"I have an existing education '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void GivenIHaveAnExistingEducationToEdit(string oldCountry, string oldUniversity, string oldTitle, string oldDegree, string oldGraduationYear)
        {
        }

        [When(@"I edit my education to '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenIEditMyEducationTo(string newCountry, string newUniversity, string newTitle, string newDegree, string newGraduationYear)
        {
        }

        [Then(@"I should see my updated education details including '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void ThenIShouldSeeMyUpdatedEducationDetails(string newCountry, string newUniversity, string newTitle, string newDegree, string newGraduationYear)
        {
        }

        //EditEducationDuplicate
        [Given(@"I have two education records:")]
        public void GivenIHaveTwoEducationRecords(Table table)
        {
        }

        [When(@"I try to edit '([^']*) / ([^']*) / ([^']*) / ([^']*)' to '([^']*) / ([^']*) / ([^']*) / ([^']*)'")]
        public void WhenITryToEditTo(string oldCountry, string oldUniversity, string oldTitle, string oldDegree, string newCountry, string newUniversity, string newTitle, string newDegree)
        {
        }

        //DeleteEducation
        [When(@"I delete my education record")]
        public void WhenIDeleteMyEducationRecord()
        {
        }

        [Then(@"the education record should no longer appear in the list")]
        public void ThenTheEducationRecordShouldNoLongerAppearInTheList()
        {
        }

        //ViewEducation
        [Given(@"I have multiple education records added:")]
        public void GivenIHaveMultipleEducationRecordsAdded(Table table)
        {
        }

        [When(@"I view the education list")]
        public void WhenIViewTheEducationList()
        {
        }

        [Then(@"I should see all education records displayed correctly")]
        public void ThenIShouldSeeAllEducationRecordsDisplayedCorrectly()
        {
        }

        //InvalidCharacterEducation
        [When(@"I try to add an education record with invalid characters '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenITryToAddAnEducationRecordWithInvalidCharacters(string country, string university, string title, string degree, string graduationYear)
        {
        }

        //LongInputEducation
        [When(@"I try to add an education record with long input '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenITryToAddAnEducationRecordWithLongInput(string country, string university, string title, string degree, string graduationYear)
        {
        }

        [Then(@"the system should save and display the long input correctly without errors")]
        public void ThenTheSystemShouldSaveAndDisplayTheLongInputCorrectlyWithoutErrors()
        {
        }

    }
}
