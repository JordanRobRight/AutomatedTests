using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
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
        #endregion

        #region  --- Properties ---

        private IWebDriver _driver;
        private IEnumerable<IWebElement> ModalButtons => GetModalButtons();
        internal IWebElement ModalCancelButton => GetModalCancelButton();
        internal IWebElement ModalNameEditField => GetNameField();
        internal IWebElement ModalChannelSelection => GetChannelSelection();
        internal IWebElement ModalCreateCustomPlaylistCheckbox => GetCustomCheckbox();
        internal IWebElement ModalSaveButton => GetModalSavebutton();

        private IEnumerable<IWebElement> ModalInputFields => GetModalInputFields();

        #endregion

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

        private IWebElement GetModalCancelButton()
        {
            // if (ModalButtons == null) GetModalButtons();

            var cancelButton = ModalButtons.FirstOrDefault(a => a.GetAttribute("aria-label") != null &&
                a.GetAttribute("aria-label").Equals("Close", StringComparison.OrdinalIgnoreCase));

            var cancelSpan = cancelButton.FindElement(By.TagName("span"));
            return cancelSpan;
            //return cancelButton;
        }

        private IWebElement GetModalSavebutton()
        {
            // if (ModalButtons == null) GetModalButtons();

            var saveButton = ModalButtons.FirstOrDefault(a => a.GetAttribute("type") != null &&
                a.GetAttribute("type").Equals("submit", StringComparison.OrdinalIgnoreCase) && a.Text.Equals("Save", StringComparison.OrdinalIgnoreCase));

            return saveButton;
        }

        private IEnumerable<IWebElement> GetModalInputFields()
        {
            var GetModalContainer = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _modalContainer);
            var GetModalSection = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _modalSection);
            var inputFields = GetModalSection.FindElements(By.TagName("input")).ToList();

            return inputFields;
        }

        private IWebElement GetNameField()
        {
            // if (ModalInputFields == null) GetModalInputFields();

            return this.ModalInputFields.FirstOrDefault(a => a.GetAttribute("name") != null &&
                a.GetAttribute("name")
                    .Equals("playlist-info-field-name", StringComparison.OrdinalIgnoreCase));
        }

        private IWebElement GetCustomCheckbox()
        {
            // if (ModalInputFields == null) GetModalInputFields();

            return this.ModalInputFields.FirstOrDefault(a => a.GetAttribute("name") != null &&
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
