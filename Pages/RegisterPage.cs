using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;


        // Locators
        private readonly By FirstNameField = By.Name("firstName");
        private readonly By LastNameField = By.Name("lastName");
        private readonly By EmailField = By.Name("email");
        private readonly By PasswordField = By.Name("password");
        private readonly By ConfirmPasswordField = By.Name("confirmPassword");
        private readonly By TermsCheckbox = By.Name("terms");
        private readonly By JoinButton = By.Id("submit-btn");

        // Constructor
        public RegisterPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void OpenJoin()
        {
            var joinButton = _wait.Until(
        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
            By.XPath("//button[normalize-space()='Join']")));
            joinButton.Click();

            // Wait for the popup/modal to appear
            //_wait.Until(
              //  SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                //    By.CssSelector(".ui.tiny.modal.transition.visible.active")));
        }

        // Methods to interact with elements
        public void EnterFirstName(string firstName)
        {
            var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(FirstNameField));
            element.Clear();
            element.SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(LastNameField));
            element.Clear();
            element.SendKeys(lastName);
        }

        public void EnterEmail(string email)
        {
            var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(EmailField));
            element.Clear();
            element.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(PasswordField));
            element.Clear();
            element.SendKeys(password);
        }

        public void EnterConfirmPassword(string confirmPassword)
        {
            var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(ConfirmPasswordField));
            element.Clear();
            element.SendKeys(confirmPassword);
        }

        public void CheckTerms()
        {
            var checkbox = _driver.FindElement(TermsCheckbox);
            if (!checkbox.Selected)
            {
                checkbox.Click();
            }
        }

        public void ClickJoin()
        {
            _driver.FindElement(JoinButton).Click();
        }

        // Get error message for any field 
        public string GetErrorMessage(string fieldName)
        {
            string fieldAttr = "";

            switch (fieldName.ToLower())
            {
                case "first name":
                    fieldAttr = "firstName";
                    break;
                case "last name":
                    fieldAttr = "lastName";
                    break;
                case "email":
                    fieldAttr = "email";
                    break;
                case "password":
                    fieldAttr = "password";
                    break;
                case "confirm password":
                    fieldAttr = "confirmPassword";
                    break;
            }

            var errorElement = _driver.FindElement(
                By.CssSelector($"div.field.error input[name='{fieldAttr}'] + div")
            );
            return errorElement.Text;
        }

    }
}
