using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

            //if (_configuration.IsRemoteDriver)
            //{
            //    _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            //}
            //else
            //{
                ChromeOptions co = new ChromeOptions();    // set the desired browser
                co.AddAdditionalCapability("platform", "Windows 7");
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                _driver = new ChromeDriver(path);
            //}

            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
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

            while (maskModal.Displayed)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            //while (overLayModal.Displayed)
            //{
            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            //}
        }

        public void CreatePlaylists()
        {
            IWebElement playlistsSideBarMenuButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            WaitForMaskModal();
            playlistsSideBarMenuButton.Click();



            IWebElement addPlaylistButton = _driver.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            addPlaylistButton.Click();

            IWebElement playlistAddForm = _driver.FindElement(By.Id("form-name"));
            //TODO: send to base strings
            string playlistName = "Automated Playlist Test " + DateTime.Now;

            playlistAddForm.SendKeys(playlistName);

            string filterID = "//*[@id='playlist-info-form']/div[1]/div[2]/div//*[@id='select-filter']";

            IWebElement selectFilter = GetElement(By.XPath(filterID));
            if (selectFilter.Displayed)
            {
                //create select element object 
                selectFilter.SendKeys("chevy" + Keys.Enter);

                var selectElement = new SelectElement(selectFilter);
                selectElement.SelectByText("Chevy TV");
            }


            IWebElement saveButton = _driver.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton.Click();

            //TODO: Assert to check if the playlist was actually playlist got created. 

            IWebElement newPlaylist = GetElement(ByType.ClassName, "lgfe-cm-card");



            //Assert.IsTrue(newPlaylist.Displayed);
            //Assert.AreEqual(newPlaylist, "Automated Playlist Test");

            //TODO: Assert calling API.
            //string apiPlayList = APITests.LG20.SmokeTest.GetPlayListByName("newPlaylist", "username", "password", _configuration.Environment);

            //Assert.AreEqual(newPlaylist, apiPlayList);


            //TODO: Update this assert to take into account the environment.
            Assert.AreEqual("https://portal.test.dcimliveguide.com/#playlists", _driver.Url.Trim());
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
            IWebElement imageWidget = _driver.FindElement(By.CssSelector(BaseStrings.imageWidgetCssSelector));
            WaitForMaskModal();
            imageWidget.Click();

            IWebElement imageAssestLibrarySearchInput = _driver.FindElement(By.Id("asset-search"));
            imageAssestLibrarySearchInput.SendKeys("chev");  //in the future this should grab the whole collection of assests and pick a random asset          

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
            IWebElement videoWidgetButton = _driver.FindElement(By.CssSelector(BaseStrings.videoWidgetCssSelector));
            videoWidgetButton.Click();

            IWebElement videoAssestLibrarySearchInput = _driver.FindElement(By.Id("asset-search"));
            videoAssestLibrarySearchInput.SendKeys("a");  //in the future this should grab the whole collection of assests and pick a random asset

            IWebElement videoAssestSelection = _driver.FindElement(By.CssSelector(BaseStrings.videoAssestSelectionCssSelector));
            WaitForMaskModal();
            videoAssestSelection.Click();

            IWebElement videoWidgetDoneButton = _driver.FindElement(By.CssSelector(BaseStrings.videoWidgetDoneButtonCssSelector));
            //WaitForMaskModal();
            videoWidgetDoneButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            //TODO: Assert that the saved worked.
        }

        public void AddScreenFeedWidget()
        {
            IWebElement screenfeedWidgetButton = _driver.FindElement(By.CssSelector(BaseStrings.screenFeedWidgetCssSelector));
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

            IWebElement allDayCheckBox = _driver.FindElement(By.CssSelector("#asset-info-form > div.lg-modal__field.schedule-modal-time-wrapper > div:nth-child(1) > label > span.lgfe-input-checkbox__custom-input"));
            allDayCheckBox.Click();


            IWebElement submitSchedule = _driver.FindElement(By.CssSelector(BaseStrings.submitScheduleCssSelector));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));//not waiting on mask modal clicking the calendar pop up
            
            submitSchedule.Click();

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

            AddWeatherWidget();

            AddFinanceWidget();

            AddTrafficWidget();

            AddTriviaWidget();

            //AddHealthWidget(); HEALTH WIDGET TO BE ADDED HERE

            AddImageWidget();

            AddVideoWidget();

            AddScreenFeedWidget();

            AddBrandWidget();

            PlaylistSchedule();

            PlaylistPublish();

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

            if (playlistDiv.Text.Contains(""))
            {
                IWebElement playerChannelDropdown = GetElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));

                playerChannelDropdown.Click();
          
                IWebElement gmChannelSelection = GetElement(By.XPath(BaseStrings.gmChannelSelectionXPath));

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                gmChannelSelection.Click();
            }

            if (!playlistDiv.Displayed)
            {
                IWebElement playerChannelDropdown = GetElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));

                playerChannelDropdown.Click();

                IWebElement gmChannelSelection = GetElement(By.XPath(BaseStrings.gmChannelSelectionXPath));

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                gmChannelSelection.Click();
            }
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

            _driver.Quit();
        }
        //[TestCase]
        public void DeleteProtocolWITHOUTlogin()
        {
            //Login();

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

                    if (playlistContext.Contains(expectedMessage))
                    {
                        IWebElement deletePlaylistButton = _driver.FindElement(By.CssSelector(BaseStrings.deletePlaylistButtonCssSelector));

                        WaitForMaskModal();

                        deletePlaylistButton.Click();

                        IAlert alert = _driver.SwitchTo().Alert();

                        alert.Accept();

                        playlistSearch.SendKeys("Automated Playlist Test");

                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                        //TODO: Validate the playlist has been deleted. API??
                    }
                    else
                    {
                        Logout();
                    }
                }
                else
                {
                    Logout();
                }
            }
            else
            {
                Logout();
            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Logout();
        }

        [TestCase]
        public void ContactUsWithrequiredFields()
        {
            Login();

            string contactUsLinkCssSelector = "#interaction-nav-bar-container > div.inbc-help-menu-wrapper > ul > li:nth-child(1) > a";
            IWebElement contactUsLink = GetElement(By.CssSelector(contactUsLinkCssSelector));
            WaitForMaskModal();
            contactUsLink.Click();

            IWebElement EmailUsFullNameInput = GetElement(By.Id("full-name"));
            EmailUsFullNameInput.SendKeys("Automated Tester");

            Logout();
        }

        [TestCase]
        public void ContactUsWithAllFields()
        {
            Login();

            string contactUsLinkCssSelector = "#interaction-nav-bar-container > div.inbc-help-menu-wrapper > ul > li:nth-child(1) > a";
            string sendButtonCssSelector = "#contact-us-container > form > div.lg-modal__actions > button";

            IWebElement contactUsLink = GetElement(By.CssSelector(contactUsLinkCssSelector));
            WaitForMaskModal();
            contactUsLink.Click();

            IWebElement EmailUsFullNameInput = GetElement(By.Id("full-name"));
            EmailUsFullNameInput.SendKeys("Automated Tester");

            IWebElement EmailUsTitleInput = GetElement(By.Id("title"));
            EmailUsTitleInput.SendKeys("Automated Tester");

            IWebElement EmailUsCompanyInput = GetElement(By.Id("company"));
            EmailUsCompanyInput.SendKeys("Automated Tester");

            IWebElement EmailUsPhoneNumberInput = GetElement(By.Id("phone"));
            EmailUsPhoneNumberInput.SendKeys("Automated Tester");

            IWebElement EmailUsEmialInput = GetElement(By.Id("email"));
            EmailUsEmialInput.SendKeys("Automated Tester");

            IWebElement EmailUsCommentsInput = GetElement(By.Id("comments"));
            EmailUsCommentsInput.SendKeys("Automated Tester");

            IWebElement sendButton = GetElement(By.CssSelector(sendButtonCssSelector));
            WaitForMaskModal();
            sendButton.Click();

            var newHtml = _driver.PageSource;

            if (newHtml.Contains("error"))
            {

            }

            if (EmailUsFullNameInput.Displayed)
            {
                var errorFullName = GetElement(By.CssSelector("#contact-us-container > form > div:nth-child(2)")).Text;



                if (errorFullName.Contains("error"))
                {

                }
            }
            else if (EmailUsTitleInput.Displayed)
            {

            }
            else if (EmailUsPhoneNumberInput.Displayed)
            {

            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            //Logout();
        }

        [TearDown]
        public void CleanUp()
        {
        //    bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
        //    try
        //    {
        //        // Logs the result to Sauce Labs
        //        if (_configuration.IsRemoteDriver)
        //    {
        //        ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
        //    }
        //}
        //    finally
        //    {
                // Terminates the remote webdriver session
                _driver.Quit();
            //}
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
