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
        private static readonly string _locationLink = @"//a[text()='ReplaceLocationText']";
        private static readonly string _pageUtilBarContainerClassName = @"lgfe-tile lgfe-tile-grid-item";
        #endregion
        #region --- Constructor ---
        public PlayerGeneralLocationDetails(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {

            PageContainerName = _playerContainer; //Element Div By: Id
            PageFunctionBarContainerClassName = _lgfeContainer;
            PageUtilBarContainerClassName = _pageUtilBarContainerClassName;
        }
        #endregion
        #region --- Public Methods --- 
        public IList<string> GetPlayerLocationDetailsFields
        {
            get
            {

                var location = GetPageUtilityBarContainer();
                IEnumerable<IWebElement> playerInformationFields = location.FindElements(By.TagName("strong")).ToList();
                var items = playerInformationFields.Select(a => a.Text).ToList();
                return items;
            }

        }

        public void ClickLocation(string locationName)
        {
            var location = GetPageContainer();
            IWebElement link = location.FindElement(By.XPath(_locationLink.Replace("ReplaceLocationText", locationName)));
            link.Click();
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
