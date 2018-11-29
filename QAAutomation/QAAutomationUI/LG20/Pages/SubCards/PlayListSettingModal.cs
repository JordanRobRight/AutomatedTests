using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.APITests;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class PlayListSettingModal
    {
        #region -- Fields

        private static string _playListinfoModal = @"playlist-info-modal";
        private static string _pmfbContainer = @"pmfb-container";

        private static string _modalContainer = @"lg-modal__container";
        private static string _modalSection = @"lg-modal__section";

        // Modal popup - Update to support new filter changes
        private static string _playListName = @"playlist-info-field-name"; // Element: Input By: Name
        private static string _playListDescription = @"playlist-info-description"; // Element: TextArea By: Name

        private static string _playListFilterByClientProgramAndChannel = @"Filtered by client program and channel(s)"; // Search for checkbox with this text.
        private static string _playListFilterSelectClientProgram = @"select-filter-program"; // Element: Select By: id
        private static string _playListFilterSelectFilter = @"select-filter"; // Element: Select By: id

        private static string _playListFilterByLocationAndDevice = @"Filtered by location and device(s)";
        private static string _playLIstFilterTextBoxLocation = @"enter-filter-location"; // Element: TextBox By: id
        private static string _playListFilterLocationSelectDevice = @"enter-filter-location-device"; // Element: Select By: id

        private static string _playListFilterByTags = @"Filtered by tag(s)";
        private static string _playListFilterSelectTag = @"enter-filter-locations"; // Element: Select By: id
        private static string _playListFilterByTagsText = @"Select your Tag(s)"; // Get by label
        private static string _playListFilterByTagsTextSelect = @"lgfe-select"; // element: div By: class

        private static string _playListNumberOfPlayersLabelField = @"Players:";
        private static string _playListDurationLabelField = @"Estimated Duration:";


        private readonly IWebDriver _driver;
        #endregion

        #region  --- Private Properties ---

        internal IEnumerable<IWebElement> ModalButtons => GetModalButtons();
        internal IWebElement ModalChannelSelection => GetChannelSelection();
        //internal IWebElement ModalCreateCustomPlaylistCheckbox => GetCustomCheckbox();

        //private IEnumerable<IWebElement> ModalInputFields => GetModalInputFields();
        //https://stackoverflow.com/questions/6992993/selenium-c-sharp-webdriver-wait-until-element-is-present

        #endregion

        public string PlayListNameTextField
        {
            get
            {
                var getField = GetField("input", _playListName);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", _playListName);
                getField?.SendKeys(value);
            }
        }

        public string PlayListDescriptionTextField
        {
            get
            {
                var getField = GetField("textbox", _playListDescription);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                //var getModalWindow = _driver.FindElement(By.Id("playlist-settings-modal"));
                //var getmodalSubb = getModalWindow.FindElements(By.TagName("div")).FirstOrDefault(a => a.GetAttribute("class").Equals("lg-modal lg-modal--large lg-modal--visible"));
                //var inputFields = getmodalSubb.FindElements(By.TagName("textarea")).ToList();
                //var item = inputFields.FirstOrDefault(a => a.GetAttribute("name") != null &&
                //                                                      a.GetAttribute("name")
                //                                                          .Equals(_playListDescription, StringComparison.OrdinalIgnoreCase));
                //item.SendKeys("Good test");
                //var getModalWindow1 = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.Id, "playlist-settings-modal"); //_driver.FindElement(By.Id("playlist-settings-modal"));
                //var getmodalSubb1 = getModalWindow1.FindElements(By.TagName("div")).FirstOrDefault(a => a.GetAttribute("class").Equals("lg-modal lg-modal--large lg-modal--visible"));
                //var inputFields1 = getmodalSubb1.FindElements(By.TagName("textarea")).ToList();
                //var item1 = inputFields1.FirstOrDefault(a => a.GetAttribute("name") != null &&
                //                                           a.GetAttribute("name")
                //                                               .Equals(_playListDescription, StringComparison.OrdinalIgnoreCase));
                //// "lg-modal lg-modal--large lg-modal--visible"
                //item1.SendKeys("***after test");

                var getField = GetField("textarea", _playListDescription);
                getField?.SendKeys(value);
            }
        }

        public bool FilterByClientProgramAndChannelCheckbox
        {
            get
            {
                var ckboxProgramChannel = GetCustomCheckbox("label", _playListFilterByClientProgramAndChannel);
                var item = ckboxProgramChannel.FindElements(By.ClassName("checkbox")).FirstOrDefault();
                return item.Selected;
            }
            set
            {
                if (FilterByClientProgramAndChannelCheckbox != value)
                {
                    var ckboxProgramChannelLabel = GetCustomCheckbox("label", _playListFilterByClientProgramAndChannel);
                    ckboxProgramChannelLabel.Click();
                }
            } 
        }
        public string SelectClientProgramSelectBox { get; set; }
        public string SelectYourChannelSelectBox { get; set; }
        public bool FilterByLocationAndDeviceCheckbox
        {
            get
            {
                var ckboxLocationDevice = GetCustomCheckbox("label", _playListFilterByLocationAndDevice);
                var itemLocationDevice = ckboxLocationDevice.FindElements(By.ClassName("checkbox")).FirstOrDefault();
                return itemLocationDevice.Selected;
            }
            set
            {
                if (FilterByLocationAndDeviceCheckbox != value)
                {
                    var ckboxLocationDeviceLabel = GetCustomCheckbox("label", _playListFilterByLocationAndDevice);
                    ckboxLocationDeviceLabel.Click();
                }
            }
        }
        public string SelectYourLocationTextBox { get; set; }
        public string SelectYourDeviceSelectBox { get; set; }
        public bool FilterByByTagCheckbox
        {
            get
            {
                var cbvalue = GetCustomCheckbox("label", _playListFilterByTags);
                var itemTag = cbvalue.FindElements(By.ClassName("checkbox")).FirstOrDefault();
                return itemTag.Selected;
            }
            set
            {
                if (FilterByByTagCheckbox != value)
                {
                    var cbItem = GetCustomCheckbox("label", _playListFilterByTags);
                    cbItem.Click();
                }
            }
        }
        public string SelectYouTagTypeSelectBox { get; set; }
        public string SelectYouTagSelectBox { get; set; }

        public bool FilterByClientProgramAndChannel { get; set; }

        #region --- Constructor ---

        internal PlayListSettingModal(IWebDriver driver)
        {
            _driver = driver;
        }
        #endregion

        #region --- Methods ---
        private IEnumerable<IWebElement> GetModalButtons()
        {
            var modalContainer = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _modalContainer);
            var modalContainerButtons = modalContainer.FindElements(By.TagName("button")).ToList();
            return modalContainerButtons;
        }

        public bool ModalCancelButton()
        {
            try
            {
                var cancelButton = ModalButtons.FirstOrDefault(a => a.GetAttribute("aria-label") != null &&
                                                                    a.GetAttribute("aria-label").Equals("Close", StringComparison.OrdinalIgnoreCase));

                var cancelSpan = cancelButton.FindElement(By.TagName("span"));
                if (cancelSpan != null)
                {
                    cancelSpan.Click();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;            
        }

        public bool ModalSavebutton()
        {
            try
            {
                var saveButton = ModalButtons.FirstOrDefault(a => a.GetAttribute("type") != null &&
                                                                  a.GetAttribute("type").Equals("submit", StringComparison.OrdinalIgnoreCase) && a.Text.Equals("Save", StringComparison.OrdinalIgnoreCase));
                if (saveButton != null)
                {
                    saveButton.Click();
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }

            return false;
        }

        private IEnumerable<IWebElement> GetModalInputFields(string tagName)
        {
            var getModalWindow1 = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.Id, "playlist-settings-modal"); 
            var getmodalSubb1 = getModalWindow1.FindElements(By.TagName("div")).FirstOrDefault(a => a.GetAttribute("class").Equals("lg-modal lg-modal--large lg-modal--visible"));
            var inputFields = getmodalSubb1.FindElements(By.TagName(tagName)).ToList();

            return inputFields;
        }

        private IWebElement GetField(string tagName, string fieldName)
        {
            return GetModalInputFields(tagName).FirstOrDefault(a => a.GetAttribute("name") != null &&
                a.GetAttribute("name")
                    .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }

        private IWebElement GetCustomCheckbox(string tagName, string spanText)
        {
            var tagItems = GetModalInputFields(tagName);
            var item = tagItems.FirstOrDefault(a => a.Text.Contains(spanText));
            return item;
        }

        private IWebElement GetChannelSelection()
        {
            var GetModalContainer = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _modalContainer);
            var GetModalSection = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _modalSection);
            var inputFields = GetModalSection.FindElements(By.TagName("select")).ToList();

            return inputFields.FirstOrDefault(a => a.GetAttribute("id") != null &&
                a.GetAttribute("id")
                    .Equals("select-filter", StringComparison.OrdinalIgnoreCase));
        }
        #endregion
    }
}
