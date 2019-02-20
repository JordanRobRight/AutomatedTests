using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Player
{
    public sealed class PlayerGeneralInformation : LGBasePage
    {
        #region --- Properties ---
        private static string _playerContainer = @"player-container";
        private readonly string _lgfeContainer = @"lgfe-tile-grid-wrapper";
        private static readonly string _pencilButton = @"lgfe-tile-button-wrapper";
        private static readonly string _playerInformationFields = @"//div[@class='lgfe-tile lgfe-tile-grid-item lgfe-tile-grid-top-item']//strong";
        private static readonly string _editButtonTags = @"//span[contains(text(),'player information')]/ancestor::h4/preceding-sibling::div//div//i";
        //private PlayerSettingModal _playerModel = null;

        #endregion
        #region --- Constructor ---
        public PlayerGeneralInformation(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {

            PageContainerName = _playerContainer; //Element Div By: Id
            PageFunctionBarContainerClassName = _lgfeContainer;
        }
        #endregion
        #region --- Public Methods ---
         

        public void PlayerInformationEditButtonClick()
        {
            Wait(2);
   
            IWebElement b = Driver.FindElement(By.XPath(_editButtonTags));
            b.Click();

        }

        public IList<string> GetPlayerInformationFields
        {
            get
            {
                IEnumerable<IWebElement> playerInformationFields = Driver.FindElements(By.XPath(_playerInformationFields)).ToList();
                var items = playerInformationFields.Select(a => a.Text).ToList();
                return items;
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
