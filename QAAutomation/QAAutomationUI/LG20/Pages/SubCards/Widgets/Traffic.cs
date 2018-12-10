using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Widgets
{
    internal class Traffic : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        private static string _modalClassName = @"widget-modal";
        private static string _modalContainerName = @"lg-modal__container";
        private static string _modalVisableClass = "@lg-modal lg-modal--large lg-modal--visible";
        private static string _trafficBrandSelect = @"select-brand";

        #endregion

        #region --- Properties ---

        public string BrandSelectBox
        {
            get
            {
                var selectBoxProgramChannel = GetSelect("select", _trafficBrandSelect);
                return selectBoxProgramChannel != null ? selectBoxProgramChannel.SelectedOption.Text : string.Empty;
                //////////////////////////////////IF     then                                             else 
            }

            set
            {
                var selectBoxProgramChannel = GetSelect("select", _trafficBrandSelect);
                selectBoxProgramChannel.SelectByText(value);
            }
        }

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
        internal Traffic(IWebDriver driver) : base (driver, _modalClassName, _modalContainerName, _modalVisableClass)
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
