using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Widgets
{
    internal class Traffic
    {
        #region --- Fields ---
        private IWebDriver _driver;
        #endregion

        #region --- Properties ---

        public string BrandSelectBox
        { get; set; }

        public IEnumerable<string> BrandsFromSelectionBox
        {
            get;
        }

        public string DurationTextBox
        { get; set; }

        public string TrafficZipCodeTextBox
        {
            get;
            set;
        }
        #endregion

        #region --- Constructor ---
        internal Traffic(IWebDriver driver)
        {
            _driver = driver;
        }
        #endregion

        #region --- Public Methods ---
        #endregion

        #region --- Private Methods ---
        #endregion 

    }
}
