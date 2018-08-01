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


        [TestCase]
        public void LiveGuide20()
        {
            Login();

            WaitForElementExists("page-header-container");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            //TODO: Update this assert to take into account the environment.

            Assert.AreEqual("https://portal.test.dcimliveguide.com/#playlists", _driver.Url.Trim());
        }

        public void CreatePlaylists()
        {
            IWebElement playlistsSideBarMenuButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            playlistsSideBarMenuButton.Click();

            IWebElement addPlaylistButton = _driver.FindElement(By.CssSelector(BaseStrings.addPlaylistsButtonClass));
            addPlaylistButton.Click();

            IWebElement playlistAddForm = _driver.FindElement(By.Id("form-name"));

            //TODO: Need to make the name a bit more specific like add a date to it so you can delete it later. 
            string playlistName = "Automated Playlist Test " + DateTime.Now;

            playlistAddForm.SendKeys(playlistName);

            IWebElement selectFilter = _driver.FindElement(By.Id("select-filter"));
            //create select element object 

            var selectElement = new SelectElement(selectFilter);
            selectElement.SelectByText("Chevy TV");

            IWebElement saveButton = _driver.FindElement(By.CssSelector(BaseStrings.saveButtonCSSSelector));
            saveButton.Click();

            //TODO: Assert to check if the playlist was actually playlist got created. 
            //TODO: Assert calling API.

            //TODO: Update this assert to take into account the environment.
            Assert.AreEqual("https://portal.test.dcimliveguide.com/#playlists", _driver.Url.Trim());
        }

        public void AddWeatherWidget()
        {
            IWebElement weatherWidget = _driver.FindElement(By.CssSelector(BaseStrings.weatherWidgetCSSSelector));

            weatherWidget.Click();

            IWebElement weatherZipCodeInput = _driver.FindElement(By.Id(BaseStrings.weatherZipCodeInputID));
            weatherZipCodeInput.SendKeys("53142");

            IWebElement weatherWidgetSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.weatherWidgetSaveButtonCSSSelector));
            weatherWidgetSaveButton.Click();

            IWebElement playlistSave = _driver.FindElement(By.CssSelector(BaseStrings.playlistSaveCSSSelector));
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
            financeWidget.Click();

            IWebElement selectFinanceFilter = _driver.FindElement(By.Id("select-brand"));
            //create select element object 

            var selectFinanceElement = new SelectElement(selectFinanceFilter);
            selectFinanceElement.SelectByText("Chevy");

            IWebElement saveFinanceButton = _driver.FindElement(By.CssSelector(BaseStrings.saveFinanceButtonCSSSelector));
            saveFinanceButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddTrafficWidget()
        {
            IWebElement trafficWidget = _driver.FindElement(By.CssSelector(BaseStrings.trafficWidgetCssSelector));
            trafficWidget.Click();

            IWebElement selectTrafficBrandFilter = _driver.FindElement(By.Id("select-brand"));
            //create select element object 

            var selectTrafficBrandElement = new SelectElement(selectTrafficBrandFilter);
            selectTrafficBrandElement.SelectByText("Chevy");

            IWebElement trafficZipInput = _driver.FindElement(By.Id("traffic-widget-zip"));
            trafficZipInput.SendKeys("53142");

            IWebElement trafficWidgetSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.trafficWidgetSaveButtonCssSelector));
            trafficWidgetSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddTriviaWidget()
        {
            IWebElement triviaWidget = _driver.FindElement(By.CssSelector(BaseStrings.triviaWidgetCssSelector));
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
            triviaSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddHealthWidget()
        {

        }

        public void AddImageWidget()
        {
            IWebElement imageWidget = _driver.FindElement(By.CssSelector(BaseStrings.imageWidgetCssSelector));
            imageWidget.Click();

            IWebElement imageAssestLibrarySearchInput = _driver.FindElement(By.Id("asset-search"));
            imageAssestLibrarySearchInput.SendKeys("chev");  //in the future this should grab the whole collection of assests and pick a random asset          

            IWebElement imageAssestSelection = _driver.FindElement(By.CssSelector(BaseStrings.assestCssSelector));
            imageAssestSelection.Click();

            IWebElement assestLibraryDoneButton = _driver.FindElement(By.CssSelector(BaseStrings.assestLibraryDoneButtonCssSelector));
            assestLibraryDoneButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddVideoWidget()
        {
            IWebElement videoWidgetButton = _driver.FindElement(By.CssSelector(BaseStrings.videoWidgetCssSelector));
            videoWidgetButton.Click();

            IWebElement videoAssestLibrarySearchInput = _driver.FindElement(By.Id("asset-search"));
            videoAssestLibrarySearchInput.SendKeys("nis");  //in the future this should grab the whole collection of assests and pick a random asset

            IWebElement videoAssestSelection = _driver.FindElement(By.CssSelector(BaseStrings.videoAssestSelectionCssSelector));
            videoAssestSelection.Click();

            IWebElement videoWidgetDoneButton = _driver.FindElement(By.CssSelector(BaseStrings.videoWidgetDoneButtonCssSelector));
            videoWidgetDoneButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddScreenFeedWidget()
        {
            IWebElement screenfeedWidgetButton = _driver.FindElement(By.CssSelector(BaseStrings.screenFeedWidgetCssSelector));
            screenfeedWidgetButton.Click();


            IWebElement selectScreenFeedFilter = _driver.FindElement(By.XPath("//*[@id='select-duration']"));

            var selectScreenFeedElement = new SelectElement(selectScreenFeedFilter);
            selectScreenFeedElement.SelectByText("Action Sports");


            IWebElement selectScreenFeedNumberFilter = _driver.FindElement(By.XPath("//*[@id='select-duration']"));

            IWebElement screenFeedSaveButton = _driver.FindElement(By.CssSelector(BaseStrings.screedFeedSaveButtonCssSelector));
            screenFeedSaveButton.Click();

            //TODO: Assert that the saved worked.
        }

        public void AddBrandWidget()
        {
            IWebElement brandWidgetButton = _driver.FindElement(By.CssSelector(BaseStrings.brandWidgetCssSelector));
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
            schedulePlaylist.Click();

            IWebElement schedulePlaylistStart = _driver.FindElement(By.Id("asset-begin-date-range"));
            schedulePlaylistStart.Clear();
            
            schedulePlaylistStart.SendKeys("August 1, 2018");

            IWebElement schedulePlaylistEnd = _driver.FindElement(By.Id("asset-end-date-range"));
            schedulePlaylistEnd.Clear();
            schedulePlaylistEnd.SendKeys("August 2, 2018");

            IWebElement submitSchedule = _driver.FindElement(By.CssSelector(BaseStrings.submitScheduleCssSelector));
            submitSchedule.Click();

            //TODO: Assert that the saved worked.

        }

        public void PlaylistPublish()
        {
            IWebElement playlistPublishButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistPublishButtonCssSelector));
            playlistPublishButton.Click();

            if (_configuration.Environment == Common.EnvironmentType.Prod)
            {
                url = url.Replace(".prod", string.Empty);
            }
            //            url = (_configuration.Environment == Common.EnvironmentType.Prod) ?  : url;

            IWebElement playlistDonePublishButton = _driver.FindElement(By.CssSelector(BaseStrings.publishDoneButtonCssSelector));
            playlistDonePublishButton.Click();

            //TODO: Assert that the published worked. Might be an API call.
        }

        [TestCase]
        public void LiveguidePlaylists()
        {
            Login();

            /*Start Playlists TestCase Suite ID 69 with 5 parts*/

            CreatePlaylists();

            IWebElement playlistOpenButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistOpenButtonCSSSelector));
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

            //DeleteProtocol();

            Logout();

        }


        [TestCase]
        public void Login()
        {
            string url = Common.LGUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Url;
            _driver.Navigate().GoToUrl(url);

            IWebElement query = GetElement(ByType.Id, "login-email");
            
            query.SendKeys("cbam.lgtest1@dciartform.com");
            query = GetElement(ByType.Id, "login-password");
            query.SendKeys("Cbam#test1");

            query.Submit();

            WaitForElementExists("page-header-container");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            //TODO: Why are we doing this?? 

            if (currentURL != "https://portal.test.dcimliveguide.com/#playlists")
            {
                string playlistsUrl = "https://portal.test.dcimliveguide.com/#playlists";

                _driver.Navigate().GoToUrl(playlistsUrl);

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
            playlistOpenButton.Click();

            IWebElement uploadButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadButtonCssSelector));
            uploadButton.Click();

            IWebElement uploadFromButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadFromPCCssSelector));
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
            uploadButton.Click();

            IWebElement uploadFromButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadFromPCCssSelector));
            uploadFromButton.Click();

            //TODO: Need a better way to get a file to upload. Maybe define a data folder in the solution for now.
            MiscLib.WindowsFormHelper.GetAutoIt("Open", @"C:\Users\enwright\Desktop\Toy_car.mov");


            IWebElement uploadDialogCloseButton = _driver.FindElement(By.CssSelector(BaseStrings.uploadDialogCloseButtonCssSelector));
            uploadDialogCloseButton.Click();

            //TODO: Assert here to see if the images are uploaded.
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(50));
        }

        public void Logout()
        {

            string url = Common.LGUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Url;
            _driver.Navigate().GoToUrl(url);

            //TODO: Why are we checking for this? 

            if (currentURL != "https://portal.test.dcimliveguide.com/#playlists")
            {
                string playlistsUrl = "https://portal.test.dcimliveguide.com/#playlists";

                _driver.Navigate().GoToUrl(playlistsUrl);

            }

            IWebElement logOutButton = _driver.FindElement(By.CssSelector(BaseStrings.logOutButtonCssSelector));
            logOutButton.Click();

            IWebElement confirmLogOutButton = _driver.FindElement(By.CssSelector(BaseStrings.logoutConfirmCssSelector));
            confirmLogOutButton.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            //TODO: Assert that we are logged out based on URL and maybe the Username/password fields.
        }

        [TestCase]
        public void DeleteProtocol()
        {
            Login();

            IWebElement playlistsSideBarMenuButton = _driver.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
            playlistsSideBarMenuButton.Click();

            IWebElement playlistSearch = _driver.FindElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            try
            {

                IWebElement deletePlaylistButton = _driver.FindElement(By.CssSelector(BaseStrings.deletePlaylistButtonCssSelector));

                IWebElement newPlaylistDeleteButton = _driver.FindElement(By.CssSelector(BaseStrings.newPlaylistDeleteButtonCSSSelector));

                if (newPlaylistDeleteButton.Displayed)
                {

                    newPlaylistDeleteButton.Click();

                    IAlert alert = _driver.SwitchTo().Alert();

                    alert.Accept();

                    playlistSearch.SendKeys("Automated Playlist Test");

                    //TODO: Validate the playlist has been deleted. 

                    //newPlaylistDeleteButton.Click();

                    //_driver.SwitchTo().Alert();

                    //alert.Accept();

                    //playlistSearch.SendKeys("Automated Playlist Test");
                }
            }
            catch (NoSuchElementException)
            {
       
                //TODO: These statements can be removed since the Cleanup method will be called automatically. But we should throw an assert still.
                Logout();

                _driver.Quit();
            }

            //    i++;
            //}

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));


            _driver.Quit();
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
            return query;
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
        
    }
}
