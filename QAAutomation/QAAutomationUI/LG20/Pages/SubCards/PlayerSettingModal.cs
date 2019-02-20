using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using QA.Automation.APITests;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class PlayerSettingModal : ModalBasePage
    {
        #region -- Fields 

        //private static string _playListinfoModal = @"playlist-info-modal";
        //private static string _pmfbContainer = @"pmfb-container";

        private static string _modalContainer = @"lg-modal__container";
        private static string _modalSection = @"lg-modal__section";

        // Modal popup - Update to support new filter changes
        private static string _playerSettingModal = @"player-info-modal"; // Element: Div By: Id
        private static string _playerSettingModalVisiableClass = @"lg-modal lg-modal--large lg-modal--visible"; // Element: N/A By: ClassName
        private static string _playerName = @"playerName"; // Element: Input By: Name
        private static string _playerDescription = @"description"; // Element: TextArea By: Name

        private static string _playListFilterByClientProgramAndChannelCheckbox = @"Filtered by client program and channel(s)"; // Search for checkbox with this text.
        // RK - 1/31/19 - This css selector has a index in it .. child(8). I'm not sure if this is a good idea to do this because the index might change to due other factors like different login/permission
        // could we possibly find a more flexible solution here. Otherwise we can leave it for now and come up with something later. 

        private static string _playerZoneSelect = @"//div[@class='lgfe-select']//label[text()='Zone']/following-sibling::select"; // Element: Select By: xpath (OR) can use //div[@class='lgfe-select']//label/following-sibling::select
        private static string _playListFilterChannelSelect = @"select-filter"; // Element: Select By: id

        private static string _playListFilterByLocationAndDeviceCheckbox = @"Filtered by location and device(s)";
        private static string _playLIstFilterLocationTextBox = @"enter-filter-location"; // Element: TextBox By: id
        private static string _playListFilterLocationSelectionContainer = @"eac-container-enter-filter-location";
        private static string _playListFilterLocationDeviceSelect = @"enter-filter-location-device"; // Element: Select By: id

        private static string _playListFilterTagSection = @"lg-modal__field filterByTagsTopBox";
        private static string _playListFilterByTagsCheckbox = @"Filtered by tag(s)";
        private static string _playListFilterTagTypeSelect = @"enter-filter-locations"; // Element: Select By: id
        private static string _playListFilterFieldsClass = @"lg-modal__field";
        private static string _playListFilterByTagsTypeLabel = @"Select your Tag(s)"; // Get by label
        //private static string _playListFilterByTagsSelect = @"lgfe-select"; // element: div By: class

        private static string _playListFilterPlayerTagsSection = @"lg-modal__field tags-field"; // Element: Div By: ClassName
        private static string _playListFilterPlayerTagsWrapper = @"pim-all-tags-wrapper";

        private static string _playListNumberOfPlayersLabelField = @"Players:";
        private static string _playListDurationLabelField = @"Estimated Duration:";


        private readonly IWebDriver _driver;
        #endregion

        #region  --- Private Properties ---

        //internal IEnumerable<IWebElement> ModalButtons => GetModalButtons();
        //internal IWebElement ModalChannelSelection => GetChannelSelection();

        #endregion

        #region --- Public Properties ---

        public bool IsModalDisplay
        {
            get
            {
                var getModal = GetModal();
                return getModal != null;
            }
        }
        public string PlayerNameTextField
        {
            get
            {
                var getField = GetField("input", "name", _playerName);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {
                var getField = GetField("input", "name", _playerName);
                getField.SendKeysOrClear(value);
                
            }
        }

        public string PlayerDescriptionTextField
        {
            get
            {
                var getField = GetField("textbox", "name", _playerDescription);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {
               
                var getField = GetField("textarea", "name", _playerDescription);
                getField.SendKeysOrClear(value);
            }
        }

        public bool FilterByClientProgramAndChannelCheckbox
        {
            get
            {
                var ckboxProgramChannel = GetCustomCheckbox("label", _playListFilterByClientProgramAndChannelCheckbox);
                var item = ckboxProgramChannel.FindElements(By.ClassName("checkbox")).FirstOrDefault();
                return item != null && item.Selected;
            }
            set
            {
                if (FilterByClientProgramAndChannelCheckbox != value)
                {
                    var ckboxProgramChannelLabel = GetCustomCheckbox("label", _playListFilterByClientProgramAndChannelCheckbox);
                    ckboxProgramChannelLabel.Click();
                }
            } 
        }
        
        // no id to select in select tag
        public string SelectZoneSelectBox
        {
            get
            {
                var selectPlayerZone = GetSelect("select", _playerZoneSelect);
                return selectPlayerZone != null ? selectPlayerZone.SelectedOption.Text : string.Empty;
                //////////////////////////////////IF     then                                             else 
            }

            set
            {
                var selectBoxProgramChannel = GetSelect("select", _playerZoneSelect);
                selectBoxProgramChannel.SelectByText(value);
            }
        }
        public string SelectYourChannelSelectBox
        {
            get
            {
                var selectBoxChannel = GetSelect("select", _playListFilterChannelSelect);
                return selectBoxChannel != null ? selectBoxChannel.SelectedOption.Text : string.Empty;
            }

            set
            {
                var selectBoxChannel = GetSelect("select", _playListFilterChannelSelect);
                selectBoxChannel.SelectByText(value);

            }
        }
        public bool? FilterByLocationAndDeviceCheckbox
        {
            get
            {
                var ckboxLocationDevice = GetCustomCheckbox("label", _playListFilterByLocationAndDeviceCheckbox);
                var itemLocationDevice = ckboxLocationDevice.FindElements(By.ClassName("checkbox")).FirstOrDefault();
                return itemLocationDevice?.Selected;
            }
            set
            {
                if (FilterByLocationAndDeviceCheckbox != value)
                {
                    var ckboxLocationDeviceLabel = GetCustomCheckbox("label", _playListFilterByLocationAndDeviceCheckbox);
                    ckboxLocationDeviceLabel.Click();
                }
            }
        }
        public string SelectYourLocationTextBox
        {
            get
            {
                var getField = GetField("input", "id", _playLIstFilterLocationTextBox);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {
                var selectedItem = GetLocationItem(value);
                selectedItem.Click();
             }
        }
        public bool FilterByByTagCheckbox
        {
            get
            {
                var cbvalue = GetCustomCheckbox("label", _playListFilterByTagsCheckbox);
                var itemTag = cbvalue.FindElements(By.ClassName("checkbox")).FirstOrDefault();
                return itemTag.Selected;
            }
            set
            {
                if (FilterByByTagCheckbox != value)
                {
                    var cbItem = GetCustomCheckbox("label", _playListFilterByTagsCheckbox);
                    cbItem.Click();
                }
            }
        }

        public string SelectYouTagTypeSelectBox
        {
            get
            {
                var selectBoxYourTagType = GetSelect("select", _playListFilterTagTypeSelect);
                return selectBoxYourTagType != null ? selectBoxYourTagType.SelectedOption.Text : string.Empty;
            }

            set
            {
                var selectBoxYourTagType = GetSelect("select", _playListFilterTagTypeSelect);
                selectBoxYourTagType.SelectByText(value);
            }
        }
        public string SelectYouTagSelectBox
        {
            get
            {
                var optionValues = GetYourTagSelect().FindElements(By.TagName("option"));
                var theList = string.Join(",", optionValues.Select(a => a.Text.Trim()));
                return theList;
            }

            set
            {
                try
                {
                    var getSelect = GetYourTagSelect();
                    Actions select = new Actions(_driver);
                    select.MoveToElement(getSelect);
                    select.Click();
                    select.SendKeys(value);
                    select.SendKeys(Keys.Tab);
                    select.Build().Perform();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        #endregion

        #region --- Constructor ---

        internal PlayerSettingModal(IWebDriver driver) : base(driver, _playerSettingModal, _modalContainer, _playerSettingModalVisiableClass)
        {
            _driver = driver;
        }
        #endregion

        #region --- Methods ---

        private IWebElement GetYourTagSelect()
        {
            var getModal = GetModal();
            var getTagSection = getModal.FindElements(By.TagName("div")).FirstOrDefault(a => a.GetAttribute("class") != null && a.GetAttribute("class").Equals(_playListFilterTagSection)); // "lg-modal__field filterByTagsTopBox")); 
            var getTagField = getTagSection.FindElements(By.ClassName(_playListFilterFieldsClass));
            var getSelects = getTagField.Where(a => a.FindElement(By.TagName("label")).Text.Equals("Select your Tag(s)")).Select(a => a).FirstOrDefault(b => b.FindElement(By.TagName("select")) != null);
            return getSelects;
        }

        private IWebElement GetLocationItem(string locationToSearch)
        {
            var getField = GetField("input", "id", _playLIstFilterLocationTextBox);
            getField?.SendKeys(locationToSearch);
            getField?.SendKeys(Keys.ArrowDown);
            var selectionDiv = GetField("div", "id", _playListFilterLocationSelectionContainer);
            var selectedItem = selectionDiv.FindElements(By.TagName("li")).FirstOrDefault(a => a.FindElement(By.ClassName("selected")) != null);

            return selectedItem;
        }
        

        public bool ClickOffScreen()
        {
            try
            {
                var cancelButton = GetModalButtons().FirstOrDefault(a => a.GetAttribute("aria-label") != null &&
                                                                         a.GetAttribute("aria-label").Equals("Close", StringComparison.OrdinalIgnoreCase));

                //var cancelButton = GetModalButtons().FirstOrDefault(a => a.GetAttribute("aria-label") != null &&
                //                                                         a.GetAttribute("aria-label").Equals("Close", StringComparison.OrdinalIgnoreCase));

                var cancelSpan = cancelButton.FindElement(By.TagName("span"));
                Actions action = new Actions(_driver);
                // MoveByOffset(-100, -100)
                action.MoveToElement(cancelSpan).MoveByOffset(30, 30).Click().Build().Perform();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

               // SeleniumCommon.ClickOffScreen(this._driver, SeleniumCommon.ByType.Id, _playListSettingModal);
               
            }
            catch (Exception)
            {
                return false;
                //Console.WriteLine(e);
                //throw;
            }
            return true;
        }

        //private IWebElement GetModal()
        //{
        //    var getModalWindow = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.Id, _playListSettingModal);
        //    var getActualModal = getModalWindow.FindElements(By.TagName("div")).FirstOrDefault(a => a.GetAttribute("class").Equals(_playListSettingModalVisiableClass));
        //    return getActualModal;
        //}

        //private IEnumerable<IWebElement> GetModalInputFields(string tagName)
        //{
        //    var getModalDialog = GetModal();
        //    var inputFields = getModalDialog.FindElements(By.TagName(tagName)).ToList();

        //    return inputFields;
        //}
        //private SelectElement GetSelect(string tagName, string fieldName)
        //{
        //    var inputField = GetModalInputFields(tagName).FirstOrDefault(a => a.GetAttribute("id") != null 
        //                                                                    && a.GetAttribute("id")
        //                                                                        .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        //    var actualSelect = new SelectElement(inputField);
        //    return actualSelect;
        //}

        //private IWebElement GetField(string tagName, string attribute, string fieldName)
        //{
        //    return GetModalInputFields(tagName).FirstOrDefault(a => a.GetAttribute(attribute) != null && a.GetAttribute(attribute)
        //                                        .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        //}

        private IWebElement GetCustomCheckbox(string tagName, string spanText)
        {
            var tagItems = GetModalInputFields(tagName);
            var item = tagItems.FirstOrDefault(a => a.Text.Equals(spanText, StringComparison.OrdinalIgnoreCase));
            return item;
        }

        private IWebElement GetChannelSelection()
        {
            var getModalSection = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _modalSection);
            var inputFields = getModalSection.FindElements(By.TagName("select")).ToList();

            return inputFields.FirstOrDefault(a => a.GetAttribute("id") != null && a.GetAttribute("id")
                                .Equals("select-filter", StringComparison.OrdinalIgnoreCase));
        }
        #endregion
    }
}
