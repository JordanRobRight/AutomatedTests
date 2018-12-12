using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    class ClientMenuItem
    {

        public ClientMenuItem(IWebDriver driver)
        {

        }

        public string Name { get; set; }
        public IWebElement WebElement { get; set; }
    }
}