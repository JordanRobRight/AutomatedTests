using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using QA.Automation.Common;
using QA.Automation.UITests.LG20.Pages;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;


namespace QA.Automation.UITests
{
    //TODO: Need a better way to pass in these items. 
    [TestFixture("chrome", "63", "Windows 10", "", "")]
    [Parallelizable(ParallelScope.Children)]
    public class UnitTest1 
    {
        //private IWebDriver _driver;
        private ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();

        private String browser;
        private String version;
        private String os;
        private String deviceName;
        private String deviceOrientation;
        private readonly TestConfiguration _configuration = null;

        //private const string un = @"DCIArtform";

        //private const string ak = @"a4277bd1-3492-4562-99bc-53dd349c52e1";

        public UnitTest1(String browser, String version, String os, String deviceName, String deviceOrientation)
        {
            this.browser = browser;
            this.version = version;
            this.os = os;
            this.deviceName = deviceName;
            this.deviceOrientation = deviceOrientation;
            //_configuration = TestConfiguration.GetTestConfiguration();
            _configuration = ConfigurationSettings.GetSettingsConfiguration<TestConfiguration>();
        }

        [SetUp]
        public void Init()
        {
            _driver.Value = new ChromeBrowser(browser, version, os, deviceName, deviceOrientation, _configuration,
                TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.MethodName).Driver;

            //Login();

            #region  -- old code --
            /*
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability(CapabilityType.BrowserName, browser);
            caps.SetCapability(CapabilityType.Version, version);
            caps.SetCapability(CapabilityType.Platform, os);
            caps.SetCapability("deviceName", deviceName);
            caps.SetCapability("deviceOrientation", deviceOrientation);
            caps.SetCapability("username", _configuration.SauceLabsUser);
            caps.SetCapability("accessKey", _configuration.SauceLabsKey);
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);

            if (_configuration.IsRemoteDriver)
            {
                //_driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
                caps.SetCapability("name",
                    string.Format("{0}:{1}: [{2}]",
                        TestContext.CurrentContext.Test.ClassName,
                        TestContext.CurrentContext.Test.MethodName,
                        string.Empty));
                //TestContext.CurrentContext.Test.Properties.Get("Description")));
                _driver.Value = new CustomDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));


            }
            else
            {
                ChromeOptions co = new ChromeOptions();    // set the desired browser
                co.AddAdditionalCapability("platform", "Windows 7");
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                _driver.Value = new ChromeDriver(path);
            }


            _driver.Value.Manage().Window.Maximize();
            _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
            _driver.Value.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
            */
            #endregion -- --
        }

        [TestCase]//Test case 586 edit playlist
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
            OffClick();
            //step 20 logout
            LogOutWithoutLogin();
        }


        public void OffClick()
        {
            IWebElement offClick = _driver.Value.FindElement(By.CssSelector(BaseStrings.offClickCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            var element = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            new Actions(_driver.Value).MoveToElement(element).Click().Perform();
        }

        [TestCase] //Test Case 9182
        [Category("All")]
        [Description("Test Case 9182")]
        public void FavoritePlaylist()
        {
            //Step 1 login
            Login();
            IWebElement playlistSearch = _driver.Value.FindElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");
            //Step 2 Hover over a Favorite icon (heart) thats in black
            IWebElement favoritIconHeart = _driver.Value.FindElement(By.CssSelector(BaseStrings.favoriteIconHeartCssSelector));
            Actions action = new Actions(_driver.Value);
            action.MoveToElement(favoritIconHeart).Perform();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Step 3 Hover over a Favorite icon (heart) thats in red
            IWebElement favoritIconHeart1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.favoriteIconHeartCssSelector));
            Actions action1 = new Actions(_driver.Value);
            action1.MoveToElement(favoritIconHeart1).Perform();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
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
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 9 enter all required field values and select save
            IWebElement addPlaylistButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            addPlaylistButton.Click();
            string playlistName = "Automated Playlist Test " + DateTime.Now.ToString();
            IWebElement playlistAddForm = _driver.Value.FindElement(By.Id("form-name"));
            playlistAddForm.SendKeys(playlistName);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            string categoryID = "//*[@id='playlist-info-form']/div[1]/div[2]/div//*[@id='select-filter']";
            IWebElement selectCategory = _driver.Value.FindElement(By.XPath(categoryID));
            selectCategory.SendKeys("chevy" + Keys.Enter);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement saveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton.Click();
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

        [TestCase]
        [Category("All")]
        [Category("SmokeTests")]
        public void LiveGuide20()
        {
            Login();

            WaitForElementExists("page-header-container");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            Assert.AreEqual($"https://portal.{_configuration.Environment.ToString()}.dcimliveguide.com/#playlists".ToLower(), _driver.Value.Url.Trim().ToLower());
        }
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

        public void SelectAutomatedPlaylist()
        {
            string playlistName = "Automated Playlist Test";
            IWebElement playlistAddForm = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            playlistAddForm.SendKeys(playlistName);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement playlistSelection = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));
            playlistSelection.Click();
        }

        [TestCase] //Test case #580
        [Category("All")]
        [Category("SmokeTests")]
        [Description("Test case #580")]
        public void CreatePlaylists()
        {
            //step 1 login
            Login();//remove to do full test

            
            
            //step 2
            var functionBar = GetElement(ByType.ClassName, "pmfb-container").FindElements(By.TagName("button"));
            IWebElement addPlaylistButton = null;

            foreach (var functionBarItem in functionBar)
            {
                if (functionBarItem.Text.Contains("Add New Playlist"))//(functionBarItem.Text == "Add New Playlist") both work
                {
                    addPlaylistButton = functionBarItem;
                    break;
                }
            }
            addPlaylistButton.Click();

            //Step 4 Select 'X' to close window

            var modalContainer = GetElement(ByType.ClassName, "lg-modal__container");
            var modalContainerButtons = modalContainer.FindElements(By.TagName("button"));
            IWebElement playlistAddXButton = null;

            foreach ( var button in modalContainerButtons)
            {
                if(button.Text != "Save")
                {
                    playlistAddXButton = button;
                }               
            }

            playlistAddXButton.Click();
            
            //Step 5 Select '+' to add new playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            foreach (var functionBarItem in functionBar)
            {
                if (functionBarItem.Text.Contains("Add New Playlist"))//(functionBarItem.Text == "Add New Playlist") both work
                {
                    addPlaylistButton = functionBarItem;
                    break;
                }
            }
            addPlaylistButton.Click();
            _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            //Step 6 Click outside the window to close it 

            IWebElement offClick = _driver.Value.FindElement(By.CssSelector(BaseStrings.offClickCssSelector));

            Actions action = new Actions(_driver.Value);
            action.MoveToElement(offClick).MoveByOffset(100,100).Click().Perform();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 7 Select '+' to add new playlist
            foreach (var functionBarItem in functionBar)
            {
                if (functionBarItem.Text.Contains("Add New Playlist"))//(functionBarItem.Text == "Add New Playlist") both work
                {
                    addPlaylistButton = functionBarItem;
                    break;
                }
            }
            addPlaylistButton.Click();

            //Step 8 Select Create a Custom Playlist - Filtered Check box
            var modalContainerCheckBox = modalContainer.FindElement(By.ClassName("lgfe-input-checkbox__custom-input"));
            modalContainerCheckBox.Click();

            //Step 9 Select Save
            IWebElement saveButton = null;
            
            _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            foreach (var button in modalContainerButtons)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                if (button.Text.Contains("Save"))
                {
                    saveButton = button;
                    break;
                }
            }
            saveButton.Click();

            //Step 10 Select Ok
            IAlert alert = _driver.Value.SwitchTo().Alert();
            alert.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 11 Enter a playlist name
            string playlistName = "Automated Playlist Test " + DateTime.Now.ToString();
            IWebElement playlistAddForm = _driver.Value.FindElement(By.Id("form-name"));
            playlistAddForm.SendKeys(playlistName);

            //Step 12 Select save
            saveButton.Click();
            IAlert mustSelectLocaiton = _driver.Value.SwitchTo().Alert();
            mustSelectLocaiton.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 13 Enter location name (this is the first input field after the Custom checkbox)
            IWebElement locationInput = _driver.Value.FindElement(By.Id("select-filter-location"));
            locationInput.SendKeys("System Test Location Two Buick");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            locationInput.Submit();

            //IWebElement locationDropDown = GetElement(ByType.);
            //var locationList = GetElement(ByType.);

            // IWebElement selectLocationFromDropDown = _driver.Value.FindElement(By.CssSelector("#eac-container-select-filter-location > ul > li"));
            //selectLocationFromDropDown.Click();


            //Step 14 Select save
            saveButton.Click();
            //IAlert mustSelectLocaiton = _driver.Value.SwitchTo().Alert();
            mustSelectLocaiton.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 15 Select device drop down
            IWebElement selectLocationDeviceFilter = _driver.Value.FindElement(By.Id("select-filter-location-device"));
            //create select element object 
            var selectLocationDeviceElement = new SelectElement(selectLocationDeviceFilter);

            //Step 16 Select all devices
            selectLocationDeviceElement.SelectByValue("all");
            //Step 17 Select save
            saveButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 18 New playlist has been created

            //Step 19 Select '+' to add a new playlist

            foreach (var functionBarItem in functionBar)
            {
                if (functionBarItem.Text.Contains("Add New Playlist"))//(functionBarItem.Text == "Add New Playlist") both work
                {
                    addPlaylistButton = functionBarItem;
                    break;
                }
            }
            //Step 20 Logout
            LogOutWithoutLogin();


            //TODO: Assert to check if the playlist was actually playlist got created. 

            //IWebElement newPlaylist = GetElement(ByType.ClassName, "lgfe-cm-card");          

            //Assert.IsTrue(newPlaylist.Displayed);
            //Assert.AreEqual(newPlaylist, "Automated Playlist Test");

            //TODO: Assert calling API.
            //string apiPlayList = APITests.LG20.SmokeTest.GetPlayListByName("newPlaylist", "username", "password", _configuration.Environment);

            //Assert.AreEqual(newPlaylist, apiPlayList);

            //TODO: Update this assert to take into account the environment.
            //Assert.AreEqual("https://portal.test.dcimliveguide.com/#playlists", _driver.Url.Trim());
            //Assert.AreEqual("https://portal.test.dcimliveguide.com/#playlists", _driver.Url.Trim());
        }

        [TestCase]// test Case #46
        [Category("All")]
        [Category("SmokeTests")]
        [Description("Test Case #46")]
        public void CreateAPlaylistHappyPath()
        {
            //Step 1 login 
            Login();
            //Step 2 select '+' to make a new playlist
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            IWebElement addPlaylistButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            addPlaylistButton.Click();
            //Step 3 enter playlist name
            string playlistName = "Automated Playlist Test " + DateTime.Now.ToString();
            IWebElement playlistAddForm = _driver.Value.FindElement(By.Id("form-name"));
            playlistAddForm.SendKeys(playlistName);
            //Step 4 Select a channel
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            string filterID = "//*[@id='playlist-info-form']/div[1]/div[2]/div//*[@id='select-filter']";
            IWebElement selectFilter = _driver.Value.FindElement(By.XPath(filterID));
            //create select element object 
            selectFilter.SendKeys("chevy" + Keys.Enter);
            //Step 5  select save
            IWebElement saveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton.Click();
            //Step 6 select done---does not exist currently (09/26/2018)
            //Step 7 new playlist has been created
            //Step 8 logout
            LogOutWithoutLogin();

        }

        [TestCase] //Test case 737
        [Category("All")]
        //[Category("SmokeTests")]
        [Description("Test case 737")]
        public void AddWeatherWidget()
        {
            //step 1 login   
            Login();
            //step 2 Select an existing Playlist
            SelectAutomatedPlaylist();
            //step 3 Select Add Weather Widget 
            IWebElement weatherWidget = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetCSSSelector));
            WaitForMaskModal();
            weatherWidget.Click();
            //step 4 Spell check all content (fields/values/buttons), including placeholder text 
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 5 Confirm text box displays with pre-filled time Duration (not editable)
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 6 Weather pre-filled description displays on lower section of window
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 7 Do not enter a Zip Code, confirm placeholder text displays 'Enter Zip Code'
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 8 Select Save
            IWebElement weatherWidgetSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetSaveButtonCSSSelector));
            weatherWidgetSaveButton.Click();
            //step 9 Select Add Weather Widget
            IWebElement weatherWidget1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetCSSSelector));
            WaitForMaskModal();
            weatherWidget1.Click();
            //step 10 Select Brand dropdown box
            //step 11 Select Buick Brand
            IWebElement brandDropdown = _driver.Value.FindElement(By.XPath(BaseStrings.weatherWidgetDropDown));
            var selectBrandDropDown = new SelectElement(brandDropdown);
            selectBrandDropDown.SelectByValue("buick");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 12 Select Save
            IWebElement weatherSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetSaveButtonCSSSelector));
            WaitForMaskModal();
            weatherSaveButton.Click();
            //step 13 Select Add Weather Widget
            IWebElement weatherWidget2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetCSSSelector));
            WaitForMaskModal();
            weatherWidget2.Click();
            //step 14 Enter an invalid Zip Code in text box  (letters, special characters or less or more than 5 numbers)
            IWebElement weatherZipCodeInput = _driver.Value.FindElement(By.Id(BaseStrings.weatherZipCodeInputID));
            weatherZipCodeInput.SendKeys("531");
            //step 15 Enter a valid Zip Code in text box
            IWebElement weatherZipCodeInput1 = _driver.Value.FindElement(By.Id(BaseStrings.weatherZipCodeInputID));
            weatherZipCodeInput1.Clear();
            weatherZipCodeInput1.SendKeys("53142");
            //step 16 Select Save
            IWebElement weatherSaveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.weatherWidgetSaveButtonCSSSelector));
            WaitForMaskModal();
            weatherSaveButton1.Click();
            //step 17 Create all brands ---I am not sure what this means---

            //step 18 select save from playlist screen
            IWebElement playlistSave2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSaveCSSSelector));
            WaitForMaskModal();
            playlistSave2.Click();
            //step 19 logout
            LogOutWithoutLogin();
            //TODO: Assert that the saved worked.
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
            //step 1 sign in 
            Login();
            //step 2 select an existing playlist
            SelectAutomatedPlaylist();
            //step 3 select add traffic widget
            IWebElement trafficWidget = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetCssSelector));
            WaitForMaskModal();
            trafficWidget.Click();
            //step 4 spell check all content 
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 5 confirm text box displays on lower section of window
            IWebElement trafficWidgetDescription = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetDescriptionCssSelector));
            Assert.IsTrue(trafficWidgetDescription.Text.Contains("Current conditions of local routes and maps."));
            //step 6 weather pre-filled displays on lower section of window
            //step 7 do not enter zip code confirm placeholder text displays
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 8 select save
            IWebElement trafficWidgetSaveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetSaveButtonCssSelector));
            WaitForMaskModal();
            trafficWidgetSaveButton.Click();
            //step 9 select add traffic widget
            IWebElement trafficWidget1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetCssSelector));
            WaitForMaskModal();
            trafficWidget1.Click();
            //step 10 select brand drop down box
            IWebElement brandDropdown = _driver.Value.FindElement(By.XPath(BaseStrings.trafficWidgetDropDown));
            var selectBrandDropDown = new SelectElement(brandDropdown);
            selectBrandDropDown.SelectByValue("buick");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //step 11 select buick brand
            //step 12 select save
            IWebElement trafficWidgetSaveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetSaveButtonCssSelector));
            WaitForMaskModal();
            trafficWidgetSaveButton1.Click();
            //step 13 select add traffic widget
            IWebElement trafficWidget2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetCssSelector));
            WaitForMaskModal();
            trafficWidget2.Click();
            //step 14 enter an invalid zip code
            IWebElement trafficZipInput = _driver.Value.FindElement(By.Id("traffic-widget-zip"));
            trafficZipInput.SendKeys("53");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //step 15 enter valid zip code
            trafficZipInput.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            trafficZipInput.SendKeys("53142");
            //step 16 select save
            IWebElement trafficWidgetSaveButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.trafficWidgetSaveButtonCssSelector));
            WaitForMaskModal();
            trafficWidgetSaveButton2.Click();
            //step 17 create all brands
            //step 18 select save from playlist screen
            IWebElement playlistSave = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSaveCSSSelector));
            WaitForMaskModal();
            playlistSave.Click();
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
            //step 1 login
            Login();
            //step 2 Select an existing Playlist
            SelectAutomatedPlaylist();
            //step 3 Select Add Video Widget
            IWebElement videoWidgetButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.videoWidgetCssSelector));
            videoWidgetButton.Click();
            WaitForMaskModal();
            //step 4 Spell check all content (fields/values/buttons), including placeholder text  
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            //step 5 Select 'X' to close window
            IWebElement videoXoutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.videoXoutButtonCssSelector));
            videoXoutButton.Click();
            //step 6 Select Add Video Widget
            videoWidgetButton.Click();
            //step 7 Click outside window
            OffClick();
            //step 8 Select Add Video Widget
            videoWidgetButton.Click();
            //step 9 Select any one video            
            IWebElement videoAssestLibrarySearchInput = _driver.Value.FindElement(By.Id("asset-search"));
            videoAssestLibrarySearchInput.SendKeys("a");
            IWebElement videoAssestSelection = _driver.Value.FindElement(By.XPath(BaseStrings.videoAssestSelectionXPath));
            videoAssestSelection.Click();
            //step 10 Select Done
            IWebElement videoWidgetDoneButton = _driver.Value.FindElement(By.XPath(BaseStrings.videoWidgetDoneButtonXpath));
            videoWidgetDoneButton.Click();
            //step 11 Select Add Video Widget
            videoWidgetButton.Click();
            WaitForMaskModal();
            //step 12 Select multiple videos
            //IWebElement brandDropdown = _driver.Value.FindElement(By.XPath(BaseStrings.healthWidgetDropDown));
            //var selectBrandDropDown = new SelectElement(brandDropdown);
            //selectBrandDropDown.SelectByValue("buick");
            IWebElement option1 = _driver.Value.FindElement(By.XPath("//*[@id='asset-video-select-form']/div[2]/div[1]"));
            IWebElement option2 = _driver.Value.FindElement(By.XPath("//*[@id='asset-video-select-form']/div[2]/div[2]"));
            IWebElement option3 = _driver.Value.FindElement(By.XPath("//*[@id='asset-video-select-form']/div[2]/div[3]"));

            option1.Click();
            option2.Click();
            option3.Click();

            //step 13 Select Done
            IWebElement videoWidgetDoneButton1 = _driver.Value.FindElement(By.XPath(BaseStrings.videoWidgetDoneButtonXpath));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            videoWidgetDoneButton1.Click();
            //step 14 Select Save from Playlist screen
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            IWebElement saveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSaveButtonCssSelector));
            saveButton.Click();

            //step 15 Logout
            LogOutWithoutLogin();

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

        [TestCase]
        [Category("SmokeTests")]
        [Description("Login")]
        public void Login()
        {
            #region --- old code
            /*
            string url = Common.LgUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Value.Url;
            _driver.Value.Navigate().GoToUrl(url);

            IWebElement query = GetElement(ByType.Id, "login-email");

            query.SendKeys("cbam.lgtest1@dciartform.com");
            query = GetElement(ByType.Id, "login-password");
            query.SendKeys("Cbam#test1");

            query.Submit();
            */
            #endregion ---

            Login login = new Login(_driver.Value, _configuration);
            login.GoToUrl();
            login.Perform();

            WaitForElementExists("page-header-container");

            IWebElement p = _driver.Value.FindElement(By.Id("interaction-nav-bar-container"));

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            SelectItemFromCilentMenu(_driver.Value, "GM");

            #region -- old code --
            /*
            string playlistDivCssSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div";
            IWebElement playlistDiv = _driver.Value.FindElement(By.CssSelector(playlistDivCssSelector));
            //if playlists is empty find profile dropdown 

            IWebElement playerChannelDropdown = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));
            string clientName = "GM";

            if (!playerChannelDropdown.Text.Equals(clientName, StringComparison.OrdinalIgnoreCase))
            {
                playerChannelDropdown.Click();

                var wrapper = GetElement(ByType.ClassName, "iibcuinow-menu-wrapper").FindElements(By.TagName("a"));
                IWebElement gmChannelSelection = null;

                foreach (var menuItem in wrapper)
                {
                    if (menuItem.Text == clientName)
                    {
                        gmChannelSelection = menuItem;
                        break;
                    }
                }

                //gmChannelSelection = wrapper.FirstOrDefault();

                gmChannelSelection = gmChannelSelection ?? wrapper.FirstOrDefault();

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                gmChannelSelection.Click();
            }
            */
            //}

            //if (!playlistDiv.Displayed)
            //{
            //    IWebElement playerChannelDropdown = GetElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));

            //    playerChannelDropdown.Click();

            //    IWebElement gmChannelSelection = GetElement(By.XPath(BaseStrings.gmChannelSelectionXPath));

            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //    gmChannelSelection.Click();
            //}
            #endregion

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

            MiscLib.WindowsFormHelper.GetAutoIt("Open", @"C:\Users\enwright\Desktop\galaxie.jpg");

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

        [TestCase]//Test case 1994
        [Category("SmokeTests")]
        [Description("Test case 1994")]
        public void Logout()
        {
           // Login();
            Logout logout = new Logout(_driver.Value, ConfigurationSettings.GetSettingsConfiguration<TestConfiguration>());

            SelectItemFromCilentMenu(_driver.Value, "logout");

            logout.CancelButtonClick();
            Thread.Sleep(TimeSpan.FromMilliseconds(500));
            SelectItemFromCilentMenu(_driver.Value, "logout");

            logout.LogoutAcceptButtonClick();
            //logout.Perform();


            #region --- old code

            //IWebElement logOutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logOutButtonCssSelector));
            //WaitForMaskModal();
            //logOutButton.Click();

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            //IWebElement confirmLogOutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector));
            //WaitForMaskModal();
            //confirmLogOutButton.Click();

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            //TODO: Assert that we are logged out based on URL and maybe the Username/password fields.

            #endregion
        }

        public void LogOutWithoutLogin()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            IWebElement logOutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logOutButtonCssSelector));
            WaitForMaskModal();
            logOutButton.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            IWebElement confirmLogOutButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector));
            WaitForMaskModal();
            confirmLogOutButton.Click();
        }

        [TestCase]
        [Description("Test for Test case 1456")]
        public void LogoutAfterLogin()//postive test for Test case 1456
        {
            //step 1
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

            _driver.Value.Quit();


            //TODO: Assert that we are logged out based on URL and maybe the Username/password fields.
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
            //Step 5
            deletePlaylistButton.Click();
            //Step 6
            IAlert accept = _driver.Value.SwitchTo().Alert();
            accept.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 7 refresh screen
            _driver.Value.Navigate().Refresh();
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

        [TestCase]
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
        }

        [TestCase]//Test Case #1459
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
        }

        [TestCase]//Test Case #1460
        [Category("All")]
        [Description("Test Case #1460")]
        public void PlayersStatus()
        {
            //Step 1
            Login();

            //Step 2 select players tab
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector("#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li:nth-child(3) > a > span"));
            playersTab.Click();
            WaitForMaskModal();

            //Step 3 confirm that each player contains one status---needs work TODO
            string playersGraph = _driver.Value.FindElement(By.CssSelector("#players-table > tbody")).Text;

            String expectedMessage = "ONLINE";
            Assert.True(playersGraph.Contains(expectedMessage));

            //Step 4 select a player
            IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1"));
            playerSelect.Click();

            //Step 5
            playersTab.Click();
            WaitForMaskModal();

            //Step 6
            IWebElement playerSelect2 = _driver.Value.FindElement(By.CssSelector("#player-player_QyvWE5pQCs55 > td.sorting_1"));
            playerSelect2.Click();

            //Step 7
            playersTab.Click();
            WaitForMaskModal();

            //Step 8 select a player that is not set up...---needs work TODO

            //Step 9
            playersTab.Click();
            WaitForMaskModal();

            //Step 10
            LogOutWithoutLogin();
        }

        [TestCase]// test case #1463
        [Category("All")]
        [Description("Test case #1463")]
        public void PlayerEdits()
        {
            //Step 1
            Login();

            //step 2
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 3 
            IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();

            //Step 4 
            IWebElement playerInfoDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerInfoDownArrowCssSelectors));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerInfoDownArrow.Click();

            //Step 5
            IWebElement playerInfoUpArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerInfoUpArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            playerInfoUpArrow.Click();

            //Step 6
            IWebElement playerInfoX = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerInfoXCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerInfoX.Click();

            //Step 7,8,& 9 are skipped since there isnt a products section right now
            //Step 10
            IWebElement deviceDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.deviceDownArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceDownArrow.Click();

            //Step 11
            IWebElement deviceUpArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.deviceUpArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceUpArrow.Click();

            //Step 12 
            IWebElement xOnDevice = _driver.Value.FindElement(By.CssSelector(BaseStrings.xOnDeviceCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            xOnDevice.Click();

            //Step 13 select down arrow for location
            IWebElement locationDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.locationDownArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            locationDownArrow.Click();

            //Step 14 
            IWebElement locationUpArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.locationUpArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            locationUpArrow.Click();

            //Step 15
            IWebElement locationXButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.locationXButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            locationXButton.Click();

            //Step 16
            IWebElement playlistsButton = _driver.Value.FindElement(By.XPath(BaseStrings.playlistsButtonXPath));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistsButton.Click();

            //Step 17
            IWebElement whatsPlayingDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.whatsPlayingDownArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            whatsPlayingDownArrow.Click();

            //Step 18
            IWebElement whatsPlayingUPArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.whatsPlayingUpArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            whatsPlayingUPArrow.Click();

            //Step 19
            IWebElement whatsPlayingXButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.whatsPlayingXButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            whatsPlayingXButton.Click();

            //Step 20
            IWebElement playlistInfoDownArrow = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistInfoDownArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoDownArrow.Click();

            //Step 21
            IWebElement playlistInfoUpArrow = _driver.Value.FindElement(By.XPath(BaseStrings.playlistInfoUpArrowXpath));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoUpArrow.Click();

            //Step 22
            IWebElement playlistInfoXButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistInfoXButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoXButton.Click();

            //Step 23,24,25,26,27,28,29,30,31 are not present on frontend that i can see as of 09/18/18

            //Step 31
            LogOutWithoutLogin();
        }

        [TestCase]
        [Category("All")]
        public void PlayerEditDevice()
        {
            //Step 1
            Login();

            //Step 2 select player tab
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 3 Select Any player
            IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();

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
            //step 1
            Login();
            //Step 2 select player tab
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 Select Any player
            IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1 > span"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();
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
            //step 1
            Login();
            //Step 2 select player tab
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 Select Any player
            IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();
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
            //Step 1
            Login();
            //Step 2 select player tab
            IWebElement playersTab = _driver.Value.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 Hover over the screen connect icon
            //step 4
            IWebElement playerSelect = _driver.Value.FindElement(By.CssSelector("#player-player_BgY5XvhVfYEv > td.sorting_1"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();
            //Step 5 Hover over screen connect icon
            //Step 6
            IWebElement screenConnectButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.screenConnectCssSelector));
            screenConnectButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 7
            string url = Common.LgUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Value.Url;
            _driver.Value.Navigate().GoToUrl(url);

            var tabs = _driver.Value.WindowHandles;
            if (tabs.Count > 1)
            {
                _driver.Value.SwitchTo().Window(tabs[1]);
                _driver.Value.Close();
                _driver.Value.SwitchTo().Window(tabs[0]);
            }
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

        [TestCase]//Test case #1984
        [Category("SmokeTests")]
        [Category("All")]
        [Description("Test case #1984")]
        public void OpenPlaylist()
        {
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

        [TestCase]// test case 1985
        [Category("SmokeTests")]
        [Category("All")]
        [Description("Test case 1985")]
        public void AbleToEditPlaylist()
        {
            //step 1 loging
            Login();
            //step 2 select playlists that contains no widget and select able to edit button from main menu
            IWebElement playlistSearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            playlistSearchInput.SendKeys("Automated");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            IWebElement durationSection = _driver.Value.FindElement(By.CssSelector(BaseStrings.durationSectionCssSelector));
            string duration = durationSection.Text;
            WaitForMaskModal();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Assert.IsTrue(duration.Contains("<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>"));
            if (duration.Contains("<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>"))
            {
                IWebElement playlistEditButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.editButtonCssSelector));
                playlistEditButton1.Click();
                IWebElement saveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
                saveButton1.Click();
            }
            //step 3 select playlists from main menu
            IWebElement playlistsSideBarMenuButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            //step 4 select playlists that contains widgets and select able to edit button 
            if (!duration.Contains("<span class='pmppid-time'><span class='lgfe-cm-duration-time-unit'>00<span class=visually-hidden'>hours,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'>minutes,</span></span><span class='lgfe-cm-duration-time-unit'>00<span class='visually-hidden'> seconds</span></span></span>"))
            {
                IWebElement playlistEditButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.editButtonCssSelector));
                playlistEditButton2.Click();
                IWebElement saveButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
                saveButton2.Click();
            }
            //step 5 select playlists from main menu
            IWebElement playlistsSideBarMenuButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton1.Click();
            WaitForMaskModal();
            //Step 6 logout
            LogOutWithoutLogin();
        }

        [TestCase]//Test case 1986
        [Category("All")]
        [Description("Test case 1986")]
        public void PlaylistFilters()
        {
            //step 1 signin
            Login();
            //step 2 select filter dropdown box
            IWebElement filterDropDownButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.filterButtonCssSelector));
            var selectfilterDropDownButton = new SelectElement(filterDropDownButton);
            selectfilterDropDownButton.SelectByValue("date");
            //step 3 spell check all filter values from drop down box
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 4 select the alphabetical option 
            IWebElement filterDropDownButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.filterButtonCssSelector));
            var selectfilterDropDownButton1 = new SelectElement(filterDropDownButton1);
            selectfilterDropDownButton1.SelectByValue("alphabetically");
            //step 5 confirm all the playlist display in alphabetical ascending order
            //step 6 refresh screen
            RefreshPage();
            //step 7 confirm all playlist display in date added order
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 8 select filter dropdown box

            //step 9 select the changel option
            IWebElement filterDropDownButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.filterButtonCssSelector));
            var selectfilterDropDownButton2 = new SelectElement(filterDropDownButton2);
            selectfilterDropDownButton2.SelectByValue("channel");
            //step 10 Confirm all playlist display in Channel order:  Buick TV, Buick-Chevy TV, Chevy TV,  Chevy-Buick TV, Chevy-GMC, Chevy-Buick-GMC, GMC TV
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 11 refresh screen
            RefreshPage();
            //step 12 confirm all playlists display in dated added order
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Step 13 logout
            LogOutWithoutLogin();
        }

        [TestCase]//test case 1987
        [Category("SmokeTests")]
        [Category("All")]
        [Description("Test case 1987")]
        public void PlaylistDisplayAsGrid()
        {
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
            IWebElement openButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.gridOpenButtonCssSelector));
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

        [TestCase]// test case 1988
        [Category("All")]
        [Description("Test case 1988")]
        public void PlaylistDisplayAsRow()
        {
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
            IWebElement openButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.gridOpenButtonCssSelector));
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
            //step 1 signin
            Login();
            //step 2 place curser in search box
            IWebElement playlistSearchInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.playlistSearchInputCssSelector));
            //step 3 Spell check Search box placeholder text

            //step 4 Enter invalid names (only Playlist names are considered valid entries) in Search box
            playlistSearchInput.SendKeys("123456");
            //step 5 Enter spaces in Search box
            playlistSearchInput.Clear();
            playlistSearchInput.SendKeys("     ");
            //step 6 Enter a valid Playlist name (only Playlist names are considered valid entries) in Search box
            playlistSearchInput.Clear();
            playlistSearchInput.SendKeys("Automated");
            //step 7 Enter a valid Playlist name that consist of letters, numbers and special characters
            playlistSearchInput.Clear();
            playlistSearchInput.SendKeys("Automated Playlist Test 9/6/2018");
            //step 8 From Search box, remove text from test step 6 above
            playlistSearchInput.Clear();
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
            IWebElement assetTab = _driver.Value.FindElement(By.CssSelector("#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a > span"));
            assetTab.Click();
            WaitForMaskModal();
            //Step 3
            IWebElement assetSearchInput = _driver.Value.FindElement(By.Id("assets-search"));
            assetSearchInput.Click();
            //step 4
            //spell check placeholder text

            //step 5 
            assetSearchInput.SendKeys("     ");
        }

        [TestCase]//TestCase 1991
        [Category("All")]
        [Description("TestCase 1991")]
        public void PlaylistMyProfile()
        {
            //step 1 sign in
            Login();

            //step 2 select user dropdown menu (upper right corner)
            IWebElement playerChannelDropdown = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));
            playerChannelDropdown.Click();

            //step 3 Spell check all values from dropdown box
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 4 Select My Profile myProfileButtonCssSelector
            IWebElement myProfileButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileButtonCssSelector));
            myProfileButton.Click();
            //step 5 Spell check all content (fields/values, buttons), including placeholder text 
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 6 Select Save button
            IWebElement saveButton = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileSaveButtonCssSelector));
            saveButton.Click();
            //step 7 Enter any First Name (there are not edit checks in place) and select Save\
            IWebElement playerChannelDropdown1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));
            playerChannelDropdown1.Click();
            IWebElement myProfileButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            myProfileButton1.Click();

            IWebElement myProfileFirstName = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileFirstNameInput));
            myProfileFirstName.SendKeys("Automated");
            //step 8 Enter any Last Name (there are not edit checks in place) and select Save
            IWebElement myProfileLastName = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileLastNameInput));
            myProfileLastName.SendKeys("Tester");
            //step 9 Enter any Title (there are not edit checks in place) and select Save 
            IWebElement myProfileTitle = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileTitleInput));
            myProfileTitle.SendKeys("Engineer");
            IWebElement saveButton1 = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileSaveButtonCssSelector));
            saveButton.Click();
            //step 10 Enter an invalid Email Address and select Save 
            /*****Email is a locked imput for cbam right now 10/4/2018******/
            //IWebElement playerChannelDropdown2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));
            //playerChannelDropdown2.Click();
            //IWebElement myProfileButton2 = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileButtonCssSelector));
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //myProfileButton1.Click();
            //IWebElement emailInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileEmailInput));
            //emailInput.SendKeys("test");
            //step 11 Repeat test step 10 with various invalid Email combinations
            //emailInput.Clear();
            //emailInput.SendKeys("test.com");
            //step 12 Enter a valid Email Address and select Save
            //step 13 Enter an invalid (letters, special characters-except for dash & parenthesis or less than 10 numbers) Direct Phone Number 
            IWebElement directPhoneNumberInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfilePhoneInput));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            directPhoneNumberInput.SendKeys("test1234" + Keys.Enter);
            //step 14 Repeat test step 13 with various invalid Direct Phone Number combinations
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            directPhoneNumberInput.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            directPhoneNumberInput.SendKeys("1234" + Keys.Enter);
            //step 15 Enter a valid Direct Phone Number and select Save
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            directPhoneNumberInput.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            directPhoneNumberInput.SendKeys("4141231234" + Keys.Enter);
            //step 16 Enter an invalid (letters, special characters-except for dash & parenthesis or less than 10 numbers) Mobile Number
            IWebElement mobileNumberInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileMobileInput));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            mobileNumberInput.SendKeys("abc123" + Keys.Enter);
            //step 17 Repeat test step 16 with various invalid Mobile Number combinations 
            mobileNumberInput.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            mobileNumberInput.SendKeys("123" + Keys.Enter);
            //step 18 Enter a valid Mobile Number and select Save
            mobileNumberInput.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            mobileNumberInput.SendKeys("1234567890" + Keys.Enter);
            //step 19 Enter any Street Address (there are not edit checks in place) and select Save   
            IWebElement addressInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileAddressInput));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            addressInput.SendKeys("2727 Good Faith Rd" + Keys.Enter);
            //step 20 Enter any City name (there are not edit checks in place) and select Save
            IWebElement cityInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileCityInput));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            cityInput.SendKeys("Milwuakee" + Keys.Enter);
            //step 21 Enter any State (there are not edit checks in place) and select Save
            IWebElement stateInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileStateInput));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            stateInput.SendKeys("WI" + Keys.Enter);
            //step 22 Enter an invalid Zip Code (letters, special characters or less or more than 5 numbers) and select Save
            IWebElement zipCodeInput = _driver.Value.FindElement(By.CssSelector(BaseStrings.myProfileZipInput));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            zipCodeInput.SendKeys("12345" + Keys.Enter);
            //step 23 Repeat test step 22 with various invalid Zip Code combinations 
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            zipCodeInput.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            zipCodeInput.SendKeys("53209" + Keys.Enter);
            //step 24 logout
            LogOutWithoutLogin();

        }

        public void RefreshPage()
        {
            _driver.Value.Navigate().Refresh();
        }

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

        #region -- Private Methods ---

        private void SelectItemFromCilentMenu(IWebDriver driver, string menuItemToSelect)
        {
            string playlistDivCssSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div";
            
            //IWebElement playlistDiv = _driver.Value.FindElement(By.CssSelector(playlistDivCssSelector));
            //if playlists is empty find profile dropdown 

            IWebElement playerChannelDropdown = driver.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));
            //string clientName = "GM";

            if (!playerChannelDropdown.Text.Equals(menuItemToSelect, StringComparison.OrdinalIgnoreCase))
            {
                playerChannelDropdown.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));

                var wrapper = GetElement(ByType.ClassName, "iibcuinow-menu-wrapper").FindElements(By.TagName("a"));
                IWebElement gmChannelSelection = null;
                
                foreach (var menuItem in wrapper)
                {
                    if (menuItem.Text.Equals(menuItemToSelect, StringComparison.OrdinalIgnoreCase)) // == menuItemToSelect)
                    {
                        gmChannelSelection = menuItem;
                        break;
                    }
                }

                //gmChannelSelection = wrapper.FirstOrDefault();

                gmChannelSelection = gmChannelSelection ?? wrapper.FirstOrDefault();

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                gmChannelSelection.Click();
            }
        }

        private IWebElement GetElement(ByType byType, string element)
        {
            By selector = null;
            IWebElement query = null;

            switch (byType)
            {
                case ByType.Css:
                    selector = By.CssSelector(element);
                    break;
                case ByType.Id:
                    selector = By.Id(element);
                    break;
                case ByType.Xml:
                    selector = By.XPath(element);
                    break;
                case ByType.ClassName:
                    selector = By.ClassName(element);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(byType), byType, null);
            }




            try
            {
                query = _driver.Value.FindElement(selector);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return query ?? throw new Exception("GetElement returned a null value.");
        }
        private IWebElement GetElement(By selector, string element = "")
        {
            IWebElement query = null;

            try
            {
                query = _driver.Value.FindElement(selector);
            }
            catch (NoSuchElementException nsex)
            {
                Console.WriteLine("Element couldn't be found: " + nsex);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: GetElement() {e}");
            }

            return query ?? throw new Exception("GetElement returned a null value.");
        }
        private void WaitForElementExists(string element)
        {
            WaitUntilElementExists(_driver.Value, By.Id(element));
        }

        #endregion


        #region -- Public Methods -- 
        //private static WaitForElement(IWebDriver _driver )
        //{
        //    WebDriverWait waitForElement = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        //    waitForElement.Until(ExpectedConditions.ElementIsVisible(By.Id("yourIDHere")));
        //}

        //WaitUntilElementExists(_driver, By.Id("page-header-container"));
        // New way of doing things.
        //var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));

        //this will search for the element until a timeout is reached
        public static IWebElement WaitUntilElementExists(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public static IWebElement WaitUntilElementVisible(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }

        public static IWebElement WaitUntilElementClickable(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }
        #endregion
    }

    public enum ByType
    {
        Css = 1,
        Xml = 2,
        Id = 3,
        ClassName = 4,


    }
}
