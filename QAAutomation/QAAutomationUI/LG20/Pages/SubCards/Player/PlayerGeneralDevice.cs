using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Player
{
    public sealed class PlayerGeneralDevice : LGBasePage
    {
        #region --- Properties ---
        private static string _playerContainer = @"player-container";
        private readonly string _lgfeContainer = @"lgfe-tile-grid-wrapper";
        private static readonly string _pencilButton = @"lgfe-tile-button-wrapper";
        private static readonly string _editButtonTags = @"//span[contains(text(),'tags')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _deviceValue = @"//div[@class='lgfe-tile lgfe-tile-grid-item']//div[@class='lgfe-tile-grid-item-text']";

        private static readonly string _programContainer = @"//span[contains(text(),'device')]/ancestor::div[@class='lgfe-tile lgfe-tile-grid-item']";
        private static readonly string _deviceInfo = @"lgfe-deviceinfo";
        private static readonly string _deviceFields = @"lgfe-tile-grid-item-label";
        private static readonly string _gridItem = @"lgfe-tile-grid-item-text";
        private static readonly string _deviceControlInfo = @"div.lgfe-tile-grid-item-label-text-wrapper";

        private static readonly string _deviceControlButton = @"div.device-button.lgfe-button"; 
        
        #endregion
        #region --- Constructor ---
        public PlayerGeneralDevice(IWebDriver driver, TestConfiguration config) : base(driver, config)
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

        public IEnumerable<KeyValuePair<string, string>> GetDeviceFields
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.XPath(_programContainer));
                var deviceInfos = wrapper.FindElements(By.ClassName(_deviceInfo));
                Dictionary<string, string> devices = new Dictionary<string, string>();
                foreach (var deviceInfo in deviceInfos)
                {
                    var label = deviceInfo.FindElement(By.ClassName(_deviceFields));
                    var textValue = deviceInfo.FindElement(By.ClassName(_gridItem));
                    devices.Add(label.Text, textValue.Text);
                }

                return devices;
            }
        }
        public List<string> GetDeviceButtons
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.XPath(_programContainer));
                var deviceButtons = wrapper.FindElements(By.CssSelector(_deviceControlButton));
              
                List<string> deviceButtonList = new List<string>();
                foreach (var deviceButton in deviceButtons)
                {
                    deviceButtonList.Add(deviceButton.Text);
                   
                }
                    return deviceButtonList;
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
