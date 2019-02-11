using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20
{
    public abstract class LGBasePage : IDisposable
    {
        #region --- Fields ---
        private readonly TestConfiguration _config = null;
        private IWebDriver _driver;
        #endregion


        #region --- Constructores ---
        protected LGBasePage(IWebDriver driver, TestConfiguration config)
        {
            _config = config;
            _driver = driver;
        }
        #endregion

        #region --- Public Methods ---
        public void Dispose()
        {

        }
        #endregion

        #region --- Abstract Methods ---
        public abstract void Perform();

        public abstract bool VerifyPage();
        #endregion

        #region --- Virutal Methods ---

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
            Thread.Sleep(TimeSpan.FromSeconds(numberOfSeconds + _config.SlidingWaitFactor));
        }
        #endregion

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

        #region --- Public Properties --- 
        public string SearchField
        {
            get
            {
                var searchText = GetPageUtilityBarContainer().FindElements(By.Id(PageSearchField)).FirstOrDefault();
                return searchText != null ? searchText.Text : string.Empty;

            }
            set
            {
                var searchText = GetPageUtilityBarContainer().FindElements(By.Id(PageSearchField)).FirstOrDefault();
                searchText?.SendKeys(value);
            }
        }

        public string GetCurrentUrl
        {
            get { return _driver.Url; }
        }

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
        /// The Classname of the container section of the Search field container. This string will be used for the locator by ClassName.
        /// </summary>
        protected string PageSearchField { get; set; }

        /// <summary>
        /// The Id of the container section of the page content area. This string will be used for the locator by Id.
        /// </summary>
        protected string PageContentAreaId { get; set; }
        #endregion

        public TestConfiguration Config => _config;

        public IWebDriver Driver => _driver;
        #endregion
    }

}
