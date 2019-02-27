using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class LogoutModal : ModalBasePage
    {
        #region -- Fields
        private static string _modalContainer = @"lg-modal__container";
        private static string _logoutModal = @"logout-modal"; // Element: Div By: Id
        private static string _logoutModalVisibleClass = @"lg-modal lg-modal--large lg-modal--visible"; // Element: N/A By: ClassName
        #endregion

        #region  --- Private Properties ---

        
        #endregion

        #region --- Public Properties ---
        
        #endregion

        #region --- Constructor ---

        internal LogoutModal(IWebDriver driver) : base(driver, _logoutModal, _modalContainer, _logoutModalVisibleClass)
        {
        }
        #endregion

        #region --- Methods ---
        #endregion
    }
}