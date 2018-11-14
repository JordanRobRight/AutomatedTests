using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    class SideBarItems
    {
        private IWebDriver driver;

        public SideBarItems(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string Name { get; set; }
    }
}
