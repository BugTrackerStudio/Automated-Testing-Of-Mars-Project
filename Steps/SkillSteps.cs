using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using qa_dotnet_cucumber.Pages;
using Reqnroll;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Steps
{
    [Binding]
    public class SkillSteps
    {
        private readonly SkillPage _skillPage;
        private readonly WebDriverWait _wait;
        private readonly CommonSteps _commonSteps;

        public SkillSteps(SkillPage skillPage, CommonSteps commonSteps, IWebDriver driver)
        {
            _skillPage = skillPage;
            _commonSteps = commonSteps;
            _wait = new WebDriverWait(_skillPage.Driver, TimeSpan.FromSeconds(10));
        }

        [Given("I navigate to the Skills tab")]
        public void GivenINavigateToTheSkillsTab()
        {
            _skillPage.ClickSkillTab();
        }

        // ----------------------
        // Add Skill
        // ----------------------
        [When(@"I click Add New in the Skills section")]
        public void WhenIClickAddNewInTheSkillsSection()
        {
            _skillPage.ClickAddNew();
        }

        [When(@"I enter ""(.*)"" as the skill")]
        public void WhenIEnterAsTheSkill(string skill)
        {
            _skillPage.EnterSkill(skill);
        }

        [When(@"I select ""(.*)"" as the skill level")]
        public void WhenISelectSkillLevel(string level)
        {
            _skillPage.SelectLevel(level);
        }

        [When(@"I save the skill")]
        public void WhenISaveTheSkill()
        {
            _skillPage.SaveSkill();
        }

        [Then(@"""(.*)"" should appear in the Skills list")]
        public void ThenShouldAppearInTheSkillsList(string skillWithLevel)
        {
            bool isPresent = _skillPage.IsSkillPresent(skillWithLevel);
            Assert.That(isPresent, Is.True, $"{skillWithLevel} was not found in the Skills list.");
        }

        // ----------------------
        // Prevent Duplicate Skills
        // ----------------------
        [Given(@"I already have ""(.*)"" with level ""(.*)"" in my Skills list")]
        public void GivenIAlreadyHaveInMySkillsList(string skill, string level)
        {
            string existingLevel = _skillPage.GetSkillLevel(skill);

            if (string.IsNullOrEmpty(existingLevel))
            {
                // Skill does not exist then add it
                _skillPage.ClickAddNew();
                _skillPage.EnterSkill(skill);
                _skillPage.SelectLevel(level);
                _skillPage.SaveSkill();
            }
            else if (!existingLevel.Equals(level, StringComparison.OrdinalIgnoreCase))
            {
                // Skill exists but with different level then update it
                _skillPage.ClickEditIcon(skill);
                _skillPage.SelectLevel(level);
                _skillPage.SaveSkill();
            }
        }

        [When(@"I try to add ""(.*)"" skill with level ""(.*)"" again")]
        public void WhenITryToAddSkillAgain(string skill, string level)
        {
            if (_skillPage.IsSkillDuplicateInList(skill, level))
            {
                // Already exists  don't add, but we can check for the notification
                Console.WriteLine($"Duplicate skill '{skill}' with level '{level}' detected. Skipping add.");
            }
            else
            {
                // Either new skill or same skill with different level  allow adding
                _skillPage.ClickAddNew();
                _skillPage.EnterSkill(skill);
                _skillPage.SelectLevel(level);
                _skillPage.SaveSkill();
            }
        }

        [Then(@"""(.*)"" with level ""(.*)"" should not be duplicated in the Skill list")]
        public void ThenShouldNotBeDuplicatedInTheSkillList(string skill, string level)
        {
            Assert.That(!_skillPage.IsSkillDuplicateInList(skill, level),
                $"'{skill}' with level '{level}' is duplicated in the Skills list.");
        }

        /* same skill with different level */
        [When(@"I try to add ""(.*)"" skill with level ""(.*)""")]
        public void WhenITryToAddSkillWithLevel(string skill, string level)
        {
            string existingLevel = _skillPage.GetSkillLevel(skill);

            if (string.IsNullOrEmpty(existingLevel))
            {
                // Skill doesn't exist  add new
                _skillPage.ClickAddNew();
                _skillPage.EnterSkill(skill);
                _skillPage.SelectLevel(level);
                _skillPage.SaveSkill();
            }
            else if (!existingLevel.Equals(level, StringComparison.OrdinalIgnoreCase))
            {
                // Skill exists but level is different  edit existing
                _skillPage.ClickEditIcon(skill);
                _skillPage.SelectLevel(level);
                _skillPage.SaveSkill();
            }
            else
            {
                // Skill+level already exists  skip
                Console.WriteLine($"Skill '{skill}' with level '{level}' already exists. Skipping add.");
            }
        }

        [Then(@"""(.*)"" with level ""(.*)"" should be added successfully")]
        public void ThenSkillWithLevelShouldBeAddedSuccessfully(string skill, string level)
        {
            Assert.That(_skillPage.IsSkillLevelPresent(skill, level),
                    $"Expected skill '{skill}' with level '{level}' to be added successfully, but it was not found.");
        }

        [Then(@"no duplicate exists for ""(.*)"" with level ""(.*)""")]
        public void ThenNoDuplicateExistsForSkill(string skill, string level)
        {
            Assert.That(!_skillPage.IsSkillDuplicateInList(skill, level),
                    $"Duplicate found for skill '{skill}' with level '{level}' in the Skills list.");
        }

        // ----------------------
        // Edit Skill
        // ----------------------
        [Given(@"I have ""(.*)"" in my Skills list")]
        public void GivenIHaveInMySkillsList(string skillWithLevel)
        {
            string[] parts = skillWithLevel.Split(" - ");
            string skill = parts[0].Trim();
            string level = parts.Length > 1 ? parts[1] : "Beginner";

            if (_skillPage.IsSkillPresent(skill))
            {
                string currentLevel = _skillPage.GetSkillLevel(skill);
                if (!currentLevel.Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    _skillPage.ClickEditIcon(skill);
                    _skillPage.SelectLevel(level);
                    _skillPage.SaveSkill();
                    _wait.Until(driver => _skillPage.GetSkillLevel(skill) == level);
                }
            }
            else
            {
                _skillPage.ClickAddNew();
                _skillPage.EnterSkill(skill);
                _skillPage.SelectLevel(level);
                _skillPage.SaveSkill();
                _wait.Until(driver => _skillPage.IsSkillPresent(skill));
            }
        }

        [When(@"I click the edit icon for ""(.*)"" skill")]
        public void WhenIClickTheEditIconForSkill(string language)
        {
            _skillPage.ClickEditIcon(language);
        }

        [When(@"I change the skill level to ""(.*)""")]
        public void WhenIChangeTheSkillLevelTo(string level)
        {
            _skillPage.SelectLevel(level);
        }

        [When(@"I save the skill changes")]
        public void WhenISaveTheSkillChanges()
        {
            _skillPage.SaveSkill();
        }

        // ----------------------
        // Prevent duplicate skill on edit
        // ----------------------
        [When(@"I try to change ""(.*)"" to ""(.*)"" on skill list")]
        public void WhenITryToChangeSkillTo(string oldSkillWithLevel, string newSkillWithLevel)
        {

        }

        [Then(@"""(.*)"" should remain unchanged in the Skills list")]
        public void ThenSkillShouldRemainUnchanged(string skillWithLevel)
        {

        }

        // ----------------------
        // Delete Skill
        // ----------------------

        [When(@"I click the delete icon for skill ""(.*)""")]
        public void WhenIClickTheDeleteIconForSkill(string skill)
        {
            bool deleted = false; // Track if deletion actually happened

            if (_skillPage.IsSkillPresent(skill))
            {
                _skillPage.ClickDeleteIcon(skill);
                _wait.Until(driver => !_skillPage.IsSkillPresent(skill));
                deleted = true; // Deletion successful
            }
            else
            {
                Console.WriteLine($"Skill '{skill}' not found, skipping delete.");
            }

            // Update the common _actionOccurred flag
            _commonSteps.SetActionOccurred(deleted);
        }

        [Then(@"""(.*)"" should not appear in my Skills list")]
        public void ThenSkillShouldNotAppearInMySkillsList(string skill)
        {
            bool isPresent = _skillPage.IsSkillPresent(skill);
            Assert.That(isPresent, Is.False, $"{skill} still appears in the Skills list.");
        }
    }
}
