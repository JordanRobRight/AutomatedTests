using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    public class PlayLists : LGBasePage
    {

        #region -- Fields --

        private readonly string _playListinfoModal = @"playlist-info-modal";
        private readonly string _pmfbContainer = @"pmfb-container";

        private readonly string _modalContainer = @"lg-modal__container";
        private readonly string _modalSection = @"lg-modal__section";

        private string _addPlayListButtonId = "";
        private string _searchPlayListField = "";
        private string _playListSortDropDown = "";
        private string _playListDisplayByRowButton = "";
        private string _playListDisplayByGridButton = "";
        private string _playListScrollArea = "";
        #endregion

        #region -- Constructors --
        public PlayLists(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            //this.Driver;
        }

        private void Test()
        {
            PlayListItem pl =  PlayListItems.First(a => a.Name.Contains("test"));
        }
        #endregion

        #region -- Methods -- 

        #region -- Overrides --

        // Example of overriding the WaitFor element method.
        public override void WaitFor(string itemToWaitFor)
        {
            base.WaitFor(itemToWaitFor);
        }
        public override void Perform()
        {
            throw new NotImplementedException();
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region -- Properties --

        internal IWebElement AddPlayListButton => GetAddButton();
        private IEnumerable<IWebElement> ModalButtons => GetModalButtons();
        internal IWebElement ModalCancelButton => GetModalCancelButton();
        internal IWebElement ModalNameEditField => GetNameField();
        internal IWebElement ModalChannelSelection => GetChannelSelection();
        internal IWebElement ModalCreateCustomPlaylistCheckbox => GetCustomCheckbox();
        internal IWebElement ModalSaveButton => GetModalSavebutton();


        private IEnumerable<IWebElement> ModalInputFields => GetModalInputFields();

        public List<PlayListItem> PlayListItems => GetPlayList(Driver);

        #endregion


        private IWebElement GetAddButton()
        {
            var buttons = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _pmfbContainer).FindElements(By.TagName("button"));

            IWebElement button = buttons.FirstOrDefault(a => a.GetAttribute("title") != null &&
                a.GetAttribute("title").Equals("Add New Playlist", StringComparison.OrdinalIgnoreCase));

            return button;
        }

        private IEnumerable<IWebElement> GetModalButtons()
        {
            var modalContainer = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _modalContainer);
            var modalContainerButtons = modalContainer.FindElements(By.TagName("button")).ToList();
            return modalContainerButtons;
        }

        private IWebElement GetModalCancelButton()
        {
           // if (ModalButtons == null) GetModalButtons();

            var cancelButton = ModalButtons.FirstOrDefault(a => a.GetAttribute("aria-label") != null &&
                a.GetAttribute("aria-label").Equals("Close", StringComparison.OrdinalIgnoreCase));

            return cancelButton;
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
            var GetModalContainer = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _modalContainer);
            var GetModalSection = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _modalSection);
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
            var GetModalContainer = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _modalContainer);
            var GetModalSection = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _modalSection);
            var inputFields = GetModalSection.FindElements(By.TagName("select")).ToList();

            return inputFields.FirstOrDefault(a => a.GetAttribute("id") != null &&
                a.GetAttribute("id")
                    .Equals("select-filter", StringComparison.OrdinalIgnoreCase));
        }

        private List<PlayListItem> GetPlayList(IWebDriver driver)
        {
            List<PlayListItem> pls = new List<PlayListItem>();

            try
            {
                // THis is just an example using the menu are the right so the locators need to change to support Playlist screen

                // Get the main container for the menu.
                IWebElement p = driver.FindElement(By.Id("interaction-nav-bar-container"));
                // Get the menu that is for the various pages like playlist,assets and such.

                IWebElement p1 = p.FindElement(By.ClassName("inbc-menu-wrapper"));

                // Find all elements that have an a tagname.
                IEnumerable<IWebElement> p2 = p1.FindElements(By.TagName("a")).ToList();

                foreach (IWebElement we in p2)
                {
                    PlayListItem pli = new PlayListItem(driver) { Name = we.Text };
                    pls.Add(pli);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return pls;
        }
    }

}
