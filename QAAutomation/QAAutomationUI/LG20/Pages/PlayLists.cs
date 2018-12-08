using System;
using System.Collections.Generic;
using System.Linq;
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

       // private readonly string _modalContainer = @"lg-modal__container";
       // private readonly string _modalSection = @"lg-modal__section";

        private string _addPlayListButtonId = "";
        private string _searchPlayListField = "";
        private string _playListSortDropDown = "";
        private string _playListDisplayByRowButton = "";
        private string _playListDisplayByGridButton = "";
        private string _playListScrollArea = "";

        // Modal popup - Update to support new filter changes
       // private readonly string _playListName = @"playlist-info-field-name"; // Element: Input By: Name
       // private readonly string _playListDescription = @"playlist-info-description"; // Element: TextArea By: Name

        //private readonly string _playListFilterByClientProgramAndChannel = @"Filtered by client program and channel(s)"; // Search for checkbox with this text.
        //private readonly string _playListFilterSelectClientProgram = @"select-filter-program"; // Element: Select By: id
        //private readonly string _playListFilterSelectFilter = @"select-filter"; // Element: Select By: id

        //private readonly string _playListFilterByLocationAndDevice = @"Filtered by location and device(s)";
        //private readonly string _playLIstFilterTextBoxLocation = @"enter-filter-location"; // Element: TextBox By: id
        //private readonly string _playListFilterLocationSelectDevice = @"enter-filter-location-device"; // Element: Select By: id

        //private readonly string _playListFilterByTags = @"Filtered by tag(s)";
        //private readonly string _playListFilterSelectTag = @"enter-filter-locations"; // Element: Select By: id
        //private readonly string _playListFilterByTagsText = @"Select your Tag(s)"; // Get by label
        //private readonly string _playListFilterByTagsTextSelect = @"lgfe-select"; // element: div By: class

        //private readonly string _playListNumberOfPlayersLabelField = @"Players:";
        //private readonly string _playListDurationLabelField = @"Estimated Duration:";

        private PlayListSettingModal _playListModel = null;

        #endregion

        #region -- Constructors --
        public PlayLists(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
        }

        //private void Test()
        //{
        //    PlayListItem pl =  PlayListItems.First(a => a.Name.Contains("test"));
        //}
        #endregion

        #region -- Methods -- 

        #region -- Overrides --

        public override void Wait(int seconds = 2)
        {
            base.Wait(seconds);
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

        internal SubCards.PlayListSettingModal PlayListModal
        {
            get
            {
                if (_playListModel == null)
                {
                    _playListModel = new PlayListSettingModal(this.Driver);
                    
                }
                return _playListModel;
            }
            set => _playListModel = value;
        } 

        public List<PlayListItem> PlayListItems => GetPlayList(Driver);

        public bool AddPlayList()
        {
            try
            {
                var addButton = AddButtonClick();
                if (addButton)
                {
                    _playListModel = new PlayListSettingModal(this.Driver);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
        #endregion

        public bool AddButtonClick()
        {
            try
            {
                var buttons = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _pmfbContainer).FindElements(By.TagName("button"));

                IWebElement button = buttons.FirstOrDefault(a => a.GetAttribute("title") != null &&
                                                                 a.GetAttribute("title").Equals("Add New Playlist", StringComparison.OrdinalIgnoreCase));
                button.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                //throw;
            }
            return true;
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
