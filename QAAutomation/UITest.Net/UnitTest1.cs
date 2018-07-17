using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace QA.Automation.UITest.Net
{
    public class UnitTest1
    {
        private IWebDriver _driver;
        private readonly string _browser;
        private readonly string _version;
        private readonly string _os;
        private readonly string _deviceName;
        private readonly string _deviceOrientation;
        private const string Un = @"DCIArtform";

        private const string Ak = @"a4277bd1-3492-4562-99bc-53dd349c52e1";
        private readonly bool _isRemoteDriver = false;

        public UnitTest1(string browser, string version, string os, string deviceName, string deviceOrientation)
        {
            _browser = browser;
            _version = version;
            _os = os;
            _deviceName = deviceName;
            _deviceOrientation = deviceOrientation;
        }

        public void Init()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability(CapabilityType.BrowserName, _browser);
            caps.SetCapability(CapabilityType.Version, _version);
            caps.SetCapability(CapabilityType.Platform, _os);
            caps.SetCapability("deviceName", _deviceName);
            caps.SetCapability("deviceOrientation", _deviceOrientation);
            caps.SetCapability("username", Un);
            caps.SetCapability("accessKey", Ak);

            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            Uri site = new Uri("http://ondemand.saucelabs.com:80/wd/hub");

            if (_isRemoteDriver)
            {
                _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            }
            else
            {
                ChromeOptions co = new ChromeOptions();    // set the desired browser
                co.AddAdditionalCapability("platform", "Windows 7");
                //desiredCapability.SetCapability("platform", "Windows 7"); // operating system to use
                // DesiredCapabilities desiredCapability = co;

                _driver = new ChromeDriver();
            }


//            driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));


        }

        public void GoogleTestDotNet()
        {
            _driver.Navigate().GoToUrl("http://www.google.com");
            StringAssert.Contains("Google", _driver.Title);
            IWebElement query = _driver.FindElement(By.Name("q"));
            query.SendKeys("Sauce Labs");
            query.Submit();
        }

        public void CleanUp()
        {
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            try
            {
                // Logs the result to Sauce Labs
                if (_isRemoteDriver)
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

    }
}
