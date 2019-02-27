using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class ConfigureTickersSection : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private static readonly string _editButtonTickers = @"//span[contains(text(),'Tickers')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _tileGridWrapper = @"lgfe-tile-grid-item-hero-wrapper";//class name to select location details section
        private static readonly string _locationDetailItem = @"lgfe-item";
        private static string _locationsContainer = @"location-container";
        private static readonly string _editButtonTags = @"//span[contains(text(),'tags')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _tickersContainerClass = @"tickers-container";//id
        private TickerManagementModal _tickersModal = null;
        internal TickerManagementModal TickerManagementModal
        {
            get
            {
                if (_tickersModal == null)
                {
                    _tickersModal = new TickerManagementModal(this.Driver);

                }
                return _tickersModal;
            }
            set => _tickersModal = value;
        }
        #endregion

        #region --- Constructor ---
        internal ConfigureTickersSection(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = _locationsContainer; //Element Div By: Id
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

        public void TickersEditButtonClick()
        {
            Wait(2);
            IWebElement button = Driver.FindElement(By.XPath(_editButtonTickers));
            button.Click();
        }

        public List<string> GetTickersFields
        {
            get
            {
                var tickersContainerList = GetPageContainer().FindElement(By.Id(_tickersContainerClass));
                var tickersContainerSpanList = tickersContainerList.FindElements(By.TagName("span"));
                List<string> tickersFields = new List<string>();
                foreach (var tickersFiled in tickersContainerSpanList)
                {                    
                    tickersFields.Add(tickersFiled.Text);
                }
                return tickersFields;
            }

        }

        #endregion

        #region --- Private Methods ---



        #endregion
    }
}