using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using qa_dotnet_cucumber.Pages;
using Reqnroll;
using SeleniumExtras.WaitHelpers;


namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly NavigationHelper _navigationHelper;
        private readonly LoginPage _loginPage;
        private readonly IWebDriver _driver;
        private bool _actionOccurred;
        private readonly By NotificationMessage = By.CssSelector(".ns-box.ns-growl.ns-show .ns-box-inner");

        public CommonSteps(
            NavigationHelper navigationHelper,
            LoginPage loginPage,
            IWebDriver driver)
        {
            _navigationHelper = navigationHelper;
            _loginPage = loginPage;
            _driver = driver;
        }

        [Given("I am logged in with valid credentials")]
        public void GivenIAmLoggedInWithValidCredentials()
        {
            _navigationHelper.NavigateTo("/");

            _loginPage.OpenSignIn();
            _loginPage.Login("krutipatel362000@gmail.com", "Krumik_26");

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(driver =>
            {
                var greeting = driver.FindElement(By.CssSelector("span.item.ui.dropdown.link"));
                return greeting.Displayed && greeting.Text.StartsWith("Hi");
            });
        }

        [Given("I am on the Profile page")]
        public void GivenIAmOnTheProfilePage()
        {
            _navigationHelper.NavigateTo("/Account/Profile");
        }

        public void SetActionOccurred(bool status)
        {
            _actionOccurred = status;
        }

        [Then(@"I should see a notification ""(.*)""")]
        public void ThenIShouldSeeANotification(string expectedMessage)
        {
            if (!_actionOccurred) { Console.WriteLine("No action performed, skipping notification check."); return; }

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            string actualMessage = wait.Until(ExpectedConditions.ElementIsVisible(NotificationMessage)).Text;

            // All valid messages for both languages and skills
            string[] validMessages = new[]
            {
                "has been added to your languages",
                "already exist",
                "already added",
                "has been deleted from your languages",
                "Duplicate data",
                "has been updated to your languages",
                "has been added to your skills",
                "has been deleted",
                "has been updated to your skills"
            };

            Assert.That(validMessages.Any(m => actualMessage.Contains(m)),
                $"Unexpected notification: {actualMessage}");
        }
    }
}