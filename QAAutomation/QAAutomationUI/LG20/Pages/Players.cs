using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class Players : LGBasePage
    {
        internal Players(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {

        }

        public override void Perform()
        {
            throw new NotImplementedException();
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }

        public override void Wait(int numberOfSeconds = 5)
        {
            base.Wait(numberOfSeconds);
        }
    }
}
