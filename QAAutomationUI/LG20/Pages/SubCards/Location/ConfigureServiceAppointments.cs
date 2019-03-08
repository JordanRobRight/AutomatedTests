using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class ConfigureServiceAppointments : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private static readonly string _editButtonServiceAppointment = @"//span[contains(text(),'Service')]/ancestor::h4/preceding-sibling::div//div//i ";
        private static string _locationsContainer = @"location-container";
        private static string salesAppointmentsForm = @"sales-appointments-form";//ID
        private static readonly string _appointmentRow = @"div.flex-grid--item.appointments-row";
        private static readonly string _salesAppointmentLinkClass = @"lgfe-tile-grid-item-no-content";//class name
        private ServiceAppointmentManagementModal _saleAppointmentModal = null;
        internal ServiceAppointmentManagementModal ServiceAppointmentManagementModal
        {
            get
            {
                if (_saleAppointmentModal == null)
                {
                    _saleAppointmentModal = new ServiceAppointmentManagementModal(this.Driver);

                }
                return _saleAppointmentModal;
            }
            set => _saleAppointmentModal = value;
        }

        #endregion

        #region --- Constructor ---
        internal ConfigureServiceAppointments(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
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

      
        public void ServiceAppointmentEditButtonClick()
        {
            Wait(2);
            IWebElement button = Driver.FindElement(By.XPath(_editButtonServiceAppointment));
            button.Click();           
        }

        //buton to CreateSalesAppointmentsLink when no appointments are present in the section
        public void CreateServiceAppointmentsButton()
        {
            var salesAppointmentGridItem = GetPageContainer().FindElement(By.ClassName(_salesAppointmentLinkClass));
            var a = salesAppointmentGridItem.FindElements(By.TagName("a"));
            foreach (IWebElement item in a)
            {
                if (item.Text == "Create an Appointment")
                {
                    item.Click();
                    break;
                }
            }
        }
               

             //validation
        public List<string> GetSalesAppointmentsDetails
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.TagName(_appointmentRow));
                var customerData = wrapper.FindElements(By.TagName("span"));
                List<string> customerDataList = new List<string>();
                foreach(var list in customerData)
                {
                    customerDataList.Add(list.Text);
                }
                return customerDataList;
            }
            
        }
       
        #endregion

        #region --- Private Methods ---



        #endregion
    }
}
