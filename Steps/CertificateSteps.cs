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
    public class CertificateSteps
    {
        public readonly CertificatePage _certificatePage;
        private readonly WebDriverWait _wait;
        private readonly CommonSteps _commonSteps;

        public CertificateSteps(CertificatePage certificatePage, CommonSteps commonSteps, IWebDriver driver)
        {
            _certificatePage = certificatePage;
            _commonSteps = commonSteps;
            _wait = new WebDriverWait(_certificatePage.Driver, TimeSpan.FromSeconds(10));
        }

        //AddCertificate
        [When(@"I add my certificate including '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenIAddMyCertificate()
        {
            string award = JsonReader.GetValue("CertificateData", "AddCertificate", "Certificate/Award");
            string from = JsonReader.GetValue("CertificateData", "AddCertificate", "Certificate From");
            string year = JsonReader.GetValue("CertificateData", "AddCertificate", "Year");
        }

        [Then(@"I should see my certificate details including '([^']*)', '([^']*)', '([^']*)'")]
        public void ThenIShouldSeeMyCertificateDetails(string award, string from, string year)
        {
        }

        //AddCertificateValidation
        [When(@"I try to add a certificate with missing details '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenITryToAddACertificateWithMissingDetails(string award, string from, string year)
        {
        }

        //DuplicateCertificate
        [Given(@"I have an existing certificate '([^']*)' from '([^']*)' in year '([^']*)'")]
        public void GivenIHaveAnExistingCertificate(string award, string from, string year)
        {
        }

        [When(@"I add the same certificate again")]
        public void WhenIAddTheSameCertificateAgain()
        {
        }

        //AllowSameNameDifferentFrom
        [When(@"I add the same certificate '([^']*)' but from '([^']*)'")]
        public void WhenIAddTheSameCertificateButFrom(string award, string newFrom)
        {
        }

        [Then(@"the system should allow adding the record successfully")]
        public void ThenTheSystemShouldAllowAddingTheRecordSuccessfully()
        {
        }

        //AddMultipleCertificates
        [When(@"I add multiple certificates:")]
        public void WhenIAddMultipleCertificates(Table table)
        {
        }

        [Then(@"all added certificates should appear correctly in the list")]
        public void ThenAllAddedCertificatesShouldAppearCorrectlyInTheList()
        {
        }

        //CancelCertificate
        [When(@"I start adding a new certificate and click cancel")]
        public void WhenIStartAddingANewCertificateAndClickCancel()
        {
        }

        [Then(@"the system should discard my input and close the add form")]
        public void ThenTheSystemShouldDiscardMyInputAndCloseTheAddForm()
        {
        }

        //EditCertificate
        [When(@"I edit my certificate to '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenIEditMyCertificateTo(string newAward, string newFrom, string newYear)
        {
        }

        [Then(@"I should see my updated certificate details including '([^']*)', '([^']*)', '([^']*)'")]
        public void ThenIShouldSeeMyUpdatedCertificateDetails(string newAward, string newFrom, string newYear)
        {
        }

        //EditCertificateDuplicate
        [Given(@"I have two certificates:")]
        public void GivenIHaveTwoCertificates(Table table)
        {
        }

        [When(@"I try to edit '([^']*)' to '([^']*)' from '([^']*)'")]
        public void WhenITryToEditToFrom(string oldAward, string newAward, string from)
        {
        }

        //DeleteCertificate
        [When(@"I delete my certificate")]
        public void WhenIDeleteMyCertificate()
        {
        }

        [Then(@"the certificate should no longer appear in the list")]
        public void ThenTheCertificateShouldNoLongerAppearInTheList()
        {
        }

        //ViewCertificates
        [Given(@"I have multiple certificates added:")]
        public void GivenIHaveMultipleCertificatesAdded(Table table)
        {
        }

        [When(@"I view the certificate list")]
        public void WhenIViewTheCertificateList()
        {
        }

        [Then(@"I should see all certificate details displayed correctly")]
        public void ThenIShouldSeeAllCertificateDetailsDisplayedCorrectly()
        {
        }

        //InvalidCharacterValidation
        [When(@"I try to add a certificate with invalid characters '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenITryToAddACertificateWithInvalidCharacters(string award, string from, string year)
        {
        }

        //LongInputValidation
        [When(@"I try to add a certificate with long input '([^']*)', '([^']*)', '([^']*)'")]
        public void WhenITryToAddACertificateWithLongInput(string award, string from, string year)
        {
        }

        [Then(@"the system should save and display the long input correctly without errors")]
        public void ThenTheSystemShouldSaveAndDisplayTheLongInputCorrectlyWithoutErrors()
        {
        }
    }
}
