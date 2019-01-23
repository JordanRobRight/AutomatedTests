using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class PlayLists : LGBasePage
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
        private readonly string _playListDescription = @"//label[text()='Playlist Description']//following-sibling::textarea[@id='form-textarea']"; // xpath to find unique player description emelent
        private readonly string _playerListNameCss = @"lgfe-cm-information";// first element in playlist
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

      

        #region -- Overrides --

        // This method can be removed because the base class has it. 
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

        
        # endregion
        

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

        #region -- Public Methods --
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

        // I not sure why this method is here and I don't think it is needed.
        // If you are going to clear out the description for the playlist settings modal, you should just be able to set null to the description field and that should clear it out.
        // 
        public void ClearDescriptionTextbox()
        {
            Wait(2);
            IWebElement clearDescription = Driver.FindElement(By.XPath(_playListDescription));
            clearDescription.Clear();
           
        }

        // RK - Comments - 1/16/19
        // A suggestion. Convert this method to build a return List<string> of playlist names and in the CreatePlaylists in UnitTest1.cs do the verification there. 
        // I see that this method will expand out more to include the ability to click on the individual items in the playlist line item. Actually the GetPlayList private method I believe is doing something similar
        // On the .Contains method call. This should be changed to be Equal with StringComparison.OrdinalIgnoreCase filter on it because using .Contains will include items that might not want.
        // For instance,  the name of the playlist is 'Test' that you just created but you have Test1, Test2 and Test3 in the playlist section. The result would be true because it matched the word 
        // Test which is what all three items contain. If you change it to equals, then the result would be false because there isn't an playlist of Test.
        // One more item - the test method should do the asserts not in the POM. There could be an instance
        // depending on user roles that the message might be slightly different which is where we do the test in the Test method. So returning a list of strings, the test method will determine what to validate.
        public string VerifyCreatedPlaylist(string createdPlaylistName)
        {
            Wait(2);

            IList<IWebElement> List = Driver.FindElements(By.ClassName(_playerListNameCss));
            foreach (IWebElement playList_List1 in List)
            {
                string toCompare = playList_List1.Text;
                if (toCompare.Contains(createdPlaylistName))
                {
                    // Assert.IsTrue(toCompare.Contains(createdPlaylistName),"created playerlist not presnet in container");
                    //toCompare.Should().Be(createdPlaylistName);
                    break;

                }
                //Assert.IsTrue(toCompare.Contains(createdPlaylistName), "created playerlist not presnet in container");

            }

            return createdPlaylistName;
        }
        // RK - 1/22/19 - This is an example of what I was talking about in my comments in the above method. Please take a look at this example and let me know your opinion comments 
        public IEnumerable<string> GetPlayLists 
        {
            get
            {
                IList<IWebElement> playListItems = Driver.FindElements(By.ClassName(_playerListNameCss));
                var items = playListItems.Select(a => a.Text).ToList();
                return items;
            }
        }

        #endregion

        #region -- Private Methods --
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

        #endregion
    }

}
