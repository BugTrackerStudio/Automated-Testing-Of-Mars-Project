using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Log;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace qa_dotnet_cucumber.Pages
{
    public class CertificatePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;

        // ----------------------
        // Locators
        // ----------------------
        private readonly By AddNewButton = By.XPath("//div[contains(@class,'ui teal button') and normalize-space()='Add New']");
        private readonly By CertificateAward = By.XPath("//input[@placeholder='Certificate or Award']");
        private readonly By CertificateFrom = By.XPath("//input[@name='certificationFrom']");
        private readonly By YearDropdown = By.Name("certificationYear");
        private readonly By CertificateListItems = By.CssSelector("div.form-wrapper table tbody tr");
        private readonly By NotificationMessage = By.CssSelector(".ns-box.ns-growl.ns-show .ns-box-inner");
        private readonly By SaveButton = By.XPath("//input[@value='Add'] | //input[@value='Update']");
        private readonly By CancelButton = By.XPath("//input[@value='Cancel']");

        public CertificatePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Actions
      /*  public void ClickAddButton() => AddNewButton.Click();
        public void EnterCertificateAward(string award) => CertificateAward.SendKeys(award);
        public void EnterCertificateFrom(string from) => CertificateFrom.SendKeys(from);
        public void SelectYear(string year) => YearDropdown.SendKeys(year);
        public void ClickSave() => SaveButton.Click();
        public void ClickCancel() => CancelButton.Click(); */

    }
}
