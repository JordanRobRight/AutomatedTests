using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    class SideBarItem
    {
        private IWebDriver driver;

        public SideBarItem(IWebDriver driver)
        {
            // Remove driver property
            this.driver = driver;
        }

        public string Name { get; set; }
    }
}
