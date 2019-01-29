using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class Players : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---
        private static readonly string _playerNameTdItem = @"pt-status-text-name pt-player-status-online";
        private static readonly string _playersTableTBody = @"#players-table > div.table-responsive > table > tbody";
        private static readonly string _playerOfflineStatus = @"[class*='pdwurt-status-dot pt-player-status-offline']";
        private static readonly string _playerOfflineStatusPlayerPage = @"[class*='pdwurt-status-dot pdwurt-status-offline']";
        private static readonly string _playerOnlineStatusPlayersPage = @"[class*='pdwurt-status-dot pt-player-status-online']";

        #endregion

        #region --- Constructor ---
        internal Players(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {

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

        public bool VerifyOfflineStatusPlayerPage
        {
            get
            {
                Wait(2);
                IWebElement offlinePlayerStatus = Driver.FindElement(By.CssSelector(_playerOfflineStatusPlayerPage));
                return offlinePlayerStatus.Displayed;
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