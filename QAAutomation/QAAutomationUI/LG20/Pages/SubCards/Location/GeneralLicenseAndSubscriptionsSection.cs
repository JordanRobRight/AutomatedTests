using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class GeneralLicenseAndSubscriptionsSection : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private static readonly string _licenseTableWrapper = @"license-table_wrapper";//ID
        private static readonly string _locationDetailItem = @"lgfe-item";
        private static string _locationsContainer = @"location-container";
        private static string _playerSpanText = @"pt-status-text-name ";
        #endregion

        #region --- Constructor ---
        internal GeneralLicenseAndSubscriptionsSection(IWebDriver driver, TestConfiguration config) : base(driver, config)
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

        public List<string> GetLicensesAndSubscriptionsFields
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.Id(_licenseTableWrapper));
                var ths = wrapper.FindElements(By.TagName("th"));
                List<string> licenseSubscriptionList = new List<string>();
                foreach (var licenseFiled in ths)
                {
                    licenseSubscriptionList.Add(licenseFiled.Text);
                }
                return licenseSubscriptionList;
            }
        }

        public void SelectLicensePlayer(string playerName)
        {
            var licensePlayers = GetPageContainer().FindElements(By.ClassName(_playerSpanText));
            foreach (IWebElement player in licensePlayers)
            {
                if (player.Text == playerName)
                {
                    player.Click();
                    break;
                }
            }  
        }


        #endregion

        #region --- Private Methods ---



        #endregion
    }
}
