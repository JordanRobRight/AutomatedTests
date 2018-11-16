using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
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

        public ChromeBrowser(string browser, string version, string os, string deviceName, string deviceOrientation, TestConfiguration _configuration, string testName, string testClassName, string testMethodName)
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability(CapabilityType.BrowserName, browser);
            caps.SetCapability(CapabilityType.Version, version);
            caps.SetCapability(CapabilityType.Platform, os);
            caps.SetCapability("deviceName", deviceName);
            caps.SetCapability("deviceOrientation", deviceOrientation);
            caps.SetCapability("username", _configuration.SauceLabsUser);
            caps.SetCapability("accessKey", _configuration.SauceLabsKey);
            caps.SetCapability("name", testName);

            if (_configuration.IsRemoteDriver)
            {
                //_driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
                caps.SetCapability("name",
                    string.Format("{0}:{1}: [{2}]",
                        testClassName,
                        testMethodName,
                        string.Empty));
                //TestContext.CurrentContext.Test.Properties.Get("Description")));
                _driver = new CustomDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));


            }
            else
            {
                ChromeOptions co = new ChromeOptions();    // set the desired browser
                co.AddAdditionalCapability("platform", "Windows 7");
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                _driver= new ChromeDriver(path);
            }


            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_configuration.WaitTimeInSeconds);
        }

        public IWebDriver Driver => _driver;
    }
}
