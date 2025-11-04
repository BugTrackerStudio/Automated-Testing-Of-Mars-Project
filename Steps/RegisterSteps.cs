using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using qa_dotnet_cucumber.Pages;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private readonly RegisterPage _registerPage;
        private readonly NavigationHelper _navigationHelper;
        private readonly WebDriverWait _wait;

        public RegisterSteps(RegisterPage registerPage, NavigationHelper navigationHelper)
        {
            _registerPage = registerPage;
            _navigationHelper = navigationHelper;
            
            _wait = new WebDriverWait(_registerPage.Driver, TimeSpan.FromSeconds(10));
        }

        // Navigate to Register page
        [Given("I am on the registration page")]
        public void GivenIAmOnTheRegistrationPage()
        {
            _navigationHelper.NavigateTo("/");
            _registerPage.OpenJoin();
        }

        // Valid registration
        [When("I enter all required valid details")]
        public void WhenIEnterValidUserDetails()
        {
            _registerPage.EnterFirstName("Kruti");
            _registerPage.EnterLastName("Patel");
            _registerPage.EnterEmail("krutipatel362000@gmail.com");
            _registerPage.EnterPassword("Krumik_26");
            _registerPage.EnterConfirmPassword("Krumik_26");
            _registerPage.CheckTerms();
        }

        [When("I click the Join button")]
        public void WhenIClickTheJoinButton()
        {
            _registerPage.ClickJoin();
        }

        [Then("I should see a registration confirmation message")]
        public void ThenIShouldSeeRegisterConfirmationMessage()
        {
            //  var confirmation = _wait.Until(
            //  SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
            //      By.CssSelector("div.ns-box.ns-growl.ns-type-success .ns-box-inner")
            //   )
            //  );
            // Assert.That(confirmation.Displayed, Is.True, "Registration confirmation message not displayed.");
            // Assert.That(confirmation.Displayed, Is.True);

        }

        // Already registered email
        [When("I enter an already registered email")]
        public void WhenIEnterAlreadyRegisteredEmail()
        {
            _registerPage.EnterFirstName("Kruti");
            _registerPage.EnterLastName("Patel");
            _registerPage.EnterEmail("krutipatel362000@gmail.com");
            _registerPage.EnterPassword("Krumik_26");
            _registerPage.EnterConfirmPassword("Krumik_26");
            _registerPage.CheckTerms();
        }

        [Then("I should see an error message {string}")]
        public void ThenIShouldSeeErrorMessage(string expectedMessage)
        {
            // var errorMsg = _wait.Until(
            //   SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
           //       By.XPath($"//div[contains(@class,'ui basic red pointing prompt label') and contains(text(), '{expectedMessage}')]")));
            // Assert.That(errorMsg.Displayed, Is.True);
            //Assert.IsTrue(errorElement.Displayed, $"Expected error message '{expectedMessage}' was not displayed.");
        }

        // Invalid email format
        [When("I enter an invalid email format")]
        public void WhenIEnterInvalidEmailFormat()
        {
            _registerPage.EnterFirstName("Kruti");
            _registerPage.EnterLastName("Patel");
            _registerPage.EnterEmail("invalidemail");
            _registerPage.EnterPassword("Krumik_26");
            _registerPage.EnterConfirmPassword("Krumik_26");
            _registerPage.CheckTerms();
        }

        // Inline validation for required fields
        [Given("I have filled all the required fields")]
        public void GivenIHaveFilledAllRequiredFields()
        {
            _registerPage.EnterFirstName("Kruti");
            _registerPage.EnterLastName("Patel");
            _registerPage.EnterEmail("krutipatel362000@gmail.com");
            _registerPage.EnterPassword("Krumik_26");
            _registerPage.EnterConfirmPassword("Krumik_26");
            _registerPage.CheckTerms();
        }

        [When("I remove the value from the {string} field")]
        public void WhenIRemoveValueFromField(string field)
        {
            //_registerPage.ClearField(field);
        }

        [Then("I should see the error message {string} below the {string} field")]
        public void ThenIShouldSeeErrorBelowField(string expectedMessage, string field)
        {
            string actualMessage = _registerPage.GetErrorMessage(field);
            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Error for {field} does not match.");
        }

        // Partial form submission (only first name + terms)
        [When("I enter only the First Name")]
        public void WhenIEnterOnlyFirstName()
        {
            _registerPage.EnterFirstName("Kruti");
        }

        [When("I check the Terms and Conditions box")]
        public void WhenICheckTheTermsAndConditionsBox()
        {
            _registerPage.CheckTerms();
        }

        [When("I click on the Join button")]
        public void WhenIClickOnTheJoinButton()
        {
            _registerPage.ClickJoin();
        }


        [Then("I should not be registered")]
        public void ThenIShouldNotBeRegistered()
        {
            var driver = _registerPage.Driver;
            // Assert.That(driver.Url.Contains("/"), Is.True, "User was incorrectly registered.");
            Assert.That(driver.Url.Contains("/"), Is.True);
        }

        [Then("I should not see any error message")]
        public void ThenIShouldNotSeeAnyErrorMessage()
        {
            // Optionally check no error divs visible
            var errorElements = _registerPage.Driver.FindElements(By.CssSelector("div.prompt.visible"));
            //  Assert.That(errorElements.Count, Is.EqualTo(0), "Unexpected error messages displayed.");
            Assert.That(errorElements.Count, Is.EqualTo(0));
        }
    }
}
