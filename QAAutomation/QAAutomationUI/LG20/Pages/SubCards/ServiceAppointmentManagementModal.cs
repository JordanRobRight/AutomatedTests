using OpenQA.Selenium;
using QA.Automation.UITests.Selenium;
using System;
using System.Linq;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class ServiceAppointmentManagementModal : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        private static string _modalClassName = @"greeting-management-modal";
        private static string _modalContainerName = @"lg-modal__container";
        private static string _modalVisableClass = @"lg-modal lg-modal--large lg-modal--visible";
        private static string _appointmentTimeSelect = @"select-filter";
        private static readonly string _currenteDate = @"#datepickers-container > div:nth-child(3) > div.datepicker--content > div > div.datepicker--cells.datepicker--cells-days > div.datepicker--cell.datepicker--cell-day.-current-.-selected-";
        private static string serviceAppointmentsForm = @"greeting-management-form";//ID
        private static readonly string plussButtonClass = @"pl-utility-add-button";
        private static readonly string _yearDropdown = @"//div[@id='greeting-management-modal']//div[@class='lg-modal__field']//label[@for='vehicleYear']/following-sibling::div";
        private static readonly string _yearDropdownOption = @"//div[@id='greeting-management-modal']//div[@class='lg-modal__field']//label[@for='vehicleYear']/following-sibling::div//option[contains(text(),'ReplaceYear')]";//selects a option 
        #endregion

        #region --- Properties ---
        private static readonly string _currentDateCss = @"div.datepicker--cell.datepicker--cell-day.-current-";
        private static readonly string timeDropDownXpath = @"//label[contains(text(),'Appointment Time')]/following-sibling::div[@class='lgfe-select']//select[@id='select-filter']";
        #endregion

        #region --- Constructor ---
        internal ServiceAppointmentManagementModal(IWebDriver driver) : base(driver, _modalClassName, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
            
            FormID = serviceAppointmentsForm;
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

        public string MakeTextBox
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

        public string ModelTextBox
        {
            get
            {
                var getField = GetField("input", "id", "vehicleModel");
                return getField != null ? getField.Text : string.Empty;
            }
            set
            {
                var getField = GetField("input", "id", "vehicleModel");
                getField?.SendKeysOrClear(value);
            }
        }

        public string AdvisorTextBox
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
            
            IWebElement currentDate = _driver.FindElement(By.CssSelector(_currentDateCss));
            currentDate.Click();            
        }
        public void ClickAppointmentTime()
        {
            IWebElement timeDropDown = _driver.FindElement(By.XPath(timeDropDownXpath));
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
        
        public void YearSelectBox(string year)
        {
            IWebElement clickYearDropdown = _driver.FindElement(By.XPath(_yearDropdown));
            clickYearDropdown.Click();           
        
            IWebElement option = _driver.FindElement(By.XPath(_yearDropdownOption.Replace("ReplaceYear", year)));
            option.Click();
            clickYearDropdown.Click();            
        }

        #endregion

        #region --- Private Methods ---

        #endregion

    }
}