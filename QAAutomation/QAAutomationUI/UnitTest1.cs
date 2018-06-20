using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

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
        private readonly UITests.TestConfiguration _configuration = null;

        public UnitTest1 (String browser, String version, String os, String deviceName, String deviceOrientation)
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

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
         }


        [TestCase]
        public void LiveGuide20()
        {
            string url = Common.LGUtils.GetUrlBaseUrl(_configuration.Environment.ToString(), _configuration.BaseUrl);

            _driver.Navigate().GoToUrl(url);

            IWebElement query = GetElement("login-email");
            
            query.SendKeys("cbam.lgtest1@dciartform.com");
            query = GetElement("login-password");
            query.SendKeys("Cbam#test1");

            query.Submit();

            WaitForElementExists("page-header-container");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            Assert.AreEqual("${url}/#playlists", _driver.Url.Trim());
            //Assert.AreEqual("https://lg-frontend-test.azurewebsites.net/#playlists", _driver.Url.Trim());
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
