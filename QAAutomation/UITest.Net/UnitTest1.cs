using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace QAAutomationUI
{

    [TestFixture("chrome", "63", "Windows 10", "", "")]
    public class UnitTest1
    {
        private IWebDriver driver;
        private String browser;
        private String version;
        private String os;
        private String deviceName;
        private String deviceOrientation;
        private const string un = @"DCIArtform";

        private const string ak = @"a4277bd1-3492-4562-99bc-53dd349c52e1";
        private bool IsRemoteDriver = false;

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
            //caps.SetCapability("username", "SAUCE_USERNAME");
            //caps.SetCapability("accessKey", "SAUCE_ACCESS_KEY");
            caps.SetCapability("username", un);
            caps.SetCapability("accessKey", ak);

            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            Uri site = new Uri("http://ondemand.saucelabs.com:80/wd/hub");

            if (IsRemoteDriver)
            {
                driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            }
            else
            {
                        ChromeOptions co = new ChromeOptions();    // set the desired browser
                        co.AddAdditionalCapability("platform", "Windows 7");
                //desiredCapability.SetCapability("platform", "Windows 7"); // operating system to use
                // DesiredCapabilities desiredCapability = co;

                driver = new ChromeDriver();
                //driver
            }


//            driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));


        }

        //[TestCase]
        //public void Test1()
        //{
        //    bool local = false;
        //    IWebDriver w = null;

        //    if (local)
        //    {
        //        w = new ChromeDriver();
        //    }
        //    else
        //    {


        //        //w driver;
        //        ChromeOptions co = new ChromeOptions();    // set the desired browser
        //        co.AddAdditionalCapability("platform", "Windows 7");
        //        //desiredCapability.SetCapability("platform", "Windows 7"); // operating system to use
        //        // DesiredCapabilities desiredCapability = co;
        //        w = new RemoteWebDriver(new Uri("http://YOUR_USERNAME:YOUR_ACCESS_KEY@ondemand.saucelabs.com:80/wd/hub"), co);
        //        // w = new RemoteWebDriver(new Uri("http://YOUR_USERNAME:YOUR_ACCESS_KEY@ondemand.saucelabs.com:80/wd/hub");
        //    }

        //}

        [TestCase]
        public void GoogleTestDotNet()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            StringAssert.Contains("Google", driver.Title);
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Sauce Labs");
            query.Submit();
        }

        [TearDown]
        public void CleanUp()
        {
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            try
            {
                // Logs the result to Sauce Labs
                if (IsRemoteDriver)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                }
            }
            finally
            {
                // Terminates the remote webdriver session
                driver.Quit();
            }
        }
        //  }

    }
}
