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
        private readonly By UsernameField = By.Id("username");
        private readonly By PasswordField = By.Id("password");
        private readonly By LoginButton = By.CssSelector("button[type='submit']");
        private readonly By SuccessMessage = By.CssSelector(".flash.success");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Login(string username, string password)
        {
            _driver.FindElement(UsernameField).SendKeys(username);
            _driver.FindElement(PasswordField).SendKeys(password);
            _driver.FindElement(LoginButton).Click();
        }

        public string GetSuccessMessage()
        {
            return _driver.FindElement(SuccessMessage).Text;
        }
    }

}