using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using QA.Automation.UITests.Selenium;
using System;
using System.Linq;
namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class TagManagementModal : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        private static string _modalClassName = @"tag-location-setting-modal";
        private static string _modalContainerName = @"lg-modal__container";
        private static string _modalVisableClass = @"lg-modal lg-modal--large lg-modal--visible";
        private static string _appointmentTimeSelect = @"select-filter";
       // private IWebElement tagsContainer => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, addTagsForm);

        private static readonly string _currenteDate = @"#datepickers-container > div:nth-child(3) > div.datepicker--content > div > div.datepicker--cells.datepicker--cells-days > div.datepicker--cell.datepicker--cell-day.-current-.-selected-";
        #endregion

        #region --- Properties ---








        #endregion

        #region --- Constructor ---
        internal TagManagementModal(IWebDriver driver) : base(driver, _modalClassName, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
        }
        #endregion

        #region --- Public Methods ---
        public string TagManagementTextField
        {
            get
            {
                var getField = GetField("input", "id", "edit-tags-input");
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", "edit-tags-input");
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
        
        public void AppointmentDaySelectDate()
        {
            var getField = GetField("input", "id", "appointmentDate");
            getField.Click();
            
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

        public bool ClickOffScreen()
        {
            try
            {
                var cancelButton = GetModalButtons().FirstOrDefault(a => a.GetAttribute("aria-label") != null &&
                                                                         a.GetAttribute("aria-label").Equals("Close", StringComparison.OrdinalIgnoreCase));

                //var cancelButton = GetModalButtons().FirstOrDefault(a => a.GetAttribute("aria-label") != null &&
                //                                                         a.GetAttribute("aria-label").Equals("Close", StringComparison.OrdinalIgnoreCase));

                var cancelSpan = cancelButton.FindElement(By.TagName("span"));
                Actions action = new Actions(_driver);
                // MoveByOffset(-100, -100)
                action.MoveToElement(cancelSpan).MoveByOffset(300, 300).Click().Build().Perform();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                // SeleniumCommon.ClickOffScreen(this._driver, SeleniumCommon.ByType.Id, _playListSettingModal);

            }
            catch (Exception)
            {
                return false;
                //Console.WriteLine(e);
                //throw;
            }
            return true;
        }


        /* public void AddTagManagementButton()
         {
             var buttons = tagsContainer.GetElementFromCompoundClass(By.TagName("button"), _buttonsClass);
             buttons.Click();
             Wait(2);
         }*/
        #endregion

        #region --- Private Methods ---

        #endregion

    }
}