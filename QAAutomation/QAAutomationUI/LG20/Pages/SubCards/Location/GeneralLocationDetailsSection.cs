using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class GeneralLocationDetailsSection : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

       
        private static readonly string _editButtonLocationDetails = @"//span[contains(text(),'location details')]/ancestor::h4/preceding-sibling::div//div//i";
        private static readonly string _tileGridWrapper = @"lgfe-tile-grid-item-hero-wrapper";//class name to select location details section
        
        private static readonly string _locationsContainer = @"location-container";
        private static readonly string formInfo = @"playlist-info-form";
        private static readonly string  modalFields = "lg-modal__field";
        private static readonly string timeStamps = "time-stamps";//class name
        private static readonly string locationModalID = @"location-information-modal";//ID
        private GeneralLocationDetailsSectionModal _generalLocationDetailsSectionModal = null;
        internal GeneralLocationDetailsSectionModal GeneralLocationDetailsSectionModal
        {
            get
            {
                if (_generalLocationDetailsSectionModal == null)
                {
                    _generalLocationDetailsSectionModal = new GeneralLocationDetailsSectionModal(this.Driver);

                }
                return _generalLocationDetailsSectionModal;
            }
            set => _generalLocationDetailsSectionModal = value;
        }

        #endregion

        #region --- Constructor ---
        internal GeneralLocationDetailsSection(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = _locationsContainer; //Element Div By: Id
            ModalID = locationModalID;
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
        public void LocationDetailsEditButtonClick()
        {
            Wait(2);

            // var buttons = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, _pencilButton).FindElements(By.TagName("div"));
            //   IWebElement button = buttons.FirstOrDefault(a => a.GetAttribute("text") != null && a.GetAttribute("text").Equals("tags", StringComparison.OrdinalIgnoreCase));
            IWebElement b = Driver.FindElement(By.XPath(_editButtonLocationDetails));
            b.Click();
            // List<IWebElement> buttons2 = Driver.FindElements(By.TagName("i")).ToList();
            //IWebElement button28 = buttons2.FirstOrDefault(a => a.xpa(test) != null && a.GetAttribute("text").Equals("tags", StringComparison.OrdinalIgnoreCase));
            
        }
        
        //fields from location detail section
        public List<string> GetLocationDetailsSectionFields
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.ClassName(_tileGridWrapper));
                var locationDetailsList = wrapper.FindElements(By.TagName("strong"));     
                List<string> locationDetailFieldList = new List<string>();
                foreach (var locationDetails in locationDetailsList)
                {
                    string locationDetailLabel = locationDetails.Text;
                    locationDetailFieldList.Add(locationDetailLabel);                    
                }
                return locationDetailFieldList;
            }
            
        }
        
        // Fields from location settings modal
        public List<string> GetLocationSettingsFieldValue
        {
            get
            {
                var formInformation = GetModal().FindElement(By.Id(formInfo));
                var div = formInformation.FindElements(By.ClassName(modalFields));
                List< string> locationSettingFieldList = new List<string>();
                foreach (var info in div)
                {
                    var label = info.FindElement(By.TagName("label"));                    
                    locationSettingFieldList.Add(label.Text);
                }
                return locationSettingFieldList;

            }
        }


        public List<string> GetTimeStampFields
        {
            get
            {
                var timeStamp = GetModal().FindElement(By.ClassName(timeStamps));
                var div = timeStamp.FindElements(By.TagName("span"));
                List<string> locationSettingTimeStampsFeilddList = new List<string>();
                foreach (var info in div)
                {
                    var label = info.FindElement(By.TagName("strong"));
                    locationSettingTimeStampsFeilddList.Add(label.Text);
                }
                return locationSettingTimeStampsFeilddList;
            }
        }
        #endregion

        #region --- Private Methods ---



        #endregion
    }
}

