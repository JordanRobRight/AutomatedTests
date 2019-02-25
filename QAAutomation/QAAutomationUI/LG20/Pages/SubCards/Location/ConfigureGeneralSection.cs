using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class ConfigureGeneralSection : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private static readonly string _editButtonLocationDetails = @"//span[contains(text(),'location details')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _tileGridWrapper = @"lgfe-tile-grid-item-hero-wrapper";//class name to select location details section
        private static readonly string _locationDetailItem = @"lgfe-item";
        private static string _locationsContainer = @"location-container";
        private static readonly string _editButtonTags = @"//span[contains(text(),'tags')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _tagsLengendClassName = @"pim-tags-legend";
        private static readonly string generalWelcomeInputID = @"general-welcome-input";
        #endregion

        #region --- Constructor ---
        internal ConfigureGeneralSection(IWebDriver driver, TestConfiguration config) : base(driver, config)
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

        public string WelcomeMessageTextField
        {

            get
            {
                var getField = GetField("input", "type", generalWelcomeInputID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {
                var getField = GetField("input", "type", generalWelcomeInputID);
                getField?.SendKeysOrClear(value);
            }
        }
        #endregion

        #region --- Protected Methods ---
        protected IWebElement GetField(string tagName, string attribute, string fieldName)
        {
            return GetPageInputFields(tagName).FirstOrDefault(a => a.GetAttribute(attribute) != null && a.GetAttribute(attribute)
                                                                        .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }

        protected IEnumerable<IWebElement> GetPageInputFields(string tagName)
        {
            var getModalDialog = GetPageContainer();
            var inputFields = getModalDialog.FindElements(By.TagName(tagName)).ToList();

            return inputFields;
        }

        #endregion

        #region --- Private Methods ---



        #endregion
    }
}
