using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG2
{ 
    public abstract class LGBasePage : IDisposable
    {
        private TestConfiguration _config = null;
        private ThreadLocal<IWebDriver> _driver;

        public LGBasePage(ThreadLocal<IWebDriver> driver, TestConfiguration config)
        {
            _config = config;
            _driver = driver;
        }
        public void Dispose()
        {

        }

        public abstract void Perform();

        public abstract bool VerifyPage();

        public TestConfiguration Config => _config;

        public ThreadLocal<IWebDriver> Driver => _driver;
    }     

}
