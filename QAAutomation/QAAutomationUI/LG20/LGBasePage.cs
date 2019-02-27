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
        private readonly TestSystemConfiguration _config = null;
        private IWebDriver _driver;
        #endregion 

        #region --- Constructores ---
        protected LGBasePage(IWebDriver driver, TestSystemConfiguration config)
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
            Thread.Sleep(TimeSpan.FromSeconds(numberOfSeconds));
        }

        #endregion

        #region --- Protected Methods ---

        protected IWebElement GetPageContainer()
        {
            return _driver.FindElement(By.Id(PageContainerName));
        }

        protected IWebElement GetModal()
        {
            return _driver.FindElement(By.Id(ModalID));
        }        

        protected IWebElement GetPageFunctionBarContainer()
        {
            return GetPageContainer().FindElement(By.ClassName(PageFunctionBarContainerClassName));
        }

        protected IWebElement GetPageUtilityBarContainer()
        {
            return GetPageContainer().GetElementFromCompoundClass(By.TagName("div"), PageUtilBarContainerClassName);
        }

        protected IWebElement GetPageTableSarchField()
        {
            return GetPageContainer().GetElementFromCompoundClass(By.TagName("div"), PageUtilBarContainerClassName);
        }

        protected IWebElement GetPageContentArea()
        {
            return GetPageContainer().FindElement(By.Id(PageContentAreaId));
        }

        #endregion

        #region --- Protected Properties ---

        protected string PageContainerName { get; set; }
        protected string ModalID { get; set; }
        //protected string FormID { get; set; }

        protected string PageFunctionBarContainerClassName { get; set; }

        protected string PageUtilBarContainerClassName { get; set; }
       // protected string PageTableSearchClassName { get; set; }

        protected string PageSearchField { get; set; }

        protected string PageContentAreaId { get; set; }
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

               // searchText.SendKeysOrClear(value);
                searchText.SendKeysOrClear(value);
            }
        }

        public string SearchFieldLocations
        {
            get
            {
                var searchText = GetPageTableSarchField().FindElements(By.TagName("input")).FirstOrDefault();
                return searchText != null ? searchText.Text : string.Empty;

            }
            set
            {
                var searchText = GetPageTableSarchField().FindElements(By.TagName("input")).FirstOrDefault();
                searchText.SendKeysOrClear(value);
            }
        }


        public TestSystemConfiguration Config => _config;

        public IWebDriver Driver => _driver;

        #endregion
    }

}