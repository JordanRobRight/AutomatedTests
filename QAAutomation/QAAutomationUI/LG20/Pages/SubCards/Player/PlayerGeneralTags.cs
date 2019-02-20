using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Player
{
    public sealed class PlayerGeneralTags : LGBasePage
    {
        #region --- Properties ---
        private static string _playerContainer = @"player-container";
        private readonly string _lgfeContainer = @"lgfe-tile-grid-wrapper";
        private static readonly string _pencilButton = @"lgfe-tile-button-wrapper";
        private static readonly string _editButtonTags = @"//span[contains(text(),'tags')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _systemGeneratedTagList = @"//div[@class='pim-tags-legend']//div";//xpath

        #endregion 
        #region --- Constructor ---
        public PlayerGeneralTags(IWebDriver driver, TestConfiguration config) : base(driver, config)
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

         
        public IList<string> GetSystemGeneratedListOfTags
        {
            get
            {
                IEnumerable<IWebElement> TagList = Driver.FindElements(By.XPath(_systemGeneratedTagList)).ToList();              
                var items = TagList.Select(a => a.Text).ToList();                            
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
