using System;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class GeneralSDSPriceLists : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private AccessoryPricingModal _priceListsModal = null;
        private static string _locationsContainer = @"location-container";        
        private static string _buttonsClass = @"location-filter-entry-wrapper-input";
        //private static string formID = @"accessory-pricelist-info-form";
        internal AccessoryPricingModal GeneralSDSPriceListsModal
        {
            get
            {
                if (_priceListsModal == null)
                {
                    _priceListsModal = new AccessoryPricingModal(this.Driver);

                }
                return _priceListsModal;
            }
            set => _priceListsModal = value;
        }

        #endregion

        #region --- Constructor ---
        internal GeneralSDSPriceLists(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = _locationsContainer; //Element Div By: Id
           
            //FormID = formID;
        }

        #endregion

        #region --- Override Methods ---
        public override void Perform()
        {
            throw new NotImplementedException();
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region --- Public Methods ---
        public void SelectPriceOption(string button)
        {
            Wait(2);
            var buttons = Driver.FindElements(By.ClassName(_buttonsClass));
            foreach (var lgfeButton in buttons)
            {
                if (lgfeButton.Text == button) { lgfeButton.Click(); break; }
            }
        }
        
        
        #endregion

        #region --- Private Methods ---



        #endregion
    }
}
