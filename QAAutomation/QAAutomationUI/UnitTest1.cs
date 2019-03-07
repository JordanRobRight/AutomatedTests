using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using QA.Automation.Common;
using QA.Automation.UITests.LG20.Pages;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;
using FluentAssertions;
using FluentAssertions.Collections;
using QA.Automation.Common.Models;
using QA.Automation.UITests.LG20.Pages.SubCards.Location;
using QA.Automation.UITests.LG20.Pages.SubCards.Player;

[assembly: LevelOfParallelism(2)]

namespace QA.Automation.UITests
{
    //TODO: Need a better way to pass in these items. 
    [TestFixture("chrome", "63", "Windows 10", "", "")]
    [Parallelizable(ParallelScope.Children)]
    public class UnitTest1 
    {
        private ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();

        private readonly string _browser;
        private readonly string _version;
        private readonly string _os;
        private readonly string _deviceName;
        private readonly string _deviceOrientation;
        private readonly TestSystemConfiguration _configuration = null;
        private readonly TestDataConfiguration _testDataConfiguration;

        //private const string un = @"DCIArtform";
        //private const string ak = @"a4277bd1-3492-4562-99bc-53dd349c52e1";

        public UnitTest1(string browser, string version, string os, string deviceName, string deviceOrientation)
        {
            this._browser = browser;
            this._version = version;
            this._os = os;
            this._deviceName = deviceName;
            this._deviceOrientation = deviceOrientation;
            _configuration = ConfigurationSettings.GetSettingsConfiguration<TestSystemConfiguration>();

            //var testDataFromFile = ConfigurationSettings.GetSettingsConfiguration<TestData>("TestData.json");

            _testDataConfiguration = new TestDataConfiguration(_configuration.TestDataFolder, _configuration.Environment);
        }


        [SetUp]
        public void Init()
        {
            _driver.Value = new ChromeBrowser(_browser, _version, _os, _deviceName, _deviceOrientation, _configuration)
                                .CreateBrowser(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.MethodName);
        }

        #region --- SmokeTest Tests ---
        [TestCase]
        [Category("All")]
        [Category("SmokeTests")]
        public void LiveGuide20()
        {
            Login();

            Assert.AreEqual($"https://portal.{_configuration.Environment.ToString()}.dcimliveguide.com/#playlists".ToLower(), _driver.Value.Url.Trim().ToLower());
        }

        [TestCase]
        [Category("SmokeTests")]
        [Description("Login")]
        public void Login()
        {
            LoginTest(_configuration.LGUser, _configuration.LGPassword);
            ClientMenu cm = new ClientMenu(_driver.Value, _configuration);
            if (!cm.CurrentClient.Equals("GM", StringComparison.OrdinalIgnoreCase))
            {
                cm.SelectClient("GM");
                cm.Wait(3);
            }
        }

        //[TestCase]
        [Category("SmokeTests")]
        [Description("Login")]
        public void LoginTest(string user, string password)
        {
            Login login = new Login(_driver.Value, _configuration);
            login.GoToUrl();
            login.UserName = user; // _configuration.LGUser;
            login.Password = password; // _configuration.LGPassword;
            login.Wait(2);
            login.ClickSignIn("Sign In");
            login.WaitForElement();

            login.GetCurrentUrl.Should().NotContain(".dcimliveguide.com/login");
        }

        // RK - 2/26/19 - Commented out this test because a newer test replaces this one. TC #1456

        //[TestCase]//Test case 1994
        //[Category("SmokeTests")]
        //[Description("Test case 1994")]
        //public void Logout()
        //{
        //    Login();

        //    Logout logout = new Logout(_driver.Value, ConfigurationSettings.GetSettingsConfiguration<TestConfiguration>());
        //    logout.Wait(2);
        //    //SelectItemFromCilentMenu(_driver.Value, "logout");

        //    logout.CancelButtonClick();
        //    logout.Wait(1);
        //    logout.LogoutModal.IsModalDisplay.Should().BeTrue();
        //    //SelectItemFromCilentMenu(_driver.Value, "logout");

        //    logout.LogoutAcceptButtonClick();
        //}

        [TestCase]//test case 1987
        [Category("SmokeTests")]
        [Category("All")]
        [Description("Test case 1987")]
        public void PlaylistDisplayAsGrid()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 1 login
            Login();
            //step 2 hover over grid icon
            IWebElement gridIcon = _driver.Value.FindElement(By.CssSelector(BaseStrings.gridIconButtonCssSelector));
            Actions action = new Actions(_driver.Value);
            action.MoveToElement(gridIcon).Perform();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 3 select grid icon
            gridIcon.Click();
            //step 4 use scroll bar to navigate through all rolls

            //step 5 select any of the playlist
            IWebElement playlistSearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistSearchInput.SendKeys("Automated");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement openButton = _driver.Value.FindElement(By.XPath(BaseStrings.gridOpenButtonCssSelector));
            openButton.Click();
            //step 6 select playlists from main menu
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            //step 7 select grid icon
            IWebElement gridIcon1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.gridIconButtonCssSelector));
            gridIcon1.Click();
            //step 8 confirm that all the data that appears for a grid's playlist, is the same data that appears when a row is selected (3 horizontal lines) select row icon
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 9 logout
            LogOutWithoutLogin();
        }

       // [TestCase]
       // [Category("Debugonly")]
       //  [Ignore("This test case is for debugging only")]        
        public void CreatePlaylistTest()
        {
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            //step 1 login
            Login();//remove to do full test

            PlayLists pls = new PlayLists(_driver.Value, _configuration);

            var status = pls.AddPlayList();
            if (!status)
            {
                //exception add button wasn't clicked. 
            }
            pls.Wait();

            //pls.PlayListModal.ClickOffScreen();

            //SeleniumCommon.ClickOffScreen(_driver.Value, string.Empty);

            pls.PlayListModal.ModalCancelButtonClick();

            //pls.AddPlayListButton.Click();
            pls.Wait();

            //pls.PlayListModal.ModalCancelButton();

            //PlayListSettingModal p = new PlayListSettingModal(_driver.Value);
            // p.PlayListDescriptionTextField = "Test";

            pls.Wait();
            var textValue = pls.PlayListModal.PlayListNameTextField;
            pls.PlayListModal.PlayListNameTextField = "Test name";

            pls.PlayListModal.PlayListDescriptionTextField = "Test description";
            var ch1 = pls.PlayListModal.FilterByClientProgramAndChannelCheckbox;
            
            pls.PlayListModal.FilterByClientProgramAndChannelCheckbox = true;
            var ch2 = pls.PlayListModal.FilterByClientProgramAndChannelCheckbox;

            pls.PlayListModal.SelectClientProgramSelectBox = "Guest TV";
            pls.PlayListModal.SelectYourChannelSelectBox = "Chevy-Buick-GMC TV";

            var program = pls.PlayListModal.SelectClientProgramSelectBox;

            // pls.PlayListModal.FilterByLocationAndDeviceCheckbox = true;

            //pls.PlayListModal.SelectYourLocationTextBox = "KUDICK CHEVROLET BUICK";
            //  pls.PlayListModal.SelectYourDeviceSelectBox = "Player: LG-QAROB";

            //  var location1 = pls.PlayListModal.SelectYourLocationTextBox;
            //  var location2 = pls.PlayListModal.SelectYourDeviceSelectBox;


            pls.PlayListModal.FilterByByTagCheckbox = true;
            pls.Wait();

            pls.PlayListModal.SelectYouTagTypeSelectBox = "Player";
            var theList = pls.PlayListModal.SelectYouTagSelectBox;
            pls.Wait();
            pls.PlayListModal.SelectYouTagSelectBox = "RobTest";

            var type1 = pls.PlayListModal.SelectYouTagTypeSelectBox;
            var type2 = pls.PlayListModal.SelectYouTagSelectBox;
            pls.Wait();
            pls.PlayListModal.ClickModalSaveButton();
            pls.Wait();
        }

        [TestCase] //Test case #580
        [Category("All")]
        [Category("SmokeTests")]
        [Description("Test case #580")]
        public void CreatePlaylists()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(9));
            //step 1 login
            Login();//remove to do full test

            PlayLists pls = new PlayLists(_driver.Value, _configuration);

            //step 2
            pls.AddButtonClick();

            pls.Wait(3);
            //Step 4 Select 'X' to close window

            pls.PlayListModal.ModalCancelButtonClick();
            pls.Wait();
            pls.PlayListModal.ClickConfirmModalContinueButton();
            pls.Wait(3);
            pls.PlayListModal.IsModalDisplayed.Should().BeFalse("Msg1:Modal should be closed");

            //Step 5 Select '+' to add new playlist

            pls.AddButtonClick();

            pls.Wait();


            pls.PlayListModal.ClickOffScreen();

            pls.Wait();
            pls.PlayListModal.ClickConfirmModalContinueButton();
            pls.PlayListModal.IsModalDisplayed.Should().BeFalse("Msg2:Modal should be closed");// if IsModalDisplay is false then it means Modal was closed else display message
                                                                                          //Step 7 Select '+' to add new playlist

            pls.AddButtonClick();

            pls.Wait();

            //Step 8 Select Create a Custom Playlist - Filtered Check box

            //pls.ModalCreateCustomPlaylistCheckbox.Click();

            //pls.ModalCreateCustomPlaylistCheckbox.SendKeys(Keys.Space);

            //Step 9 Select Save

            pls.PlayListModal.ClickModalSaveButton();
            pls.Wait();

            // The name is still not filled in
            // TODO: Need to update - Works locally while debugging but not when running without debugging. 
            //pls.PlayListModal.IsModalDisplayed.Should().BeTrue("Msg3:Modal should still be open");
            //pls.Wait();

            pls.PlayListModal.ModalCancelButtonClick();
            pls.Wait();
            pls.PlayListModal.ClickConfirmModalContinueButton();
            pls.PlayListModal.IsModalDisplayed.Should().BeFalse("Msg4:Modal should be closed");

            //Step 10 Select Ok
            pls.AddButtonClick();
            pls.Wait(3);
            //step 11: Remove the Playlist Description 
            pls.PlayListModal.PlayListDescriptionTextField = "Automated Playlist Test Desc" + DateTime.Now.ToString();
            pls.PlayListModal.ClickModalSaveButton();
            pls.Wait(3);
            pls.PlayListModal.IsModalDisplayed.Should().BeTrue("Msg5:Modal should be closed");
            pls.PlayListModal.PlayListDescriptionTextField = null;

            pls.PlayListModal.PlayListDescriptionTextField = null;

            //Step 12:  Enter a playlist name
            string playlistName = "Automated Playlist Test " + DateTime.Now.ToString();

            pls.PlayListModal.PlayListNameTextField = playlistName;

            pls.PlayListModal.PlayListDescriptionTextField = null;

            //Step 12 Select save

            pls.PlayListModal.ClickModalSaveButton();
            pls.Wait();

            pls.PlayListModal.IsModalDisplayed.Should().BeFalse("Modal should be closed");
            pls.GetPlayLists.Any(a => a.Equals(playlistName, StringComparison.OrdinalIgnoreCase)).Should().BeFalse("New Playlist wasn't created. ");
            //pls.VerifyCreatedPlaylist(playlistName).Should().Contain("Automated Playlist Test");
            if (pls.PlayListModal.IsModalDisplayed)
            {
                //Step 13 Enter location name (this is the first input field after the Custom checkbox)

                pls.PlayListModal.PlayListDescriptionTextField = "System Test Location Two Buick";
                pls.Wait();
                pls.PlayListModal.ClickModalSaveButton();
                pls.PlayListModal.IsModalDisplayed.Should().BeFalse("Msg6:Modal should be closed");

            }

            //Step 14 Select save

            //Step 15 Select device drop down

            //Step 16 Select all devices
            //Step 17 Select save
            //Step 18 New playlist has been created

            //Step 19 Select '+' to add a new playlist

            //TODO: Need to check with Margie on this test. 

            //Step 20 Logout
            LogOutWithoutLogin();

            //TODO: Assert calling API.
            //string apiPlayList = APITests.LG20.SmokeTest.GetPlayListByName("newPlaylist", "username", "password", _configuration.Environment);
        }


        [TestCase]// test Case #583
        [Category("All")]
        [Category("SmokeTests")]
        [Description("Test Case #583")]
        public void CreateAPlaylistHappyPath()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(9));
            //Step 1 login 
            Login();
            PlayLists pls = new PlayLists(_driver.Value, _configuration);

            //Step 2 select '+' to make a new playlist
            WaitForMaskModal();

            pls.AddButtonClick();
            pls.Wait();
            //Step 3 enter playlist name
            string playlistName = "Automated Playlist Test " + DateTime.Now.ToString();
            pls.PlayListModal.PlayListNameTextField = playlistName;

            //Step 4 Select a channel

            /* pls.PlayListModal.FilterByClientProgramAndChannelCheckbox = true;
             pls.Wait(1);
             pls.PlayListModal.SelectClientProgramSelectBox = "Guest TV";
             pls.Wait(1);
             pls.PlayListModal.SelectYourChannelSelectBox = "Chevy TV";
             pls.Wait();*/

            //Step 5  select save

            pls.PlayListModal.ClickModalSaveButton();

            pls.Wait(3);
            pls.PlayListModal.IsModalDisplayed.Should().BeFalse("Modal should be closed");
            pls.Wait(2);
            //Step 6 select done---does not exist currently (09/26/2018)
            //Step 7 new playlist has been created
            pls.GetPlayLists.Any(a => a.Equals(playlistName, StringComparison.OrdinalIgnoreCase)).Should().BeFalse("New Playlist wasn't created. ");
            // pls.VerifyCreatedPlaylist(playlistName);

            //Step 8 logout
            LogOutWithoutLogin();
        }

        [TestCase]// test case 1985
        [Category("SmokeTests")]
        [Category("All")]
        [Description("Test case 1985")]
        public void AbleToEditPlaylist()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 1 loging
            Login();
            //SelectAutomatedPlaylist
            SideBar sb = new SideBar(_driver.Value, _configuration);
            //step 2 select playlists that contains no widget and select able to edit button from main menu
            IWebElement playlistSearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            playlistSearchInput.SendKeys("Automated");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
          //  IWebElement durationSection = _driver.Value.FindElement(By.CssSelector(BaseStrings.durationSectionCssSelector));
        //    string duration = durationSection.Text;
            WaitForMaskModal();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Assert.IsTrue(duration.Contains("<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>"));
        //    if (duration.Contains("<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>"))
        //    {
                IWebElement playlistEditButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.editButtonCssSelector));
                playlistEditButton1.Click();
                PlayListSettingModal plsm = new PlayListSettingModal(_driver.Value);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                plsm.ClickModalSaveButton();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                //TODO: Get element playlist-no-content and get the text from it to compare to.
                //IWebElement saveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
                //saveButton1.Click();
        //    }
            //step 3 select playlists from main menu
            //IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            sb.SelectMenu("Playlists");
            sb.Wait(2);
            //playlistsSideBarMenuButton.Click();
            //WaitForMaskModal();
            //step 4 select playlists that contains widgets and select able to edit button 
         //   if (!duration.Contains("<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>"))
        //    {
               
                IWebElement playlistEditButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.editButtonCssSelector));
                playlistEditButton2.Click();
                
                //TODO: Get the lement lgfe-card-matrix js-drag-drop-playlist lgfe-card-matrix--layout-row and see if there is more than one
                PlayListSettingModal plsm2 = new PlayListSettingModal(_driver.Value);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                plsm.ClickModalSaveButton();
               // System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                //IWebElement saveButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
                //saveButton2.Click();
      //      }
            //step 5 select playlists from main menu
            WaitForMaskModal();
           
            //IWebElement playlistsSideBarMenuButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            sb.SelectMenu("Playlists");
            sb.Wait(2);
           
            //playlistsSideBarMenuButton1.Click();
            //WaitForMaskModal();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //TODO:Checking that you get a list of playlists.
            //Step 6 logout
            LogOutWithoutLogin();
        }


        [TestCase]//Test case #1984
        [Category("SmokeTests")]
        [Category("All")]
        [Description("Test case #1984")]
        public void OpenPlaylist()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Step 1
            Login();
            //Step 2 Select any playlist that contains no widgets and select the open button
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            IWebElement playlistSearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistSearchInput.SendKeys("Automated");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement durationSection = _driver.Value.FindElement(By.CssSelector(BaseStrings.durationSectionCssSelector));
            string duration = durationSection.Text;
            WaitForMaskModal();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Assert.IsTrue(duration.Contains("<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>"));
            if (duration.Contains("<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>"))
            {
                IWebElement playlistOpenButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));
                playlistOpenButton1.Click();
            }
            IWebElement playlistOpenButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));
            playlistOpenButton.Click();
            //Step 3 Selectplaylist from main menu
            IWebElement playlistsSideBarMenuButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton1.Click();
            //Step 4 select any playlist that contains widgets and select the open button
            if (duration != "<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>")
            {
                IWebElement playlistOpenButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));
                playlistOpenButton2.Click();
            }
            //Step 5 select  playlists from main menu
            IWebElement playlistsSideBarMenuButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton2.Click();
            LogOutWithoutLogin();

        }

        #endregion

        // #region --- All Tests ---
        [TestCase]
        [Category("All")]
        [Description("Test case 586 edit playlist")]
        public void EditPlaylist()
        {
            //Step 1
            Login();
            //Step 2 select edit icon
            IWebElement playlistEditButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistEditButtonCssSelector));
            playlistEditButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 spell check all content
            //step 4 select playlist name text box and edit name
            IWebElement playlistTitleInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistTitleInputCssSelector));
            playlistTitleInput.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistTitleInput.SendKeys("Edited Automated Test Playlist");
            //Step 5 select save
            IWebElement playlistSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistEditSaveButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistSaveButton.Click();
            //Step 6 Select edit for any playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistEditButton.Click();
            //step 7 select playlist description text box and edit description
            IWebElement playlistDescriptionTextArea = _driver.Value.FindElement(By.CssSelector("#form-textarea"));
            playlistDescriptionTextArea.SendKeys("this is a test");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 8 select save
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistSaveButton.Click();
            //step 9 select edit icon for any playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistEditButton.Click();
            //step 10 select add tag text box and enter any tag name
            IWebElement playlistEditTagsSection = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistTagSectionCssSelector));
            playlistEditTagsSection.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistEditTagsSection.SendKeys("Edited Tags Section");
            //step 11 select add button
            IWebElement playlistTagAddButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistTagAddButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistTagAddButton.Click();
            //step 12 select save
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistSaveButton.Click();
            //TODO: Need to valdate that the Tag was saved and the change doesn't display in Playlist
            //step 13 Select edit icon
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //WaitForMaskModal();
            playlistEditButton.Click();
            //step 14 Delete any tag
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            IWebElement tagDeleteButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.tagDeleteButtonCssSelector));
            playlistEditTagsSection.Clear();
            tagDeleteButton.Click();
            //step 15 select save
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistSaveButton.Click();
            //step 16 select edit icon for same playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            //WaitForMaskModal();
            playlistEditButton.Click();
            //step 17 close 'x' edit window
            IWebElement playlistEditCloseButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistEditCloseButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistEditCloseButton.Click();
            //step 18 select edit icon for same playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistEditButton.Click();
            //Step 19 click outside window
            //OffClick();
            SeleniumCommon.ClickOffScreen(_driver.Value, SeleniumCommon.ByType.Css, BaseStrings.playlistSideBarMenuCssSelector);
            //step 20 logout
            LogOutWithoutLogin();
        }


        //public void OffClick()
        //{
        //    IWebElement offClick = _driver.Value.FindElement(By.CssSelector(BaseStrings.offClickCssSelector));
        //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
        //    var element = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
        //    new Actions(_driver.Value).MoveToElement(element).Click().Perform();
        //}

        /*
        [TestCase] //Test Case 1982
        // RK - 3/5/19 - Favorite icon was removed from the playlist section. Turning off the test.
        
        [Category("All")]
        [Description("Test Case 1982")]
        public void FavoritePlaylist()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            //Step 1 login
            Login();
            IWebElement playlistSearch = _driver.Value.FindElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Step 2 Hover over a Favorite icon (heart) thats in black
            IWebElement favoritIconHeart = _driver.Value.FindElement(By.CssSelector(BaseStrings.favoriteIconHeartCssSelector));
            Actions action = new Actions(_driver.Value);
            action.MoveToElement(favoritIconHeart).Perform();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Step 3 Hover over a Favorite icon (heart) thats in red
            IWebElement favoritIconHeart1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.favoriteIconHeartCssSelector));
            Actions action1 = new Actions(_driver.Value);
            action1.MoveToElement(favoritIconHeart1).Perform();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 4 Search for any existing Playlist and select favorite icon (heart)            
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //IWebElement favoritIconHeart = _driver.Value.FindElement(By.CssSelector(BaseStrings.favoriteIconHeartCssSelector));
            IWebElement favoritIconHeart2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.favoriteIconHeartCssSelector));
            favoritIconHeart2.Click();
            //Step 5 Refresh screen
            _driver.Value.Navigate().Refresh();
            //Step 6 Select the favorite icon again from the test step 2 above
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement favoritIconHeart3 = _driver.Value.FindElement(By.CssSelector(BaseStrings.favoriteIconHeartCssSelector));
            favoritIconHeart3.Click();
            //Step 7 refresh page
            _driver.Value.Navigate().Refresh();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Step 8 add new playlist, select '+'
            PlayLists pls = new PlayLists(_driver.Value, _configuration);
            //pls.AddButtonClick();
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            
            WaitForMaskModal();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 9 enter all required field values and select save
            
            IWebElement addPlaylistButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            pls.AddButtonClick();
            //addPlaylistButton.Click();
            string playlistName = "Automated Playlist Test " + DateTime.Now.ToString();
            //IWebElement playlistAddForm = _driver.Value.FindElement(By.Id("form-name"));
            //playlistAddForm.SendKeys(playlistName);
            pls.PlayListModal.PlayListNameTextField = playlistName;
            pls.Wait();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //string categoryID = "//*[@id='playlist-info-form']/div[1]/div[2]/div//*[@id='select-filter']";
            pls.PlayListModal.FilterByClientProgramAndChannelCheckbox = true;
            pls.Wait(1);
            pls.PlayListModal.SelectClientProgramSelectBox = "Guest TV";
            pls.Wait(1);
            pls.PlayListModal.SelectYourChannelSelectBox = "Chevy TV";
            pls.Wait(2);
            //IWebElement selectCategory = _driver.Value.FindElement(By.XPath(categoryID));
            //selectCategory.SendKeys("chevy" + Keys.Enter);
            // System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            // IWebElement saveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            // saveButton.Click();
            pls.PlayListModal.ClickModalSaveButton();
            //Step 10 Select favorite icon for new playlist created in test step 7 
            IWebElement favoritIconHeart4 = _driver.Value.FindElement(By.CssSelector(BaseStrings.favoriteIconHeartCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            favoritIconHeart4.Click();
            //step 11 refresh page
            _driver.Value.Navigate().Refresh();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 12 select favorite icon again for new playlist in test step 7
            //step 13 refresh screen
            //Step 14 logout
            LogOutWithoutLogin();
        }
        */


        public void SelectAutomatedPlaylist()
        {
            string playlistName = "Automated Playlist Test";
            IWebElement playlistAddForm = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistAddForm.SendKeys(playlistName);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement playlistSelection = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));
            playlistSelection.Click();
        }

        
        //[TestCase]
        //public void EditWeatherWidget()
        //{
        //    Login();
        //    CreatePlaylists();
        //    IWebElement playlistOpenButton = _driver.Value.FindElement(By.CssSelector(Base.playlistOpenButtonCSSSelector));
        //    playlistOpenButton.Click();
        //    AddWeatherWidget();

        //    //IWebElement weatherEditButton = _driver.Value.FindElement(By.TagName("button"));

        //    //weatherEditButton.Click();

        //    //IWebElement weatherEditButton1 = _driver.Value.FindElement(By.TagName("<button type="button" data-button-type="edit" title="Edit" class="lgfe - cm - utility - button button - unstyled js - playlist - edit"><span aria-hidden="true" class="[fa fa - pencil]"></span> <span class="visually - hidden">Edit </span></button>"));

        //    IWebElement weatherEditButton = _driver.Value.FindElement(By.ClassName("js-playlist-edit"));

        //    //Assert.IsTrue(bodyTag.Text.Contains("weather"));

        //    weatherEditButton.Click();

        //    //if (bodyTag.GetText().contains("weather")
        //    //   {
        //    //    weatherEditButton.Click(); 
        //    //   }
        //}

        //[TestCase] //Testcase 781
        public void AddFinanceWidget()
        {
            //Step 1 
            Login();
            //Step 2 
            SelectAutomatedPlaylist();
            //Step 3 Select Add Finance Widget 
            IWebElement financeWidget = _driver.Value.FindElement(By.CssSelector(BaseStrings.financeWidgetCSSSelector));
            WaitForMaskModal();
            financeWidget.Click();
            //Step 4 Spell check all content (fields/values/buttons), including placeholder text   
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 5 Confirm text box displays with pre-filled time Duration (not editable)
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 6 Finance pre-filled description displays on lower section of window
            IWebElement financeWidgetDescription = _driver.Value.FindElement(By.CssSelector(BaseStrings.financeWidgetDescriptionCssSelector));
            Assert.IsTrue(financeWidgetDescription.Text.Contains("Data includes the NASDAQ, NYSE, S&P 500, TSX, Dow 30, top gainers and losers, and companies trading on those exchanges."));
            //Step 7 Select Save
            IWebElement saveFinanceButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveFinanceButtonCSSSelector));
            WaitForMaskModal();
            saveFinanceButton.Click();
            //Step 8 Select Add Finance Widget
            IWebElement financeWidget1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.financeWidgetCSSSelector));
            WaitForMaskModal();
            financeWidget1.Click();
            //Step 9 Select Brand dropdown box
            IWebElement financeWidgeBrandDropDown = _driver.Value.FindElement(By.XPath(BaseStrings.financeWidgetDropDown));

            //Step 10 Select Buick Brand 
            IWebElement brandDropdown = _driver.Value.FindElement(By.XPath(BaseStrings.weatherWidgetDropDown));
            var selectBrandDropDown = new SelectElement(brandDropdown);
            selectBrandDropDown.SelectByValue("buick");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 11 Select Save
            IWebElement saveFinanceButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveFinanceButtonCSSSelector));
            WaitForMaskModal();
            saveFinanceButton1.Click();
            //Step 12 Create all brands

            //Step 13 Select Save from Playlist screen
            IWebElement playlistSave2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSaveCSSSelector));
            WaitForMaskModal();
            playlistSave2.Click();
            //Step 14 log out
            LogOutWithoutLogin();

            //TODO: Assert that the saved worked.
        }


        [TestCase]//test case 800
        [Category("All")]
        [Description("Test case 800")]
        //[Category("SmokeTests")]
        public void AddTrafficWidget()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            //step 1 sign in 
            Login();
            //step 2 select an existing playlist
            SelectAutomatedPlaylist();
            PlayList pl = new PlayList(_driver.Value, _configuration);// TestConfiguration.GetTestConfiguration());

            pl.Wait();


            IWebElement traffic = pl.PlayListWidets.FirstOrDefault(a => a.Text.ToLower().Contains("traffic"));
            //WidgetListItem tr = pl.Widgets.FirstOrDefault(a => a.Name.ToLower().Contains("traffic"));
            Assert.IsNotNull(traffic);

            traffic.Click();

            pl.Wait(2);

            //step 3 select add traffic widget
            //IWebElement trafficWidget = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetCssSelector));
            //WaitForMaskModal();
            //trafficWidget.Click();

            LG20.Pages.SubCards.Widgets.Traffic tr = new LG20.Pages.SubCards.Widgets.Traffic(_driver.Value);

            //step 4 spell check all content 
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 5 confirm text box displays on lower section of window
            //IWebElement trafficWidgetDescription = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetDescriptionCssSelector));
            //Assert.IsTrue(trafficWidgetDescription.Text.Contains("Current conditions of local routes and maps."));
            Assert.IsTrue(tr.TrafficDescription.Contains("Current conditions of local routes and maps."));
            

            //step 6 weather pre-filled displays on lower section of window
            //step 7 do not enter zip code confirm placeholder text displays
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 8 select save

            tr.ClickModalSaveButton();
            //IWebElement trafficWidgetSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetSaveButtonCssSelector));
            //WaitForMaskModal();
            //trafficWidgetSaveButton.Click();
            pl.Wait(2);

            //TODO: Check to see if the widget was saved.
            //step 9 select add traffic widget
            traffic.Click();
            //IWebElement trafficWidget1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetCssSelector));
            //WaitForMaskModal();
            //trafficWidget1.Click();
            pl.Wait(2);

            //step 10 select brand drop down box
            //IWebElement brandDropdown = _driver.Value.FindElement(By.XPath(BaseStrings.trafficWidgetDropDown));
            //var selectBrandDropDown = new SelectElement(brandDropdown);
            //selectBrandDropDown.SelectByValue("buick");
            tr = new LG20.Pages.SubCards.Widgets.Traffic(_driver.Value);
            tr.BrandSelectBox = "Buick";

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //step 11 select buick brand
            //step 12 select save
            // IWebElement trafficWidgetSaveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetSaveButtonCssSelector));
            tr.ClickModalSaveButton();
            WaitForMaskModal();
            //trafficWidgetSaveButton1.Click();
            //TODO: Check to see if the widget was saved.
            //step 13 select add traffic widget
            //IWebElement trafficWidget2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetCssSelector));
            //WaitForMaskModal();
            //trafficWidget2.Click();
            traffic.Click();
            //step 14 enter an invalid zip code
            tr = new LG20.Pages.SubCards.Widgets.Traffic(_driver.Value);
            pl.Wait(2);
            //IWebElement trafficZipInput = _driver.Value.FindElement(By.Id("traffic-widget-zip"));
            //trafficZipInput.SendKeys("53");
            tr.TrafficZipCodeTextBox = "53";
            pl.Wait(2);

            tr.ClickModalSaveButton();
            pl.Wait(2);
            Assert.IsTrue(tr.TrafficZipCodeError);
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 15 enter valid zip code
            tr.ClearTrafficZipCodeTextbox();
            //trafficZipInput.Clear();
            pl.Wait(2);
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            // trafficZipInput.SendKeys("53142");

            tr.TrafficZipCodeTextBox = "53142";
            //step 16 select save
            IWebElement trafficWidgetSaveButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetSaveButtonCssSelector));
            tr.ClickModalSaveButton();
            //WaitForMaskModal();
            //trafficWidgetSaveButton2.Click();
            //step 17 create all brands
            //step 18 select save from playlist screen
            //IWebElement playlistSave = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSaveCSSSelector));
            // WaitForMaskModal();
            //playlistSave.Click();
            pl.Wait(2);
            pl.SavePlayList();
            //step 19 logout
            LogOutWithoutLogin();
            //TODO: Assert that the saved worked.
        }


        //[TestCase] //testcase 808
        //[Description("Test Case 808")]
        public void AddTriviaWidget()
        {
            //Step 1 signIn
            Login();
            //Step 2 select existing playlist
            SelectAutomatedPlaylist();
            //Step 3 select add trivia widget
            IWebElement triviaWidget = _driver.Value.FindElement(By.CssSelector(BaseStrings.triviaWidgetCssSelector));
            WaitForMaskModal();
            triviaWidget.Click();//NOT VISIBLE on half screen
                                 //Step 4 Spell check all content (fields/values/buttons), including placeholder text 

            //Step 5 Confirm text box displays with pre-filled time Duration (not editable)
            IWebElement triviaWidgetDuration = _driver.Value.FindElement(By.CssSelector(BaseStrings.triviaWidgetDurationCssSelector));
            //---Assert.IsTrue(triviaWidgetDuration.Text.Contains("45")); 
            //Step 6 Trivia pre-filled description displays on lower section of window
            IWebElement triviaDescriptionText = _driver.Value.FindElement(By.CssSelector(BaseStrings.triviaSaveButtonCssSelector));
            //---Assert.IsTrue(triviaDescriptionText.Text.Contains("GM specific Q&A for customers sitting in the lounge."));
            //Step 7 Select Save
            IWebElement triviaWidgetSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.triviaWidgetSaveButtonCssSelector));
            triviaWidgetSaveButton.Click();
            //Step 8 Select Add Trivia Widget
            IWebElement triviaWidget1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.triviaWidgetSaveButtonCssSelector));
            WaitForMaskModal();
            triviaWidget1.Click();
            //Step 9 Select Brand dropdown box //Step 10 Select Buick Brand
            IWebElement triviaWidgetBrand = _driver.Value.FindElement(By.XPath(BaseStrings.trafficWidgetDropDown));
            var selectBrandDropDown = new SelectElement(triviaWidgetBrand);
            selectBrandDropDown.SelectByValue("buick");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 11 Select Save 
            IWebElement triviaWidgetSaveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.triviaWidgetSaveButtonCssSelector));
            triviaWidgetSaveButton1.Click();
            //Step 12 Select Add Trivia Widget
            IWebElement triviaWidget2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.triviaWidgetCssSelector));
            WaitForMaskModal();
            triviaWidget2.Click();
            //Step 13 Select Number to Show dropdown box  
            IWebElement triviaWidgetDurationDD = _driver.Value.FindElement(By.XPath(BaseStrings.triviaWidgetDurationDropDown));
            var selectDurationDropDown = new SelectElement(triviaWidgetDurationDD);
            selectDurationDropDown.SelectByValue("2");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 14 Select any number (1-10) - add parameters to test w/all 10?

            //Step 15 Select Save 
            IWebElement triviaWidgetSaveButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.triviaWidgetSaveButtonCssSelector));
            triviaWidgetSaveButton2.Click();
            //Step 16 Create all Brands

            //Step 17 Select Save from playlist screen
            IWebElement playlistSave = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSaveCSSSelector));
            WaitForMaskModal();
            playlistSave.Click();
            //Step 18 Logout
            LogOutWithoutLogin();

            //TODO: Assert that the saved worked.
        }

        //[TestCase]//test case 809
        //[Description("Test case 809")]
        public void AddHealthWidget()
        {
            //step 1 sign in
            Login();
            //step 2 select an existing playlist
            SelectAutomatedPlaylist();
            //step 3 select add health widget
            IWebElement healthWidgetIcon = _driver.Value.FindElement(By.CssSelector(BaseStrings.healthWidgetCssSelector));
            healthWidgetIcon.Click();
            //step 4 spell check all content (fields/values/buttons), including placeholder text
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 5 confirm text box displays with pre-filled time duration (not editable)
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 6 health pre-filled description displays on lower section of window

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 7 select save
            IWebElement healthSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.healthWidgetSaveButtonCssSelector));
            healthSaveButton.Click();
            WaitForMaskModal();
            //step 8 select add widget
            IWebElement healthWidgetIcon1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.healthWidgetCssSelector));
            healthWidgetIcon1.Click();
            //step 9 select brand dropdown box

            IWebElement brandDropdown = _driver.Value.FindElement(By.XPath(BaseStrings.healthWidgetDropDown));
            var selectBrandDropDown = new SelectElement(brandDropdown);
            selectBrandDropDown.SelectByValue("buick");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //step 10 select buick brand

            //step 11 select save
            WaitForMaskModal();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            healthSaveButton.Click();
        }

        //[TestCase]//test case 72
        //[Description("Test case 72")]
        public void AddImageWidget()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

            IWebElement imageWidget = _driver.Value.FindElement(By.CssSelector(BaseStrings.imageWidgetCssSelector));
            WaitForMaskModal();
            imageWidget.Click();

            IWebElement imageAssestLibrarySearchInput = _driver.Value.FindElement(By.Id("asset-search"));
            imageAssestLibrarySearchInput.SendKeys("dci");  //in the future this should grab the whole collection of assests and pick a random asset          

            IWebElement imageAssestSelection = _driver.Value.FindElement(By.CssSelector(BaseStrings.assestCssSelector));
            WaitForMaskModal();
            imageAssestSelection.Click();

            IWebElement assestLibraryDoneButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.assestLibraryDoneButtonCssSelector));
            WaitForMaskModal();
            assestLibraryDoneButton.Click();

            //TODO: Assert that the saved worked.
        }



        [TestCase]//Test case 834
        [Category("All")]
        //[Category("SmokeTests")]
        [Description("Test case 834")]
        public void AddVideoWidget()
        {
            Login();

            SelectAutomatedPlaylist();

            PlayList pl = new PlayList(_driver.Value, _configuration);// TestConfiguration.GetTestConfiguration());
            pl.Wait(2);
            
            IWebElement videoButton = pl.PlayListWidets.FirstOrDefault(a => a.Text.ToLower().Contains("video"));
            
            Assert.IsNotNull(videoButton);

            videoButton.Click();

            pl.Wait(2);

            LG20.Pages.SubCards.Widgets.video videoWidget = new LG20.Pages.SubCards.Widgets.video(_driver.Value);

            videoWidget.SearchVideoAssets();

            videoWidget.SelectVideo();

            //make a new list of widgets in playlist and compare to the original to assert if the new widget was added

            #region --- Old Code ---

            //IWebElement videoAssestLibrarySearchInput = _driver.Value.FindElement(By.Id("asset-search"));
            //videoAssestLibrarySearchInput.SendKeys("a");
            //IWebElement videoAssestSelection = _driver.Value.FindElement(By.XPath(BaseStrings.videoAssestSelectionXPath));
            //videoAssestSelection.Click();
            ////step 10 Select Done
            //IWebElement videoWidgetDoneButton = _driver.Value.FindElement(By.XPath(BaseStrings.videoWidgetDoneButtonXpath));
            //videoWidgetDoneButton.Click();
            ////step 11 Select Add Video Widget
            //videoWidgetButton.Click();
            //WaitForMaskModal();
            ////step 12 Select multiple videos
            ////IWebElement brandDropdown = _driver.Value.FindElement(By.XPath(BaseStrings.healthWidgetDropDown));
            ////var selectBrandDropDown = new SelectElement(brandDropdown);
            ////selectBrandDropDown.SelectByValue("buick");
            //IWebElement option1 = _driver.Value.FindElement(By.XPath("//*[@id='asset-video-select-form']/div[2]/div[1]"));
            //IWebElement option2 = _driver.Value.FindElement(By.XPath("//*[@id='asset-video-select-form']/div[2]/div[2]"));
            //IWebElement option3 = _driver.Value.FindElement(By.XPath("//*[@id='asset-video-select-form']/div[2]/div[3]"));

            //option1.Click();
            //option2.Click();
            //option3.Click();

            ////step 13 Select Done
            //IWebElement videoWidgetDoneButton1 = _driver.Value.FindElement(By.XPath(BaseStrings.videoWidgetDoneButtonXpath));
            //Thread.Sleep(TimeSpan.FromSeconds(10));
            //videoWidgetDoneButton1.Click();
            ////step 14 Select Save from Playlist screen
            //Thread.Sleep(TimeSpan.FromSeconds(10));
            //IWebElement saveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSaveButtonCssSelector));
            //saveButton.Click();

            ////step 15 Logout
            //LogOutWithoutLogin();


            #endregion

        }

        public void AddScreenFeedWidget()
        {
            IWebElement screenfeedWidgetButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.screenFeedWidgetCssSelector));
            WaitForMaskModal();
            screenfeedWidgetButton.Click();

            IWebElement selectScreenFeedFilter = _driver.Value.FindElement(By.XPath("//*[@id='select-duration']"));
            var selectScreenFeedElement = new SelectElement(selectScreenFeedFilter);
            selectScreenFeedElement.SelectByText("Best Bites");


            IWebElement selectScreenFeedNumberFilter = _driver.Value.FindElement(By.XPath("//*[@id='select-duration']"));

            IWebElement screenFeedSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.screedFeedSaveButtonCssSelector));
            screenFeedSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddBrandWidget()
        {
            IWebElement brandWidgetButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.brandWidgetCssSelector));
            WaitForMaskModal();
            brandWidgetButton.Click();

            IWebElement brandSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.brandSaveButtonCssSelector));
            brandSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void PlaylistSchedule()
        {
            //IWebElement playlistOpenButton = _driver.Value.FindElement(By.CssSelector(Base.playlistOpenButtonCSSSelector));
            //playlistOpenButton.Click();

            IWebElement schedulePlaylist = _driver.Value.FindElement(By.CssSelector(BaseStrings.schedulePlaylistCssSelector));
            WaitForMaskModal();
            schedulePlaylist.Click();

            IWebElement schedulePlaylistStart = _driver.Value.FindElement(By.Id("asset-begin-date-range"));
            schedulePlaylistStart.Clear();

            DateTime dateInputStart = DateTime.Today;
            DateTime dateInputEnd = dateInputStart.AddDays(30);

            schedulePlaylistStart.SendKeys(dateInputStart.ToString("MM/dd/yyyy"));

            IWebElement schedulePlaylistEnd = _driver.Value.FindElement(By.Id("asset-end-date-range"));
            schedulePlaylistEnd.Clear();
            schedulePlaylistEnd.SendKeys(dateInputEnd.ToString("MM/dd/yyyy"/*+Keys.Enter*/));

            IWebElement allDayCheckBox = _driver.Value.FindElement(By.CssSelector("#asset-info-form > div.lg-modal__field.schedule-modal-time-wrapper > div:nth-child(1) > label"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            allDayCheckBox.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));//not waiting on mask modal clicking the calendar pop up

            IWebElement submitSchedule = _driver.Value.FindElement(By.XPath(BaseStrings.submitScheduleCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));//not waiting on mask modal clicking the calendar pop up

            IWebElement gearIcon = _driver.Value.FindElement(By.XPath(BaseStrings.gearIconXpath));
            gearIcon.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

            submitSchedule.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            WaitForMaskModal();
            //TODO: Assert that the saved worked.

        }

        public void PlaylistPublish()
        {
            IWebElement playlistPublishButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistPublishButtonCssSelector));
            WaitForMaskModal();
            playlistPublishButton.Click();

            //if (_configuration.Environment == Common.EnvironmentType.Prod)
            //{
            //    url = url.Replace(".prod", string.Empty);
            //}
            //            url = (_configuration.Environment == Common.EnvironmentType.Prod) ?  : url;

            IWebElement playlistDonePublishButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.publishDoneButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            WaitForMaskModal();
            playlistDonePublishButton.Click();

            //TODO: Assert that the published worked. Might be an API call.
        }


        public void LiveguidePlaylists()
        {
            Login();

            /*Start Playlists TestCase Suite ID 69 with 5 parts*/

            CreatePlaylists();

            IWebElement playlistOpenButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));

            WaitForMaskModal();

            playlistOpenButton.Click();
            WaitForMaskModal();
            AddWeatherWidget();
            WaitForMaskModal();

            AddFinanceWidget();
            WaitForMaskModal();

            AddTrafficWidget();
            WaitForMaskModal();

            AddTriviaWidget();
            WaitForMaskModal();

            //AddHealthWidget(); HEALTH WIDGET TO BE ADDED HERE
            //WaitForMaskModal();
            AddImageWidget();
            WaitForMaskModal();

            AddVideoWidget();
            WaitForMaskModal();

            AddScreenFeedWidget();
            WaitForMaskModal();

            AddBrandWidget();
            WaitForMaskModal();

            PlaylistSchedule();
            WaitForMaskModal();

            PlaylistPublish();
            WaitForMaskModal();

            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistsSideBarMenuButton.Click();

            //String expectedMessage = "Automated Playlist Test";
            //String message = _driver.Value.FindElement(By.CssSelector(BaseStrings.newPlaylistDiv)).Text;
            //Assert.True(message.Contains(expectedMessage));

            DeleteProtocolWITHOUTlogin();

            LogOutWithoutLogin();

        }

        
       // [TestCase]
       // [Description("LiveGuideAssets")]
        public void LiveguideAssets2()//just method calls 
        {
            Login();

            //IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(Base.playlistSideBarMenuCssSelector));
            //playlistsSideBarMenuButton.Click();

            //TODO: Assert that we are on the playlist page

            var sidebartest = new SideBar(_driver.Value, _configuration);

            sidebartest.GetMenuItem("assets");

            sidebartest.SelectMenu("assets");


            var assettest = new Assets(_driver.Value, _configuration);

            assettest.GetAssetAddButton();

            assettest.GetAssetSearchInput();

            assettest.GetDisplayOptionItem("Display as Grid");

            assettest.SelectDisplayOption("Display as Grid");


            #region -- old code --

            //IWebElement playlistSearch = _driver.Value.FindElement(By.Id("playlists-search"));
            //playlistSearch.SendKeys("Automated Playlist Test");

            ////TODO: Assert that a model is up. 

            //AssetUploadingImage();

            //AssetUploadingVideo();

            #endregion
        }

        //[TestCase]
        //[Description("LiveGuideClientMenu")]
        public void ClientMenuTest()
        {
            Login();

          //  IWebElement playerChannelDropdown = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));

         //   playerChannelDropdown.Click();

            var ClientMenuTest = new ClientMenu(_driver.Value, _configuration);

            //ClientMenuTest.GetClientMenuItem("GM");

            ClientMenuTest.SelectClient("GM");
        }

        [TestCase]
        [Description("LiveGuideAssets")]
        public void LiveguideAssets()
        {
            Login();

            //IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(Base.playlistSideBarMenuCssSelector));
            //playlistsSideBarMenuButton.Click();

            //TODO: Assert that we are on the playlist page

            IWebElement playlistSearch = _driver.Value.FindElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");

            //TODO: Assert that a model is up. 

            AssetUploadingImage();

            AssetUploadingVideo();

        }

        public void AssetUploadingImage()
        {
            IWebElement playlistOpenButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));
            WaitForMaskModal();
            playlistOpenButton.Click();

            IWebElement uploadButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.uploadButtonCssSelector));
            WaitForMaskModal();
            uploadButton.Click();

            IWebElement uploadFromButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.uploadFromPCCssSelector));
            WaitForMaskModal();
            uploadFromButton.Click();

            //TODO: Need a better way to get a file to upload. Maybe define a data folder in the solution for now.

            //MiscLib.WindowsFormHelper.GetAutoIt("Open", @"C:\Users\enwright\Desktop\galaxie.jpg");

            IWebElement uploadDialogCloseButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.uploadDialogCloseButtonCssSelector));
            uploadDialogCloseButton.Click();

            //TODO: Assert here to see if the images are uploaded.

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(50));
        }

        public void AssetUploadingVideo()
        {
            //IWebElement playlistOpenButton = _driver.Value.FindElement(By.CssSelector(Base.playlistOpenButtonCSSSelector));
            //playlistOpenButton.Click();

            IWebElement uploadButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.uploadButtonCssSelector));
            WaitForMaskModal();
            uploadButton.Click();

            IWebElement uploadFromButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.uploadFromPCCssSelector));
            WaitForMaskModal();
            uploadFromButton.Click();

            //TODO: Need a better way to get a file to upload. Maybe define a data folder in the solution for now.
            MiscLib.WindowsFormHelper.GetAutoIt("Open", @"C:\Users\enwright\Desktop\Toy_car.mov");


            IWebElement uploadDialogCloseButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.uploadDialogCloseButtonCssSelector));
            WaitForMaskModal();
            uploadDialogCloseButton.Click();

            //TODO: Assert here to see if the images are uploaded.
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(50));
        }

        [TestCase] //Test case 737
        [Category("All")]
        //[Category("SmokeTests")]
        [Description("Test case 737")]
        public void AddWeatherWidget()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            //step 1 login   
            Login();
            //step 2 Select an existing Playlist
            SelectAutomatedPlaylist();
            //step 3 Select Add Weather Widget 
            PlayList pl = new PlayList(_driver.Value, _configuration);// TestConfiguration.GetTestConfiguration());
            pl.Wait();
            //IWebElement weatherWidget = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetCSSSelector));
            //WaitForMaskModal();
            //weatherWidget.Click();

            IWebElement weatherButton = pl.PlayListWidets.FirstOrDefault(a => a.Text.ToLower().Contains("weather"));
            //WidgetListItem tr = pl.Widgets.FirstOrDefault(a => a.Name.ToLower().Contains("traffic"));
            Assert.IsNotNull(weatherButton);

            weatherButton.Click();

            pl.Wait(2);

            LG20.Pages.SubCards.Widgets.Weather weatherWidget = new LG20.Pages.SubCards.Widgets.Weather(_driver.Value);
            //step 4 Spell check all content (fields/values/buttons), including placeholder text 
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 5 Confirm text box displays with pre-filled time Duration (not editable)
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 6 Weather pre-filled description displays on lower section of window
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 7 Do not enter a Zip Code, confirm placeholder text displays 'Enter Zip Code'
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //step 8 Select Save
            //IWebElement weatherWidgetSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetSaveButtonCSSSelector));
            //weatherWidgetSaveButton.Click();
            weatherWidget.ClickModalSaveButton();
            pl.Wait(2);

            //step 9 Select Add Weather Widget
            //IWebElement weatherWidget1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetCSSSelector));
            //WaitForMaskModal();
            //weatherWidget1.Click();

            weatherButton.Click();
            pl.Wait(2);

            //step 10 Select Brand dropdown box
            //step 11 Select Buick Brand
            //IWebElement brandDropdown = _driver.Value.FindElement(By.XPath(BaseStrings.weatherWidgetDropDown));
            //var selectBrandDropDown = new SelectElement(brandDropdown);
            //selectBrandDropDown.SelectByValue("buick");

            weatherWidget = new LG20.Pages.SubCards.Widgets.Weather(_driver.Value);
            weatherWidget.BrandSelectBox = "Buick";
            pl.Wait(2);

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 12 Select Save
            //IWebElement weatherSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetSaveButtonCSSSelector));
            //WaitForMaskModal();
            //weatherSaveButton.Click();

            weatherWidget.ClickModalSaveButton();
            pl.Wait(2);
            //step 13 Select Add Weather Widget
            // IWebElement weatherWidget2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetCSSSelector));
            WaitForMaskModal();
            // weatherWidget2.Click();
            //step 14 Enter an invalid Zip Code in text box  (letters, special characters or less or more than 5 numbers)

            weatherButton.Click();

            //IWebElement weatherZipCodeInput = _driver.Value.FindElement(By.Id(BaseStrings.weatherZipCodeInputID));
            //weatherZipCodeInput.SendKeys("531");

            weatherWidget = new LG20.Pages.SubCards.Widgets.Weather(_driver.Value);
            pl.Wait(2);
            weatherWidget.WeatherZipCodeTextBox = "531";
            pl.Wait(2);

            weatherWidget.ClickModalSaveButton();
            pl.Wait(1);
            Assert.IsTrue(weatherWidget.WeatherZipCodeError);

            //step 15 Enter a valid Zip Code in text box
            //IWebElement weatherZipCodeInput1 = _driver.Value.FindElement(By.Id(BaseStrings.weatherZipCodeInputID));
            //weatherZipCodeInput1.Clear();

            //weatherZipCodeInput1.SendKeys("53142");
            weatherWidget.ClearWeatherZipCodeTextbox();
            pl.Wait(1);
            weatherWidget.WeatherZipCodeTextBox = "53142";
            pl.Wait(1);


            //step 16 Select Save
            //IWebElement weatherSaveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetSaveButtonCSSSelector));
            //WaitForMaskModal();
            //weatherSaveButton1.Click();
            weatherWidget.ClickModalSaveButton();

            //step 17 Create all brands ---I am not sure what this means---

            //step 18 select save from playlist screen
            //IWebElement playlistSave2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSaveCSSSelector));
            //WaitForMaskModal();
            //playlistSave2.Click();
            pl.Wait(2);
            pl.SavePlayList();
            //step 19 logout
            LogOutWithoutLogin();
            //TODO: Assert that the saved worked.
        }

        public void LogOutWithoutLogin()
        {
            #region -- old code --
            /*
            IWebElement logOutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logOutButtonCssSelector));
            WaitForMaskModal();
            logOutButton.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            IWebElement confirmLogOutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector));
            WaitForMaskModal();
            confirmLogOutButton.Click();*/
            #endregion -- old code --
                
            Logout logout = new Logout(_driver.Value, _configuration);
            SideBar sb = new SideBar(_driver.Value, _configuration);
            sb.Wait(1);
            sb.SelectMenu("Log Out");//select log out from left navigation bar
            sb.Wait(2);
            logout.LogoutModal.IsModalDisplay.Should().BeTrue();                  
            logout.LogoutModal.ClickModalButton("Logout");
        }

        [TestCase]
        [Description("Test for Test case 1456")]
        public void LogoutAfterLogin()//postive test for Test case 1456
        {
            #region --old code--
            /* //step 1
              Login();

              string url = Common.LgUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
              string currentURL = _driver.Value.Url;
              _driver.Value.Navigate().GoToUrl(url);

              IWebElement logOutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logOutButtonCssSelector));
              WaitForMaskModal();
              //step 2
              logOutButton.Click();

              IWebElement logOutCancelButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logOutCancelButtonCssSelector));
              WaitForMaskModal();
              //step 3
              logOutCancelButton.Click();

              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
              //Step 4
              logOutButton.Click();


              IWebElement confirmLogOutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector));
              WaitForMaskModal();
              //step 5
              confirmLogOutButton.Click();

              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
              //step 6
              Login();

              IWebElement playerChannelDropdown = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));

              WaitForMaskModal();
              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
              //step 7
              playerChannelDropdown.Click();

              IWebElement LogOutChannelSelection = _driver.Value.FindElement(By.XPath(BaseStrings.logOutChannelSelectionXPath));
              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
              //step 8
              LogOutChannelSelection.Click();


              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

              IWebElement logOutCancelButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.logOutCancelButtonCssSelector2));
              WaitForMaskModal();
              logOutCancelButton2.Click();//step 9

              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
              playerChannelDropdown.Click();
              IWebElement LogOutChannelSelection2 = _driver.Value.FindElement(By.XPath(BaseStrings.logOutChannelSelectionXPath2));
              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
              LogOutChannelSelection2.Click();//step 10

              IWebElement confirmLogOutButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector2));
              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
              confirmLogOutButton2.Click();//step 11 

              System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

              Login();//step 12

              _driver.Value.Quit();*/
            #endregion


            ClientMenu cm = new ClientMenu(_driver.Value, _configuration);
            Logout logout = new Logout(_driver.Value, _configuration);
            SideBar sb = new SideBar(_driver.Value, _configuration);
            Login login = new Login(_driver.Value, _configuration);
            Login();
            //sb.Wait(3);

            sb.SelectMenu("Log Out");
            sb.Wait(1);
            logout.LogoutModal.IsModalDisplay.Should().BeTrue();
            logout.LogoutModal.ClickModalButton("cancel");

            logout.Wait(2);
            logout.LogoutModal.IsModalDisplay.Should().BeFalse();
            sb.SelectMenu("Log Out");
            sb.Wait(1);

            //logout.LogoutModal.ClickModalButton("Logout");
            logout.LogoutModal.ClickModalButton("Logout");
            string[] tagsToCompare = new[] { "Sign In", "" };
            login.GetSignInButton.Should().Contain(tagsToCompare).And.HaveCount(tagsToCompare.Length);

            Login();

            cm.SelectClient("Logout");
            cm.Wait(1);
            logout.LogoutModal.IsModalDisplay.Should().BeTrue();
            logout.LogoutModal.ClickModalButton("cancel");
            logout.Wait(2);
            logout.LogoutModal.IsModalDisplay.Should().BeFalse();
            cm.SelectClient("Logout");
            cm.Wait(1);

            logout.LogoutModal.ClickModalButton("Logout");
            
            login.GetSignInButton.Should().Contain(tagsToCompare).And.HaveCount(tagsToCompare.Length);

        }
        [TestCase]//Test case 585
        [Category("All")]
        [Description("Test case 585")]
        public void DeletePlaylist()
        {
            //Step 1
            Login();
            //Step 2
            IWebElement deletePlaylistButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.deletePlaylistButtonCssSelector));
            deletePlaylistButton.Click();
            //Step 3 spell check
            //Step 4 select cancel
            IAlert cancel = _driver.Value.SwitchTo().Alert();
            cancel.Dismiss();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //TODO: Need assert here to validate that the playlist wasn't deleted.
            //Step 5
            deletePlaylistButton.Click();
            //Step 6
            IAlert accept = _driver.Value.SwitchTo().Alert();
            accept.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 7 refresh screen
            _driver.Value.Navigate().Refresh();
            //TODO: Need assert here to validate the the playlist has been deleted. 
            //Step 8 logout
            LogOutWithoutLogin();
            //Step 9
            Login();
            //Step 10
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            //Step 11
            LogOutWithoutLogin();
        }


        [TestCase]
        [Category("All")]
        public void DeleteProtocol()
        {
            Login();

            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();

            IWebElement playlistSearch = _driver.Value.FindElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");

            IWebElement newPlaylistDeleteButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.newPlaylistDeleteButtonCSSSelector));

            if (newPlaylistDeleteButton.Displayed)
            {
                IWebElement deletePlaylistButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.deletePlaylistButtonCssSelector));

                deletePlaylistButton.Click();

                IAlert alert = _driver.Value.SwitchTo().Alert();

                alert.Accept();

                playlistSearch.SendKeys("Automated Playlist Test");

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                WaitForMaskModal();

                //TODO: Validate the playlist has been deleted. API??
            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //_driver.Quit();
        }
        //[TestCase]
        public void DeleteProtocolWITHOUTlogin()
        {
            //Login();
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();

            IWebElement playlistSearch = _driver.Value.FindElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            IWebElement newPlaylistDeleteButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.newPlaylistDeleteButtonCSSSelector));
            String expectedMessage = "Automated Playlist Test";
            String message = _driver.Value.FindElement(By.CssSelector(BaseStrings.newPlaylistDiv)).Text;
            Assert.True(message.Contains(expectedMessage));

            if (message.Contains(expectedMessage))
            {
                String newPlaylistDiv = _driver.Value.FindElement(By.CssSelector(BaseStrings.newPlaylistDiv)).Text;
                Assert.True(newPlaylistDiv.Contains(expectedMessage));

                if (newPlaylistDiv.Contains(expectedMessage))
                {
                    IWebElement playlistArea = _driver.Value.FindElement(By.ClassName("playlists-content"));

                    var playlistContext = playlistArea.Text;

                    IWebElement deletePlaylistButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.deletePlaylistButtonCssSelector));

                    if (playlistContext.Contains(expectedMessage))
                    {

                        WaitForMaskModal();

                        deletePlaylistButton.Click();

                        IAlert alert = _driver.Value.SwitchTo().Alert();

                        alert.Accept();

                        WaitForMaskModal();

                        playlistSearch.SendKeys("Automated Playlist Test");

                        WaitForMaskModal();

                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                        //TODO: Validate the playlist has been deleted. API??
                    }



                }
                else
                {

                }
            }
            else
            {

            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));


        }

        /*    [TestCase]
            [Category("All")]
            public void ContactUsWithrequiredFields()
            {
                //step 1
                Login();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                IWebElement contactUsLink = _driver.Value.FindElement(By.CssSelector(BaseStrings.contactUsLinkCssSelector));
                WaitForMaskModal();

                //step 2
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                contactUsLink.Click();

                IWebElement sendButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.sendButtonCssSelector));
                WaitForMaskModal();

                //step 3 
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                sendButton.Click();

                // step 4
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                IWebElement EmailUsFullNameInput = _driver.Value.FindElement(By.Id("full-name"));
                EmailUsFullNameInput.SendKeys("Automated Tester");

                //step 5
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                WaitForMaskModal();
                sendButton.Click();

                //Step 6
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                IWebElement EmailUsPhoneNumberInput = _driver.Value.FindElement(By.Id("phone"));
                EmailUsPhoneNumberInput.SendKeys("Auto Test");

                //step 7
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                WaitForMaskModal();
                sendButton.Click();

                //Step 8
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                EmailUsPhoneNumberInput.Clear();
                EmailUsPhoneNumberInput.SendKeys("1234567890");

                //Step 9
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                WaitForMaskModal();
                sendButton.Click();

                //Step 10
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                WaitForMaskModal();
                IWebElement EmailUsEmialInput = _driver.Value.FindElement(By.Id("email"));
                EmailUsEmialInput.SendKeys("Automated Tester");

                //Step 11
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                WaitForMaskModal();
                sendButton.Click();

                //Step 12
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                EmailUsEmialInput.Clear();
                EmailUsEmialInput.SendKeys("test@");

                //Step 13
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                WaitForMaskModal();
                sendButton.Click();

                //Step 14
                EmailUsEmialInput.Clear();
                EmailUsEmialInput.SendKeys("test@dci");

                //Step 15
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                WaitForMaskModal();
                sendButton.Click();

                //Step 16
                EmailUsEmialInput.Clear();
                EmailUsEmialInput.SendKeys("test@dci.");

                //Step 17
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                sendButton.Click();

                //Step 18
                EmailUsEmialInput.Clear();
                EmailUsEmialInput.SendKeys("test@dci.c");

                //Step 19
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                sendButton.Click();

                //Step 20
                EmailUsEmialInput.Clear();
                EmailUsEmialInput.SendKeys("test@dci.com");

                //Step 21
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                sendButton.Click();

                //Step 22
                IWebElement EmailUsCommentsInput = _driver.Value.FindElement(By.Id("comments"));
                EmailUsCommentsInput.SendKeys("Automated Tester");

                //Step 23
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                sendButton.Click();

                //Step 24
                IWebElement ContactUsDoneButton = _driver.Value.FindElement(By.CssSelector("#notifications-form > div > button"));
                ContactUsDoneButton.Click();

                LogOutWithoutLogin();
            } */
        [TestCase]
        [Category("All")]
        [Description("Test Case #1457")]
        public void ContactUsWithrequiredFields_POM()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("TestContactUs.json");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(11));
            //Step 1
            Login();

            //Step 2
            SideBar sb = new SideBar(_driver.Value, _configuration);

            sb.SelectMenu("Contact Us");
            ContactUs cus = new ContactUs(_driver.Value, _configuration);

            cus.Wait(4);
            cus.ClickSendButton();
            cus.Wait(2);
            cus.IsFullNameFieldError.Should().BeTrue();

            
            cus.ContactFullNameTextField = td.TestAnswers["FullName"];
            cus.ClickSendButton();


            cus.ContactPhoneTextField = td.TestAnswers["InvalidPhoneNumber1"]; // non numeric data in phone number field  
            cus.ClickSendButton();

            cus.ContactPhoneTextField = null;
            cus.ContactPhoneTextField = td.TestAnswers["InvalidPhoneNumber2"]; // invalid data in phone number field
            cus.ContactPhoneTextField = null;
            cus.ContactPhoneTextField = td.TestAnswers["PhoneNumber"]; //valid data
            cus.ClickSendButton();

            cus.ContactEmailTextField = td.TestAnswers["InvalidEmail1"]; // invalid email data
            cus.ClickSendButton();
            cus.IsEmailFieldError.Should().BeTrue();
            cus.ContactEmailTextField = null;

            cus.ContactEmailTextField = td.TestAnswers["InvalidEmail2"]; // invalid email data
            cus.ClickSendButton();
            cus.IsEmailFieldError.Should().BeTrue();
            cus.ContactEmailTextField = null;

            cus.ContactEmailTextField = td.TestAnswers["InvalidEmail3"]; // invalid email data
            cus.ClickSendButton();
            cus.IsEmailFieldError.Should().BeTrue();
            cus.ContactEmailTextField = null;

            cus.ContactEmailTextField = td.TestAnswers["InvalidEmail4"]; // invalid email data
            cus.ClickSendButton();
            cus.IsEmailFieldError.Should().BeTrue();
            cus.ContactEmailTextField = null;

            cus.ContactEmailTextField = td.TestAnswers["InvalidEmail5"]; // invalid email data
            cus.ClickSendButton();
            cus.IsEmailFieldError.Should().BeTrue();
            cus.ContactEmailTextField = null;

            cus.ContactEmailTextField = td.TestAnswers["Email"]; // valid email data
            cus.ClickSendButton();

            cus.ContactCommentsTextField = td.TestAnswers["Comments"];
            cus.ClickSendButton();
            cus.Wait(8);

            cus.IsNotificationPopupDisplayed.Should().BeTrue(); //verify notification popup being shown after send is clicked
            cus.ContactNotificationMessage.Should().Be("Mail sent and will be processed in the order it was received.");//verify success message in notification popup;

            cus.ClickDone();

            cus.Wait(2);

            LogOutWithoutLogin();
        }

        /*[TestCase]//Test Case #1459
        [Category("All")]
        [Description("Test Case #1459")]
        public void ContactUsWithAllFields()
        {
            //Step 1
            Login();

            //Step 2
            IWebElement contactUsLink = _driver.Value.FindElement(By.CssSelector(BaseStrings.contactUsLinkCssSelector));
            WaitForMaskModal();
            contactUsLink.Click();

            //Step 3
            IWebElement EmailUsFullNameInput = _driver.Value.FindElement(By.Id("full-name"));
            EmailUsFullNameInput.SendKeys("Automated Tester");

            //Step 4
            IWebElement EmailUsTitleInput = _driver.Value.FindElement(By.Id("title"));
            EmailUsTitleInput.SendKeys("Automated Tester");

            //Step 5
            IWebElement EmailUsCompanyInput = _driver.Value.FindElement(By.Id("company"));
            EmailUsCompanyInput.SendKeys("Automated Tester");

            //Step 6
            IWebElement EmailUsPhoneNumberInput = _driver.Value.FindElement(By.Id("phone"));
            EmailUsPhoneNumberInput.SendKeys("9876543210");

            //Step 7
            IWebElement EmailUsEmialInput = _driver.Value.FindElement(By.Id("email"));
            EmailUsEmialInput.SendKeys("AutomatedTester@dcim.com");

            //Step 8
            IWebElement EmailUsCommentsInput = _driver.Value.FindElement(By.Id("comments"));
            EmailUsCommentsInput.SendKeys("Automated Tester");

            IWebElement sendButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.sendButtonCssSelector));
            WaitForMaskModal();
            //Step 9
            sendButton.Click();

            //var newHtml = _driver.PageSource;


            //newHtml.ToString();



            //if (newHtml.Contains("contactErrorInput"))
            //{
            //    EmailUsFullNameInput.Clear();
            //    EmailUsFullNameInput.SendKeys("Automated Tester 2");

            //    EmailUsTitleInput.Clear();
            //    EmailUsTitleInput.SendKeys("AutomatedTester@mailinator.com");

            //    EmailUsCompanyInput.Clear();
            //    EmailUsCompanyInput.SendKeys("Automated Tester 2");

            //    EmailUsPhoneNumberInput.Clear();
            //    EmailUsPhoneNumberInput.SendKeys("123-123-1234");

            //    EmailUsEmialInput.Clear();
            //    EmailUsEmialInput.SendKeys("AutomatedTester@mailinator.com");

            //    EmailUsCommentsInput.Clear();
            //    EmailUsCommentsInput.SendKeys("Automated Tester 2");

            //    sendButton.Click();
            //}

            WaitForMaskModal();

            IWebElement ContactUsDoneButton = _driver.Value.FindElement(By.CssSelector("#notifications-form > div > button"));
            //Step 10
            ContactUsDoneButton.Click();

            //if (EmailUsFullNameInput.Displayed)
            //{
            //    var errorFullName = _driver.Value.FindElement(By.CssSelector("#contact-us-container > form > div:nth-child(2)")).Text;



            //    if (errorFullName.Contains("error"))
            //    {

            //    }
            //}
            //else if (EmailUsTitleInput.Displayed)
            //{

            //}
            //else if (EmailUsPhoneNumberInput.Displayed)
            //{

            //}

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 11
            LogOutWithoutLogin();
        } */

        [TestCase]//Test Case #1459
        [Category("All")]
        [Description("Test Case #1459")]
        public void ContactUsWithAllFields_POM()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(9));
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("TestContactUs.json");
            //Step 1
            Login();

            //Step 2
            SideBar sb = new SideBar(_driver.Value, _configuration);

            sb.SelectMenu("Contact Us");
            ContactUs cus = new ContactUs(_driver.Value, _configuration);
            cus.Wait();
            cus.ContactFullNameTextField = td.TestAnswers["FullName"];
            //cus.ContactFullNameTextField = "LG-AUTOTEST";
            cus.ContactFullNameTextField.Should().Be("LG-AUTOTEST", "It should be: LG-AUTOTEST");
            cus.ContactTitleTextField = td.TestAnswers["Title"];
            cus.ContactTitleTextField.Should().Be("Automated Tester", "It should be: Automated Tester");
            cus.ContactCompanyTextField = td.TestAnswers["Company"];
            cus.ContactCompanyTextField.Should().Be("TestCompany", "It should be: TestCompany");
            cus.ContactPhoneTextField = td.TestAnswers["PhoneNumber"];
            cus.ContactPhoneTextField.Should().Be("1234567890", "It should be: 1234567890");
            cus.ContactEmailTextField = td.TestAnswers["Email"];
            cus.ContactEmailTextField.Should().Be("autotest@test.com", "It should be: autotest@test.com");
            cus.ContactCommentsTextField = td.TestAnswers["Comments"];
            cus.ContactCommentsTextField.Should().Be("Automated Tester", "It should be: Automated Tester");

           // cus.Wait(2);

            cus.ClickSendButton();
            cus.Wait(8);

            cus.IsNotificationPopupDisplayed.Should().BeTrue(); //verify notification popup being shown after send is clicked

            cus.ContactNotificationMessage.Should().Be("Mail sent and will be processed in the order it was received.");//verify success message in notification popup

            cus.ClickDone();

            cus.Wait(4);
            LogOutWithoutLogin();
        }


        [TestCase]//Test Case #1460
        [Category("All")]
        [Description("Test Case #1460")]
        public void PlayersStatus()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("Test1460.json");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(9));
            //Step 1
            Login();

            ClientMenu client = new ClientMenu(_driver.Value, _configuration);
            client.Wait();
            client.SelectClient("GM");

            SideBar sb = new SideBar(_driver.Value, _configuration);

            Players player = new Players(_driver.Value, _configuration);

            //Step 2 select players tab
            sb.SelectMenu("Players");

            //Step 3 confirm that each player contains one status
            player.VerifyOnlineStatus.Should().BeTrue("No players were found to be online");
            player.VerifyOfflineStatusPlayersPage.Should().BeTrue("No players were found to be offline");

            //Step 4 select a player
            var playerName = td.TestAnswers["Player"]; // _envData.Player;
            player.SelectPlayer(td.TestAnswers["PlayerOnline"]);
            player.Wait(2);

            PlayerDetail playerDetail = new PlayerDetail(_driver.Value, _configuration);
            playerDetail.IsPlayerOfflineStatus.Should().BeFalse("player not found to be online");
            player.Wait();

            //Step 5
            sb.SelectMenu("Players");
            player.Wait();
            player.SelectPlayer(playerName);
            player.Wait(2);
            playerDetail = new PlayerDetail(_driver.Value, _configuration);
            playerDetail.IsPlayerOfflineStatus.Should().BeTrue("player not found to be offline");// CK: currently  LG-QAROB is a offline player, need to work on online player

            //Step 6
            player.SelectPlayer(playerName);
            player.Wait();

            //Step 7
            sb.SelectMenu("Players");
            player.Wait();

            //Step 8 select a player that is not set up...---needs work //TODO:Select a player that is not setup.

            //Step 9
            sb.SelectMenu("Players");
            player.Wait();

            //Step 10
            LogOutWithoutLogin();
        }

        [TestCase]// test case #1463
        [Category("All")]
        [Description("Test case #1463")]
        public void PlayerEdits()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("TestData.json");
            //Step 1
            Login();

            Players player = new Players(_driver.Value, _configuration);

            //step 2
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 3 
            //IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1"));
            var playerName = td.TestAnswers["Player"];

            //IWebElement playerSelect = GetPlayer(_driver.Value, "LG-QAROB");

            //            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //playerSelect.Click();

            player.SelectPlayer(playerName);

            player.Wait();
            
            //Step 4 
            IWebElement playerInfoDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerInfoDownArrowCssSelectors));
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            playerInfoDownArrow.Click();

            //Step 5
            IWebElement playerInfoUpArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerInfoUpArrowCssSelector));
//            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            player.Wait(2);
            playerInfoUpArrow.Click();

            //Step 6
            IWebElement playerInfoX = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerInfoXCssSelector));
//            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            playerInfoX.Click();

            //Step 7,8,& 9 are skipped since there isnt a products section right now
            //Step 10
            IWebElement deviceDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.deviceDownArrowCssSelector));
//            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            deviceDownArrow.Click();

            //Step 11
            IWebElement deviceUpArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.deviceUpArrowCssSelector));
//            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            deviceUpArrow.Click();

            //Step 12 
            IWebElement xOnDevice = _driver.Value.FindElement(By.CssSelector(BaseStrings.xOnDeviceCssSelector));
            //            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            xOnDevice.Click();

            //Step 13 select down arrow for location
            IWebElement locationDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.locationDownArrowCssSelector));
            //            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            locationDownArrow.Click();

            //Step 14 
            IWebElement locationUpArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.locationUpArrowCssSelector));
            //            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            locationUpArrow.Click();

            //Step 15
            IWebElement locationXButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.locationXButtonCssSelector));
            //            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            locationXButton.Click();

            //Step 16
            IWebElement playlistsButton = _driver.Value.FindElement(By.XPath(BaseStrings.playlistsButtonXPath));
            //            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            playlistsButton.Click();

            //Step 17
            IWebElement whatsPlayingDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.whatsPlayingDownArrowCssSelector));
            //            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            whatsPlayingDownArrow.Click();

            //Step 18
            IWebElement whatsPlayingUPArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.whatsPlayingUpArrowCssSelector));
            //            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            player.Wait(2);
            whatsPlayingUPArrow.Click();

            //Step 19
            IWebElement whatsPlayingXButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.whatsPlayingXButtonCssSelector));
            player.Wait(2);
//            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            whatsPlayingXButton.Click();

            //Step 20
            IWebElement playlistInfoDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistInfoDownArrowCssSelector));
            player.Wait(2);
//            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoDownArrow.Click();

            //Step 21
            IWebElement playlistInfoUpArrow = _driver.Value.FindElement(By.XPath(BaseStrings.playlistInfoUpArrowXpath));
            player.Wait(2);
//            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoUpArrow.Click();

            //Step 22
            IWebElement playlistInfoXButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistInfoXButtonCssSelector));
            player.Wait(2);
//            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoXButton.Click();

            //Step 23,24,25,26,27,28,29,30,31 are not present on frontend that i can see as of 09/18/18

            //Step 31
            LogOutWithoutLogin();
        }

        [TestCase]
        [Category("All")]
        public void PlayerEditDevice()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("TestData.json");
            //Step 1
            Login();

            Players player = new Players(_driver.Value, _configuration);

            //Step 2 select player tab
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 3 Select Any player

            //IWebElement playerTable =
            //    SeleniumCommon.GetElement(_driver.Value, SeleniumCommon.ByType.Id, "players-table");
            //IEnumerable<IWebElement> trs = playerTable.FindElements(By.TagName("tr")).ToList();
            //string playerName = "LG-QARob";
            //IWebElement playerSelect = trs.FirstOrDefault(a => a).FindElements(By.TagName("td"))
            //    .FirstOrDefault(b => b.Text.Equals(playerName, StringComparison.OrdinalIgnoreCase));

            var playerName = td.TestAnswers["Player"];

            //IWebElement playerSelect = GetPlayer(_driver.Value, "LG-QAROB");
            //IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1"));
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //playerSelect.Click();

            player.SelectPlayer(playerName);
            player.Wait(2);

            //Step 4 From the Device card, slect the ping device button
            IWebElement devicePingDeviceButton = _driver.Value.FindElement(By.XPath(BaseStrings.devicePingDeviceButtonXPath));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            devicePingDeviceButton.Click();

            //Step 5 Double click on the image that display under the Ping Data
            IWebElement pingDataImage = _driver.Value.FindElement(By.Id("sampleScreen"));
            IWebElement SampleScreen = _driver.Value.FindElement(By.Id("sampleScreen"));

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //new SelectElement(_driver.Value.FindElement(By.Id("sampleScreen")).Click());
            new Actions(_driver.Value).DoubleClick(SampleScreen).Perform();

            //Step 6 Select cancel
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            IAlert alert = _driver.Value.SwitchTo().Alert();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            alert.Dismiss();

            //Step 7 Double click on the image that display under ping data
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            new Actions(_driver.Value).DoubleClick(_driver.Value.FindElement(By.Id("sampleScreen"))).Perform();

            //Step 8 Select Ok
            IAlert alert2 = _driver.Value.SwitchTo().Alert();
            alert2.Accept();

            //Step 9 Select refresh button
            IWebElement pageRefreshButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.pageRefreshButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            pageRefreshButton.Click();

            //Step 10 from the device card , select the refresh app button
            IWebElement deviceRefreshAppButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.deviceRefreshAppButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceRefreshAppButton.Click();
            IAlert alert3 = _driver.Value.SwitchTo().Alert();
            alert3.Accept();
            //Step 11 From the device card select the restart app button
            IWebElement deviceRestartAppButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.deviceRestartAppButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceRestartAppButton.Click();
            IAlert alert4 = _driver.Value.SwitchTo().Alert();
            alert4.Accept();

            //Step 12 From the device card, select the restart device button
            IWebElement deviceRestartDeviceButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.deviceRestartDeviceButtonCssSelector));

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceRestartDeviceButton.Click();
            IAlert alert5 = _driver.Value.SwitchTo().Alert();
            alert5.Accept();

            //Step 13 Logout
            LogOutWithoutLogin();

        }

        [TestCase]//Test case #1469
        [Category("All")]
        [Description("Test case #1469")]
        public void PlayerAddNewChannel()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("TestData.json");
            //TODO: No asserts and no adding of channel and checking for it ??
            //step 1
            Login();

            Players player = new Players(_driver.Value, _configuration);
            //Step 2 select player tab
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 Select Any player
            //IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1 > span"));

            var playerName = td.TestAnswers["Player"];

            //IWebElement playerSelect = GetPlayer(_driver.Value, "LG-QAROB");

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //playerSelect.Click();
            player.SelectPlayer(playerName);
            player.Wait(2);


            //step 4 channelJoinButtonCssSelector
            //IWebElement channelJoinButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.channelJoinButtonCssSelector));//nothing should happen
            //Step 5
            IWebElement channelFilterInput = _driver.Value.FindElement(By.Id("channel-input"));
            channelFilterInput.SendKeys("test");
            //Step 6
            //channelJoinButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //IAlert alert = _driver.SwitchTo().Alert();
            //alert.Accept();
            //IWebElement xDeleteFilterButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.xOutButtonCssSelector));
            //xDeleteFilterButton.Click();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //alert.Accept();
            //Step 7 
            channelFilterInput.Clear();
            channelFilterInput.SendKeys("test filter");
            //Step 8 
            //channelJoinButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //IAlert wishToJoinDeviceAlert = _driver.SwitchTo().Alert();
            //alert.Accept();
            //xDeleteFilterButton.Click();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 9
            channelFilterInput.Clear();
            channelFilterInput.SendKeys("test filter 2");
            //Step 10
            //channelJoinButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //alert.Accept();
            //xDeleteFilterButton.Click();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 11  
            IWebElement pageRefreshButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.pageRefreshButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            pageRefreshButton.Click();
            //step 12
            LogOutWithoutLogin();
        }

        [TestCase]//Test case 1487
        [Category("All")]
        [Description("Test case 1487")]
        public void PlayerDeleteChannel()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("TestData.json");
            //step 1
            Login();
            
            Players player = new Players(_driver.Value, _configuration);

            //Step 2 select player tab
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 Select Any player
            // IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1"));
            var playerName = td.TestAnswers["Player"];

            //IWebElement playerSelect = GetPlayer(_driver.Value, "LG-QAROB");

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //playerSelect.Click();

            player.SelectPlayer(playerName);
            player.Wait(2);

            //Step 4 select configure button 
            IWebElement configureButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.configureButtonCssSelector));
            Assert.IsTrue(configureButton.Displayed);
            configureButton.Click();


            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            IWebElement filterSection = _driver.Value.FindElement(By.CssSelector(BaseStrings.filterSectionCssSelection));

            if (filterSection.Text.Contains("test"))
            {
                IWebElement testFilterXout = _driver.Value.FindElement(By.CssSelector(BaseStrings.testFilterXoutButtonCssSelector));
                testFilterXout.Click();
                IAlert alert = _driver.Value.SwitchTo().Alert();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                alert.Dismiss();
            }

            //Step 5
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //xOutButton1.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //alert.Dismiss();
            //Step 6
            IWebElement pageRefreshButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.pageRefreshButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            pageRefreshButton.Click();
            //Step 7
            LogOutWithoutLogin();
        }

        [TestCase]//Test case 1488
        [Category("All")]
        [Description("Test case 1488")]
        public void PlayerScreenConnect()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("TestData.json");
            //Step 1
            Login();

            PlayerDetail playerDetail = new PlayerDetail(_driver.Value, _configuration);
            Players player = new Players(_driver.Value, _configuration);
            //Step 2 select player tab
            SideBar sb = new SideBar(_driver.Value, _configuration);
            sb.SelectMenu("Players");
            player.Wait(2);
            player.PlayersSelectScreenConnect();
            player.Wait(4);
            player.GetCurrentTab().Should().ContainAny("screenconnect.com");
            player.CloseScreenconnect();
            player.Wait(2);
            player.GetCurrentTab().Should().NotContainAny("screenconnect.com");
            player.GetCurrentTab().Should().ContainAny("dcimliveguide.com");
            //Step 3 Hover over the screen connect icon
            //step 4
            var playName = td.TestAnswers["Player"]; //_envData.Player; //@"LG-QAROB";
            player.SelectPlayer(playName);
            player.Wait(2);
            playerDetail.PlayerDetailSelectScreenConnect();
            //Step 5 Hover over screen connect icon
            //Step 6
            player.Wait(4);
            playerDetail.GetCurrentTab().Should().ContainAny("screenconnect.com");

            //start from playlist tab
            playerDetail.PlayerDetailCloseScreenconnect();
            player.Wait(2);
            // verify url DCI url after closing screenconnect url
            playerDetail.GetCurrentTab().Should().NotContainAny("screenconnect.com");
            playerDetail.GetCurrentTab().Should().ContainAny("dcimliveguide.com");

            playerDetail.SelectTab("PLAYLISTS");
            playerDetail.PlayerDetailSelectScreenConnect();
            player.Wait(2);
            playerDetail.GetCurrentTab().Should().ContainAny("screenconnect.com");
            playerDetail.PlayerDetailCloseScreenconnect();
            playerDetail.Wait(2);
            playerDetail.GetCurrentTab().Should().NotContainAny("screenconnect.com");
            playerDetail.GetCurrentTab().Should().ContainAny("dcimliveguide.com");
            player.Wait(2);

            //start from configure tab
            playerDetail.SelectTab("CONFIGURE");
            playerDetail.PlayerDetailSelectScreenConnect();
            player.Wait(2);
            playerDetail.GetCurrentTab().Should().ContainAny("screenconnect.com");
            playerDetail.PlayerDetailCloseScreenconnect();
            playerDetail.Wait(2);
            playerDetail.GetCurrentTab().Should().NotContainAny("screenconnect.com");
            playerDetail.GetCurrentTab().Should().ContainAny("dcimliveguide.com");
            player.Wait(2);

            //Step 7

            //Step 8
            LogOutWithoutLogin();
        }


        [TestCase]//Test case #1983
        [Category("All")]
        [Description("Test case #1983")]
        public void CopyPlaylist()
        {
            //step1
            Login();
            //step 2 search for any  existing playlist and select copy icon (dbl papers)            
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            IWebElement playlistSearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistSearchInput.SendKeys("Automated");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement playlistCopyButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.copyPlaylistButtonCssSelector));
            playlistCopyButton.Click();
            //step 3 select save
            IWebElement saveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton.Click();
            //step 4 refresh screen
            _driver.Value.Navigate().Refresh();
            //step 5 confirm that the new copy created contains the following from the original playlist:Name remains with (Copy 1) at the end of playlist name time stamp will vary channel tags and estimated duration will carry over
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement playlistsSideBarMenuButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton2.Click();
            WaitForMaskModal();
            IWebElement playlistSearchInput2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistSearchInput2.SendKeys("Automated");
            IWebElement playlistCopyName = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistCopyTitleNameCssSelector));

            WaitForMaskModal();
            IWebElement playlistCopyName1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistCopyTitleNameCssSelector));

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            Assert.IsTrue(playlistCopyName1.Text.Contains("Copy"));
            //step 6 select copy icon for the same copied playlist (copy 1) form above
            IWebElement playlistCopySearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistCopySearchInput.SendKeys("Copy");
            WaitForMaskModal();
            IWebElement playlistCopyButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.copyPlaylistButtonCssSelector));
            playlistCopyButton1.Click();
            //step 7 select save
            IWebElement saveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton1.Click();
            //step 8 confirm that the new copy created contains the following from the original playlist;Name remains with copy 2 at end of playlist name, time stamp will vary channel tags estimated duration will carry over
            IWebElement playlistCopy2SearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistCopy2SearchInput.SendKeys("Copy");
            WaitForMaskModal();
            //Step 9
            LogOutWithoutLogin();


        }

        [TestCase]//Test case 1986
        [Category("All")]
        [Description("Test case 1986")]
        public void PlaylistFilters()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 1 signin
            Login();

            PlayLists pls = new PlayLists(_driver.Value, _configuration);

            //step 2 select filter dropdown box
            IWebElement filterDropDownButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.filterButtonCssSelector));
            var selectfilterDropDownButton = new SelectElement(filterDropDownButton);
            selectfilterDropDownButton.SelectByValue("date");
            //step 3 spell check all filter values from drop down box
            //TODO: Need check the playlist order and assert if there is an error.
            pls.Wait();

            //step 4 select the alphabetical option 
            IWebElement filterDropDownButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.filterButtonCssSelector));
            var selectfilterDropDownButton1 = new SelectElement(filterDropDownButton1);
            selectfilterDropDownButton1.SelectByValue("alphabetically");
            pls.Wait();
            //step 5 confirm all the playlist display in alphabetical ascending order
            //TODO: Need check the playlist order and assert if there is an error.
            //step 6 refresh screen
            SeleniumCommon.RefreshPage(_driver.Value);
            //step 7 confirm all playlist display in date added order
            //pls.Wait();
            
            //Step 13 logout
            LogOutWithoutLogin();
        }

       
        [TestCase]// test case 1988
        [Category("All")]
        [Description("Test case 1988")]
        public void PlaylistDisplayAsRow()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 1 login
            Login();
            //step 2 hover over grid icon
            IWebElement rowIcon = _driver.Value.FindElement(By.CssSelector(BaseStrings.rowOpenButtonCssSelector));
            Actions action = new Actions(_driver.Value);
            action.MoveToElement(rowIcon).Perform();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 3 select grid icon
            rowIcon.Click();
            //step 4 use scroll bar to navigate through all rolls

            //step 5 select any of the playlist
            IWebElement playlistSearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistSearchInput.SendKeys("Automated");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement openButton = _driver.Value.FindElement(By.XPath(BaseStrings.gridOpenButtonCssSelector));
            openButton.Click();
            //step 6 select playlists from main menu
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            //step 7 select grid icon
            IWebElement gridIcon1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.gridIconButtonCssSelector));
            gridIcon1.Click();
            //step 8 confirm that all the data that appears for a grid's playlist, is the same data that appears when a row is selected (3 horizontal lines) select row icon
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 9 logout
            LogOutWithoutLogin();
        }

        [TestCase]//TestCase 1989
        [Category("All")]
        [Description("TestCase 1989")]
        public void PlaylistSearchBox()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            //step 1 signin
            Login();
            PlayLists pls = new PlayLists(_driver.Value, _configuration);
            //step 2 place curser in search box
            IWebElement playlistSearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            //step 3 Spell check Search box placeholder text
            pls.SearchField = "123456";
            //step 4 Enter invalid names (only Playlist names are considered valid entries) in Search box
            //playlistSearchInput.SendKeys("123456"); old code
            //step 5 Enter spaces in Search box
            pls.SearchField = null;
            //playlistSearchInput.Clear(); old code
            // playlistSearchInput.SendKeys("     "); old code
            pls.SearchField = "     ";
            //step 6 Enter a valid Playlist name (only Playlist names are considered valid entries) in Search box
            // playlistSearchInput.Clear(); old code
            pls.SearchField = null;
            pls.SearchField = "Automated";
            //playlistSearchInput.SendKeys("Automated"); old code
            //step 7 Enter a valid Playlist name that consist of letters, numbers and special characters
            //playlistSearchInput.Clear();old code
            pls.SearchField = null;
            // playlistSearchInput.SendKeys("Automated Playlist Test 9/6/2018");//old code // failing here, because the search functionality will search for playlist as an when its typed

            pls.SearchField = "test 05-";
            //step 8 From Search box, remove text from test step 6 above
            // playlistSearchInput.Clear(); old code
            pls.SearchField = null;
            //step 9 logout
            LogOutWithoutLogin();
        }


        [TestCase]//Test case 2147
        [Category("All")]
        [Description("Test case 2147")]
        public void CBAM_PlayerPagination()
        {
            //Step 1
            Login();
            //Step 2 
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector("#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li:nth-child(3) > a > span"));
            playersTab.Click();
            WaitForMaskModal();
            //Step 3
            /*Scroll to bottom of page (right corner) to view Pagination - Players Pagination will  display, but only if more than 15 players exists will it become selectable, if less than 15 players exist, the pagination buttons will be unselectable. The remainder of this test case cannot be executed, if less than 15 players exists, resume w/this test case only if applicable. */

            //Step 4 
            //Select various numbered pages (Ex: 1, 5 , 50, 100, 500...)

            //Step 5
            //Select NEXT various times

            //Step 6 
            //Select PREVIOUS times

            //Step 7
            LogOutWithoutLogin();
        }

        [TestCase] //test case 2150
        [Category("All")]
        [Description("Test case 2150")]
        public void CBAM_AssetsSearchBox()
        {
            //Step 1
            Login();

            //Step 2
            SideBar sb = new SideBar(_driver.Value, _configuration);

            sb.SelectMenu("assets");
            sb.Wait();

            //TODO: Assert that we are on the correct page. 

            //Step 3
            Assets assetPage = new Assets(_driver.Value, _configuration);
            
            //TODO: Move this method to a property in order to get and set data.
            //IWebElement assetSearchInput = _driver.Value.FindElement(By.Id("assets-search"));
            IWebElement assetSearchInput = assetPage.GetAssetSearchInput();
            assetSearchInput.Click();

            //step 4
            //spell check placeholder text

            //step 5 
            assetSearchInput.SendKeys("     ");
            assetPage.Wait();

            //TODO: Assert here to validate items returned.
        }
        private void DetermineErrorInput(IEnumerable<KeyValuePair<bool, string>> fieldList, string field)
        {
            foreach (KeyValuePair<bool, string> error in fieldList)
            {
                bool b = error.Key;
                string s = error.Value;
                b.Should().BeTrue();
                s.Should().Be(field);
            }
        }

        [TestCase]//TestCase 1991
        [Category("All")]
        [Description("TestCase 1991")]
        public void PlaylistMyProfile()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));

            EnvironmentTestData ta = _testDataConfiguration.GetDataFromFiles("TestMyProfile.json", "Test1991.json");
            //step 1 sign in
            Login();

            IWebElement playerChannelDropdown = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));
            playerChannelDropdown.Click();
            ClientMenu cm = new ClientMenu(_driver.Value, _configuration);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            cm.SelectClient("My Profile");


            cm.Wait(3);
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            // cm.GetClientMenuItem("GM");
            //cm.SelectClient("GM");
            cm.SelectClient("My Profile");

            //step 3 Spell check all values from dropdown box
            cm.Wait();
            //step 4 Select My Profile myProfileButtonCssSelector
            //step 5 Spell check all content (fields/values, buttons), (including placeholder text : to do)

            MyProfile mp = new MyProfile(_driver.Value, _configuration);
            List<string> tagsToCompare1 = _testDataConfiguration.ConvertStringToList(ta.TestAnswers["tagsToCompare"]) ;
           // mp.GetMyProfileFields.Should().Contain(tagsToCompare1);
            mp.GetMyProfileFields.Should().Contain(tagsToCompare1).And.HaveCount(tagsToCompare1.Count);

            mp.IsEmailFieldExists.Should().BeTrue("Email field does not exists");//verify email field

            //step 6 Select Save button
            mp.ClickSaveButton();

            //step 7 Enter any First Name (there are not edit checks in place) and select Save\
          
            mp.ProfileFirstNameTextField = null;
            mp.ProfileFirstNameTextField = ta.TestAnswers["FirstName"];
            mp.ClickSaveButton();
            
            mp.ProfileLastNameTextField = null;
            mp.ProfileLastNameTextField = ta.TestAnswers["LastName"];          
            mp.ClickSaveButton();
            
            mp.ProfileTitleTextField = null;
            mp.ProfileTitleTextField = ta.TestAnswers["Title"]; 
            mp.ClickSaveButton();     
           
            mp.ProfileDirectPhoneTextField = null;//just to clear previous data to perform following validations
            mp.ProfileDirectPhoneTextField = ta.TestAnswers["InvalidDirectPhoneNumber1"]; //invalid direct phone number
            mp.ClickSaveButton();
            DetermineErrorInput(mp.IsErrorInput, "Direct Phone Number");
            //  DetermineErrorInput(mp.IsErrorInput.Select(a => a.Key).ToList(), "Direct Phone Number");
                        
            mp.ProfileDirectPhoneTextField = null;       
            mp.ProfileDirectPhoneTextField = ta.TestAnswers["InvalidDirectPhoneNumber2"]; //invalid direct phone number
            mp.ClickSaveButton();

            // verify the key and value returned
            DetermineErrorInput(mp.IsErrorInput, "Direct Phone Number");
            //  DetermineErrorInput(mp.IsErrorInput.Select(a => a.Key).ToList(), "Direct Phone Number");
                       
            // mp.IsErrorInput.Should().BeTrue("Error not found");
            mp.ProfileDirectPhoneTextField = null;
            //mp.ClearDirectPhoneTextbox();
            mp.ProfileDirectPhoneTextField = ta.TestAnswers["ValidDirectPhoneNumber"]; //valid direct phone number
            mp.ClickSaveButton();
          
            mp.ProfileMobileNumberTextField = null;
            mp.ProfileMobileNumberTextField = ta.TestAnswers["InvalidMobileNumber1"];//invalid mobile number
            //  mp.ProfileDirectPhoneTextField = "error";
            mp.ClickSaveButton();
            // verify the key and value returned
            DetermineErrorInput(mp.IsErrorInput, "Mobile Number");
            // DetermineErrorInput(mp.IsErrorInput.Select(a => a.Key).ToList(), "Mobile Number");
           
            // mp.IsErrorInput.Should().BeTrue("Error not found");
            mp.ProfileMobileNumberTextField = null;
            

            mp.ProfileMobileNumberTextField = ta.TestAnswers["InvalidMobileNumber2"]; //invalid mobile number
            mp.ClickSaveButton();
            // verify the key and value returned
            DetermineErrorInput(mp.IsErrorInput, "Mobile Number");
            //  DetermineErrorInput(mp.IsErrorInput.Select(a => a.Key).ToList(), "Mobile Number");
            //  mp.IsErrorInput.Should().BeTrue("Error not found");
                       
            mp.ProfileMobileNumberTextField = null;         
            mp.ProfileMobileNumberTextField = ta.TestAnswers["ValidMobileNumber"]; // valid mobile number
            mp.ClickSaveButton();            
                      
            mp.ProfileStreetAddressTextField = null;
            mp.ProfileStreetAddressTextField = ta.TestAnswers["StreetAddress"]; 
            mp.ClickSaveButton();

            mp.ProfileCityTextField = null;
            mp.ProfileCityTextField = ta.TestAnswers["City"]; 
            mp.ClickSaveButton();
          
            mp.ProfileStateTextField = null;
            mp.ProfileStateTextField = ta.TestAnswers["State"]; 
            mp.ClickSaveButton();
           
            mp.ProfileZipTextField = null;
            mp.ProfileZipTextField = ta.TestAnswers["InvalidZip1"]; //invalid zip code data
            mp.ClickSaveButton();
            // verify the key and value returned
            DetermineErrorInput(mp.IsErrorInput, "Zip");
            //  DetermineErrorInput(mp.IsErrorInput.Select(a => a.Key).ToList(), "Zip");

            //  mp.IsErrorInput.Should().BeTrue("Error not found");
            
            mp.ProfileZipTextField = null;
            mp.ProfileZipTextField = ta.TestAnswers["InvalidZip2"]; //invalid zip code data
            mp.ClickSaveButton();

            // verify the key and value returned
            DetermineErrorInput(mp.IsErrorInput, "Zip");
            // DetermineErrorInput(mp.IsErrorInput.Select(a => a.Key).ToList(), "Zip");
            // mp.IsErrorInput.Should().BeTrue("Error not found");

            mp.ProfileZipTextField = null;
            mp.ProfileZipTextField = ta.TestAnswers["ValidZip"]; //valid data
            mp.ClickSaveButton();

            mp.IsSaveEnabledAfterSaved.Should().BeTrue("Save button still not enabled");//if save button is enabled then save is success, currently there is no success message displayed by application 

            LogOutWithoutLogin();


        }
        [TestCase]
        // [Category("All")]
        [Description("Test")]
        // RK 2/15/19 - Change the name to something like TestPlayerPage
        // I understand that this test was just to debug the player, but it is useful to just go through the page for like a smoke test or something.
        public void ToDebugPlayers()
        {
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(13));
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFile("TestData.json");
            Login();

            SideBar sb = new SideBar(_driver.Value, _configuration);
            sb.SelectMenu("Players");

            Players player = new Players(_driver.Value, _configuration);
            PlayerDetail playerDetail = new PlayerDetail(_driver.Value, _configuration);
            PlayerPlaylistWhatsPlaying pwp = new PlayerPlaylistWhatsPlaying(_driver.Value, _configuration);
            PlayerPlaylistInfo ppl = new PlayerPlaylistInfo(_driver.Value, _configuration);
            PlayerConfigureChannel pc = new PlayerConfigureChannel(_driver.Value, _configuration);
            var playerName = td.TestAnswers["Player"]; //  _envData.Player;
            player.Wait(1);
            player.SelectPlayer(playerName);
            player.Wait(2);

            playerDetail.SelectTab("CONFIGURE");

            string[] tagsToCompare = new[] { "Select A Channel", "Chevy-Buick-GMC TV Chevy, Buick & GMC Lounge TV" };
            pc.GetChannelFields.Should().Contain(tagsToCompare).And.HaveCount(tagsToCompare.Length);

            playerDetail.SelectTab("PLAYLISTS");


            player.Wait(2);

            // RK - 2/20/19 - Need to rethink these asserts because they might change because of the dates. 
            string[] tagsToComparePI = new[] { "Name: Rob Expire items", "Id: 69a84aff-3155-417f-97b9-658087a9df29", "Schedule: January 2, 2019 - February 1, 2019 12:00 am - 11:59 pm", "Author: RKos@dciartform.com", "Created: 11/2/2018 12:20:36 AM", "Updated: 1/31/2019 10:30:53 AM", "Estimated Duration: 00:11:17", "4", "2", "3", "7", "1", "1", "Name: Rob Defer Install", "Id: 9bbb9199-34f6-4030-b9bc-45719091c320", "Schedule: January 30, 2019 - January 30, 2023 12:00 am - 11:59 pm", "Author: RKos@dciartform.com", "Created: 1/31/2019 2:46:00 AM", "Updated: 2/13/2019 2:14:37 AM", "Estimated Duration: 00:01:45", "1", "1", "1", "Name: Scott Widget Test", "Id: 56d7406b-ef9b-48ed-b27e-4d7f83b15503", "Schedule: October 16, 2018 - October 18, 2018 12:00 am - 11:59 pm", "Author: lamers@dciartform.com", "Created: 10/16/2018 7:39:36 PM", "Updated: 2/15/2019 3:45:41 AM", "Estimated Duration: 00:03:15", "2", "2", "1" };
            //ppl.GetPlaylistInfo.Should().Contain(tagsToComparePI).And.HaveCount(tagsToComparePI.Length);

            string[] tagsToCompare2 = new[] { "Rob Expire items", "January 2, 2019 - February 1, 2019 12:00 am - 11:59 pm", "brand: Brand", "traffic: Traffic", "video: 0f6ee191-1127-47a2-89d3-5b397abc90c6 (1)...", "image: Lighthouse.jpg", "weather: Weather", "traffic: Traffic", "screenfeedvideo: WeatherNational", "screenfeedvideo: ReutersNews", "screenfeedvideo: Engadget", "weather: Weather", "traffic: Traffic", "screenfeedvideo: Destinations", "screenfeedvideo: BestBites", "screenfeedvideo: APNews", "image: Landing", "screenfeedvideo: BestBites (Copy 1)", "image: 878455-code-geass-simple-background.jpg", "image: ISS-Rim.jpg", "Rob Defer Install", "January 30, 2019 - January 30, 2023 12:00 am - 11:59 pm", "image: ISS-Rim.jpg", "traffic: Traffic", "weather: Weather", "Scott Widget Test", "October 16, 2018 - October 18, 2018 12:00 am - 11:59 pm", "brand: Brand", "traffic: Traffic", "weather: Weather", "traffic: Traffic", "weather: Weather" };
            pwp.GetWhatsPlayingFields.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Length);

            PlayerGeneralDevice pd = new PlayerGeneralDevice(_driver.Value, _configuration);

            playerDetail.SelectTab("GENERAL");
            string[] tagsToCompare3 = new[] { "Ping Device", "Refresh App", "Restart App", "Restart Device" };
            pd.GetDeviceButtons.Should().Contain(tagsToCompare3).And.HaveCount(tagsToCompare3.Length);

            string[] strArrayKey = pd.GetDeviceFields.Select(x => (x.Key)).ToArray();
            string[] strArrayValue = pd.GetDeviceFields.Select(x => (x.Value)).ToArray();
            string[] tagsToCompareKey = new[] { "Last Online:", "Model", "Computer Name:", "Ping Data:", "OS / Version", "Memory / Hard Drive", "Hard Line / Wifi Connected", "IP Address" };
            string[] tagsToCompareValue = new[] { "2/15/2019 5:03:18 AM", "GA-N3050N", "LG-QAROB", "--", "Linux: Ubuntu 18.04 (bionic)", "1.8GB / 58GB (69% free)", "Connection: Ethernet", "216.206.142.2" };
            // RK - 2/20/19 - Need to rethink these asserts because they might change because of the dates. 

            // strArrayKey.Should().Contain(tagsToCompareKey).And.HaveCount(tagsToCompareKey.Length);
            //strArrayValue.Should().Contain(tagsToCompareValue).And.HaveCount(tagsToCompareValue.Length);

            PlayerGeneralInstalledPrograms pgd = new PlayerGeneralInstalledPrograms(_driver.Value, _configuration);
            string[] tagsToCompareD3 = new[] { "LCC", "LGP", "WidgetWeather", "WidgetImage", "WidgetTraffic", "WidgetVideo", "WidgetBrand", "WidgetStatusBoard", "WidgetAdBuilder", "WidgetPricing" };
            pgd.GetInstalledProgramsLabel.Should().Contain(tagsToCompareD3).And.HaveCount(tagsToCompareD3.Length);

            PlayerGeneralTags pt = new PlayerGeneralTags(_driver.Value, _configuration);

            string[] tagsToCompare4 = new[] { "Standard Tags", "Admin Tags", "System Tags", "", "", "" };
            pt.GetSystemGeneratedListOfTags.Should().Contain(tagsToCompare4).And.HaveCount(tagsToCompare4.Length);

            PlayerGeneralLocationDetails pld = new PlayerGeneralLocationDetails(_driver.Value, _configuration);

            string[] tagsToCompare5 = new[] { "Phone:", "Dealer Code :", "" };
            pld.GetPlayerLocationDetailsFields.Should().Contain(tagsToCompare5).And.HaveCount(tagsToCompare5.Length);

            PlayerGeneralInformation pgi = new PlayerGeneralInformation(_driver.Value, _configuration);
            // pgi.PlayerInformationEditButtonClick();
            pgi.Wait(2);

            string[] tagsToCompare6 = new[] { "Player Name: LG-QAROB", "Description:", "ID:", "Product:", "Subscription:", "License Key:", "Expiration Date:", "Zone:", "Last Online:", "Created:", "Updated:", "Registered:", "Registered By:", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            pgi.GetPlayerInformationFields.Should().Contain(tagsToCompare6).And.HaveCount(tagsToCompare6.Length);

            //  pgi.PlayerModal.PlayerNameTextField = "ds";
            //pgi.PlayerModal.SelectZoneSelectBox = "Service";
            //pgi.PlayerModal.ModalCancelButtonClick();
            // pgi.PlayerModal.ModalContinueButtonClick();
            //  pgi.PlayerModal.ModalSaveButtonClick();

        }
        [TestCase]
        [Category("Pending")]
        [Description("TestCase 2138")]
        public void CBAMPlayerSearchBox()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFiles("TestPlayers.json", "Test2138.json");

      
            Login();
            SideBar sb = new SideBar(_driver.Value, _configuration);         
            Players player = new Players(_driver.Value, _configuration);

            sb.SelectMenu(td.TestAnswers["PlayerMenu"]);
            player.Wait(9);

            string stringBefore = null;
            foreach (var recordCount in player.GetTotalRecordCount)
            {
                stringBefore = recordCount.ToString();
            }
            player.Wait(2);
            char countBefore = stringBefore[0]; //get the first char 

            player.SearchBox = td.TestAnswers["InvalidPlayerName"];// invalid player name
            player.Wait(3);

            List<string> tagsToCompare = _testDataConfiguration.ConvertStringToList(td.TestAnswers["tagsToCompare"]);

            player.GetNoMatchingRecordText.Should().Contain(tagsToCompare).And.HaveCount(tagsToCompare.Count);

          //  string[] tagsToCompare = new[] { "No matching records" };
          //  player.GetNoMatchingRecordText.Should().Contain(tagsToCompare).And.HaveCount(tagsToCompare.Length);//validate after invalid data entered
            player.Wait();

            player.SearchBox = null;
            player.SearchBox = td.TestAnswers["PlayerName"];//valid player name
            // player.SearchBox = "LG-QAROB";//valid player name
            player.Wait(7);
            player.PlayerName = "LG-QAROB";//send data to getter and setter property, it is used in xpath under GetPlayerDisplayed method
            //string[] tagsToCompare1 = new[] { "LG-QAROB" };
            List<string> tagsToCompare2 = _testDataConfiguration.ConvertStringToList(td.TestAnswers["tagsToCompare2"]);
           // player.GetPlayerNameDisplayed.Should().Contain(tagsToCompare1).And.HaveCount(tagsToCompare1.Length);//validate player is shown after search
            player.GetPlayerNameDisplayed.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Count);//validate player is shown after search
            player.Wait(2);

            player.SearchBox = null;
            player.SearchBox = td.TestAnswers["LocationName"];//search by location
            player.GetPlayerNameDisplayed.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Count);//validate player is shown after search

            player.SearchBox = null;
            player.SearchBox = td.TestAnswers["zoneName"];//search by zone
            player.GetPlayerNameDisplayed.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Count);//validate player is shown after search

            player.SearchBox = null;
            player.SearchBox = td.TestAnswers["ClientProgram"];//search by client program
            player.GetPlayerNameDisplayed.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Count);//validate player is shown after search

            player.SearchBox = null;
            player.SearchBox = td.TestAnswers["Subscription"];//search by subscription
            player.GetPlayerNameDisplayed.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Count);//validate player is shown after search

            player.SearchBox = null;
            player.SearchBox = td.TestAnswers["LastOnline"]; //search by last online
            player.GetPlayerNameDisplayed.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Count);//validate player is shown after search

            player.SearchBox = null;
            player.Wait(4);
            string stringAfter = null;
            foreach (var recordCount in player.GetTotalRecordCount)
            {
                stringAfter = recordCount.ToString();
            }
            player.Wait(2);
            //char countAfter = stringBefore[0];  //get the first char          
            stringAfter.Should().BeEquivalentTo(stringBefore);

            LogOutWithoutLogin();
        }

        [TestCase]
        [Category("Pending")]
        [Description("TestCase 2295")]
        public void CBAMPlayerGeneralLocationDetailsCard()
        {

            Login();
             EnvironmentTestData td = _testDataConfiguration.GetDataFromFiles("TestPlayers.json", "Test2295.json");
            
            Players player = new Players(_driver.Value, _configuration);        
            SideBar sb = new SideBar(_driver.Value, _configuration);
            PlayerGeneralLocationDetails playerGeneralLocationDetails = new PlayerGeneralLocationDetails(_driver.Value,_configuration);
            //PlayerGeneralLocationDetails pldPlayerGeneralLocationDetails = new PlayerGeneralLocationDetails(_driver.Value, _configuration);
            LocationDetail locationDetail = new LocationDetail(_driver.Value,_configuration);
            PlayerDetail PlayerDetail = new PlayerDetail(_driver.Value,_configuration);

            sb.SelectMenu(td.TestAnswers["PlayerMenu"]);
            
            player.Wait(9);

            player.SearchBox = td.TestAnswers["PlayerName"];
            player.Wait(7);
            player.SelectPlayer(td.TestAnswers["PlayerName"]);
            player.Wait(2);

            //validate player details title
            PlayerDetail.PlayerDetailTitle = td.TestAnswers["PlayerDetailTitle"];
            PlayerDetail.IsPageTitle.Should().BeTrue();

            //validate the location details fields
            List<string> tagsToCompare = _testDataConfiguration.ConvertStringToList(td.TestAnswers["tagsToCompare"]);
            playerGeneralLocationDetails.GetPlayerLocationDetailsFields.Should().Contain(tagsToCompare).And.HaveCount(tagsToCompare.Count);
            
            
            //playerGeneralLocationDetails.LocationName = td.TestAnswers["LocationName"];//set value of location name to be clicked on
            playerGeneralLocationDetails.ClickLocation(td.TestAnswers["LocationName"]);
            player.Wait();

            //validate location name KUDICK CHEVROLET BUICK navigated to
            locationDetail.locationName = td.TestAnswers["LocationName"];
            List<string> tagsToCompare2 = _testDataConfiguration.ConvertStringToList(td.TestAnswers["LocationName"]);
            locationDetail.GetLocationName.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Count);

            sb.SelectMenu(td.TestAnswers["PlayerMenu"]);
            player.SelectPlayer(td.TestAnswers["PlayerName"]);
            PlayerDetail.IsPageTitle.Should().BeTrue();

            playerGeneralLocationDetails.ClickLocation(td.TestAnswers["LocationName"]);

            //Step 10 under review

            locationDetail.BackNavigation();
            PlayerDetail.Wait(2);
            PlayerDetail.IsPageTitle.Should().BeTrue();

            LogOutWithoutLogin();
        }

        [TestCase]
        [Category("Pending")]
        [Description("TestCase 2293")]
        public void CBAMPlayerGeneralEditPlayerInformationCard()
        {
            EnvironmentTestData td = _testDataConfiguration.GetDataFromFiles("TestPlayers.json", "Test2293.json");
         
            Login();
            SideBar sb = new SideBar(_driver.Value, _configuration);
            sb.SelectMenu(td.TestAnswers["PlayerMenu"]);
            Players player = new Players(_driver.Value, _configuration);
            PlayerGeneralInformation playerGeneralInformation = new PlayerGeneralInformation(_driver.Value, _configuration);
            player.Wait(6);
            player.SearchBox = td.TestAnswers["PlayerName"];
            player.Wait(7);
            player.SelectPlayer(td.TestAnswers["PlayerName"]);
            
            playerGeneralInformation.Wait(1);
            playerGeneralInformation.PlayerInformationEditButtonClick();
            playerGeneralInformation.Wait(2);
            playerGeneralInformation.PlayerInformationModal.IsModalDisplay.Should().BeTrue();

            List<string> tagsToCompare = _testDataConfiguration.ConvertStringToList(td.TestAnswers["PlayerGeneralInformationFields"]);
            playerGeneralInformation.GetPlayerInformationFields.Should().Contain(tagsToCompare).And.HaveCount(tagsToCompare.Count);
                      
            playerGeneralInformation.PlayerInformationModal.ModalCancelButtonClick();
            playerGeneralInformation.Wait(1);
            playerGeneralInformation.PlayerInformationModal.IsModalDisplay.Should().BeFalse();

            playerGeneralInformation.Wait(1);
            playerGeneralInformation.PlayerInformationEditButtonClick();
            playerGeneralInformation.Wait(2);
            playerGeneralInformation.PlayerInformationModal.IsModalDisplay.Should().BeTrue();

            playerGeneralInformation.PlayerInformationModal.PlayerNameTextField = null;
            playerGeneralInformation.PlayerInformationModal.PlayerNameTextField = td.TestAnswers["InvalidPlayerName"];
            playerGeneralInformation.PlayerInformationModal.ClickModalSubmitButton("Save");
            playerGeneralInformation.Wait(3);

            string toCompareAfterNameChange = td.TestAnswers["ToCompareAfterNameChange"];
            string afterPlayerNameChange = playerGeneralInformation.GetPlayerInformationFields.FirstOrDefault().ToString();
            afterPlayerNameChange.Should().Contain(toCompareAfterNameChange).And.HaveLength(toCompareAfterNameChange.Length);//validate after name of player is changed
            
            playerGeneralInformation.Wait(1);
            playerGeneralInformation.PlayerInformationEditButtonClick();
            playerGeneralInformation.Wait(2);
            playerGeneralInformation.PlayerInformationModal.IsModalDisplay.Should().BeTrue();
            List<string> tagsToCompare2 = _testDataConfiguration.ConvertStringToList(td.TestAnswers["PlayerDescriptionPlaceHolder"]);
            playerGeneralInformation.PlayerInformationModal.GetDescriptionPlaceHolder.Should().Contain(tagsToCompare2).And.HaveCount(tagsToCompare2.Count);

            playerGeneralInformation.Wait(2);
            playerGeneralInformation.PlayerInformationModal.PlayerDescriptionTextField = td.TestAnswers["PlayerDescription"];
            playerGeneralInformation.PlayerInformationModal.ClickModalSubmitButton("Save");
            playerGeneralInformation.Wait(3);

            playerGeneralInformation.PlayerInformationEditButtonClick();
            playerGeneralInformation.Wait(2);
            playerGeneralInformation.PlayerInformationModal.IsModalDisplay.Should().BeTrue();
            List<string> tagsToCompare3 = _testDataConfiguration.ConvertStringToList(td.TestAnswers["DisableFieldsValue"]);
            playerGeneralInformation.PlayerInformationModal.GetLockedFields.Should().Contain(tagsToCompare3).And.HaveCount(tagsToCompare3.Count);
            playerGeneralInformation.PlayerInformationModal.ClickOffScreen();
            
            playerGeneralInformation.Wait(1);
            playerGeneralInformation.PlayerInformationEditButtonClick();
            playerGeneralInformation.Wait(2);
            playerGeneralInformation.PlayerInformationModal.IsModalDisplay.Should().BeTrue();

            //set back data to previous state
            playerGeneralInformation.Wait(2);
            //RK 3/6/19 - Not sure why this the description doesn't get cleared out either. That is weird. 
            playerGeneralInformation.PlayerInformationModal.PlayerDescriptionTextField = "";
            playerGeneralInformation.PlayerInformationModal.PlayerNameTextField = null;//not sure why description field is not getting cleared after save
            playerGeneralInformation.PlayerInformationModal.PlayerNameTextField = td.TestAnswers["PlayerName"];
           // playerGeneralInformation.PlayerInformationModal.PlayerDescriptionTextField = "";
            playerGeneralInformation.PlayerInformationModal.ClickModalSubmitButton("Save");
            playerGeneralInformation.Wait(3);
            LogOutWithoutLogin();

        }
        [TestCase]
        //[Category("All")]
        [Description("Test")]
        public void TODebug_Location()
        {
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            Login();

            SideBar sb = new SideBar(_driver.Value, _configuration);
            ClientMenu cm = new ClientMenu(_driver.Value, _configuration);
            cm.SelectClient("GM");
            cm.Wait();
            LocationDetail ld = new LocationDetail(_driver.Value,_configuration);
            Locations loc = new Locations(_driver.Value, _configuration);
            GeneralLocationDetailsSection lds = new GeneralLocationDetailsSection(_driver.Value, _configuration);
            GeneralLicenseAndSubscriptionsSection ls = new GeneralLicenseAndSubscriptionsSection(_driver.Value, _configuration);
            GeneralTags GeneralTags = new GeneralTags(_driver.Value,_configuration);
            ConfigureGeneralSection ConfigureGeneralSection = new ConfigureGeneralSection(_driver.Value,_configuration);
            ConfigureTickersSection tickers = new ConfigureTickersSection(_driver.Value, _configuration);
            ConfigureSalesAppointments sales = new ConfigureSalesAppointments(_driver.Value,_configuration);
            ConfigureSMCSettings smc = new ConfigureSMCSettings(_driver.Value,_configuration);
            GeneralSDSPriceLists GeneralSDSPriceLists = new GeneralSDSPriceLists(_driver.Value, _configuration);
            SalesAppointmentManagementModal SalesAppointmentManagementModal = new SalesAppointmentManagementModal(_driver.Value);
            TagManagementModal TagManagement = new TagManagementModal(_driver.Value);
            ConfigureSDSSettingsSection ConfigureSDSSettingsSection = new ConfigureSDSSettingsSection(_driver.Value,_configuration);
            sb.SelectMenu("Locations");                     
            sb.Wait(6);
            //sb.Wait(3);
            //loc.IsLocationHeader.Should().ContainAny("");            
            String[] tagsToCompareLoc = new[] { "Name", "", "Location Code", "", "# of License", "", "Address", "", "City", "", "State", "", "Zip", "", "Client Programs", "" };
            loc.GetLocationColumn.Should().Contain(tagsToCompareLoc).And.HaveCount(tagsToCompareLoc.Length);

      
           
            loc.SearchBox = "KUDICK CHEVROLET BUICK";//search the location by name 
            loc.Wait(6);
            loc.SelectLocation("KUDICK CHEVROLET BUICK");//currently clicking location name
            lds.Wait(3);
           // ld.GetModals.Should().Contain(new[] { "Name", "Location Code", "# of License", "Address", "City", "State", "Zip", "Client Programs", "" });
            /*  GeneralTags.LocationTagsEditButtonClick();
              GeneralTags.Wait(2);


              // lds.IsLocationSettingsFieldValue.Should().Contain(new[] { "Phone", "Client", "BAC", "Location Code(s)", "Reportable", "Status", "Updated", "Registered", "Registered By", "Sales District Code", "Zone Code", "Region Code", "Dealer Key", "Appointment Date", "Facing Warehouse Code", "Created" });
              TagManagement.TagManagementTextField = "test";
              //ld.LocationModal.TagManagementTextField = "test";

              GeneralTags.AddTagManagementButton();*/
            ld.SelectTab_LocationDetail("CONFIGURE");

            
            sales.SalesAppointmentEditButtonClick();
           
            // ld.SelectConfigureTab_LocationDetail();
            //ld.SelectTab("GENERAL");


            //sales.SalesAppointmentEditButtonClick();
            SalesAppointmentManagementModal.AddGreetingAppointmentModalPlusButton();
            //sales.ClickAddGreetingAppointmentButton();
            // ld.LocationModal.SaleAppointmentCustomerName = "cus";
            ld.Wait(2);
            SalesAppointmentManagementModal.CustomerNameTextBox = "customer";
            SalesAppointmentManagementModal.VehicleTextBox = "customer8989";
            SalesAppointmentManagementModal.SalespersonTextBox = "customer11111";
            SalesAppointmentManagementModal.CustomMessageTextBox = "customer3232";
            ld.Wait(3);
            SalesAppointmentManagementModal.SelectAppointmentDay();
            ld.Wait(2);
            // SalesAppointmentManagement.ClickAppointmentTime();
            SalesAppointmentManagementModal.AppointmentTimeSelectBox = "5:00 am";
            ld.Wait(2);
           
            sales.SaleAppointmentModal.ClickModalSubmitButton("Add");
            sales.Wait(3);

            sales.SaleAppointmentModal.ClickModalSubmitButton("Save");
            sales.Wait(4);
            ld.SelectTab_LocationDetail("GENERAL");
            lds.Wait(2);

            String[] tagsToCompareLDS = new[] { "Phone:", "Client:", "BAC:", "Location Code(s):", "Reportable:", "Status:", "Created:", "Updated:", "Registered:", "Registered By:", "Sales District Code:", "Zone Code:", "Region Code:", "Dealer Key:", "Appointment Date:", "Facing Warehouse Code:" };
            lds.GetLocationDetailsSectionFields.Should().Contain(tagsToCompareLDS).And.HaveCount(tagsToCompareLDS.Length);

            
            lds.LocationDetailsEditButtonClick();
            lds.Wait(2);            
            lds.GeneralLocationDetailsSectionModal.IsModalDisplay.Should().BeTrue();
            String[] tagsToCompare = new[] { "Name", "Address", "City", "State", "Zip", "Primary Phone Number", "Website", "Client", "BAC", "Location Code(s)", "Reportable", "Status", "Sales District Code", "Zone Code", "Region Code", "Dealer Key", "Appointment Date", "Facing Warehouse Code" };
            lds.GetLocationSettingsFieldValue.Should().Contain(tagsToCompare).And.HaveCount(tagsToCompare.Length);


            String[] tagsToCompareTimeStamp = new[] { "Created:", "Registered:", "Updated:", "Registered By:" };
            lds.GetTimeStampFields.Should().Contain(tagsToCompareTimeStamp).And.HaveCount(tagsToCompareTimeStamp.Length);

            lds.GeneralLocationDetailsSectionModal.ModalCancelButtonClick();
            lds.Wait(2);

            //GeneralTags.GetLocationTagsFields.Should().Contain(new[] { "Standard Tags", "Admin Tags Code", "System Tags", "" });
            String[] tagsToCompareTags = new[] { "Standard Tags", "Admin Tags", "System Tags" };
            GeneralTags.GetLocationTagsFields.Should().Contain(tagsToCompareTags).And.HaveCount(tagsToCompareTags.Length);

            GeneralTags.LocationTagsEditButtonClick();
            GeneralTags.Wait(2);


            // lds.IsLocationSettingsFieldValue.Should().Contain(new[] { "Phone", "Client", "BAC", "Location Code(s)", "Reportable", "Status", "Updated", "Registered", "Registered By", "Sales District Code", "Zone Code", "Region Code", "Dealer Key", "Appointment Date", "Facing Warehouse Code", "Created" });
            TagManagement.TagManagementTextField = "test";
            //ld.LocationModal.TagManagementTextField = "test";
            GeneralTags.TagManagementModal.ClickModalSubmitButton("Add");
            GeneralTags.TagManagementModal.ClickOffScreen();
            GeneralTags.Wait(2);
           // GeneralTags.tagManagementModal.ModalCancelButtonClick();
           // GeneralTags.AddTagManagementButton();
           ld.SelectTab_LocationDetail("CONFIGURE");
            // sales.CreateSalesAppointmentsLink();


            String[] tagsToCompareSMC = new[] { "Sales Appointments", "Menu Pricing", "Default Settings" };
            smc.GetSMCSettingsField.Should().Contain(tagsToCompareSMC).And.HaveCount(tagsToCompareSMC.Length);

            String[] tagsToCompareTicker = new[] { "", "", "JB Active Ticker Test", "", "", "JB Inactive Ticker Test", "", "", "JB Active Ticker Lonnnnnnnnnnng Teeeeeeeeest TessssssssT", "", "", "JB Active Ticker Looooonnnnnnnnnnnger TeeeeeeeeeesT Strrrrrrrrrrrrriiiiiiiiiing" };
            tickers.GetTickersFields.Should().Contain(tagsToCompareTicker).And.HaveCount(tagsToCompareTicker.Length);

            ConfigureGeneralSection.WelcomeMessageTextField = "Automation message";
            ls.Wait(2);

            ld.SelectTab_LocationDetail("GENERAL");

            ls.Wait(3);

            String[] tagsToCompareTickerLic = new[] { "", "", "License Key", "Client Program", "Zone", "Subscription", "Registration Date", "Expires", "Player Name", "" };
            ls.GetLicensesAndSubscriptionsFields.Should().Contain(tagsToCompareTickerLic).And.HaveCount(tagsToCompareTickerLic.Length);




            // lds.LocationDetailsEditButtonClick();           
            // ld.SelectConfigureTab_LocationDetail();
            // ld.SelectGeneralTab_LocationDetail();
            loc.Wait(2);


        }

        [TestCase]
        //[Category("All")]
        [Description("Test")]
        public void TODebug_Location2()
        {
           // System.Threading.Thread.Sleep(TimeSpan.FromSeconds(11));
            Login();
            ClientMenu cm = new ClientMenu(_driver.Value, _configuration);
            cm.SelectClient("Subaru");
            cm.Wait();
            SideBar sb = new SideBar(_driver.Value, _configuration);
            Locations loc = new Locations(_driver.Value, _configuration);
            LocationDetail ld = new LocationDetail(_driver.Value, _configuration);
            GeneralSDSPriceLists GeneralSDSPriceLists = new GeneralSDSPriceLists(_driver.Value,_configuration);
            ConfigureSMCSettings smc = new ConfigureSMCSettings(_driver.Value, _configuration);
            ConfigureSalesAppointments sales = new ConfigureSalesAppointments(_driver.Value, _configuration);
            AccessoryPricingModal AccessoryPricingModal = new AccessoryPricingModal(_driver.Value);
            TickerManagementModal TickerManagementModal = new TickerManagementModal(_driver.Value);
            ConfigureTickersSection ConfigureTickersSection = new ConfigureTickersSection(_driver.Value,_configuration);
            ConfigurePriceListsSection ConfigurePriceListsSection = new ConfigurePriceListsSection(_driver.Value,_configuration);
            ConfigureSDSSettingsSection ConfigureSDSSettingsSection = new ConfigureSDSSettingsSection(_driver.Value, _configuration);
            ConfigureServiceAppointments ConfigureServiceAppointments = new ConfigureServiceAppointments(_driver.Value,_configuration);
            sb.SelectMenu("Locations");
            sb.Wait(6);
            loc.SearchBox = "Sommer's Subaru";//search the location by name 
            loc.Wait(7);
            loc.SelectLocation("Sommer's Subaru");//currently clicking location name
            loc.Wait(2);
            ld.SelectTab_LocationDetail("CONFIGURE");
            loc.Wait(2);

            String[] tagsToCompareTicker = new[] { "", "", "", "General", "Price Lists", "SDS Price Lists", "Tickers", "Service Appointments", "SMC Settings", "SDS Settings" };
            ld.GetModals.Should().Contain(tagsToCompareTicker).And.HaveCount(tagsToCompareTicker.Length);



            ConfigureServiceAppointments.ServiceAppointmentEditButtonClick();
            ConfigureServiceAppointments.Wait(2);
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.AddGreetingAppointmentModalPlusButton();
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.CustomerNameTextBox = "name1";
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.MakeTextBox = "111e";
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.ModelTextBox = "rx";
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.AdvisorTextBox = "testPerson";
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.SelectAppointmentDay();
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.CustomMessageTextBox = "automation test";
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.AppointmentTimeSelectBox="5:00 am";
            //ConfigureServiceAppointments.ServiceAppointmentManagementModal.YearSelectBox("1962");
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.YearSelectBox("1965");
            ConfigureServiceAppointments.Wait(2);
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.ClickModalSubmitButton("Add");
            ConfigureServiceAppointments.Wait(4);
            ConfigureServiceAppointments.ServiceAppointmentManagementModal.ClickModalSubmitButton("Save");
            ConfigureServiceAppointments.Wait(4);

            String[] tagsToCompareSDS = new[] { "Financial Calculator", "Display Accessory MSRP Pricing", "Default Settings", "BCC Email:" };
            ConfigureSDSSettingsSection.GetSDSSettingsTexts.Should().Contain(tagsToCompareSDS).And.HaveCount(tagsToCompareSDS.Length);

            ConfigureSDSSettingsSection.BCCEmailTextField = null;
            ConfigureSDSSettingsSection.BCCEmailTextField = "brodsko@dciartform.com";
            ConfigureSDSSettingsSection.Wait(3);

            ConfigurePriceListsSection.SelectPriceListsOption ( "Edit Default SMC Service Price List");
            ConfigurePriceListsSection.Wait(2);
            ConfigurePriceListsSection.PriceListModal.AddCustomPricePlusButton();
            ConfigurePriceListsSection.PriceListModal.CustomPriceDescriptionText("automation test");
            ConfigurePriceListsSection.PriceListModal.CustomPriceInputText("500.00");
            ConfigurePriceListsSection.PriceListModal.ClickModalSubmitButton("Save");
            ConfigurePriceListsSection.Wait(4);
            ConfigureTickersSection.TickersEditButtonClick();
            ConfigureTickersSection.Wait(2);
            ConfigureTickersSection.TickerManagementModal.AddTickerItemModalPlusButton();
            ConfigureTickersSection.Wait(2);
            ConfigureTickersSection.TickerManagementModal.TickerFeedText("ticker automation");
            //ConfigureTickersSection.tickerManagementModal.TickersTextField = "test";
            ConfigureTickersSection.TickerManagementModal.ClickSaveTickerManagementButton();
            ConfigureTickersSection.Wait(3);

            GeneralSDSPriceLists.SelectPriceOption("Edit SDS Price Options");
            loc.Wait(2);
            GeneralSDSPriceLists.GeneralSDSPriceListsModal.AccessoryPricingCheckboxValue = "factory";
            GeneralSDSPriceLists.GeneralSDSPriceListsModal.SelectCheckbox = true;
            // GeneralSDSPriceLists.GeneralSDSPriceListsModal.AccessoryPricing = "kit";
            // GeneralSDSPriceLists.GeneralSDSPriceListsModal.FactoryCheckbox = true;
            GeneralSDSPriceLists.GeneralSDSPriceListsModal.ClickModalSubmitButton("Save");
            GeneralSDSPriceLists.Wait(2);
            //GeneralSDSPriceLists.GeneralSDSPriceListsModal.ClickSaveCancelSaleAppointmentButton("Save");
            //AccessoryPricingModal.IsModalDisplay.Should().BeTrue();
            //smc.IsSMCSettingsField.Should().Contain(new[] { "Default Settings", "Custom Ads", "Dare to Compare (Competitive Pricing)", "Service Menu" });


        }

        //public void RefreshPage()
        //{
        //    _driver.Value.Navigate().Refresh();
        //}

        [TearDown]
        public void CleanUp()
        {
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            try
            {
                // Logs the result to Sauce Labs
                if (_configuration.IsRemoteDriver)
                {
                    ((IJavaScriptExecutor)_driver.Value).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                }
            }
            finally
            {
                // Terminates the remote webdriver session
                _driver.Value.Quit();
            }
        }


        #region -- Public Methods -- 

        public void WaitForMaskModal()
        {
            IWebElement maskModal = _driver.Value.FindElement(By.ClassName("main-container-mask"));
            IWebElement overLayModal = _driver.Value.FindElement(By.ClassName("lg-modal__overlay"));

            while (maskModal.Displayed || overLayModal.Displayed)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            //while (overLayModal.Displayed)
            //{
            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            //}
        }

        #endregion
    }

}
