using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Player
{
    public sealed class PlayerConfigureChannel : LGBasePage
    {
        #region --- Properties ---
        private static string _playerContainer = @"player-container";
        private readonly string _lgfeContainer = @"lgfe-tile-grid-wrapper";
        //private static readonly string _gridItem = @"div.lgfe-tile.lgfe-tile-grid-item";//class name
        //private static readonly string _gridLabel = @"lgfe-tile-grid-item-label";
        private static readonly string _channelLabelXpath = @"//div[@class='lgfe-tile-grid-item-group']//span[contains(text(),'channels')]/ancestor::h4/following-sibling::div//div[@class='lgfe-tile-grid-item-label']";
        #endregion
        #region --- Constructor ---
        public PlayerConfigureChannel(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {

            PageContainerName = _playerContainer; //Element Div By: Id
            PageFunctionBarContainerClassName = _lgfeContainer;
        }
        #endregion
        #region --- Public Methods ---
         
        public List<string> GetChannelFields 
        {
            get
            {
                List<IWebElement> labels = GetPageContainer().FindElements(By.XPath(_channelLabelXpath)).ToList();
                List<string> channelLabelList = new List<string>();
                foreach (var channelLabel in labels)
                {
                    //string s = channelLabel.Text;
                    channelLabelList.Add(channelLabel.Text);
                }
                return channelLabelList;
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
        //public string Name { get; set; }
    }
}
