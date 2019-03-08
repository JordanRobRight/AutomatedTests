using System;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages
{
    public class Logout : LGBasePage
    {
        #region -- Fields --         

        #endregion

        #region -- Properties ---        
        private LogoutModal _logoutModal = null;
        internal LogoutModal LogoutModal
        {
            get
            {
                if (_logoutModal == null)
                {
                    _logoutModal = new LogoutModal(this.Driver);

                }
                return _logoutModal;
            }
            set => _logoutModal = value;
        }
        #endregion

        #region -- Constructors --


        public Logout(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {

        }
        #endregion

        #region -- Methods --


            #region -- Override Methods

        public override void GoToUrl()
        {
        }


        public override void Perform()
        {
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }

            #endregion

        #endregion

       
    }
}
