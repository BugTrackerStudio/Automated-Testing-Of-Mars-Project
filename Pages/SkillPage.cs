using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qa_dotnet_cucumber.Pages
{
    public class SkillPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public IWebDriver Driver => _driver;

        // ----------------------
        // Locators
        // ----------------------
        private readonly By SkillsTab = By.XPath("//a[@data-tab='second' and normalize-space()='Skills']");
        private readonly By AddNewButton = By.XPath("//div[contains(@class,'ui teal button') and normalize-space()='Add New']");
        private readonly By SkillInput = By.XPath("//input[@placeholder='Add Skill']");
        private readonly By LevelDropdown = By.Name("level");
        private readonly By SaveButton = By.XPath("//input[@value='Add'] | //input[@value='Update']");
        private readonly By CancelButton = By.XPath("//input[@value='Cancel']");
        private readonly By NotificationMessage = By.CssSelector(".ns-box.ns-growl.ns-show .ns-box-inner");
        private readonly By SkillListItems = By.CssSelector("div.form-wrapper table tbody tr");

        public SkillPage(IWebDriver driver)
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

        // call this method to switch to Skills tab
        public void ClickSkillTab()
        {
            var skillTab = Driver.FindElement(SkillsTab);
            skillTab.Click();

            // Optional wait until the Skills section becomes visible
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.CssSelector("div[data-tab='second']")).Displayed);
        }

        // Add Skill
        public List<string> GetAllSkills()
        {
            var skills = new List<string>();

            try
            {
                var rows = _driver.FindElements(By.XPath("//div[contains(@class,'scrollTable')]//table//tbody/tr"));
                foreach (var row in rows)
                {
                    try
                    {
                        var cols = row.FindElements(By.TagName("td"));
                        if (cols.Count >= 1)
                        {
                            string skill = cols[0].Text.Trim();
                            if (!string.IsNullOrEmpty(skill))
                                skills.Add(skill);
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                        continue;
                    }
                }
            }
            catch (NoSuchElementException)
            {
                // Table or rows not found
            }

            return skills;
        }
        public void ClickAddNew()
        {
            WaitForNotificationsToDisappear();
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewButton)).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(SkillInput));
        }

        public void EnterSkill(string skill)
        {
            var input = _wait.Until(d =>
            {
                var element = d.FindElement(SkillInput);
                return (element.Displayed && element.Enabled) ? element : null;
            });

            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", input);
            input.Clear();
            input.SendKeys(skill);
        }

        public void SelectLevel(string level)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LevelDropdown));

            var dropdown = new SelectElement(_driver.FindElement(LevelDropdown));
            if (string.IsNullOrEmpty(level))
                level = "Beginner"; // default level (if not provided)
            dropdown.SelectByText(level);
        }

        public void SaveSkill()
        {
            var saveButton = _driver.FindElement(SaveButton);
            saveButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            try
            {
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

        // Edit Skill
        public void ClickEditIcon(string skill)
        {
            var editIcon = _driver.FindElement(By.XPath($"//tr[td[1][normalize-space()='{skill}']]//i[contains(@class,'write')]"));
            editIcon.Click();
        }

        // Verification
        public string GetNotification()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(NotificationMessage)).Text;
        }

        public bool IsSkillPresent(string skillWithLevel)
        {
            string skill;
            string level = null;

            var parts = skillWithLevel.Split(" - ");
            skill = parts[0].Trim();
            if (parts.Length == 2)
                level = parts[1].Trim();

            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElements(SkillListItems).Count > 0);

                var rows = _driver.FindElements(SkillListItems);
                foreach (var row in rows)
                {
                    var cols = row.FindElements(By.TagName("td"));
                    if (cols.Count >= 1)
                    {
                        string skillText = cols[0].Text.Trim();
                        string levelText = cols.Count > 1 ? cols[1].Text.Trim() : "";

                        if (skillText.Equals(skill, StringComparison.OrdinalIgnoreCase) &&
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

        public string GetSkillLevel(string skill)
        {
            try
            {
                var skillRow = _driver.FindElement(By.XPath($"//td[text()='{skill}']/parent::tr"));
                var levelCell = skillRow.FindElement(By.XPath("./td[2]"));
                return levelCell.Text.Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        // Check if skill+level exists (works for multi-level duplicates)
        public bool IsSkillLevelPresent(string skill, string level)
        {
            var rows = _driver.FindElements(SkillListItems);
            foreach (var row in rows)
            {
                var cols = row.FindElements(By.TagName("td"));
                if (cols.Count >= 2)
                {
                    string existingSkill = cols[0].Text.Trim();
                    string existingLevel = cols[1].Text.Trim();
                    if (existingSkill.Equals(skill, StringComparison.OrdinalIgnoreCase) &&
                        existingLevel.Equals(level, StringComparison.OrdinalIgnoreCase))
                    {
                        return true; // Skill+level exists
                    }
                }
            }
            return false;
        }

        // Duplicate Check (Name + Level)
        public bool IsSkillDuplicateInList(string skill, string level)
        {
            var rows = _driver.FindElements(SkillListItems);
            int count = 0;
            foreach (var row in rows)
            {
                var cols = row.FindElements(By.TagName("td"));
                if (cols.Count >= 2)
                {
                    string existingSkill = cols[0].Text.Trim();
                    string existingLevel = cols[1].Text.Trim();
                    if (existingSkill.Equals(skill, StringComparison.OrdinalIgnoreCase) &&
                        existingLevel.Equals(level, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;
                    }
                }
            }
            return count > 1; // true only if duplicate exists
        }

        // Delete Skill
        public void ClickDeleteIcon(string skill)
        {
            var deleteIcon = _driver.FindElement(By.XPath($"//tr[td[1][normalize-space()='{skill}']]//i[contains(@class,'remove')]"));
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(deleteIcon));
            deleteIcon.Click();
        }
    }
}
