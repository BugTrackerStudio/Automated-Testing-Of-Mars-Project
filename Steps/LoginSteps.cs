using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using qa_dotnet_cucumber.Pages;

namespace qa_dotnet_cucumber.Steps
{

    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly NavigationHelper _navigationHelper;

        public LoginSteps(LoginPage loginPage, NavigationHelper navigationHelper)
        {
            _loginPage = loginPage;
            _navigationHelper = navigationHelper;
        }

        //successful login
        [Given("I am on the Login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _navigationHelper.NavigateTo("/");
            _loginPage.OpenSignIn();
        }

        [When("I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            _loginPage.Login("krutipatel362000@gmail.com", "Krumik_26");
        }

        [Then("I should see the secure area")]
        public void ThenIShouldSeeTheSecureArea()
        {
            var driver = _loginPage.Driver;

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var userGreeting = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.CssSelector("span.item.ui.dropdown.link")
                )
            );
            Assert.That(userGreeting.Text.StartsWith("Hi"), Is.True, "User greeting not found; login may have failed.");
        }

        // Invalid credentials
        [When("I enter invalid credentials")]
        public void WhenIEnterInvalidCredentials()
        {
            _loginPage.Login("wrongemail@example.com", "wrongpassword");
        }

        [Then("I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            var driver = _loginPage.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var errorMsg = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.CssSelector(".ns-box.ns-growl.ns-type-error .ns-box-inner") 
                )
            );
            Assert.That(errorMsg.Displayed, Is.True, "Error message not displayed.");
            Assert.That(errorMsg.Text.Contains("Confirm your email"), Is.True, "Expected error text not found.");
        }

        // Empty email
        [When("I leave the email field empty")]
        public void WhenILeaveTheEmailFieldEmpty()
        {
            _loginPage.Login("", "Krumik_26");
        }

        [Then("I should see a validation message for invalid email")]
        public void ThenIShouldSeeValidationMessageForInvalidEmail()
        {
            var driver = _loginPage.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var emailError = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.XPath("//input[@name='email']/following-sibling::div[contains(@class,'prompt')]") 
                )
            );
            Assert.That(emailError.Displayed, Is.True, "Email validation message not displayed.");
            Assert.That(emailError.Text.Contains("Please enter a valid email address"), Is.True, "Expected email validation text not found.");
        }

        // Empty password
        [When("I leave the password field empty")]
        public void WhenILeaveThePasswordFieldEmpty()
        {
            _loginPage.Login("krutipatel362000@gmail.com", "");
        }

        [Then("I should see a validation message for password length")]
        public void ThenIShouldSeeValidationMessageForPasswordLength()
        {
            var driver = _loginPage.Driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var passwordError = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.XPath("//input[@name='password']/following-sibling::div[contains(@class,'prompt')]")
                )
            );
            Assert.That(passwordError.Displayed, Is.True, "Password validation message not displayed.");
        }


        // Invalid email format
        [When("I enter an invalid email format in the username field")]
        public void WhenIEnterInvalidEmailFormat()
        {
            _loginPage.Login("invalidemailformat", "Krumik_26");
        }

        [Then("I should see a validation message for invalid email format")]
        public void ThenIShouldSeeValidationMessageForInvalidEmailFormat()
        {
            ThenIShouldSeeValidationMessageForInvalidEmail(); 
        }
    }
}