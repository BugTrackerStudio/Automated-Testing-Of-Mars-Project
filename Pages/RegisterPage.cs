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
            var join = _wait.Until(
        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
            By.XPath("//a[normalize-space()='Join']")));
            join.Click();
        }

        // Methods to interact with elements
        public void EnterFirstName(string firstName)
        {
            _driver.FindElement(FirstNameField).Clear();
            _driver.FindElement(FirstNameField).SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            _driver.FindElement(LastNameField).Clear();
            _driver.FindElement(LastNameField).SendKeys(lastName);
        }

        public void EnterEmail(string email)
        {
            _driver.FindElement(EmailField).Clear();
            _driver.FindElement(EmailField).SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(PasswordField).Clear();
            _driver.FindElement(PasswordField).SendKeys(password);
        }

        public void EnterConfirmPassword(string confirmPassword)
        {
            _driver.FindElement(ConfirmPasswordField).Clear();
            _driver.FindElement(ConfirmPasswordField).SendKeys(confirmPassword);
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

        // Clear any field dynamically
        public void ClearField(string fieldName)
        {
            switch (fieldName.ToLower())
            {
                case "first name":
                    _driver.FindElement(FirstNameField).Clear();
                    break;
                case "last name":
                    _driver.FindElement(LastNameField).Clear();
                    break;
                case "email":
                    _driver.FindElement(EmailField).Clear();
                    break;
                case "password":
                    _driver.FindElement(PasswordField).Clear();
                    break;
                case "confirm password":
                    _driver.FindElement(ConfirmPasswordField).Clear();
                    break;
            }
        }

        // Get error message for any field 
        public string GetErrorMessage(string fieldName)
        {
            string fieldNameAttr = fieldName.ToLower().Replace(" ", "");
            var errorDiv = _driver.FindElement(By.CssSelector($"div.field.error input[name='{fieldNameAttr}'] + div"));
            return errorDiv.Text;
        }
    }
}
