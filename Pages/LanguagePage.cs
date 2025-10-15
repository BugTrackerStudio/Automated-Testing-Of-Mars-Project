using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Log;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace qa_dotnet_cucumber.Pages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;

        // ----------------------
        // Locators
        // ----------------------
        private readonly By AddNewButton = By.XPath("//div[contains(@class,'ui teal button') and normalize-space()='Add New']");
        private readonly By LanguageInput = By.XPath("//input[@placeholder='Add Language']");
        private readonly By LevelDropdown = By.Name("level");
        private readonly By SaveButton = By.XPath("//input[@value='Add'] | //input[@value='Update']");
        private readonly By CancelButton = By.XPath("//input[@value='Cancel']");
        private readonly By NotificationMessage = By.CssSelector(".ns-box.ns-growl.ns-show .ns-box-inner");
        private readonly By LanguageListItems = By.CssSelector("div.form-wrapper table tbody tr");

        public LanguagePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void WaitForNotificationsToDisappear()
        {
            try
            {
                _wait.Until(d =>
                {
                    var notifs = d.FindElements(NotificationMessage);
                    return notifs.All(n => !n.Displayed);
                });
            }
            catch { /* Ignore timeout */ }
        }

        // ----------------------
        // Add Language
        // ----------------------

        public List<string> GetAllLanguages()
        {
            var languages = new List<string>();

            try
            {
                // Select all <tr> under any <tbody> inside the table
                var rows = _driver.FindElements(By.XPath(
                    "//div[contains(@class,'scrollTable')]//table//tbody/tr"
                ));

                foreach (var row in rows)
                {
                    try
                    {
                        var cols = row.FindElements(By.TagName("td"));
                        if (cols.Count >= 1)
                        {
                            string language = cols[0].Text.Trim();
                            if (!string.IsNullOrEmpty(language))
                                languages.Add(language);
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        // Skip this row, fetch fresh next iteration
                        continue;
                    }
                }
            }
            catch (NoSuchElementException)
            {
                // Table or rows not found, return empty list
            }

            return languages;
        }

        public void ClickAddNew()
        {
            // Wait for any previous notifications to disappear
            WaitForNotificationsToDisappear();
            if (IsAddNewButtonVisible())
            {
                _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
                _wait.Until(ExpectedConditions.ElementIsVisible(LanguageInput)); //wait for input box to appear

            }
        }

        public void EnterLanguage(string language)
        {
            // Wait until the input is visible and enabled
            var input = _wait.Until(d =>
            {
                var element = d.FindElement(LanguageInput);
                return (element.Displayed && element.Enabled) ? element : null;
            });
            // Scroll the input into view
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", input);

            input.Clear();
            input.SendKeys(language);
        }

        public void SelectLevel(string level)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(LevelDropdown));

            var dropdown = new SelectElement(_driver.FindElement(LevelDropdown));
            if (string.IsNullOrEmpty(level))
                level = "Basic"; // default level
            dropdown.SelectByText(level);
        }

        public void SaveLanguage()
        {
            var saveButton = _driver.FindElement(SaveButton);
            saveButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

            try
            {
                // Wait until any notification appears or disappears
                wait.Until(d =>
                {
                    var notifs = d.FindElements(NotificationMessage);
                    return notifs.Count > 0 ? notifs[0].Displayed : true;
                });
            }
            catch (WebDriverTimeoutException)
            {
                // Ignore if no notification appears
            }
        }

        public void CancelAddOrEdit()
        {
            _driver.FindElement(CancelButton).Click();
        }

        // ----------------------
        // Edit Language
        // ----------------------
        public void ClickEditIcon(string language)
        {
            var editIcon = _driver.FindElement(By.XPath($"//tr[td[1][normalize-space()='{language}']]//i[contains(@class,'write')]"));
            editIcon.Click();
        }

        // ----------------------
        // Verification
        // ----------------------
        public string GetNotification()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(NotificationMessage)).Text;
        }

        public bool IsLanguagePresent(string languageWithLevel)
        {
            string language;
            string level = null;

            // Split only if " - " exists
            var parts = languageWithLevel.Split(" - ");
            language = parts[0].Trim();
            if (parts.Length == 2)
                level = parts[1].Trim();

            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElements(By.CssSelector("div.form-wrapper table tbody tr")).Count > 0);

                var rows = _driver.FindElements(By.CssSelector("div.form-wrapper table tbody tr"));
                foreach (var row in rows)
                {
                    var cols = row.FindElements(By.TagName("td"));
                    if (cols.Count >= 1)
                    {
                        string langText = cols[0].Text.Trim();
                        string levelText = cols.Count > 1 ? cols[1].Text.Trim() : "";

                        // If level is null, ignore it in comparison
                        if (langText.Equals(language, StringComparison.OrdinalIgnoreCase) &&
                            (level == null || levelText.Equals(level, StringComparison.OrdinalIgnoreCase)))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public string GetLanguageLevel(string language)
        {
            try
            {
                // Locate the row that contains the language name
                var languageRow = _driver.FindElement(
                    By.XPath($"//td[text()='{language}']/parent::tr")
                );

                // In that row, find the level column (usually the next <td>)
                var levelCell = languageRow.FindElement(By.XPath("./td[2]"));

                // Return the level text (trimmed for safety)
                return levelCell.Text.Trim();
            }
            catch (NoSuchElementException)
            {
                // If language is not found, return empty
                return string.Empty;
            }
        }

        // ----------------------
        // Duplicate & Limit Checks
        // ----------------------
        public bool IsDuplicateLanguage(string language)
        {
            return IsLanguagePresent(language); // If already present, it's duplicate
        }

        public bool IsLanguageUnique(string language)
        {
            var elements = _driver.FindElements(By.XPath($"//td[text()='{language}']"));
            return elements.Count <= 1; // returns true if language appears only once
        }

        public int GetLanguagesCount()
        {
            try
            {
                var languageRows = _driver.FindElements(By.CssSelector(
                    "div.twelve.wide.column.scrollTable table.ui.fixed.table tbody tr"
                ));

                // Count only rows where the first <td> has text
                return languageRows.Count(row =>
                    row.Displayed && !string.IsNullOrWhiteSpace(row.FindElement(By.XPath("./td[1]")).Text)
                );
            }
            catch (NoSuchElementException)
            {
                // If the table or rows are not found, return 0
                return 0;
            }
        }

        public bool IsAddNewButtonVisible()
        {
            try
            {
                return _driver.FindElement(AddNewButton).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false; // Button hidden when 4 languages exist
            }
        }

        // ----------------------
        // Delete Language
        // ----------------------
        public void ClickDeleteIcon(string language)
        {
            var deleteIcon = _driver.FindElement(By.XPath($"//tr[td[1][normalize-space()='{language}']]//i[contains(@class,'remove')]"));

            // Wait until the element is clickable
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(deleteIcon));

            deleteIcon.Click(); // Deletes immediately, no confirmation needed
        }
    }
}
