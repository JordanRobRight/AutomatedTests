using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Location
{
    internal class ConfigureSalesAppointments : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---

        private static readonly string _editButtonSalesAppointment = @"//span[contains(text(),'Sale')]/ancestor::h4/preceding-sibling::div//div//i  ";
        private static string _locationsContainer = @"location-container";
        private static string salesAppointmentsForm = @"sales-appointments-form";//ID
        private SalesAppointmentManagementModal _saleAppointmentModal = null;
        private static readonly string _appointmentRow = @"div.flex-grid--item.appointments-row";
        private static readonly string _salesAppointmentLinkClass = @"lgfe-tile-grid-item-no-content";//class name
        internal SalesAppointmentManagementModal SaleAppointmentModal
        {
            get
            {
                if (_saleAppointmentModal == null)
                {
                    _saleAppointmentModal = new SalesAppointmentManagementModal(this.Driver);

                }
                return _saleAppointmentModal;
            }
            set => _saleAppointmentModal = value;
        }

        #endregion

        #region --- Constructor ---
        internal ConfigureSalesAppointments(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = _locationsContainer; //Element Div By: Id
            //FormID = salesAppointmentsForm;
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

      
        public void SalesAppointmentEditButtonClick()
        {
            Wait(2);
            IWebElement button = Driver.FindElement(By.XPath(_editButtonSalesAppointment));
            button.Click();           
        }

        //buton to CreateSalesAppointmentsLink when no appointments are present in the section
        public void CreateSalesAppointmentsButton()
        {
            var salesAppointmentGridItem = GetPageContainer().FindElement(By.ClassName(_salesAppointmentLinkClass));
            var a = salesAppointmentGridItem.FindElements(By.TagName("a"));
            foreach (IWebElement item in a)
            {
                if (item.Text == "Create a Sales Appointment")
                {
                    item.Click();
                    break;
                }
            }
        }
               

             //to do :validation
        public List<string> GetSalesAppointmentsDetails
        {
            get
            {
                var wrapper = GetPageContainer().FindElement(By.CssSelector(_appointmentRow));
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
