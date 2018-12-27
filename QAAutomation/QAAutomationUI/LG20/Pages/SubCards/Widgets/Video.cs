using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Widgets
{
    internal class video : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        private static string _modalClassName = @"asset-search";
        private static string _modalContainerName = @"lg-modal__content";
        private static string _modalVisableClass = @"lg-modal__panel";
        //private static string _videoBrandSelect = @"select-brand";
        //private static string _videoDescription = @"lg-modal-description";
        //private static string _videoDescriptionContent = @"lg-modal-description-content";
        #endregion

        #region --- Properties ---

       


       

        

        public string videoTextBox
        {
            get
            {
                var textbox = GetField("input", "id", "asset-search");
                return textbox.Text;
            }
            set
            {
                var textbox = GetField("input", "id", "asset-search");
                textbox.SendKeys(value);
            }
        }

        
        #endregion

        #region --- Constructor ---
        internal video(IWebDriver driver) : base(driver, _modalClassName, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
        }
        #endregion

        #region --- Public Methods ---

        public void SearchVideoAssets()
        {
            var searchBox = GetField("input", "id", "asset-search");
            searchBox.SendKeys("test");
        }

        public void SelectVideo()
        {
            var assetSelection = GetField("div","class", "avsfim-name");
            assetSelection.Click();
        }

        #endregion

        #region --- Private Methods ---

        #endregion

    }
}
