using OpenQA.Selenium;
using System;
using System.Linq;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class AccessoryPricingModal : ModalBasePage
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


        private static string formID = @"accessory-pricelist-info-form";
        private static string _modalClassName = @"location-settings-accessory-pricelist-modal";
        //private static string _accessoryPricingFactoryCheckbox = @"factory";

        private static string accessoryPricingCheckboxValue;
        
       

        #endregion

        #region --- Constructor ---
        internal AccessoryPricingModal(IWebDriver driver) : base(driver, _modalClassName, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
            // _modalClassName = modalClassName;
            FormID = formID;
        }
        #endregion

        #region --- Public Methods ---

        public string AccessoryPricingCheckboxValue
        {
            get
            {

                return accessoryPricingCheckboxValue;
            }
            set { accessoryPricingCheckboxValue = value; }

        }

        public bool IsModalDisplay
        {
            get
            {
                var getModal = GetModal();
                return getModal != null;
            }
        }

        public bool SelectCheckbox
        {
            get
            {
                var ckboxProgramChannel = GetCustomCheckbox("th", AccessoryPricingCheckboxValue);
                var item = ckboxProgramChannel.FindElements(By.ClassName("accessory-pricelist-checkbox-value")).FirstOrDefault();
                return item != null && item.Selected;
            }
            set
            {
                if (SelectCheckbox != value)
                {
                    var ckboxProgramChannelLabel = GetCustomCheckbox("th", AccessoryPricingCheckboxValue);
                    ckboxProgramChannelLabel.Click();
                }
            }
        }
        public void ClickSaveCancelSaleAppointmentButton(string buttonText)// try ModalSaveButtonClick instead
        {
            var buttons = GetFormArea().FindElements(By.TagName("button"));

            foreach (IWebElement button in buttons)
            {
                if (button.Text == buttonText)
                {
                    button.Click();
                    break;
                }
            }
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