using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    class SideBarItem
    {
        IWebDriver _driver;
        public SideBarItem(IWebDriver driver)
        {
            _driver = driver;
        }
                
        public string Name { get; set; }
        public IWebElement WebElement { get; set; }
    }
}
