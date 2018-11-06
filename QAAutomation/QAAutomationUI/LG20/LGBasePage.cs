using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20
{ 
    public abstract class LGBasePage : IDisposable
    {
        private readonly TestConfiguration _config = null;
        private IWebDriver _driver;

        public LGBasePage(IWebDriver driver, TestConfiguration config)
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

        public IWebDriver Driver => _driver;
    }     

}
