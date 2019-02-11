using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    class SideBarItem
    {

        IWebDriver driver;
        public SideBarItem(IWebDriver driver)
        {

        }
                
        public string Name { get; set; }
        public IWebElement WebElement { get; set; }
    }
}
