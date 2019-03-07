using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages 
{
    internal class PlayerDetail : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---
        private static readonly string _playersTableTBody = @"#players-table > div.table-responsive > table > tbody";
        private static readonly string _playerOfflineStatus = @"[class*='pdwurt-status-dot pt-player-status-offline']";
        private static readonly string _playerOfflineStatusPlayerPage = @"[class*='pdwurt-status-dot pdwurt-status-offline']";
        private static readonly string _playerOnlineStatusPlayersPage = @"[class*='pdwurt-status-dot pt-player-status-online']";
        private static readonly string _pdwurtStatusWrapper = @"pdwurt-status-wrapper";
        private static readonly string _pdwurButtons = @"pdwur-item-text";
        private static readonly string xpathPlugPlayerName = @"//a[contains(@href,'LG-QA')]";
        private static readonly string _pageSearchField = @"VueTables__search-field";// search field class name
        private static readonly string _pageUtilBarContainerClassName = @"form-group form-inline pull-left VueTables__search";
        private static string _playerContainer = @"player-container";
        private static string _playerName = @"//span[text()='ReplaceText']";
        private static string _playerDetailTitle = @"//div[text()='ReplaceText']";
        public string PlayerName { get; set; }

        #endregion

        #region --- Constructor ---
        internal PlayerDetail(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = _playerContainer; //Element Div By: Id
            PageSearchField = _pageSearchField;
            PageUtilBarContainerClassName = _pageUtilBarContainerClassName;
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
        protected void PlayerID(object sender, EventArgs e)
        {

            //Response.
        }
        #region --- Public Methods ---



        //        public bool VerifyOfflineStatusPlayerDetailPage
        public bool IsPlayerOfflineStatus
        {
            get
            {
                IWebElement offlinePlayerStatus = null;
                Wait(2);
                try
                {
                    offlinePlayerStatus = Driver.FindElement(By.CssSelector(_playerOfflineStatusPlayerPage));
                }
                catch (NoSuchElementException e)
                {
                    offlinePlayerStatus = Driver.FindElement(By.CssSelector(_playerOnlineStatusPlayersPage));
                }

                return offlinePlayerStatus?.Displayed ?? false;
            }
        }


        public string GetCurrentTab()
        {
            var tabs = Driver.WindowHandles;
            if (tabs.Count == 2)
            {
                var secondTab = Driver.SwitchTo().Window(tabs[1]);
                string url2 = secondTab.Url;
                return url2;
            }
            Driver.SwitchTo().Window(tabs[0]);
            string url = Driver.Url;
            return url;
        }

        public void SelectTab(String tabName)
        {
            var butons = Driver.FindElements(By.ClassName(_pdwurButtons));
            foreach (var button in butons)
            {
                if (button.Text == tabName) { button.Click(); break; }
            }
        }

        public void PlayerDetailSelectScreenConnect()
        {
            var links = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _pdwurtStatusWrapper).FindElements(By.TagName("i"));
            IWebElement link = links.FirstOrDefault(a => a.GetAttribute("class") != null &&
                                                             a.GetAttribute("class").Equals("fa fa-plug", StringComparison.OrdinalIgnoreCase));
            link.Click();
        }

        public void PlayerDetailCloseScreenconnect()
        {
            var currentTab = Driver.WindowHandles;
            Driver.Close();
        }

        //gets page title, "Player Details"
        public string PlayerDetailTitle {get;set;}
        public bool IsPageTitle
        {
            get
            {
                var getPageTitle = GetPageContainer().FindElement(By.XPath(_playerDetailTitle.Replace("ReplaceText", PlayerDetailTitle)));
                bool pageTitle = getPageTitle.Enabled;          
                return pageTitle;
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
            IWebElement playerTable = SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _playersTableTBody);

            IEnumerable<IWebElement> trs = playerTable.FindElements(By.TagName("tr")).ToList();
            IWebElement tdItem = trs.FirstOrDefault(tr => tr.FindElements(By.TagName("td")) != null && tr.GetInnerHTML().Contains(playerName));
            var playerSelect = tdItem.FindElements(By.TagName("span")).FirstOrDefault(a => string.Equals(a.Text, playerName, StringComparison.OrdinalIgnoreCase));
            return playerSelect;
        }
        #endregion
    }
}