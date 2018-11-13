using System;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    public class Logout : LGBasePage
    {
        #region -- Fields --         
        private static string _logout = "#interaction-nav-bar-container > div.inbc-help-menu-wrapper > ul > li:nth-child(2) > a > span";
        private static string _cancelButton = "#logout-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div > button:nth-child(1)";
        private static string _acceptLogoutButton = "#logout-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div > button:nth-child(2)";

        #endregion

        #region -- Properties ---        
        private IWebElement LogoutButton => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _logout);
        public IWebElement LogoutCancelButton => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _cancelButton);
        public IWebElement LogoutAcceptButton => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _acceptLogoutButton);
        public IWebElement LogoutModal => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, "lg-modal__container");
        //private IWebElement cancelButton => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, "lg-modal__container");
        public IWebElement cancelButtonSelection = null;
        public IWebElement logoutButtonSelection = null;


        #endregion

        #region -- Constructors --
       

        public Logout(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {

        }
        #endregion

        #region -- Methods --

        public void CancelButton()
        {
            var modal = SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, "iibcuinow-menu-wrapper").FindElements(By.TagName("button"));
            

            foreach (var modalItem in modal)
            {
                if (modalItem.Text == "cancel")
                {
                    LogoutCancelButton.Click();                   
                }
                //else if(modalItem.Text == "logout")
                //{
                //    LogoutAcceptButton = modalItem;                    
                //}
            }
        }

            #region -- Override Methods

        public override void GoToUrl()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Driver.Navigate().GoToUrl(url);
        }

        public void LogoutButtonClick()
        {
            WaitFor();
            LogoutButton.Click();
        }

        public void CancelButtonClick()
        {
            WaitFor();
            cancelButtonSelection.Click();
        }

        public void LogoutAcceptButtonClick()
        {
            WaitFor();
            LogoutAcceptButton.Click();
        }

        public override void Perform()
        {
            LogoutButton.Click();
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }

            #endregion

        #endregion

       
    }
}
