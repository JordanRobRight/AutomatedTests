using System;
using System.IO;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using QA.Automation.Common;

namespace QA.Automation.UITests
{
    [TestFixture("chrome", "63", "Windows 10", "", "")]
    public class UnitTest1
    {
        private IWebDriver _driver;
        private String browser;
        private String version;
        private String os;
        private String deviceName;
        private String deviceOrientation;
        //private const int _waitTimeInSeconds = 30;
        //private bool IsRemoteDriver = false;
        private TestConfiguration _configuration => TestConfiguration.GetTestConfiguration();

        //private const string un = @"DCIArtform";

        //private const string ak = @"a4277bd1-3492-4562-99bc-53dd349c52e1";

        public UnitTest1(String browser, String version, String os, String deviceName, String deviceOrientation)
        {
            this.browser = browser;
            this.version = version;
            this.os = os;
            this.deviceName = deviceName;
            this.deviceOrientation = deviceOrientation;
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

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
        }

        /*
        [TestCase]
        public void googleTest()
        {
            _driver.Navigate().GoToUrl("http://www.google.com");
            StringAssert.Contains("Google", _driver.Title);
            IWebElement query = _driver.FindElement(By.Name("q"));
            query.SendKeys("Sauce Labs");
            query.Submit();
        }
        */

        [TestCase]
        public void LiveGuide20()
        {
            Login();

            WaitForElementExists("page-header-container");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            Assert.AreEqual("https://portal.test.dcimliveguide.com/#playlists", _driver.Url.Trim());
        }

        [TestCase]
        public void LiveguidePlaylists()
        {
            Login();

            string addPlaylistsButtonClass = "#playlists-container > div.pm-function-bar.js-playlists-function-bar > div > button";

            string saveButtonCSSSelector = "#playlist-info-form > div.lg-modal__actions > button";
            string playlistOpenButtonCSSSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(2)";
            string weatherWidgetCSSSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-favorite-widgets.js-drag-drop-favorite-widgets > button:nth-child(1)";
            string weatherZipCodeInputID = "weather-widget-zip";
            string weatherWidgetSaveButtonCSSSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
            string playlistSaveCSSSelector = "#playlist-container > div.playlist-content-wrapper.js-playlist-content > div > div:nth-child(3) > button:nth-child(1)";
            string financeWidgetCSSSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-favorite-widgets.js-drag-drop-favorite-widgets > button:nth-child(2)";
            string saveFinanceButtonCSSSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
            string trafficWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-favorite-widgets.js-drag-drop-favorite-widgets > button:nth-child(3)";
            string trafficWidgetSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
            string triviaWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-favorite-widgets.js-drag-drop-favorite-widgets > button:nth-child(4)";
            string triviaSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
            string imageWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-image-widget-button";
            string assestCssSelector = "#asset-image-select-form > div.aisf-image-matrix > div:nth-child(2) > label";
            string assestLibraryDoneButtonCssSelector = "#asset-image-select-form > div.aisf-action-bar > button";
            string videoWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-video-widget-button";
            string videoAssestSelectionCssSelector = "#asset-video-select-form > div.avsf-image-matrix > div:nth-child(1) > label";
            string videoWidgetDoneButtonCssSelector = "#asset-video-select-form > div.avsf-action-bar > button";
            string screenFeedWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-screenfeedvideo-widget-button";
            string screedFeedSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
            string brandWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-brand-widget-button";
            string brandSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";




            IWebElement addPlaylistButton = _driver.FindElement(By.CssSelector(addPlaylistsButtonClass));
            addPlaylistButton.Click();

            IWebElement playlistAddForm = _driver.FindElement(By.Id("form-name"));
            playlistAddForm.SendKeys("Automated Playlist Test");

            IWebElement selectFilter = _driver.FindElement(By.Id("select-filter"));
            //create select element object 

            var selectElement = new SelectElement(selectFilter);
            selectElement.SelectByText("Chevy TV");

            IWebElement saveButton = _driver.FindElement(By.CssSelector(saveButtonCSSSelector));
            saveButton.Click();

            /*Start Playlists TestCase Suite ID 69 with 5 parts*/

            //adding a widget, here this test adds all widgets and uses test dates


            IWebElement playlistOpenButton = _driver.FindElement(By.CssSelector(playlistOpenButtonCSSSelector));
            playlistOpenButton.Click();

            IWebElement weatherWidget = _driver.FindElement(By.CssSelector(weatherWidgetCSSSelector));
            weatherWidget.Click();

            IWebElement weatherZipCodeInput = _driver.FindElement(By.Id(weatherZipCodeInputID));
            weatherZipCodeInput.SendKeys("53142");

            IWebElement weatherWidgetSaveButton = _driver.FindElement(By.CssSelector(weatherWidgetSaveButtonCSSSelector));
            weatherWidgetSaveButton.Click();

            IWebElement playlistSave = _driver.FindElement(By.CssSelector(playlistSaveCSSSelector));
            playlistSave.Click();

            IWebElement financeWidget = _driver.FindElement(By.CssSelector(financeWidgetCSSSelector));
            financeWidget.Click();

            IWebElement selectFinanceFilter = _driver.FindElement(By.Id("select-brand"));
            //create select element object 

            var selectFinanceElement = new SelectElement(selectFinanceFilter);
            selectFinanceElement.SelectByText("Chevy");

            IWebElement saveFinanceButton = _driver.FindElement(By.CssSelector(saveFinanceButtonCSSSelector));
            saveFinanceButton.Click();

            IWebElement trafficWidget = _driver.FindElement(By.CssSelector(trafficWidgetCssSelector));
            trafficWidget.Click();

            IWebElement selectTrafficBrandFilter = _driver.FindElement(By.Id("select-brand"));
            //create select element object 

            var selectTrafficBrandElement = new SelectElement(selectTrafficBrandFilter);
            selectTrafficBrandElement.SelectByText("Chevy");

            IWebElement trafficZipInput = _driver.FindElement(By.Id("traffic-widget-zip"));
            trafficZipInput.SendKeys("53142");

            IWebElement trafficWidgetSaveButton = _driver.FindElement(By.CssSelector(trafficWidgetSaveButtonCssSelector));
            trafficWidgetSaveButton.Click();

            IWebElement triviaWidget = _driver.FindElement(By.CssSelector(triviaWidgetCssSelector));
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

            IWebElement triviaSaveButton = _driver.FindElement(By.CssSelector(triviaSaveButtonCssSelector));
            triviaSaveButton.Click();

            //HEALTH WIDGET TO BE ADDED HERE          

            IWebElement imageWidget = _driver.FindElement(By.CssSelector(imageWidgetCssSelector));
            imageWidget.Click();

            IWebElement imageAssestLibrarySearchInput = _driver.FindElement(By.Id("asset-search"));
            imageAssestLibrarySearchInput.SendKeys("road");  //in the future this should grab the whole collection of assests and pick a random asset          

            IWebElement imageAssestSelection = _driver.FindElement(By.CssSelector(assestCssSelector));
            imageAssestSelection.Click();

            IWebElement assestLibraryDoneButton = _driver.FindElement(By.CssSelector(assestLibraryDoneButtonCssSelector));
            assestLibraryDoneButton.Click();

            IWebElement videoWidgetButton = _driver.FindElement(By.CssSelector(videoWidgetCssSelector));
            videoWidgetButton.Click();

            IWebElement videoAssestLibrarySearchInput = _driver.FindElement(By.Id("asset-search"));
            videoAssestLibrarySearchInput.SendKeys("service");  //in the future this should grab the whole collection of assests and pick a random asset

            IWebElement videoAssestSelection = _driver.FindElement(By.CssSelector(videoAssestSelectionCssSelector));
            videoAssestSelection.Click();

            IWebElement videoWidgetDoneButton = _driver.FindElement(By.CssSelector(videoWidgetDoneButtonCssSelector));
            videoWidgetDoneButton.Click();

            IWebElement screenfeedWidgetButton = _driver.FindElement(By.CssSelector(screenFeedWidgetCssSelector));
            screenfeedWidgetButton.Click();

            //string screenFeedFilterCssSelector = '//*[@id="select - duration"]';
            IWebElement selectScreenFeedFilter = _driver.FindElement(By.XPath("//*[@id='select-duration']"));
            //create select element object 

            var selectScreenFeedElement = new SelectElement(selectScreenFeedFilter);
            selectScreenFeedElement.SelectByText("Action Sports");

            //string screenFeedNumberFilterCssSelector = "//*[@id="select - duration"]";
            IWebElement selectScreenFeedNumberFilter = _driver.FindElement(By.XPath("//*[@id='select-duration']"));
            //create select element object 

            //var selectScreenFeedNumberElement = new SelectElement(selectScreenFeedNumberFilter);
            //selectScreenFeedNumberElement.SelectByText("10");

            IWebElement screenFeedSaveButton = _driver.FindElement(By.CssSelector(screedFeedSaveButtonCssSelector));
            screenFeedSaveButton.Click();

            IWebElement brandWidgetButton = _driver.FindElement(By.CssSelector(brandWidgetCssSelector));
            brandWidgetButton.Click();

            IWebElement brandSaveButton = _driver.FindElement(By.CssSelector(brandSaveButtonCssSelector));
            brandSaveButton.Click();

            //go back to main playlist screen
            string playlistSideBarMenuCssSelector = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a > span";
            IWebElement playlistsSideBarMenuButton = _driver.FindElement(By.CssSelector(playlistSideBarMenuCssSelector));
            playlistsSideBarMenuButton.Click();

            //PlaylistSchedule();

            //DeleteProtocol();
            
        }

        [TestCase]
        public void PlaylistSchedule()
        {
            Login();

            string schedulePlaylistCssSelector = "#playlist-container > div.playlist-content-wrapper.js-playlist-content > div > div.pm-action-bar.pm-action-bar-upper > button:nth-child(2)";
            string submitScheduleCssSelector = "#asset-info-form > div.lg-modal__actions.schedule-modal-button-wrapper > button:nth-child(1)";
            string playlistOpenButtonCSSSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(2)";

            IWebElement playlistOpenButton = _driver.FindElement(By.CssSelector(playlistOpenButtonCSSSelector));
            playlistOpenButton.Click();

            IWebElement schedulePlaylist = _driver.FindElement(By.CssSelector(schedulePlaylistCssSelector));
            schedulePlaylist.Click();

            IWebElement schedulePlaylistStart = _driver.FindElement(By.Id("asset-begin-date-range"));
            schedulePlaylistStart.Clear();
            schedulePlaylistStart.SendKeys("August 1, 2018");

            IWebElement schedulePlaylistEnd = _driver.FindElement(By.Id("asset-end-date-range"));
            schedulePlaylistEnd.Clear();
            schedulePlaylistEnd.SendKeys("August 2, 2018");

            IWebElement submitSchedule = _driver.FindElement(By.CssSelector(submitScheduleCssSelector));
            submitSchedule.Click();


        }

        [TestCase]
        public void uploadTest()
        {
            Login();

            string playlistOpenButtonCSSSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(2)";
            string uploadButtonCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button:nth-child(5)";
            string uploadFromPCCssSelector = "#asset-upload-form > div > div > div > p > button";

            IWebElement playlistOpenButton = _driver.FindElement(By.CssSelector(playlistOpenButtonCSSSelector));
            playlistOpenButton.Click();

            IWebElement uploadButton = _driver.FindElement(By.CssSelector(uploadButtonCssSelector));
            uploadButton.Click();

            IWebElement uploadFromButton = _driver.FindElement(By.CssSelector(uploadFromPCCssSelector));
            uploadFromButton.Click();

            //String script = "document.getElementById('fileName').value='" + "C:\\\\downloads\\\\file.txt" + "';";
            //((IJavaScriptExecutor)_driver).ExecuteScript(script);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(50));

            
        }

        [TestCase]
        public void Login()
        {
            string url = Common.LGUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl, true);
            string currentURL = _driver.Url;
            _driver.Navigate().GoToUrl(url);

            IWebElement query = GetElement("login-email");

            query.SendKeys("cbam.lgtest1@dciartform.com");

            query = GetElement("login-password");
            query.SendKeys("Cbam#test1");

            query.Submit();

            WaitForElementExists("page-header-container");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            if (currentURL != "https://portal.test.dcimliveguide.com/#playlists")
            {
                string playlistsUrl = "https://portal.test.dcimliveguide.com/#playlists";

                _driver.Navigate().GoToUrl(playlistsUrl);

            }
        }

        [TestCase]
        public void DeleteProtocol()
        {
            Login();


            string playlistSideBarMenuCssSelector = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a > span";
            IWebElement playlistsSideBarMenuButton = _driver.FindElement(By.CssSelector(playlistSideBarMenuCssSelector));
            playlistsSideBarMenuButton.Click();

            IWebElement playlistSearch = _driver.FindElement(By.Id("playlists-search"));
            playlistSearch.SendKeys("Automated Playlist Test");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

            int i = 0;
            while (i < 10)
            {
                string deletePlaylistButtonCssSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(1) > button:nth-child(3)";
                string newPlaylistDeleteButtonCSSSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(1) > button:nth-child(3)";

                IWebElement deletePlaylistButton = _driver.FindElement(By.CssSelector(deletePlaylistButtonCssSelector));

                IWebElement newPlaylistDeleteButton = _driver.FindElement(By.CssSelector(newPlaylistDeleteButtonCSSSelector));

                newPlaylistDeleteButton.Click();

                IAlert alert = _driver.SwitchTo().Alert();

                alert.Accept();

                playlistSearch.SendKeys("Automated Playlist Test");

                i++;
            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(50));
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

        private IWebElement GetElement(string element)
        {
            IWebElement query = _driver.FindElement(By.Id(element));
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
}
