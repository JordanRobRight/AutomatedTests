using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
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
        private static readonly string _playerOnlineStatus = @"[class*='pdwurt-status-dot pt-player-status-online']";
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

        public bool VerifyOfflineStatus()
        {
            Wait(2);
            IList<IWebElement> List = Driver.FindElements(By.CssSelector(_playerOfflineStatus));
            foreach (IWebElement offlinePlayers_List in List)
            {
                if (List.Count != 0)
                {
                    Assert.IsTrue(offlinePlayers_List.Enabled);
                }
                break;

            }
            return true;
        }

        public bool VerifyOnlineStatus()
        {
            Wait(2);
            IList<IWebElement> List = Driver.FindElements(By.CssSelector(_playerOnlineStatus));
            foreach (IWebElement onlinePlayers_List in List)
            {
                if (List.Count != 0)
                {
                    Assert.IsTrue(onlinePlayers_List.Enabled);
                }
                break;
            }
            return true;
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
