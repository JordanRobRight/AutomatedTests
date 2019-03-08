using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    internal class TickerManagementModal : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        private static string formID = @"ticker-info-form";//this has 2 element with same ID
        private static string _modalClassName = @"location-settings-ticker-modal";
        private static string _modalContainerName = @"lg-modal__container";
        private static string _modalVisableClass = @"lg-modal lg-modal--large lg-modal--visible";
        private static readonly string plussButton = @"//div[@id='location-settings-ticker-modal']//form[@id='ticker-info-form']//div[@class='pl-utility-add-button']";
        private static readonly string _tableTextArea = @"//table[@id='location-ticker-table']//tr//td//textarea";
        private static readonly string _saveButton = @"//form[@id='ticker-info-form']//button[text()='Save']";
        private static readonly string _cancelButton = @"//form[@id='ticker-info-form']//button[text()='Cancel']";
        #endregion

        #region --- Properties ---








        #endregion

        #region --- Constructor ---
        internal TickerManagementModal(IWebDriver driver) : base(driver, _modalClassName, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
            FormID = formID;
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

        
        public void AddTickerItemModalPlusButton()
        {
            IWebElement btn = _driver.FindElement(By.XPath(plussButton));
            //var button = GetFormArea().FindElement(By.ClassName(plussButtonClass));//modal has 2 forms with same ID
            btn.Click();
        }

        
        

       public void TickerFeedText(string testData)
        {
            List<IWebElement> test = _driver.FindElements(By.XPath(_tableTextArea)).ToList();
            IWebElement lastTD=  test.LastOrDefault();
            lastTD.SendKeys(testData);
          
        }

        public void ClickSaveTickerManagementButton()
        {
            IWebElement saveButton = _driver.FindElement(By.XPath(_saveButton));
            saveButton.Click();
        }

        public void ClickCancelTickerManagementButton(string buttonText)
        {
            IWebElement saveButton = _driver.FindElement(By.XPath(_cancelButton));
            saveButton.Click();
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