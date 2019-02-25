using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class ConfigureSDSSettingsSection : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---       
        
        private static string _locationsContainer = @"location-container";
        private static readonly string _sdsSettingsForm = @"sds-defaults-form";//id
        private static readonly string _emailInput = @"email";
        #endregion

        #region --- Constructor ---
        internal ConfigureSDSSettingsSection(IWebDriver driver, TestConfiguration config) : base(driver, config)
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
        

        public List<string> GetSDSSettingsTexts
        {
            get
            {

                var smcSettingsFormList = GetPageContainer().FindElement(By.Id(_sdsSettingsForm));
                var smcSettingsFormLabelList = smcSettingsFormList.FindElements(By.TagName("label"));     
                List<string> smsSettingsFields = new List<string>();
                foreach (var smsField in smcSettingsFormLabelList)
                {
                    smsSettingsFields.Add(smsField.Text);                    
                }
                return smsSettingsFields;
            }            
        }


        public string BCCEmailTextField
        {

            get
            {
                var getField = GetField("input", "type", _emailInput);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {
                var getField = GetField("input", "type", _emailInput);
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
