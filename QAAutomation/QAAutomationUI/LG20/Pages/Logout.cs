using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        private static string _modalLogout = "logout-modal";
        private static string _modalClass = "lg-modal__wrapper";
        private static string _modalButtonArea = "lg-modal__actions logoutModalBtns";
        private static string _modalForm = "lg-modal__panel tab-panel-widget__panel";

        #endregion

        #region -- Properties ---        
        private IWebElement LogoutButton => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _logout);

        private IWebElement ModelDialog => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _modalClass);
        private IWebElement ModelLogoutDialog => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, _modalLogout);
        private IWebElement Model_modalClass => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _modalClass);
        private IWebElement ModelForm => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _modalForm);

        private IWebElement ModelDialogButtonArea => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _modalButtonArea);

        private IWebElement LogoutCancelButton => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _cancelButton);
        private IWebElement LogoutAcceptButton => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, _acceptLogoutButton);
        public IWebElement LogoutModal => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, "lg-modal__container");

        private IEnumerable<IWebElement> ModelButtons => BuildModel();
        #endregion

        #region -- Constructors --
       

        public Logout(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {

        }
        #endregion

        #region -- Methods --

        private IEnumerable<IWebElement> BuildModel()
        {
            List<IWebElement> buttons = new List<IWebElement>();
            try
            {
                var innerArea = ModelLogoutDialog.FindElement(By.ClassName("lg-modal__content"));

                var modalbuttons = innerArea.FindElements(By.TagName("button"));

                foreach (var button in modalbuttons)
                {
                    buttons.Add(button);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return buttons;
        }

        private IWebElement FindButton(string text)
        {
            IWebElement button = ModelButtons.FirstOrDefault(a => a.Text.Equals(text, StringComparison.OrdinalIgnoreCase));
            return button;
        }

            #region -- Override Methods

        public override void GoToUrl()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Driver.Navigate().GoToUrl(url);
        }

        public void LogoutButtonClick()
        {
            Wait();
            LogoutButton.Click();
        }

        public void CancelButtonClick()
        {
            Wait();
            FindButton("cancel")?.Click();
        }

        public void LogoutAcceptButtonClick()
        {
            Wait();
            FindButton("logout")?.Click();
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
