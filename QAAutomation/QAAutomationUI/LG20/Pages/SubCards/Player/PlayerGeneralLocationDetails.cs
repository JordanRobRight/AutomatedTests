using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Player
{
    public sealed class PlayerGeneralLocationDetails : LGBasePage
    {
        #region --- Properties ---
        private static string _playerContainer = @"player-container";
        private readonly string _lgfeContainer = @"lgfe-tile-grid-wrapper";
        private static readonly string _pencilButton = @"lgfe-tile-button-wrapper";
        private static readonly string _playerLocationDetailsFields = @"//div[@class='lgfe-tile lgfe-tile-grid-item']//strong";
        private static readonly string _editButtonTags = @"//span[contains(text(),'player information')]/ancestor::h4/preceding-sibling::div//div//i";
        #endregion
        #region --- Constructor ---
        public PlayerGeneralLocationDetails(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {

            PageContainerName = _playerContainer; //Element Div By: Id
            PageFunctionBarContainerClassName = _lgfeContainer;
        }
        #endregion
        #region --- Public Methods --- 
        
        public IList<string> GetPlayerLocationDetailsFields
        {
            get
            {
                IEnumerable<IWebElement> playerInformationFields = Driver.FindElements(By.XPath(_playerLocationDetailsFields)).ToList();
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
