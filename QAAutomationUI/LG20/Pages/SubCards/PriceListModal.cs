using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class PriceListModal : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        //private static string _modalClassName = @"tag-location-setting-modal";
        private static string _modalContainerName = @"lg-modal__container";
        private static string _modalVisableClass = @"lg-modal lg-modal--large lg-modal--visible";
        private static string _appointmentTimeSelect = @"select-filter";
        // private IWebElement tagsContainer => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, addTagsForm);

        private static readonly string _currenteDate = @"#datepickers-container > div:nth-child(3) > div.datepicker--content > div > div.datepicker--cells.datepicker--cells-days > div.datepicker--cell.datepicker--cell-day.-current-.-selected-";
        #endregion

        #region --- Properties ---


        private static string formID = @"pricelist-info-form-my-pricelist";
        private static string _modalClassName = @"location-settings-pricelist-modal";
        private static readonly string plussButtonClass = @"pl-utility-add-button";
        private static readonly string _tableTextArea = @"//table[@id='location-pricelist-table']//tr//td//textarea";
        private static readonly string _tableInputArea = @"//table[@id='location-pricelist-table']//tr//td//input";
        #endregion

        #region --- Constructor ---
        internal PriceListModal(IWebDriver driver) : base(driver, _modalClassName, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
            // _modalClassName = modalClassName;
            FormID = formID;
        }
        #endregion

        #region --- Public Methods ---

        public bool IsModalDisplay
        {
            get
            {
                var getModal = GetModal();
                return getModal != null;
            }
        }


        public void AddCustomPricePlusButton()
        {           
            var button = GetFormArea().FindElement(By.ClassName(plussButtonClass));
            button.Click();
        }

        public void CustomPriceDescriptionText(string testData)
        {
            List<IWebElement> test = _driver.FindElements(By.XPath(_tableTextArea)).ToList();
            IWebElement lastTD = test.LastOrDefault();
            lastTD.SendKeys(testData);


        }

        public void CustomPriceInputText(string testData)
        {
            List<IWebElement> test = _driver.FindElements(By.XPath(_tableInputArea)).ToList();
            IWebElement lastTD = test.LastOrDefault();
            lastTD.Clear();
            lastTD.SendKeys(testData);
        }

        


       

        #endregion

        #region --- Private Methods ---
        private IWebElement GetCustomCheckbox(string tagName, string spanText)
        {
            var tagItems = GetModalInputFields(tagName);
            var item = tagItems.FirstOrDefault(a => a.Text.Equals(spanText, StringComparison.OrdinalIgnoreCase));
            return item;
        }
        #endregion

    }
}