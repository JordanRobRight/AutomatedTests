using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class LocationDetail : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private static readonly string _pdwurButtons = @"pdwur-item-text";//class name
        private static string _locationContainer = @"location-container";
        private static string _modalSpanText = @"lgfe-tile-grid-item-header-text";
        //  private LocationsSettingModal _locationsModal = null;
        /*  internal SubCards.LocationsSettingModal LocationModal
          {
              get
              {
                  if (_locationsModal == null)
                  {
                      _locationsModal = new LocationsSettingModal(this.Driver);

                  }
                  return _locationsModal;
              }
              set => _locationsModal = value;
          }*/
        #endregion

        #region --- Constructor ---
        internal LocationDetail(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = _locationContainer;
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

        public void SelectTab_LocationDetail(String tab)
        {
            var buttons = Driver.FindElements(By.ClassName(_pdwurButtons));
            foreach (var button in buttons)
            {
                if (button.Text == tab) { button.Click(); break; }
            }
        }

        //use this method after selecting the tab in order to validate the tab's overall modal
        //example would be:
        //LocationDetail.SelectTab_LocationDetail("CONFIGURE");
        // LocationDetail.GetModals.Should().Contain(new[] XXX})
        public List<string> GetModals
        {
            get
            {
                var modalListClass = GetPageContainer().FindElements(By.ClassName(_modalSpanText));
                List<string> modalList = new List<string>();
                foreach (var modal in modalListClass)
                {
                    modalList.Add(modal.Text);
                }
                return modalList;
            }

            #endregion

            #region --- Private Methods ---



            #endregion
        }
    }
}
