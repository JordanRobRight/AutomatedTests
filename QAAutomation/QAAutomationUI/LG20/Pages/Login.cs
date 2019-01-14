using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    public class Login : LGBasePage
    {
        #region -- Fields -- 

        private static string _username = @"login-email";
        private static string _password = @"login-password";

        #endregion
        
        #region -- Properties ---

        private IWebElement UserName => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, _username);

        private IWebElement Password => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, _password);

        #endregion

        #region -- Constructors --
        public Login(IWebDriver driver , TestConfiguration config) : base(driver, config)
        {
            
        }
        #endregion

        #region -- Methods --

        #region -- Override Methods

        public override void GoToUrl()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Wait(9);
            Driver.Navigate().GoToUrl(url);
        }

        public override void Perform()
        {
           
            byte[] data = Convert.FromBase64String(Config.LGPassword);
            string password = Encoding.UTF8.GetString(data);
            UserName.SendKeys(Config.LGUser);
            Password.SendKeys(password);
            Wait(2);
            Password.Submit();
        }

        public override void WaitForElement(string itemToWaitFor = "")
        {
            Selenium.SeleniumCommon.WaitUntilElementExists(Driver, By.Id("page-header-container"));
            Wait();
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }

        #endregion
        
        #endregion

       
    }
}
