using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Player
{
    public sealed class PlayerGeneralInstalledPrograms : LGBasePage
    {
        #region --- Properties ---
        private static string _playerContainer = @"player-container";
        private readonly string _lgfeContainer = @"lgfe-tile-grid-wrapper";
      //  private static readonly string _pencilButton = @"lgfe-tile-button-wrapper";
        private static readonly string _editButtonTags = @"//span[contains(text(),'tags')]/ancestor::h4/preceding-sibling::div//div//i";
       
        private static readonly string _programContainer = @"div.lgfe-tile.lgfe-tile-grid-item.lgfe-tile-grid-item-programs-card";
        private static readonly string _deviceInfo = @"lgfe-deviceinfo";
        private static readonly string _installedProgramFields = @"lgfe-tile-grid-item-label";
        private static readonly string _gridItem = @"lgfe-tile-grid-item-text";

        #endregion
        #region --- Constructor ---
        public PlayerGeneralInstalledPrograms(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            PageContainerName = _playerContainer; //Element Div By: Id
            PageFunctionBarContainerClassName = _lgfeContainer;
        }
        #endregion
        #region --- Public Methods ---


        public void PlayerTagsEditButtonClick()
        {            
            Wait(2);

            IWebElement b = Driver.FindElement(By.XPath(_editButtonTags));
            b.Click();
        }

        public IEnumerable<KeyValuePair<string, string>> GetInstalledProgramsFields
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.CssSelector(_programContainer));
                var deviceInfos = wrapper.FindElements(By.ClassName(_deviceInfo));
                Dictionary<string, string> devices = new Dictionary<string, string>();
                foreach (var deviceInfo in deviceInfos)
                {
                    var label = deviceInfo.FindElement(By.ClassName(_installedProgramFields));
                    var textValue = deviceInfo.FindElement(By.ClassName(_gridItem));
                    devices.Add(label.Text, textValue.Text);
                }

                return devices;
            }
        }

        public List<string> GetInstalledProgramsLabel
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.CssSelector(_programContainer));
                var deviceInfos = wrapper.FindElements(By.ClassName(_deviceInfo));
                List<string> InstalledPrograms = new List<string>();
                foreach (var deviceInfo in deviceInfos)
                {
                    var label = deviceInfo.FindElement(By.ClassName(_installedProgramFields));
                   // var textValue = deviceInfo.FindElement(By.ClassName(_gridItem));
                    InstalledPrograms.Add(label.Text);
                }
                return InstalledPrograms;
                
            }
        }

        public override void Perform()
        {
            throw new NotImplementedException();
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
