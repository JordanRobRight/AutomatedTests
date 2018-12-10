using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        #endregion

        #region --- Private Methods ---
        private IWebElement GetPlayer(string playerName)
        {
            IWebElement playerTable = SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, "players-table");
            IEnumerable<IWebElement> trs = playerTable.FindElements(By.TagName("tr")).ToList();
            var items = trs.Where(tr => tr.FindElements(By.TagName("td")) != null && tr.GetInnerHTML().Contains(playerName)).Select(a => a).ToList();
            var playerSelect = items.FirstOrDefault(t => t.GetElementFromCompoundClass(By.TagName("span"),
                                                             _playerNameTdItem) != null && t.GetElementFromCompoundClass(By.TagName("span"),
                                                             _playerNameTdItem).Text.Equals(playerName, StringComparison.OrdinalIgnoreCase));

            return playerSelect;
        }
        #endregion 
    }
}
