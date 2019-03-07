using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class Players : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---
       // private static readonly string _playerNameTdItem = @"pt-status-text-name pt-player-status-online";
        private static readonly string _playersTableTBody = @"#players-table > div.table-responsive > table > tbody";
        private static readonly string _playerOfflineStatus = @"[class*='pdwurt-status-dot pt-player-status-offline']";
        //private static readonly string _playerOfflineStatusPlayerPage = @"[class*='pdwurt-status-dot pdwurt-status-offline']";
        private static readonly string _playerOnlineStatusPlayersPage = @"[class*='pdwurt-status-dot pt-player-status-online']";
        //private static readonly string _pdwurtStatusWrapper = @"pdwurt-status-wrapper";
        //private static readonly string _pdwurButtons = @"pdwur-item-text";
        //private static readonly string playerDetailWrapper = @"player-details-wrapper";//class
        //private static readonly string xpathPlugPlayerName = @"//a[contains(@href,'ReplaceText')]";
        private static string _playerName = @"//span[text()='ReplaceText']";
        public string PlayerName { get; set; }
        private static readonly string _pageSearchField = @"VueTables__search-field";// search field class name
        private static readonly string _pageUtilBarContainerClassName = @"form-group form-inline pull-left VueTables__search";
        private static string _playerContainer = @"players-container";
        private static string _pageFunctionBarContainerClassName = @"pdw-utility-row-top";//class name
        private static readonly string  _screenConnectClass = @"pdwurt-screen-connect";//class name

        #endregion

        #region --- Constructor ---
        internal Players(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = _playerContainer; //Element Div By: Id
            PageSearchField = _pageSearchField;
            PageUtilBarContainerClassName = _pageUtilBarContainerClassName;
            PageFunctionBarContainerClassName = _pageFunctionBarContainerClassName;
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
        //protected void PlayerID(object sender, EventArgs e)
        //{

        //    //Response.
        //}
        #region --- Public Methods ---


        public void SelectPlayer(string playName)
        {
            var player = GetPlayer(playName);
            player?.Click();
        }

        
        public bool VerifyOfflineStatusPlayersPage
        {

            get
            {
                Wait(2);
                IWebElement offlinePlayerStatus = Driver.FindElement(By.CssSelector(_playerOfflineStatus));
                return offlinePlayerStatus.Displayed;
            }

        }
        public List<string> GetNoMatchingRecordText
        {
            get
            {
                var wrapper = GetPageContainer();
                var errorText = wrapper.FindElements(By.TagName("td"));
                List<string> errorList = new List<string>();
                foreach (var errorInfo in errorText)
                {
                    errorList.Add(errorInfo.Text);
                }
                return errorList;
            }
        }
        /*public bool VerifyOfflineStatusPlayerPage
        {
            get
            {
                Wait(2);
                IWebElement offlinePlayerStatus = Driver.FindElement(By.CssSelector(_playerOfflineStatusPlayerPage));
                return offlinePlayerStatus.Displayed;
            }

        }*/

        public List<string> GetPlayerNameDisplayed//can be used after a search in players page
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.TagName("td"));
                var span = wrapper.FindElements(By.XPath(_playerName.Replace("ReplaceText", PlayerName)));
                List<string> list = new List<string>();
                foreach (var playerInfo in span)
                {
                    list.Add(playerInfo.Text);
                }
                return list;
            }

        }

        public List<string> GetTotalRecordCount
        {
            get
            {
                var wrapper = GetPageContainer();
                var recordTag = wrapper.FindElements(By.TagName("p"));
                List<string> recordCount = new List<string>();
                foreach (var playerInfo in recordTag)
                {
                    recordCount.Add(playerInfo.Text);
                }
                return recordCount;
            }
        }
        public bool VerifyOnlineStatus
        {

            get
            {
                Wait(2);
                IList<IWebElement> List = Driver.FindElements(By.CssSelector(_playerOnlineStatusPlayersPage));
                foreach (IWebElement onlinePlayers_List in List)
                {
                    if (List.Count != 0)
                    {
                    }
                    break;
                }
                return true;
            }
        }

        public string GetCurrentTab()
        {
            var tabs = Driver.WindowHandles;
            if(tabs.Count == 2)
            {
                var secondTab = Driver.SwitchTo().Window(tabs[1]);
                string url2 = secondTab.Url;
                return url2;                
            }
            Driver.SwitchTo().Window(tabs[0]);           
            string url = Driver.Url;            
            return url;
        }               
      
        public void PlayersSelectScreenConnect()
        {  
            var playerPlug = GetPageContainer().FindElement(By.ClassName(_screenConnectClass));
            playerPlug.Click();
        }

        public void CloseScreenconnect()
        {
            var currentTab = Driver.WindowHandles;
            Driver.Close();
        }
        
        /*  public string IsScreenconnectUrl
          {
              get
              {                             
                  var afterTabs = Driver.WindowHandles;

                  if (afterTabs.Count > 1)
                  {
                      Driver.SwitchTo().Window(afterTabs[1]);
                  }                
                  string url = Driver.Url;                
                  return url;
              }
          }*/

        /*  public string IsDciUrl(ReadOnlyCollection<string> beforeTabs)
          {

              Driver.SwitchTo().Window(beforeTabs[0]);//gets the liveguide tab
              string url = Driver.Url;
              return url;
          }*/


        public bool VerifyNotSetupPlayer()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region --- Private Methods ---
        private IWebElement GetPlayer(string playerName)
        {
            //IWebElement playerTable = SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, "players-table");
            IWebElement playerTable = SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _playersTableTBody);

            IEnumerable<IWebElement> trs = playerTable.FindElements(By.TagName("tr")).ToList();
            IWebElement tdItem = trs.FirstOrDefault(tr => tr.FindElements(By.TagName("td")) != null && tr.GetInnerHTML().Contains(playerName));
            var playerSelect = tdItem.FindElements(By.TagName("span")).FirstOrDefault(a => string.Equals(a.Text, playerName, StringComparison.OrdinalIgnoreCase));
            //t => t.GetElementFromCompoundClass(By.TagName("span"),
            // _playerNameTdItem) != null && t.GetElementFromCompoundClass(By.TagName("span"),
            // _playerNameTdItem).Text.Equals(playerName, StringComparison.OrdinalIgnoreCase));

            return playerSelect;
        }

       
        #endregion
    }
}