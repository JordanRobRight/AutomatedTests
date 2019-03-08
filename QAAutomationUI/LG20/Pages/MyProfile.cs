using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using QA.Automation.UITests.Selenium;
using System.Collections;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class MyProfile : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---
        private static readonly string firstNameID = @"firstName-form-plain-text";
        private static readonly string lastNameID = @"lastName-form-plain-text";
        private static readonly string titleID = @"job-title-form-plain-text";
        private static readonly string emailAddressID = @"form-email";
        private static readonly string directPhoneNumberID = @"phone-form-plain-text";
        private static readonly string mobileNumberID = @"mobile-form-plain-text";
        private static readonly string streetID = @"street-form-plain-text";
        private static readonly string cityID = @"city-form-plain-text";
        private static readonly string stateID = @"state-form-plain-text";
        private static readonly string zipID = @"zip-form-plain-text";
        private static readonly string myProfileSaveButtonCssSelector = "#profile-container > div.pc-personal-details-fields > div > div > div.lg-modal__actions > button";
        private static readonly string errorInput = @"//input[@class='lg-modal__field__input lgfe-input-text expanded errorInput']/preceding-sibling::label";
        public static readonly string textTypeList = @"//div[@id='profile-container']//input[@type='text']/preceding-sibling::label";
        public static readonly string emailField = @"//div[@id='profile-container']//input[@id='form-email']/preceding-sibling::label";
        public static readonly string profileFields = @"//div[@id='profile-container']//input[@type='text']/preceding-sibling::label";
        #endregion

        #region --- Constructor ---
        internal MyProfile(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = "profile-container"; // This is property is in the base class which is the string to the id attribute for the content container. 
        }

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

        public string ProfileFirstNameTextField
        {

            get
            {
                var getField = GetField("input", "id", firstNameID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {
                var getField = GetField("input", "id", firstNameID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileLastNameTextField
        {
            get
            {
                var getField = GetField("input", "id", lastNameID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", lastNameID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileTitleTextField
        {
            get
            {
                var getField = GetField("input", "id", titleID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", titleID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileDirectPhoneTextField
        {
            get
            {
                var getField = GetField("input", "id", directPhoneNumberID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", directPhoneNumberID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileMobileNumberTextField
        {
            get
            {
                var getField = GetField("input", "id", mobileNumberID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", mobileNumberID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileEmailTextField
        {
            get
            {
                var getField = GetField("input", "id", emailAddressID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", emailAddressID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileStreetAddressTextField
        {
            get
            {
                var getField = GetField("input", "id", streetID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", streetID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileCityTextField
        {
            get
            {
                var getField = GetField("input", "id", cityID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", cityID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileStateTextField
        {
            get
            {
                var getField = GetField("input", "id", stateID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", stateID);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ProfileZipTextField
        {
            get
            {
                var getField = GetField("input", "id", zipID);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", zipID);
                getField?.SendKeysOrClear(value);
            }
        }


        
        public void ClickSaveButton()
        {
            IWebElement saveButton = Driver.FindElement(By.CssSelector(myProfileSaveButtonCssSelector));
            saveButton.Click();
            Wait(1);
        }

       
        public  IList GetMyProfileFields
        {
            get
            {
                IEnumerable<IWebElement> profileFieldList = Driver.FindElements(By.XPath(profileFields));               
                var items = profileFieldList.Select(a => a.Text).ToList();              
                return items;
            }         
            
        }

       
        public IEnumerable<KeyValuePair<bool, string>> IsErrorInput
        {
            get
            {
                IList<IWebElement> erroInputField = Driver.FindElements(By.XPath(errorInput));

                foreach (IWebElement eFields in erroInputField)
                {
                    yield return new KeyValuePair<bool, string>(eFields.Enabled, eFields.Text);
                }
               
            }
        }

        public bool IsEmailFieldExists
        {
            get
            {
                IWebElement emailFields = Driver.FindElement(By.XPath(emailField));
                
                return emailFields.Displayed;
            }

        }
       
        public bool IsSaveEnabledAfterSaved
        {
            get
            {
                IWebElement saveEnabled = Driver.FindElement(By.CssSelector(myProfileSaveButtonCssSelector));
                

                return saveEnabled.Enabled;
            }

        }
        #endregion

        #region --- Private Methods ---


        #endregion
    }
}
