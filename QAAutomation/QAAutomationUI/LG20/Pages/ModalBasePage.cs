using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class ModalBasePage
    {
        private readonly string _modalClassName = string.Empty;
        private IWebDriver _driver = null;
        private readonly string _modalContainerName = string.Empty;
        private readonly string _modalVisableClass = string.Empty;

        internal ModalBasePage(IWebDriver driver,  string modalClassName, string modalContainerName, string modalVisableClass)
        {
            _modalClassName = modalClassName;
            _driver = driver;
            _modalContainerName = modalContainerName;
            _modalVisableClass = modalVisableClass;
        }
        public bool ModalCancelButtonClick()
        {
            try
            {
                var cancelButton = GetModalButtons().FirstOrDefault(a => a.GetAttribute("aria-label") != null &&
                                                                         a.GetAttribute("aria-label").Equals("Close", StringComparison.OrdinalIgnoreCase));

                var cancelSpan = cancelButton.FindElement(By.TagName("span"));

                if (cancelSpan != null)
                {
                    cancelSpan.Click();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool ModalSaveButtonClick()
        {
            try
            {
                var saveButton = GetModalButtons().FirstOrDefault(a => a.GetAttribute("type") != null &&
                                                                       a.GetAttribute("type").Equals("submit", StringComparison.OrdinalIgnoreCase) && a.Text.Equals("Save", StringComparison.OrdinalIgnoreCase));
                if (saveButton != null)
                {
                    saveButton.Click();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
        // Doesnt comes under _modalContainer
        public bool ModalContinueButtonClick()
        {
            try
            {
                //_modalContainerName = modalContainerName;
                var continueButton = GetModalButtons().FirstOrDefault(a => a.GetAttribute("type") != null &&
                                                                       a.GetAttribute("type").Equals("submit", StringComparison.OrdinalIgnoreCase) && a.Text.Equals("Continue", StringComparison.OrdinalIgnoreCase));
                if (continueButton != null)
                {
                    continueButton.Click();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public virtual void Wait(int numberOfSeconds = 5, int slideFactor = 0)
        {
            Thread.Sleep(TimeSpan.FromSeconds(numberOfSeconds + slideFactor));
        }

        protected IEnumerable<IWebElement> GetModalButtons()
        {
            var getModalDialog = GetModal();
            var modalContainer = getModalDialog.FindElement(By.ClassName(_modalContainerName));
            var modalContainerButtons = modalContainer.FindElements(By.TagName("button")).ToList();
            return modalContainerButtons;
        }

        protected IWebElement GetModal()
        {
            var getModalWindow = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.Id, _modalClassName);
            var getActualModal = getModalWindow.FindElements(By.TagName("div")).FirstOrDefault(a => a.GetAttribute("class").Equals(_modalVisableClass)); //why does this seem to change the id i put in to widget-modal??
            return getActualModal;
        }
        protected IEnumerable<IWebElement> GetModalInputFields(string tagName)
        {
            var getModalDialog = GetModal();
            var inputFields = getModalDialog.FindElements(By.TagName(tagName)).ToList();

            return inputFields;
        }

        protected SelectElement GetSelect(string tagName, string fieldName)
        {
            var inputField = GetModalInputFields(tagName).FirstOrDefault(a => a.GetAttribute("id") != null
                                                                              && a.GetAttribute("id")
                                                                                  .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
            var actualSelect = new SelectElement(inputField);
            return actualSelect;
        }

        protected IWebElement GetField(string tagName, string attribute, string fieldName)
        {
            return GetModalInputFields(tagName).FirstOrDefault(a => a.GetAttribute(attribute) != null && a.GetAttribute(attribute)
                                                                        .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
