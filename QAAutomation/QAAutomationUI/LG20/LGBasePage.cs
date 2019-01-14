using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

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

        public virtual void GoToUrl()
        {
            throw new NotImplementedException();
        }

        public virtual void WaitForElement(string itemToWaitFor = "")
        {
            throw new NotImplementedException();
        }

        public virtual void Wait(int numberOfSeconds = 5)
        {
            Thread.Sleep(TimeSpan.FromSeconds(numberOfSeconds));
        }

        public abstract void Perform();

        public abstract bool VerifyPage();

        #region --- Protected Methods ---

        protected IWebElement GetPageContainer()
        {
            return _driver.FindElement(By.Id(PageContainerName));
        }

        protected IWebElement GetPageFunctionBarContainer()
        {
            return GetPageContainer().FindElement(By.ClassName(PageFunctionBarContainerClassName));
        }

        protected IWebElement GetPageUtilityBarContainer()
        {
            return GetPageContainer().GetElementFromCompoundClass(By.TagName("div"), PageUtilBarContainerClassName);
        }

        protected IWebElement GetPageContentArea()
        {
            return GetPageContainer().FindElement(By.Id(PageContentAreaId));
        }

        #endregion

        #region --- Protected Properties ---
        /// <summary>
        /// The Id of the container section of the page. This string will be used for the locator by ID.
        /// </summary>
        protected string PageContainerName { get; set; }

        /// <summary>
        /// The Classname of the container section of the function bar container. This string will be used for the locator by ClassName.
        /// </summary>
        protected string PageFunctionBarContainerClassName { get; set; }

        /// <summary>
        /// The Classname of the container section of the utility bar container. This string will be used for the locator by ClassName.
        /// </summary>
        protected string PageUtilBarContainerClassName { get; set; }

        /// <summary>
        /// The Id of the container section of the page content area. This string will be used for the locator by Id.
        /// </summary>
        protected string PageContentAreaId { get; set; }
        #endregion

        public TestConfiguration Config => _config;

        public IWebDriver Driver => _driver;
    }     

}
