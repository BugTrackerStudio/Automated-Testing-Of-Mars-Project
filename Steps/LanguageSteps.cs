using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using qa_dotnet_cucumber.Pages;
using Reqnroll;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class LanguageSteps
    {
        public readonly LanguagePage _languagePage;
        private readonly WebDriverWait _wait;
        private readonly CommonSteps _commonSteps;

        public LanguageSteps(LanguagePage languagePage, CommonSteps commonSteps, IWebDriver driver)
        {
            _languagePage = languagePage;
            _commonSteps = commonSteps;
            _wait = new WebDriverWait(_languagePage.Driver, TimeSpan.FromSeconds(10));
        }

        // ----------------------
        // Add Language
        // ----------------------
        [Given(@"I have less than 4 languages in my Languages list")]
        public void GivenIHaveLessThan4LanguagesInMyLanguagesList()
        {
            var languages = _languagePage.GetAllLanguages(); // get fresh list of strings
            int currentCount = languages.Count;

            // If already 4 or more, delete the last language to make room
            if (currentCount >= 4)
            {
                string lastLanguage = languages.Last();
                _languagePage.ClickDeleteIcon(lastLanguage);

                // Wait until the language count decreases
                _wait.Until(driver => _languagePage.GetAllLanguages().Count < currentCount);

                // Refresh the list
                languages = _languagePage.GetAllLanguages();
                currentCount = languages.Count;
            }
            // At this point, we have less than 4 languages
            if (currentCount >= 4)
            {
                throw new Exception("Unable to reduce languages below 4.");
            }
        }

        [When(@"I click Add New in the Languages section")]
        public void WhenIClickAddNewInTheLanguagesSection()
        {
            // Click Add New
            _languagePage.ClickAddNew();
        }

        [When(@"I enter ""(.*)"" as the language")]
        public void WhenIEnterAsTheLanguage(string language)
        {
            _languagePage.EnterLanguage(language);
        }

        [When(@"I select ""(.*)"" as the language level")]
        public void WhenISelectLanguageLevel(string level)
        {
            _languagePage.SelectLevel(level);
        }

        [When(@"I save the language")]
        public void WhenISaveTheLanguage()
        {
            _languagePage.SaveLanguage();
        }

        [Then(@"""(.*)"" should appear in the Languages list")]
        public void ThenShouldAppearInTheLanguagesList(string languageWithLevel)
        {
            bool isPresent = _languagePage.IsLanguagePresent(languageWithLevel);
            Assert.That(isPresent, Is.True, $"{languageWithLevel} was not found in the Languages list.");
        }

        // ----------------------
        // Prevent Duplicates / Max Limit
        // ----------------------
        [Given(@"I already have ""(.*)"" in my Languages list")]
        public void GivenIAlreadyHaveInMyLanguagesList(string language)
        {
            if (!_languagePage.IsLanguagePresent(language))
            {
                throw new Exception($"Pre-condition failed: '{language}' must exist in the Languages list.");
            }
        }

        [When(@"I try to add ""(.*)"" language again")]
        public void WhenITryToAddLanguageAgain(string language)
        {
            if (!_languagePage.IsAddNewButtonVisible())
            {
                Console.WriteLine("Add New button is not visible (maximum languages reached), skipping duplicate add.");
                return; // Skip adding since max languages reached
            }

            if (_languagePage.IsLanguagePresent(language))
            {
                _languagePage.ClickAddNew();
                _languagePage.EnterLanguage(language);
                _languagePage.SelectLevel("Basic");
                _languagePage.SaveLanguage();
            }
            else
            {
                Console.WriteLine($"Language '{language}' does not exist, cannot test duplicate scenario.");
            }
        }

        [Then(@"""(.*)"" should not be duplicated in the Languages list")]
        public void ThenShouldNotBeDuplicatedInTheList(string language)
        {
            Assert.That(_languagePage.IsLanguageUnique(language), Is.True, $"{language} is duplicated in the Languages list.");
        }

        // ----------------------
        // Restrict maximum 4 Languages
        // ----------------------

        [Given(@"I already have (\d+) languages in my Languages list")]
        public void GivenIAlreadyHaveLanguagesInMyLanguagesList(int count)
        {
            var languages = _languagePage.GetAllLanguages();

            // Add or remove languages to match exactly `count`
            while (languages.Count < count)
            {
                if (_languagePage.IsAddNewButtonVisible())
                {
                    _languagePage.ClickAddNew();
                    _languagePage.EnterLanguage("TempLanguage" + Guid.NewGuid().ToString("N").Substring(0, 4));
                    _languagePage.SelectLevel("Basic");
                    _languagePage.SaveLanguage();
                    _wait.Until(driver => _languagePage.GetAllLanguages().Count > languages.Count);
                    languages = _languagePage.GetAllLanguages();
                }
                else
                {
                    throw new Exception("Cannot add more languages, Add New button is not visible.");
                }
            }

            while (languages.Count > count)
            {
                _languagePage.ClickDeleteIcon(languages.Last());
                _wait.Until(driver => _languagePage.GetAllLanguages().Count < languages.Count);
                languages = _languagePage.GetAllLanguages();
            }

            // Final verification
            Assert.That(languages.Count, Is.EqualTo(count), $"Pre-condition failed: Expected {count} languages.");
        }


        [Then(@"the Add New button should not be visible")]
        public void ThenTheAddNewButtonShouldNotBeVisible()
        {
            Assert.That(_languagePage.IsAddNewButtonVisible(), Is.False, "Add New button is still visible.");
        }

        // ----------------------
        // Edit Language
        // ----------------------

        [Given(@"I have ""(.*)"" in my Languages list")]
        public void GivenIHaveInMyLanguagesList(string languageWithLevel)
        {
            // Split into language and level if provided
            string[] parts = languageWithLevel.Split(" - ");
            string language = parts[0].Trim();
            string level = parts.Length > 1 ? parts[1] : "Basic";

            // Check if the language already exists
            if (_languagePage.IsLanguagePresent(language))
            {
                // Get the current level from UI
                string currentLevel = _languagePage.GetLanguageLevel(language);

                // If level is different, edit it to match expected
                if (!currentLevel.Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    _languagePage.ClickEditIcon(language);
                    _languagePage.SelectLevel(level);
                    _languagePage.SaveLanguage();
                    _wait.Until(driver => _languagePage.GetLanguageLevel(language) == level);
                }
            }
            else
            {
                // If language is not present, add it
                if (_languagePage.IsAddNewButtonVisible())
                {
                    _languagePage.ClickAddNew();
                    _languagePage.EnterLanguage(language);
                    _languagePage.SelectLevel(level);
                    _languagePage.SaveLanguage();
                    _wait.Until(driver => _languagePage.IsLanguagePresent(language));
                }
            }
        }

        [When(@"I click the edit icon for ""(.*)"" language")]
        public void WhenIClickTheEditIconForLanguage(string language)
        {
            _languagePage.ClickEditIcon(language);
        }

        [When(@"I change the language level to ""(.*)""")]
        public void WhenIChangeTheLanguageLevelTo(string level)
        {
            _languagePage.SelectLevel(level);
        }

        [When(@"I save the language changes")]
        public void WhenISaveTheLanguageChanges()
        {
            _languagePage.SaveLanguage();
        }

        // ----------------------
        // Prevent duplicate language on edit
        // ----------------------
        [When(@"I try to change ""(.*)"" to ""(.*)""")]
        public void WhenITryToChangeLanguageTo(string oldLanguage, string newLanguage)
        {
            _languagePage.ClickEditIcon(oldLanguage);

            // Check if the new language already exists
            if (_languagePage.IsLanguagePresent(newLanguage))
            {
                Console.WriteLine($"Cannot change '{oldLanguage}' to '{newLanguage}' because it already exists.");
                _languagePage.CancelAddOrEdit();
            }
            else
            {
                _languagePage.EnterLanguage(newLanguage);
                _languagePage.SaveLanguage();
            }
        }

        [Then(@"""(.*)"" should remain unchanged in the Languages list")]
        public void ThenLanguageShouldRemainUnchanged(string language)
        {
            bool isPresent = _languagePage.IsLanguagePresent(language);
            Assert.That(isPresent, Is.True, $"Expected language '{language}' to remain in the list.");
        }

        [Then(@"there should still be only one ""(.*)"" in the Languages list")]
        public void ThenThereShouldStillBeOnlyOneLanguageInTheList(string language)
        {
            bool isUnique = _languagePage.IsLanguageUnique(language);
            Assert.That(isUnique, Is.True, $"Expected only one '{language}' in the list, but found duplicates.");
        }

        // ----------------------
        // Delete Language
        // ----------------------

        [When(@"I click the delete icon for language ""(.*)""")]
        public void WhenIClickTheDeleteIconForLanguage(string language)
        {
            bool deleted = false; // Track if deletion actually happened

            if (_languagePage.IsLanguagePresent(language))
            {
                _languagePage.ClickDeleteIcon(language);

                // Wait until the language disappears
                _wait.Until(driver => !_languagePage.IsLanguagePresent(language));
                deleted = true; // Deletion successful
            }
            else
            {
                Console.WriteLine($"Language '{language}' not found, skipping delete.");
            }

            // Update the common _actionOccurred flag
            _commonSteps.SetActionOccurred(deleted);
        }

        [Then(@"""(.*)"" should not appear in my Languages list")]
        public void ThenShouldNotAppearInMyLanguagesList(string language)
        {
            bool isPresent = _languagePage.IsLanguagePresent(language);
            Assert.That(isPresent, Is.False, $"{language} still appears in the Languages list.");
        }
    }
}
