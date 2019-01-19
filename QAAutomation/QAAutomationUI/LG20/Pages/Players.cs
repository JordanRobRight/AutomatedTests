using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// RK - 1/18/19 - Please remove the NUnit.Framework using statement. POM classes should not have this using statement since they do not perform assertions. 

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

        // RK - 1/18/19 - I suggest that you can convert this method to a property to something like PlayerOnLine. By doing this, the player is online or offline.
        // By changing this to a property, you remove one method (for now)
        // According to the test case, there is supposed to be a 3rd state.
        // We need to account for that at some point.
        public bool VerifyOfflineStatusPlayersPage
        {

            get
            {
                Wait(2);


                IWebElement offlinePlayerStatus = Driver.FindElement(By.CssSelector(_playerOfflineStatus));
                
                 // RK - 1/18/19 - Just move this statement to UnitTest1.cs since that is where we should do the validation. This method is returning the value anyway it should be pretty easy.                   
                 
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
                        // RK - 1/18/19 - Just move this statement to UnitTest1.cs since that is where we should do the validation. This method is returning the value anyway it should be pretty easy.                   
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