using System;
using System.Threading;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG2.Pages
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

        public Login(ThreadLocal<IWebDriver> driver , TestConfiguration config) : base(driver, config)
        {
            
        }

        public override void Perform()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Driver.Value.Navigate().GoToUrl(url);

            UserName.SendKeys("cbam.lgtest1@dciartform.com");
            Password.SendKeys("Cbam#test1");
            Password.Submit();
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }
    }
}
