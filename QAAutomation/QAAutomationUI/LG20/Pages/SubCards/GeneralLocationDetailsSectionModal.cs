using OpenQA.Selenium;
using QA.Automation.UITests.Selenium;
namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class GeneralLocationDetailsSectionModal : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        private static string _modalClassName = @"location-information-modal";
        private static string _modalContainerName = @"lg-modal__container";
        private static string _modalVisableClass = @"lg-modal lg-modal--large lg-modal--visible";
        #endregion

        #region --- Properties ---





       

        #endregion

        #region --- Constructor ---
        internal GeneralLocationDetailsSectionModal(IWebDriver driver) : base(driver, _modalClassName, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
        }
        #endregion

        #region --- Public Methods ---
        public bool IsModalDisplay
        {
            get
            {
                var getModal = GetModal();
                return getModal != null;
            }
        }
        #endregion

        #region --- Private Methods ---

        #endregion

    }
}