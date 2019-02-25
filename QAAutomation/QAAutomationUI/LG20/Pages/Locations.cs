using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class Locations : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private static string _locationsContainer = @"locations-container";
        private readonly string _lgfeContainer = @"lgfe-tile-grid-wrapper";
        private static readonly string _locationsTableTBody = @"#locations-table > div.table-responsive > table > tbody";
        private static readonly string _locationTableTHead = @"#locations-table > div.table-responsive > table > thead";
        private static readonly string _locationHeader = @"pm-action-bar-header-name";
        private static readonly string _pageSearchField = @"VueTables__search-field";// search field class name
        private static readonly string _pageUtilBarContainerClassName = @"form-group form-inline pull-left VueTables__search";
        #endregion

        #region --- Constructor ---
        internal Locations(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            PageContainerName = _locationsContainer; //Element Div By: Id
            PageFunctionBarContainerClassName = _lgfeContainer;
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

        #region --- Public Methods ---
        public void SelectLocation(string locationName)
        {
            var location = GetLocation(locationName);
            location?.Click();
        }
        //to continue..
        public List<string> IsLocationHeader
        {
            get
            {
                var s = "";
                var wrapper = GetPageContainer().FindElement(By.ClassName(_locationHeader));
                var deviceInfos = wrapper.FindElements(By.TagName("div"));
                List<string> list = new List<string>();
                foreach (var deviceInfo in deviceInfos)
                {
                    s = deviceInfo.Text;
                    list.Add(s);
                    
                }
                return list;
            }

        }

        public List<string> GetLocationColumn
        {
            get
            {
                IWebElement locationTable = SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _locationTableTHead);

                IEnumerable<IWebElement> trs = locationTable.FindElements(By.TagName("tr")).ToList();

                IWebElement thItem = trs.FirstOrDefault(tr => tr.FindElements(By.TagName("th")) != null);
                var locationSelect = thItem.FindElements(By.TagName("span"));

                List<string> locationsColumnList = new List<string>();
                foreach (var lc in locationSelect)
                {
                    string locationColName = lc.Text;
                    locationsColumnList.Add(locationColName);
                }
                return locationsColumnList;
            }
        }
        #endregion

        #region --- Private Methods ---
        private IWebElement GetLocation(string locationName)
        {
            //IWebElement playerTable = SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, "players-table");
            IWebElement locationTable = SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _locationsTableTBody);

            IEnumerable<IWebElement> trs = locationTable.FindElements(By.TagName("tr")).ToList();
            IWebElement tdItem = trs.FirstOrDefault(tr => tr.FindElements(By.TagName("td")) != null && tr.GetInnerHTML().Contains(locationName));
            var locationSelect = tdItem.FindElements(By.TagName("td")).FirstOrDefault(a => string.Equals(a.Text, locationName, StringComparison.OrdinalIgnoreCase));
            //t => t.GetElementFromCompoundClass(By.TagName("span"),
            // _playerNameTdItem) != null && t.GetElementFromCompoundClass(By.TagName("span"),
            // _playerNameTdItem).Text.Equals(playerName, StringComparison.OrdinalIgnoreCase));

            return locationSelect;
        }


        #endregion 
    }
}
