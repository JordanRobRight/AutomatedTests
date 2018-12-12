using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests
{
    class ChromeBrowser
    {
        private IWebDriver _driver;
        private DesiredCapabilities caps = new DesiredCapabilities();
        private readonly TestConfiguration _configuration = new TestConfiguration();

        internal ChromeBrowser(string browser, string version, string os, string deviceName, string deviceOrientation,
            TestConfiguration configuration)
        {
            this._configuration = configuration;
            caps.SetCapability(CapabilityType.BrowserName, browser);
            caps.SetCapability(CapabilityType.Version, version);
            caps.SetCapability(CapabilityType.Platform, os);
            caps.SetCapability("deviceName", deviceName);
            caps.SetCapability("deviceOrientation", deviceOrientation);
            caps.SetCapability("username", _configuration.SauceLabsUser);
            caps.SetCapability("accessKey", _configuration.SauceLabsKey);
        }

        internal IWebDriver CreateBrowser(string testName, string testClassName, string testMethodName)
        {
            caps.SetCapability("name", testName);

            if (_configuration.IsRemoteDriver)
            {
                caps.SetCapability("name",
                    string.Format("{0}:{1}: [{2}]",
                        testClassName,
                        testMethodName,
                        string.Empty));
                _driver = new CustomDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));


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
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configuration.PageWaitTimeInSeconds);

            return _driver;
        }
            
    }
}
