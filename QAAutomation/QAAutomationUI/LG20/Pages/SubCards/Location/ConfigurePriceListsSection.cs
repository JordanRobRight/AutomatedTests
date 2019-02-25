using System;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class ConfigurePriceListsSection : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private static string _locationsContainer = @"location-container";
        private static readonly string _editButtonTags = @"//span[contains(text(),'tags')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _tickersContainerClass = @"tickers-container";//id
        private static string _buttonsClass = @"location-filter-entry-wrapper-input";
        private PriceListModal _priceListModal = null;
        internal PriceListModal PriceListModal
        {
            get
            {
                if (_priceListModal == null)
                {
                    _priceListModal = new PriceListModal(this.Driver);

                }
                return _priceListModal;
            }
            set => _priceListModal = value;
        }
        #endregion

        #region --- Constructor ---
        internal ConfigurePriceListsSection(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            PageContainerName = _locationsContainer; //Element Div By: Id
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

        public void SelectPriceListsOption(string button)//pass the button name in UnitTest, e.g: Edit Default SMC Service Price List
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
