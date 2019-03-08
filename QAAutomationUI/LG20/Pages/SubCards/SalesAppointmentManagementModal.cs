using OpenQA.Selenium;
using QA.Automation.UITests.Selenium;
using System.Threading;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class SalesAppointmentManagementModal : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        private static string _modalID = @"sales-appointments-modal";
        private static string _modalContainerName = @"lg-modal__container";
        private static string _modalVisableClass = @"lg-modal lg-modal--large lg-modal--visible";
        private static string _appointmentTimeSelect = @"select-filter";
        private static readonly string _currenteDate = @"#datepickers-container > div:nth-child(3) > div.datepicker--content > div > div.datepicker--cells.datepicker--cells-days > div.datepicker--cell.datepicker--cell-day.-current-.-selected-";
        private static string salesAppointmentsForm = @"sales-appointments-form";//ID
        private static readonly string plussButtonClass = @"pl-utility-add-button";
       
        #endregion

        #region --- Properties ---


        #endregion

        #region --- Constructor ---
        internal SalesAppointmentManagementModal(IWebDriver driver) : base(driver, _modalID, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
            
            FormID = salesAppointmentsForm;
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
        public void AddGreetingAppointmentModalPlusButton()
        {
            
            var button = GetFormArea().FindElement(By.ClassName(plussButtonClass));
            button.Click();
        }    

        public string CustomerNameTextBox
        {
            get
            {
                var getField = GetField("input", "id", "customerName");
                return getField != null ? getField.Text : string.Empty;
            }
            set
            {
                var getField = GetField("input", "id", "customerName");
                getField?.SendKeysOrClear(value);
            }
        }

        public string VehicleTextBox
        {
            get
            {
                var getField = GetField("input", "id", "vehicleMake");
                return getField != null ? getField.Text : string.Empty;
            }
            set
            {
                var getField = GetField("input", "id", "vehicleMake");
                getField?.SendKeysOrClear(value);
            }
        }

        public string SalespersonTextBox
        {
            get
            {
                var getField = GetField("input", "id", "advisor");
                return getField != null ? getField.Text : string.Empty;
            }
            set
            {
                var getField = GetField("input", "id", "advisor");
                getField?.SendKeysOrClear(value);
            }
        }

        public string CustomMessageTextBox
        {
            get
            {
                var getField = GetField("input", "id", "customerMessage");
                return getField != null ? getField.Text : string.Empty;
            }
            set
            {
                var getField = GetField("input", "id", "customerMessage");
                getField?.SendKeysOrClear(value);
            }
        }
        
        public void SelectAppointmentDay()
        {
            var getField = GetField("input", "id", "appointmentDate");
            getField.Click();
            Thread.Sleep(2000);
            IWebElement currentDate = _driver.FindElement(By.CssSelector("div.datepicker--cell.datepicker--cell-day.-current-"));
            currentDate.Click();            
        }
        public void ClickAppointmentTime()
        {
            IWebElement timeDropDown = _driver.FindElement(By.XPath("//label[contains(text(),'Appointment Time')]/following-sibling::div[@class='lgfe-select']//select[@id='select-filter']"));
            timeDropDown.Click();
        }

        public string AppointmentTimeSelectBox
        {
            get
            {
                var appointmentTime = GetSelect("select", _appointmentTimeSelect);
                return appointmentTime != null ? appointmentTime.SelectedOption.Text : string.Empty;               
            }

            set
            {
                var appointmentTime = GetSelect("select", _appointmentTimeSelect);
                appointmentTime.SelectByText(value);
            }
        }

        
        #endregion

        #region --- Private Methods ---

        #endregion

    }
}