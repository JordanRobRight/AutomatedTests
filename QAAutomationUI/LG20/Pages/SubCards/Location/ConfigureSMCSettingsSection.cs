using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class ConfigureSMCSettings : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---       
        
        private static string _locationsContainer = @"location-container";
        private static readonly string _smsSettingsForm = @"smc-defaults-form";//id

        #endregion

        #region --- Constructor ---
        internal ConfigureSMCSettings(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
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
        

        public List<string> GetSMCSettingsField
        {
            get
            {

                var smcSettingsFormList = GetPageContainer().FindElement(By.Id(_smsSettingsForm));
                var smcSettingsFormLabelList = smcSettingsFormList.FindElements(By.TagName("label"));     
                List<string> smsSettingsFields = new List<string>();
                foreach (var smsField in smcSettingsFormLabelList)
                {
                    smsSettingsFields.Add(smsField.Text);                    
                }
                return smsSettingsFields;
            }
            
        }

        
        #endregion

        #region --- Private Methods ---



        #endregion
    }
}
