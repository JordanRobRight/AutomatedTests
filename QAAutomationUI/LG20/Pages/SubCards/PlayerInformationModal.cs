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
    internal class PlayerInformationModal : ModalBasePage
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



        private readonly IWebDriver _driver;
        #endregion

        #region  --- Private Properties ---

        //internal IEnumerable<IWebElement> ModalButtons => GetModalButtons();
        //internal IWebElement ModalChannelSelection => GetChannelSelection();
        private static string playerInfoModal = @"player-info-form";//ID
        #endregion

      

        #region --- Constructor ---

        internal PlayerInformationModal(IWebDriver driver) : base(driver, _playerSettingModal, _modalContainer, _playerSettingModalVisiableClass)
        {
            _driver = driver;
            FormID = playerInfoModal;
        }
        #endregion

        #region --- public Methods ---

       
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
                var getField = GetField("input", "name", _playerDescription);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "name", _playerDescription);
                getField.SendKeysOrClear(value);
            }
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

        public List<string> GetDescriptionPlaceHolder
        {
            get
            {     
                var descriptionTextbox = GetField("input", "id", "PlayerInformationModalService-input-description");
                var placeHolderText = descriptionTextbox.GetAttribute("placeholder");
                List<string> list = new List<string>();
                list.Add(placeHolderText.ToString());
                return list;
            }
        }

        public List<string> GetLockedFields
        {
            get
            {
                var LockedProductField = GetField("input", "id", "PlayerInformationModalService-input-product");
                var placeHolderText1 = LockedProductField.GetAttribute("disabled");

                var lockedSubscriptionfield = GetField("input", "id", "PlayerInformationModalService-input-subscription");
                var placeHolderText2 = lockedSubscriptionfield.GetAttribute("disabled");

                var lockedLicensekey = GetField("input", "id", "PlayerInformationModalService-input-license-key");
                var placeHolderText3 = lockedLicensekey.GetAttribute("disabled");

                var lockedExpirationDatefield = GetField("input", "id", "PlayerInformationModalService-input-expiration-date");
                var placeHolderText4 = lockedExpirationDatefield.GetAttribute("disabled");
                List<string> list = new List<string>
                {
                    placeHolderText1.ToString(),
                    placeHolderText2.ToString(),
                    placeHolderText3.ToString(),
                    placeHolderText4.ToString()
                };
                return list;
            }
        }


        #endregion
    }
}
