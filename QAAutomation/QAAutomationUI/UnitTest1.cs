using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace QA.Automation.UITests
{
    //TODO: Need a better way to pass in these items. 
    [TestFixture("chrome", "63", "Windows 10", "", "")]
    public class UnitTest1
    {
        private IWebDriver _driver;
        private String browser;
        private String version;
        private String os;
        private String deviceName;
        private String deviceOrientation;
        private readonly UITests.TestConfiguration _configuration = null;

        //private const string un = @"DCIArtform";

        //private const string ak = @"a4277bd1-3492-4562-99bc-53dd349c52e1";

        public UnitTest1(String browser, String version, String os, String deviceName, String deviceOrientation)
        {
            this.browser = browser;
            this.version = version;
            this.os = os;
            this.deviceName = deviceName;
            this.deviceOrientation = deviceOrientation;
            _configuration = UITests.TestConfiguration.GetTestConfiguration();
        }

        [SetUp]
        public void Init()
        {
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
                _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            }
            else
            {
                ChromeOptions co = new ChromeOptions();    // set the desired browser
                co.AddAdditionalCapability("platform", "Windows 7");
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                _driver = new ChromeDriver(path);
            }

            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
        }

        [TestCase]//Test case 586 edit playlist
        public void EditPlaylist()
        {
            //Step 1
            Login();
            //Step 2 select edit icon
            IWebElement playlistEditButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistEditButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistEditButton.Click();
            //Step 3 spell check all content

            //step 4 select playlist name text box and edit name
            IWebElement playlistTitleInput = _driver.FindElement(By.CssSelector(BaseStrings.playlistTitleInputCssSelector));
            playlistTitleInput.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistTitleInput.SendKeys("Edited Automated Test Playlist");
            //Step 5 select save
            IWebElement playlistSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistEditSaveButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistSaveButton.Click();
            //Step 6 Select edit for any playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistEditButton.Click();
            //step 7 select playlist description text box and edit description
            IWebElement playlistDescriptionTextArea = _driver.FindElement(By.CssSelector("#form-textarea"));
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
            IWebElement playlistEditTagsSection = _driver.FindElement(By.CssSelector(BaseStrings.playlistTagSectionCssSelector));
            playlistEditTagsSection.Clear();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistEditTagsSection.SendKeys("Edited Tags Section");

            //step 11 select add button
            IWebElement playlistTagAddButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistTagAddButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistTagAddButton.Click();
            //step 12 select save
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistSaveButton.Click();
            //step 13 Select edit icon
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistEditButton.Click();
            //step 14 Delete any tag
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistEditTagsSection.Clear();
            //step 15 select save
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistSaveButton.Click();
            //step 16 select edit icon for same playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistEditButton.Click();
            //step 17 close 'x' edit window
            IWebElement playlistEditCloseButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistEditCloseButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistEditCloseButton.Click();
            //step 18 select edit icon for same playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            playlistEditButton.Click();
            //Step 19 click outside window
            IWebElement offClick = _driver.FindElement(By.CssSelector(BaseStrings.offClickCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            offClick.Click();
            //step 20 logout
            Logout();

        }

        [TestCase]
        public void LiveGuide20()
        {
            Login();

            WaitForElementExists("page-header-container");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            //TODO: Update this assert to take into account the environment.

            Assert.AreEqual("https://portal.test.dcimliveguide.com/#playlists", _driver.Url.Trim());
        }
        public void WaitForMaskModal()
        {
            IWebElement maskModal = _driver.FindElement(By.ClassName("main-container-mask"));
            IWebElement overLayModal = _driver.FindElement(By.ClassName("lg-modal__overlay"));

            while (maskModal.Displayed || overLayModal.Displayed)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            //while (overLayModal.Displayed)
            //{
            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            //}
        }

        [TestCase] //Test case #580
        public void CreatePlaylists()
        {
            //step 1 login
            Login();//remove to do full test

            IWebElement playlistsSideBarMenuButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            //step 2
            IWebElement addPlaylistButton = _driver.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            addPlaylistButton.Click();
            //Step 3 spell check all content (fields/values), including place holder text
            //Step 4 Select 'X' to close window
            IWebElement playlistAddXButton = _driver.FindElement(By.CssSelector(BaseStrings.playListXoutCssSelector));
            playlistAddXButton.Click();
            //Step 5 Select '+' to add new playlist
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            IWebElement addPlaylistButton1 = _driver.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            addPlaylistButton1.Click();
            //Step 6 Click outside the window to close it 
            IWebElement offClick = _driver.FindElement(By.CssSelector(BaseStrings.offClickCssSelector));
            offClick.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 7 Select '+' to add new playlist
            IWebElement addPlaylistButton2 = _driver.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            addPlaylistButton2.Click();
            //Step 8 Select Create a Custom Playlist - Filtered Check box
            IWebElement createCustomPlaylistCheckbox = _driver.FindElement(By.CssSelector(BaseStrings.createCustomPlaylistCssSelector));
            createCustomPlaylistCheckbox.Click();

            //Step 9 Select Save
            IWebElement saveButton = _driver.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton.Click();
            //Step 10 Select Ok
            IAlert alert = _driver.SwitchTo().Alert();
            alert.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 11 Enter a playlist name
            string playlistName = "Automated Playlist Test " + DateTime.Now.ToString();
            IWebElement playlistAddForm = _driver.FindElement(By.Id("form-name"));
            playlistAddForm.SendKeys(playlistName);

            //Step 12 Select save
            saveButton.Click();
            IAlert mustSelectLocaiton = _driver.SwitchTo().Alert();
            mustSelectLocaiton.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

            //Step 13 Enter location name
            IWebElement locationInput = _driver.FindElement(By.Id("select-filter-location"));
            locationInput.SendKeys("System Test Location Two Buick");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            IWebElement selectLocationFromDropDown = _driver.FindElement(By.CssSelector("#eac-container-select-filter-location > ul > li"));
            selectLocationFromDropDown.Click();
            /* locationInput.SendKeys(Keys.Down);
             //IWebElement selectLocationFilter = _driver.FindElement(By.Id("select-filter-location-device"));
             ////create select element object 
             //var selectLocationElement = new SelectElement(selectLocationFilter);
             //selectLocationElement.SelectByText("Test");

             System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
             //string filterID = "//*[@id='playlist-info-form']/div[1]/div[2]/div//*[@id='select-filter']";
             //IWebElement selectFilter = GetElement(By.XPath(filterID));
             //create select element object 
             //selectFilter.SendKeys("chevy" + Keys.Enter);
             */

            //Step 14 Select save
            IWebElement saveButton1 = _driver.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton1.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            IAlert alert1 = _driver.SwitchTo().Alert();
            alert1.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 15 Select device drop down
            IWebElement selectLocationDeviceFilter = _driver.FindElement(By.Id("select-filter-location-device"));
            //create select element object 
            var selectLocationDeviceElement = new SelectElement(selectLocationDeviceFilter);

            //Step 16 Select all devices
            selectLocationDeviceElement.SelectByValue("all");
            //Step 17 Select save
            saveButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 18 New playlist has been created

            //Step 19 Select '+' to add a new playlist
            IWebElement addPlaylistButton3 = _driver.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            addPlaylistButton3.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 20 Logout
            Logout();


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
        public void CreateAPlaylistHappyPath()
        {
            //Step 1 login 
            Login();
            //Step 2 select '+' to make a new playlist
            IWebElement playlistsSideBarMenuButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            WaitForMaskModal();
            IWebElement addPlaylistButton = _driver.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            addPlaylistButton.Click();
            //Step 3 enter playlist name
            string playlistName = "Automated Playlist Test " + DateTime.Now.ToString();
            IWebElement playlistAddForm = _driver.FindElement(By.Id("form-name"));
            playlistAddForm.SendKeys(playlistName);
            //Step 4 Select a channel
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            string filterID = "//*[@id='playlist-info-form']/div[1]/div[2]/div//*[@id='select-filter']";
            IWebElement selectFilter = GetElement(By.XPath(filterID));
            //create select element object 
            selectFilter.SendKeys("chevy" + Keys.Enter);
            //Step 5  select save
            IWebElement saveButton = _driver.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton.Click();
            //Step 6 select done---does not exist currently (09/26/2018)
            //Step 7 new playlist has been created
            //Step 8 logout
            Logout();

        }

        public void AddWeatherWidget()
        {
            IWebElement weatherWidget = _driver.FindElement(By.CssSelector(BaseStrings.weatherWidgetCSSSelector));
            WaitForMaskModal();
            weatherWidget.Click();

            IWebElement weatherZipCodeInput = _driver.FindElement(By.Id(BaseStrings.weatherZipCodeInputID));
            weatherZipCodeInput.SendKeys("53142");

            IWebElement weatherWidgetSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.weatherWidgetSaveButtonCSSSelector));
            weatherWidgetSaveButton.Click();

            IWebElement playlistSave = _driver.FindElement(By.CssSelector(BaseStrings.playlistSaveCSSSelector));
            WaitForMaskModal();
            playlistSave.Click();



            //TODO: Assert that the saved worked.
        }
        //[TestCase]
        //public void EditWeatherWidget()
        //{
        //    Login();
        //    CreatePlaylists();
        //    IWebElement playlistOpenButton = _driver.FindElement(By.CssSelector(Base.playlistOpenButtonCSSSelector));
        //    playlistOpenButton.Click();
        //    AddWeatherWidget();

        //    //IWebElement weatherEditButton = _driver.FindElement(By.TagName("button"));

        //    //weatherEditButton.Click();

        //    //IWebElement weatherEditButton1 = _driver.FindElement(By.TagName("<button type="button" data-button-type="edit" title="Edit" class="lgfe - cm - utility - button button - unstyled js - playlist - edit"><span aria-hidden="true" class="[fa fa - pencil]"></span> <span class="visually - hidden">Edit </span></button>"));

        //    IWebElement weatherEditButton = _driver.FindElement(By.ClassName("js-playlist-edit"));

        //    //Assert.IsTrue(bodyTag.Text.Contains("weather"));

        //    weatherEditButton.Click();

        //    //if (bodyTag.GetText().contains("weather")
        //    //   {
        //    //    weatherEditButton.Click(); 
        //    //   }
        //}

        public void AddFinanceWidget()
        {
            IWebElement financeWidget = _driver.FindElement(By.CssSelector(BaseStrings.financeWidgetCSSSelector));
            WaitForMaskModal();
            financeWidget.Click();

            IWebElement selectFinanceFilter = _driver.FindElement(By.Id("select-brand"));
            //create select element object 

            var selectFinanceElement = new SelectElement(selectFinanceFilter);
            selectFinanceElement.SelectByText("Chevy");

            IWebElement saveFinanceButton = _driver.FindElement(By.CssSelector(BaseStrings.saveFinanceButtonCSSSelector));
            WaitForMaskModal();
            saveFinanceButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddTrafficWidget()
        {
            IWebElement trafficWidget = _driver.FindElement(By.CssSelector(BaseStrings.trafficWidgetCssSelector));
            WaitForMaskModal();
            trafficWidget.Click();

            IWebElement selectTrafficBrandFilter = _driver.FindElement(By.Id("select-brand"));
            //create select element object 

            var selectTrafficBrandElement = new SelectElement(selectTrafficBrandFilter);
            selectTrafficBrandElement.SelectByText("Chevy");

            IWebElement trafficZipInput = _driver.FindElement(By.Id("traffic-widget-zip"));
            trafficZipInput.SendKeys("53142");

            IWebElement trafficWidgetSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.trafficWidgetSaveButtonCssSelector));
            WaitForMaskModal();
            trafficWidgetSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddTriviaWidget()
        {
            IWebElement triviaWidget = _driver.FindElement(By.CssSelector(BaseStrings.triviaWidgetCssSelector));
            WaitForMaskModal();
            triviaWidget.Click();//NOT VISIBLE on half screen

            IWebElement selectBrandTriviaFilter = _driver.FindElement(By.Id("select-brand"));
            //create select element object 

            var selectTriviaBrandElement = new SelectElement(selectBrandTriviaFilter);
            selectTriviaBrandElement.SelectByText("Chevy");

            IWebElement selectDurationTriviaFilter = _driver.FindElement(By.Id("select-duration"));
            //create select element object 

            //var selectDurationBrandElement = new SelectElement(selectDurationTriviaFilter);
            //selectDurationBrandElement.SelectByText("1");
            //Just taking the default value here I am having a hard time grabbing the value from the drop down

            IWebElement triviaSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.triviaSaveButtonCssSelector));
            WaitForMaskModal();
            triviaSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddHealthWidget()
        {

        }

        public void AddImageWidget()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15));

            IWebElement imageWidget = _driver.FindElement(By.CssSelector(BaseStrings.imageWidgetCssSelector));
            WaitForMaskModal();
            imageWidget.Click();

            IWebElement imageAssestLibrarySearchInput = _driver.FindElement(By.Id("asset-search"));
            imageAssestLibrarySearchInput.SendKeys("dci");  //in the future this should grab the whole collection of assests and pick a random asset          

            IWebElement imageAssestSelection = _driver.FindElement(By.CssSelector(BaseStrings.assestCssSelector));
            WaitForMaskModal();
            imageAssestSelection.Click();

            IWebElement assestLibraryDoneButton = _driver.FindElement(By.CssSelector(BaseStrings.assestLibraryDoneButtonCssSelector));
            WaitForMaskModal();
            assestLibraryDoneButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddVideoWidget()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

            IWebElement videoWidgetButton = _driver.FindElement(By.CssSelector(BaseStrings.videoWidgetCssSelector));

            videoWidgetButton.Click();

            IWebElement videoAssestLibrarySearchInput = _driver.FindElement(By.Id("asset-search"));
            videoAssestLibrarySearchInput.SendKeys("a");  //in the future this should grab the whole collection of assests and pick a random asset

            IWebElement videoAssestSelection = _driver.FindElement(By.XPath(BaseStrings.videoAssestSelectionXPath));
            IWebElement videoWidgetDoneButton = _driver.FindElement(By.XPath(BaseStrings.videoWidgetDoneButtonXpath));
            //WaitForMaskModal();
            videoAssestSelection.Click();


            IWebElement videoPlayIcon = _driver.FindElement(By.XPath(BaseStrings.videoPlayIconXpath));
            videoPlayIcon.Click();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

            videoWidgetDoneButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            WaitForMaskModal();
            //TODO: Assert that the saved worked.
        }

        public void AddScreenFeedWidget()
        {
            IWebElement screenfeedWidgetButton = _driver.FindElement(By.CssSelector(BaseStrings.screenFeedWidgetCssSelector));
            WaitForMaskModal();
            screenfeedWidgetButton.Click();

            IWebElement selectScreenFeedFilter = _driver.FindElement(By.XPath("//*[@id='select-duration']"));

            var selectScreenFeedElement = new SelectElement(selectScreenFeedFilter);
            selectScreenFeedElement.SelectByText("Best Bites");


            IWebElement selectScreenFeedNumberFilter = _driver.FindElement(By.XPath("//*[@id='select-duration']"));

            IWebElement screenFeedSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.screedFeedSaveButtonCssSelector));
            screenFeedSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddBrandWidget()
        {
            IWebElement brandWidgetButton = _driver.FindElement(By.CssSelector(BaseStrings.brandWidgetCssSelector));
            WaitForMaskModal();
            brandWidgetButton.Click();

            IWebElement brandSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.brandSaveButtonCssSelector));
            brandSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void PlaylistSchedule()
        {
            //IWebElement playlistOpenButton = _driver.FindElement(By.CssSelector(Base.playlistOpenButtonCSSSelector));
            //playlistOpenButton.Click();

            IWebElement schedulePlaylist = _driver.FindElement(By.CssSelector(BaseStrings.schedulePlaylistCssSelector));
            WaitForMaskModal();
            schedulePlaylist.Click();

            IWebElement schedulePlaylistStart = _driver.FindElement(By.Id("asset-begin-date-range"));
            schedulePlaylistStart.Clear();

            DateTime dateInputStart = DateTime.Today;
            DateTime dateInputEnd = dateInputStart.AddDays(30);

            schedulePlaylistStart.SendKeys(dateInputStart.ToString("MM/dd/yyyy"));

            IWebElement schedulePlaylistEnd = _driver.FindElement(By.Id("asset-end-date-range"));
            schedulePlaylistEnd.Clear();
            schedulePlaylistEnd.SendKeys(dateInputEnd.ToString("MM/dd/yyyy"/*+Keys.Enter*/));

            IWebElement allDayCheckBox = _driver.FindElement(By.CssSelector("#asset-info-form > div.lg-modal__field.schedule-modal-time-wrapper > div:nth-child(1) > label"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            allDayCheckBox.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));//not waiting on mask modal clicking the calendar pop up

            IWebElement submitSchedule = _driver.FindElement(By.XPath(BaseStrings.submitScheduleCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));//not waiting on mask modal clicking the calendar pop up

            IWebElement gearIcon = _driver.FindElement(By.XPath(BaseStrings.gearIconXpath));
            gearIcon.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

            submitSchedule.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            WaitForMaskModal();
            //TODO: Assert that the saved worked.

        }

        public void PlaylistPublish()
        {
            IWebElement playlistPublishButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistPublishButtonCssSelector));
            WaitForMaskModal();
            playlistPublishButton.Click();

            //if (_configuration.Environment == Common.EnvironmentType.Prod)
            //{
            //    url = url.Replace(".prod", string.Empty);
            //}
            //            url = (_configuration.Environment == Common.EnvironmentType.Prod) ?  : url;

            IWebElement playlistDonePublishButton = _driver.FindElement(By.CssSelector(BaseStrings.publishDoneButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            WaitForMaskModal();
            playlistDonePublishButton.Click();

            //TODO: Assert that the published worked. Might be an API call.
        }

        [TestCase]
        public void LiveguidePlaylists()
        {
            Login();

            /*Start Playlists TestCase Suite ID 69 with 5 parts*/

            CreatePlaylists();

            IWebElement playlistOpenButton = GetElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));

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

            IWebElement playlistsSideBarMenuButton = GetElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistsSideBarMenuButton.Click();

            //String expectedMessage = "Automated Playlist Test";
            //String message = _driver.FindElement(By.CssSelector(BaseStrings.newPlaylistDiv)).Text;
            //Assert.True(message.Contains(expectedMessage));

            DeleteProtocolWITHOUTlogin();

            Logout();

        }

        [TestCase]
        public void Login()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Url;
            _driver.Navigate().GoToUrl(url);

            IWebElement query = GetElement(ByType.Id, "login-email");

            query.SendKeys("cbam.lgtest1@dciartform.com");
            query = GetElement(ByType.Id, "login-password");
            query.SendKeys("Cbam#test1");

            query.Submit();

            WaitForElementExists("page-header-container");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            string playlistDivCssSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div";
            IWebElement playlistDiv = GetElement(By.CssSelector(playlistDivCssSelector));
            //if playlists is empty find profile dropdown 

            //if (playlistDiv.Text.Contains(""))
            //{
            IWebElement playerChannelDropdown = GetElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));

            playerChannelDropdown.Click();

            IWebElement gmChannelSelection = GetElement(By.XPath(BaseStrings.gmChannelSelectionXPath));

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            gmChannelSelection.Click();
            //}

            //if (!playlistDiv.Displayed)
            //{
            //    IWebElement playerChannelDropdown = GetElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));

            //    playerChannelDropdown.Click();

            //    IWebElement gmChannelSelection = GetElement(By.XPath(BaseStrings.gmChannelSelectionXPath));

            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //    gmChannelSelection.Click();
            //}
        }

        [TestCase]
        public void LiveguideAssets()
        {
            Login();

            //IWebElement playlistsSideBarMenuButton = _driver.FindElement(By.CssSelector(Base.playlistSideBarMenuCssSelector));
            //playlistsSideBarMenuButton.Click();

            //TODO: Assert that we are on the playlist page

            IWebElement playlistSearch = _driver.FindElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");

            //TODO: Assert that a model is up. 

            AssetUploadingImage();

            AssetUploadingVideo();

        }

        public void AssetUploadingImage()
        {
            IWebElement playlistOpenButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));
            WaitForMaskModal();
            playlistOpenButton.Click();

            IWebElement uploadButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadButtonCssSelector));
            WaitForMaskModal();
            uploadButton.Click();

            IWebElement uploadFromButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadFromPCCssSelector));
            WaitForMaskModal();
            uploadFromButton.Click();

            //TODO: Need a better way to get a file to upload. Maybe define a data folder in the solution for now.

            MiscLib.WindowsFormHelper.GetAutoIt("Open", @"C:\Users\enwright\Desktop\galaxie.jpg");

            IWebElement uploadDialogCloseButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadDialogCloseButtonCssSelector));
            uploadDialogCloseButton.Click();

            //TODO: Assert here to see if the images are uploaded.

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(50));
        }

        public void AssetUploadingVideo()
        {
            //IWebElement playlistOpenButton = _driver.FindElement(By.CssSelector(Base.playlistOpenButtonCSSSelector));
            //playlistOpenButton.Click();

            IWebElement uploadButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadButtonCssSelector));
            WaitForMaskModal();
            uploadButton.Click();

            IWebElement uploadFromButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadFromPCCssSelector));
            WaitForMaskModal();
            uploadFromButton.Click();

            //TODO: Need a better way to get a file to upload. Maybe define a data folder in the solution for now.
            MiscLib.WindowsFormHelper.GetAutoIt("Open", @"C:\Users\enwright\Desktop\Toy_car.mov");


            IWebElement uploadDialogCloseButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadDialogCloseButtonCssSelector));
            WaitForMaskModal();
            uploadDialogCloseButton.Click();

            //TODO: Assert here to see if the images are uploaded.
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(50));
        }

        public void Logout()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Url;
            _driver.Navigate().GoToUrl(url);

            IWebElement logOutButton = _driver.FindElement(By.CssSelector(BaseStrings.logOutButtonCssSelector));
            WaitForMaskModal();
            logOutButton.Click();

            IWebElement confirmLogOutButton = _driver.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector));
            WaitForMaskModal();
            confirmLogOutButton.Click();

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            //TODO: Assert that we are logged out based on URL and maybe the Username/password fields.
        }

        [TestCase]
        public void LogoutAfterLogin()//postive test for Test case 1456
        {
            //step 1
            Login();

            string url = Common.LgUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Url;
            _driver.Navigate().GoToUrl(url);

            IWebElement logOutButton = _driver.FindElement(By.CssSelector(BaseStrings.logOutButtonCssSelector));
            WaitForMaskModal();
            //step 2
            logOutButton.Click();

            IWebElement logOutCancelButton = _driver.FindElement(By.CssSelector(BaseStrings.logOutCancelButtonCssSelector));
            WaitForMaskModal();
            //step 3
            logOutCancelButton.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //Step 4
            logOutButton.Click();


            IWebElement confirmLogOutButton = _driver.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector));
            WaitForMaskModal();
            //step 5
            confirmLogOutButton.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 6
            Login();

            IWebElement playerChannelDropdown = GetElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));

            WaitForMaskModal();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 7
            playerChannelDropdown.Click();

            IWebElement LogOutChannelSelection = GetElement(By.XPath(BaseStrings.logOutChannelSelectionXPath));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            //step 8
            LogOutChannelSelection.Click();


            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            IWebElement logOutCancelButton2 = _driver.FindElement(By.CssSelector(BaseStrings.logOutCancelButtonCssSelector2));
            WaitForMaskModal();
            logOutCancelButton2.Click();//step 9

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            playerChannelDropdown.Click();
            IWebElement LogOutChannelSelection2 = GetElement(By.XPath(BaseStrings.logOutChannelSelectionXPath2));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            LogOutChannelSelection2.Click();//step 10

            IWebElement confirmLogOutButton2 = _driver.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector2));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            confirmLogOutButton2.Click();//step 11 

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            Login();//step 12

            _driver.Quit();


            //TODO: Assert that we are logged out based on URL and maybe the Username/password fields.
        }
        [TestCase]//Test case 585
        public void DeletePlaylist()
        {
            //Step 1
            Login();
            //Step 2
            IWebElement deletePlaylistButton = GetElement(By.CssSelector(BaseStrings.deletePlaylistButtonCssSelector));
            deletePlaylistButton.Click();
            //Step 3 spell check
            //Step 4 select cancel
            IAlert cancel = _driver.SwitchTo().Alert();
            cancel.Dismiss();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 5
            deletePlaylistButton.Click();
            //Step 6
            IAlert accept = _driver.SwitchTo().Alert();
            accept.Accept();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 7 refresh screen
            _driver.Navigate().Refresh();
            //Step 8 logout
            Logout();
            //Step 9
            Login();
            //Step 10
            IWebElement playlistsSideBarMenuButton = GetElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();
            //Step 11
            Logout();
        }
    

        [TestCase]
        public void DeleteProtocol()
        {
            Login();

            IWebElement playlistsSideBarMenuButton = GetElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();

            IWebElement playlistSearch = GetElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");

            IWebElement newPlaylistDeleteButton = GetElement(By.CssSelector(BaseStrings.newPlaylistDeleteButtonCSSSelector));

            if (newPlaylistDeleteButton.Displayed)
            {
                IWebElement deletePlaylistButton = GetElement(By.CssSelector(BaseStrings.deletePlaylistButtonCssSelector));

                deletePlaylistButton.Click();

                IAlert alert = _driver.SwitchTo().Alert();

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
            IWebElement playlistsSideBarMenuButton = GetElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();

            IWebElement playlistSearch = GetElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            IWebElement newPlaylistDeleteButton = GetElement(By.CssSelector(BaseStrings.newPlaylistDeleteButtonCSSSelector));
            String expectedMessage = "Automated Playlist Test";
            String message = _driver.FindElement(By.CssSelector(BaseStrings.newPlaylistDiv)).Text;
            Assert.True(message.Contains(expectedMessage));

            if (message.Contains(expectedMessage))
            {
                String newPlaylistDiv = _driver.FindElement(By.CssSelector(BaseStrings.newPlaylistDiv)).Text;
                Assert.True(newPlaylistDiv.Contains(expectedMessage));

                if (newPlaylistDiv.Contains(expectedMessage))
                {
                    IWebElement playlistArea = GetElement(By.ClassName("playlists-content"));

                    var playlistContext = playlistArea.Text;

                    IWebElement deletePlaylistButton = _driver.FindElement(By.CssSelector(BaseStrings.deletePlaylistButtonCssSelector));

                    if (playlistContext.Contains(expectedMessage))
                    {

                        WaitForMaskModal();

                        deletePlaylistButton.Click();

                        IAlert alert = _driver.SwitchTo().Alert();

                        alert.Accept();

                        WaitForMaskModal();

                        playlistSearch.SendKeys("Automated Playlist Test");

                        WaitForMaskModal();

                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                        //TODO: Validate the playlist has been deleted. API??
                    }

                    //Logout();//here

                }
                else
                {
                    //Logout();//here
                }
            }
            else
            {
                //Logout(); //here
            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Logout();
        }

        [TestCase]
        public void ContactUsWithrequiredFields()
        {
            //step 1
            Login();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            IWebElement contactUsLink = GetElement(By.CssSelector(BaseStrings.contactUsLinkCssSelector));
            WaitForMaskModal();

            //step 2
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            contactUsLink.Click();

            IWebElement sendButton = GetElement(By.CssSelector(BaseStrings.sendButtonCssSelector));
            WaitForMaskModal();

            //step 3 
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            sendButton.Click();

            // step 4
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            IWebElement EmailUsFullNameInput = GetElement(By.Id("full-name"));
            EmailUsFullNameInput.SendKeys("Automated Tester");

            //step 5
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            WaitForMaskModal();
            sendButton.Click();

            //Step 6
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            IWebElement EmailUsPhoneNumberInput = GetElement(By.Id("phone"));
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
            IWebElement EmailUsEmialInput = GetElement(By.Id("email"));
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
            IWebElement EmailUsCommentsInput = GetElement(By.Id("comments"));
            EmailUsCommentsInput.SendKeys("Automated Tester");

            //Step 23
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            sendButton.Click();

            //Step 24
            IWebElement ContactUsDoneButton = _driver.FindElement(By.CssSelector("#notifications-form > div > button"));
            ContactUsDoneButton.Click();

            Logout();
        }

        [TestCase]//Test Case #1459
        public void ContactUsWithAllFields()
        {
            //Step 1
            Login();

            //Step 2
            IWebElement contactUsLink = GetElement(By.CssSelector(BaseStrings.contactUsLinkCssSelector));
            WaitForMaskModal();
            contactUsLink.Click();

            //Step 3
            IWebElement EmailUsFullNameInput = GetElement(By.Id("full-name"));
            EmailUsFullNameInput.SendKeys("Automated Tester");

            //Step 4
            IWebElement EmailUsTitleInput = GetElement(By.Id("title"));
            EmailUsTitleInput.SendKeys("Automated Tester");

            //Step 5
            IWebElement EmailUsCompanyInput = GetElement(By.Id("company"));
            EmailUsCompanyInput.SendKeys("Automated Tester");

            //Step 6
            IWebElement EmailUsPhoneNumberInput = GetElement(By.Id("phone"));
            EmailUsPhoneNumberInput.SendKeys("9876543210");

            //Step 7
            IWebElement EmailUsEmialInput = GetElement(By.Id("email"));
            EmailUsEmialInput.SendKeys("AutomatedTester@dcim.com");

            //Step 8
            IWebElement EmailUsCommentsInput = GetElement(By.Id("comments"));
            EmailUsCommentsInput.SendKeys("Automated Tester");

            IWebElement sendButton = GetElement(By.CssSelector(BaseStrings.sendButtonCssSelector));
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

            IWebElement ContactUsDoneButton = _driver.FindElement(By.CssSelector("#notifications-form > div > button"));
            //Step 10
            ContactUsDoneButton.Click();

            //if (EmailUsFullNameInput.Displayed)
            //{
            //    var errorFullName = GetElement(By.CssSelector("#contact-us-container > form > div:nth-child(2)")).Text;



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
            Logout();
        }

        [TestCase]//Test Case #1460
        public void PlayersStatus()
        {
            //Step 1
            Login();

            //Step 2 select players tab
            IWebElement playersTab = _driver.FindElement(By.CssSelector("#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li:nth-child(3) > a > span"));
            playersTab.Click();
            WaitForMaskModal();

            //Step 3 confirm that each player contains one status---needs work TODO
            string playersGraph = _driver.FindElement(By.CssSelector("#players-table > tbody")).Text;

            String expectedMessage = "ONLINE";
            Assert.True(playersGraph.Contains(expectedMessage));

            //Step 4 select a player
            IWebElement playerSelect = _driver.FindElement(By.CssSelector("#player-LG-QAROB"));
            playerSelect.Click();

            //Step 5
            playersTab.Click();
            WaitForMaskModal();

            //Step 6
            IWebElement playerSelect2 = _driver.FindElement(By.CssSelector("#player-LG-SCOTT"));
            playerSelect2.Click();

            //Step 7
            playersTab.Click();
            WaitForMaskModal();

            //Step 8 select a player that is not set up...---needs work TODO

            //Step 9
            playersTab.Click();
            WaitForMaskModal();

            //Step 10
            Logout();
        }

        [TestCase]// test case #1463
        public void PlayerEdits()
        {
            //Step 1
            Login();

            //step 2
            IWebElement playersTab = _driver.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 3 
            IWebElement playerSelect = _driver.FindElement(By.CssSelector("#player-LG-QAROB"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();

            //Step 4 
            IWebElement playerInfoDownArrow = _driver.FindElement(By.CssSelector(BaseStrings.playerInfoDownArrowCssSelectors));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerInfoDownArrow.Click();

            //Step 5
            IWebElement playerInfoUpArrow = _driver.FindElement(By.CssSelector(BaseStrings.playerInfoUpArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            playerInfoUpArrow.Click();

            //Step 6
            IWebElement playerInfoX = _driver.FindElement(By.CssSelector(BaseStrings.playerInfoXCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerInfoX.Click();

            //Step 7,8,& 9 are skipped since there isnt a products section right now
            //Step 10
            IWebElement deviceDownArrow = _driver.FindElement(By.CssSelector(BaseStrings.deviceDownArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceDownArrow.Click();

            //Step 11
            IWebElement deviceUpArrow = _driver.FindElement(By.CssSelector(BaseStrings.deviceUpArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceUpArrow.Click();

            //Step 12 
            IWebElement xOnDevice = _driver.FindElement(By.CssSelector(BaseStrings.xOnDeviceCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            xOnDevice.Click();

            //Step 13 select down arrow for location
            IWebElement locationDownArrow = _driver.FindElement(By.CssSelector(BaseStrings.locationDownArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            locationDownArrow.Click();

            //Step 14 
            IWebElement locationUpArrow = _driver.FindElement(By.CssSelector(BaseStrings.locationUpArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            locationUpArrow.Click();

            //Step 15
            IWebElement locationXButton = _driver.FindElement(By.CssSelector(BaseStrings.locationXButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            locationXButton.Click();

            //Step 16
            IWebElement playlistsButton = _driver.FindElement(By.XPath(BaseStrings.playlistsButtonXPath));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistsButton.Click();

            //Step 17
            IWebElement whatsPlayingDownArrow = _driver.FindElement(By.CssSelector(BaseStrings.whatsPlayingDownArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            whatsPlayingDownArrow.Click();

            //Step 18
            IWebElement whatsPlayingUPArrow = _driver.FindElement(By.CssSelector(BaseStrings.whatsPlayingUpArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            whatsPlayingUPArrow.Click();

            //Step 19
            IWebElement whatsPlayingXButton = _driver.FindElement(By.CssSelector(BaseStrings.whatsPlayingXButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            whatsPlayingXButton.Click();

            //Step 20
            IWebElement playlistInfoDownArrow = _driver.FindElement(By.CssSelector(BaseStrings.playlistInfoDownArrowCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoDownArrow.Click();

            //Step 21
            IWebElement playlistInfoUpArrow = _driver.FindElement(By.XPath(BaseStrings.playlistInfoUpArrowXpath));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoUpArrow.Click();

            //Step 22
            IWebElement playlistInfoXButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistInfoXButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playlistInfoXButton.Click();

            //Step 23,24,25,26,27,28,29,30,31 are not present on frontend that i can see as of 09/18/18

            //Step 31
            Logout();
        }

        [TestCase]
        public void PlayerEditDevice()
        {
            //Step 1
            Login();

            //Step 2 select player tab
            IWebElement playersTab = _driver.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Step 3 Select Any player
            IWebElement playerSelect = _driver.FindElement(By.CssSelector("#player-LG-QAROB"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();

            //Step 4 From the Device card, slect the ping device button
            IWebElement devicePingDeviceButton = _driver.FindElement(By.XPath(BaseStrings.devicePingDeviceButtonXPath));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            devicePingDeviceButton.Click();

            //Step 5 Double click on the image that display under the Ping Data
            IWebElement pingDataImage = _driver.FindElement(By.Id("sampleScreen"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //new SelectElement(_driver.FindElement(By.Id("sampleScreen")).Click());
            new Actions(_driver).DoubleClick(_driver.FindElement(By.Id("sampleScreen"))).Perform();

            //Step 6 Select cancel
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            IAlert alert = _driver.SwitchTo().Alert();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            alert.Dismiss();

            //Step 7 Double click on the image that display under ping data
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            new Actions(_driver).DoubleClick(_driver.FindElement(By.Id("sampleScreen"))).Perform();

            //Step 8 Select Ok
            IAlert alert2 = _driver.SwitchTo().Alert();
            alert2.Accept();

            //Step 9 Select refresh button
            IWebElement pageRefreshButton = _driver.FindElement(By.CssSelector(BaseStrings.pageRefreshButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            pageRefreshButton.Click();

            //Step 10 from the device card , select the refresh app button
            IWebElement deviceRefreshAppButton = _driver.FindElement(By.CssSelector(BaseStrings.deviceRefreshAppButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceRefreshAppButton.Click();
            IAlert alert3 = _driver.SwitchTo().Alert();
            alert3.Accept();
            //Step 11 From the device card select the restart app button
            IWebElement deviceRestartAppButton = _driver.FindElement(By.CssSelector(BaseStrings.deviceRestartAppButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceRestartAppButton.Click();
            IAlert alert4 = _driver.SwitchTo().Alert();
            alert4.Accept();

            //Step 12 From the device card, select the restart device button
            IWebElement deviceRestartDeviceButton = _driver.FindElement(By.CssSelector(BaseStrings.deviceRestartDeviceButtonCssSelector));

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            deviceRestartDeviceButton.Click();
            IAlert alert5 = _driver.SwitchTo().Alert();
            alert5.Accept();

            //Step 13 LOGout
            Logout();

        }

        [TestCase]//Test case #1469
        public void PlayerAddNewChannel()
        {
            //step 1
            Login();
            //Step 2 select player tab
            IWebElement playersTab = _driver.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 Select Any player
            IWebElement playerSelect = _driver.FindElement(By.CssSelector("#player-LG-QAROB"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();
            //step 4 channelJoinButtonCssSelector
            IWebElement channelJoinButton = _driver.FindElement(By.CssSelector(BaseStrings.channelJoinButtonCssSelector));//nothing should happen
            //Step 5
            IWebElement channelFilterInput = _driver.FindElement(By.Id("channel-input"));
            channelFilterInput.SendKeys("test");
            //Step 6
            channelJoinButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //IAlert alert = _driver.SwitchTo().Alert();
            //alert.Accept();
            //IWebElement xDeleteFilterButton = _driver.FindElement(By.CssSelector(BaseStrings.xOutButtonCssSelector));
            //xDeleteFilterButton.Click();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //alert.Accept();
            //Step 7 
            channelFilterInput.Clear();
            channelFilterInput.SendKeys("test filter");
            //Step 8 
            channelJoinButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //IAlert wishToJoinDeviceAlert = _driver.SwitchTo().Alert();
            //alert.Accept();
            //xDeleteFilterButton.Click();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 9
            channelFilterInput.Clear();
            channelFilterInput.SendKeys("test filter 2");
            //Step 10
            channelJoinButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //alert.Accept();
            //xDeleteFilterButton.Click();
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 11  
            IWebElement pageRefreshButton = _driver.FindElement(By.CssSelector(BaseStrings.pageRefreshButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            pageRefreshButton.Click();
            //step 12
            Logout();
        }

        [TestCase]//Test case 1487
        public void PlayerDeleteChannel()
        {
            //step 1
            Login();
            //Step 2 select player tab
            IWebElement playersTab = _driver.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 Select Any player
            IWebElement playerSelect = _driver.FindElement(By.CssSelector("#player-LG-QAROB"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();
            //Step 4 
            IWebElement xOutButton1 = _driver.FindElement(By.CssSelector(BaseStrings.xOutButton1CssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15));
            xOutButton1.Click();
            IAlert alert = _driver.SwitchTo().Alert();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            alert.Dismiss();
            //Step 5
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15));
            xOutButton1.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            alert.Dismiss();
            //Step 6
            IWebElement pageRefreshButton = _driver.FindElement(By.CssSelector(BaseStrings.pageRefreshButtonCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            pageRefreshButton.Click();
            //Step 7
            Logout();
        }

        [TestCase]//Test case 1488
        public void PlayerScreenConnect()
        {
            //Step 1
            Login();
            //Step 2 select player tab
            IWebElement playersTab = _driver.FindElement(By.CssSelector(BaseStrings.playersTabCssSelector));
            playersTab.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 3 Hover over the screen connect icon
            //step 4
            IWebElement playerSelect = _driver.FindElement(By.CssSelector("#player-LG-QAROB"));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            playerSelect.Click();
            //Step 5 Hover over screen connect icon
            //Step 6
            IWebElement screenConnectButton = _driver.FindElement(By.CssSelector(BaseStrings.screenConnectCssSelector));
            screenConnectButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Step 7
            string url = Common.LgUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Url;
            _driver.Navigate().GoToUrl(url);

            var tabs = _driver.WindowHandles;
            if (tabs.Count > 1)
            {
                _driver.SwitchTo().Window(tabs[1]);
                _driver.Close();
                _driver.SwitchTo().Window(tabs[0]);
            }
            //Step 8
            Logout();

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
                    ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                }
            }
            finally
            {
                // Terminates the remote webdriver session
                _driver.Quit();
            }
        }

        #region -- Private Methods ---

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
                query = _driver.FindElement(selector);

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
                query = _driver.FindElement(selector);
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
            WaitUntilElementExists(_driver, By.Id(element));
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
