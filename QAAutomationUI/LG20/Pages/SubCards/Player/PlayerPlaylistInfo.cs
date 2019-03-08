using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Player
{
    public sealed class PlayerPlaylistInfo : LGBasePage
    {
        #region --- Properties --- 
        private static string _playerContainer = @"player-container";
        private readonly string _lgfeContainer = @"lgfe-tile-grid-wrapper";
        private static readonly string _pencilButton = @"lgfe-tile-button-wrapper";
        private static readonly string _playerInformationFields = @"//div[@class='lgfe-tile lgfe-tile-grid-item lgfe-tile-grid-top-item']//strong";
        private static readonly string _editButtonTags = @"//span[contains(text(),'player information')]/ancestor::h4/preceding-sibling::div//div//i";

        private static readonly string _playlistWrapper = @"#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(3) > div:nth-child(2) > div";// for now using css with index, it will be good if we get a ID for each section in playlist tab
        private static readonly string _gridItemWrapper = @"tile-grid-item-wrapper";//class name

        #endregion

        #region --- Constructor ---
        public PlayerPlaylistInfo(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {

            PageContainerName = _playerContainer; //Element Div By: Id
            PageFunctionBarContainerClassName = _lgfeContainer;
        }
        #endregion
        #region --- Public Methods ---
                    
        public List<string> GetPlaylistInfo
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.CssSelector(_playlistWrapper));
                var deviceInfos = wrapper.FindElements(By.ClassName(_gridItemWrapper));
                List<string> list = new List<string>();
                foreach (var deviceInfo in deviceInfos)
                {
                    string s = deviceInfo.Text;                 
                    list.Add(deviceInfo.Text);
                }
                return list;
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
