using System;
using System.Collections.Generic;
using System.Linq;
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
        private static string _loginContainer = @"user-login";
        private static string _loginContentArea = "user-login__content";

        #endregion

        #region -- Properties ---
        //get the list of button in sign in page, can be used to validate after logout
        public List<string> GetSignInButton
        {
            get
            {
                var wrapper = GetPageContainer();
                var buttons = wrapper.FindElements(By.TagName("button"));
                List<string> buttonList = new List<string>();
                foreach (var button in buttons)
                {
                    buttonList.Add(button.Text);
                }
                return buttonList;
            }
        }
        public string UserName
        {
            get
            {
                var getField = GetPageFunctionBarContainer().FindElement(By.Id(_username));
                return getField != null ? getField.Text : string.Empty;
            }
            set
            {
                var getField = GetPageFunctionBarContainer().FindElement(By.Id(_username));
                getField?.SendKeysOrClear(value);
            }
        }
        public string Password
        {
            get
            {
                var getField = GetPageFunctionBarContainer().FindElement(By.Id(_password));
                return getField != null ? getField.Text : string.Empty;
            }
            set
            {
                var getField = GetPageFunctionBarContainer().FindElement(By.Id(_password));
                getField?.SendKeysOrClear(Common.LgUtils.GetStringFromBase64(value));
            }
        }

        public string GetCurrentUrl => Driver.Url.ToString();

        #endregion

        #region -- Constructors --
        public Login(IWebDriver driver , TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = _loginContainer;
            PageFunctionBarContainerClassName = _loginContentArea;
        }
        #endregion

        #region -- Methods --

        #region -- Override Methods

        public override void GoToUrl()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Wait(4);
            Driver.Navigate().GoToUrl(url);
            Wait();
        }

        public override void Perform()
        {

            //byte[] data = Convert.FromBase64String(Config.LGPassword);
            //string password = Encoding.UTF8.GetString(data);
            //UserName.SendKeys(Config.LGUser);
            //Password.SendKeys(password);
            //Wait(2);
            //Password.Submit();
        }

        public void ClickSignIn(string buttonName)
        {
            var form = GetPageFunctionBarContainer().FindElements(By.TagName("form")).FirstOrDefault(a => a.FindElement(By.Id(_password)) != null);
            var buttons = form.FindElements(By.TagName("button"));
            var submit = buttons.FirstOrDefault(a => a.Text.Equals(buttonName, StringComparison.OrdinalIgnoreCase));
            if (submit != null)
            {
                form.Submit();
            }

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
