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

        private readonly string _playListinfoModal = @"playlist-info-modal";
        private readonly string _pmfbContainer = @"pmfb-container";

        private readonly string _modalContainer = @"lg-modal__container";
        private readonly string _modalSection = @"lg-modal__section";

        // Modal popup - Update to support new filter changes
        private readonly string _playListName = @"playlist-info-field-name"; // Element: Input By: Name
        private readonly string _playListDescription = @"playlist-info-description"; // Element: TextArea By: Name

        private readonly string _playListFilterByClientProgramAndChannel = @"Filtered by client program and channel(s)"; // Search for checkbox with this text.
        private readonly string _playListFilterSelectClientProgram = @"select-filter-program"; // Element: Select By: id
        private readonly string _playListFilterSelectFilter = @"select-filter"; // Element: Select By: id

        private readonly string _playListFilterByLocationAndDevice = @"Filtered by location and device(s)";
        private readonly string _playLIstFilterTextBoxLocation = @"enter-filter-location"; // Element: TextBox By: id
        private readonly string _playListFilterLocationSelectDevice = @"enter-filter-location-device"; // Element: Select By: id

        private readonly string _playListFilterByTags = @"Filtered by tag(s)";
        private readonly string _playListFilterSelectTag = @"enter-filter-locations"; // Element: Select By: id
        private readonly string _playListFilterByTagsText = @"Select your Tag(s)"; // Get by label
        private readonly string _playListFilterByTagsTextSelect = @"lgfe-select"; // element: div By: class

        private readonly string _playListNumberOfPlayersLabelField = @"Players:";
        private readonly string _playListDurationLabelField = @"Estimated Duration:";


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
                var count = _driver.WindowHandles.Count;
                

                var getField = GetField("input", _playListName);
                if (getField != null)
                {
                    getField.SendKeys(value);
                }
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
                var getField = GetField("textbox", _playListDescription);
                if (getField != null)
                {
                    getField.SendKeys(value);
                }
            }
        }

        public bool FilterByClientProgramAndChannelCheckbox { get; set; }
        public string SelectClientProgramSelectBox { get; set; }
        public string SelectYourChannelSelectBox { get; set; }
        public bool FilterByLocationAndDeviceCheckbox { get; set; }
        public string SelectYourLocationTextBox { get; set; }
        public string SelectYourDeviceSelectBox { get; set; }
        public bool FilterByByTagCheckbox { get; set; }
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
            //var GetModalContainer = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _modalContainer);
            var GetModalSection = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _modalSection);
            var inputFields = GetModalSection.FindElements(By.TagName(tagName)).ToList();

            return inputFields;
        }

        private IWebElement GetField(string tagName, string fieldName)
        {
            // if (ModalInputFields == null) GetModalInputFields();

            return this.GetModalInputFields(tagName).FirstOrDefault(a => a.GetAttribute("name") != null &&
                a.GetAttribute("name")
                    .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }

        private IWebElement GetCustomCheckbox(string tagName)
        {
            // if (ModalInputFields == null) GetModalInputFields();

            return this.GetModalInputFields(tagName).FirstOrDefault(a => a.GetAttribute("name") != null &&
                a.GetAttribute("name")
                    .Equals("checkbox-create-playlist", StringComparison.OrdinalIgnoreCase));
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
