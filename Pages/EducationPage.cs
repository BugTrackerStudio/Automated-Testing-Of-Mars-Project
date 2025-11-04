using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Log;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace qa_dotnet_cucumber.Pages
{
    public class EducationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;

        // ----------------------
        // Locators
        // ----------------------
        private readonly By AddNewButton = By.XPath("//div[contains(@class,'ui teal button') and normalize-space()='Add New']");
        private readonly By UniversityNameInput = By.XPath("//input[@placeholder='College/University Name']");
        private readonly By CountryDropdown = By.Name("country");
        private readonly By TitleDropdown = By.Name("title");
        private readonly By DegreeInput = By.XPath("//input[@name='degree']");
        private readonly By YearOfGraduationDropdown = By.Name("yearOfGraduation");
        private readonly By EducationListItems = By.CssSelector("div.form-wrapper table tbody tr");
        private readonly By NotificationMessage = By.CssSelector(".ns-box.ns-growl.ns-show .ns-box-inner");
        private readonly By SaveButton = By.XPath("//input[@value='Add'] | //input[@value='Update']");
        private readonly By CancelButton = By.XPath("//input[@value='Cancel']");

        public EducationPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
    }
}
