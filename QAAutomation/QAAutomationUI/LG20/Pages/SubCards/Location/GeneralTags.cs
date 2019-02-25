using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class GeneralTags : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        
        private static readonly string _tileGridWrapper = @"lgfe-tile-grid-item-hero-wrapper";//class name to select location details section
        private static string _locationsContainer = @"location-container";
        private static readonly string _editButtonTags = @"//span[contains(text(),'tags')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _tagsLengendClassName = @"pim-tags-legend";
        private static string _buttonsClass = @"user-login__action__primary lgfe-button";
        private static readonly string addTagsForm = @"add-tags-form";
        private TagManagementModal _tagManagementModal = null;
        private IWebElement tagsContainer => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, addTagsForm);
        internal TagManagementModal TagManagementModal
        {
            get
            {
                if (_tagManagementModal == null)
                {
                    _tagManagementModal = new TagManagementModal(this.Driver);

                }
                return _tagManagementModal;
            }
            set => _tagManagementModal = value;
        }
        #endregion

        #region --- Constructor ---
        internal GeneralTags(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            PageContainerName = _locationsContainer; //Element Div By: Id
        }

        #endregion

        #region --- Override Methods ---
        public override void Perform()
        {
            throw new NotImplementedException();
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region --- Public Methods ---
        public void LocationTagsEditButtonClick()
        {
            Wait(2);
            IWebElement b = Driver.FindElement(By.XPath(_editButtonTags));
            b.Click();
        }
        
        public List<string> GetLocationTagsFields
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.ClassName(_tagsLengendClassName));
                var locationDetailsList = wrapper.FindElements(By.TagName("div"));     
                List<string> locationTagsFieldsList = new List<string>();
                foreach (var locationTagField in locationDetailsList)
                {
                    string tagField = locationTagField.Text;
                    locationTagsFieldsList.Add(tagField);                    
                }
                return locationTagsFieldsList;
            }
            
        }
        public List<string> GetLocationDetailsFieldValue
        {
            get
            {

                var wrapper = GetPageContainer().FindElement(By.ClassName(_tileGridWrapper));
                var deviceInfos = wrapper.FindElements(By.TagName("span"));
                List<string> list = new List<string>();
                foreach (var deviceInfo in deviceInfos)
                {
                    string value = deviceInfo.Text;
                    list.Add(value);
                }
                return list;
            }

        }
        //need to move to TagManagementModal
        public void AddTagManagementButton()
        {
            var buttons = tagsContainer.GetElementFromCompoundClass(By.TagName("button"), _buttonsClass);
            buttons.Click();           
            Wait(2);
        }

        #endregion

        #region --- Private Methods ---



        #endregion
    }
}
