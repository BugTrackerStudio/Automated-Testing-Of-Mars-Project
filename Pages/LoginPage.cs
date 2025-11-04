using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll.BoDi;
// MUST USE with ExpectedConditions
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{

    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;

        private readonly By SignInLink = By.XPath("//a[normalize-space()='Sign In']");
        private readonly By UsernameField = By.XPath("//input[@type='email' or @placeholder='Email address' or @name='Email' or @id='email']");
        private readonly By PasswordField = By.CssSelector("input[type='password']");
        private readonly By LoginButton = By.XPath("//button[normalize-space()='Login']");


        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void OpenSignIn()
        {
            var signIn = _wait.Until(
        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
            By.XPath("//a[normalize-space()='Sign In']")));
            signIn.Click();
        }

        public void Login(string username, string password)
        {
            _driver.FindElement(UsernameField).SendKeys(username);
            _driver.FindElement(PasswordField).SendKeys(password);
            _driver.FindElement(LoginButton).Click();
        }
    }

}