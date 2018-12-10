using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    class ModalBasePage
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

        public IEnumerable<IWebElement> GetModalButtons()
        {
            var getModalDialog = GetModal();
            var modalContainer = getModalDialog.FindElement(By.ClassName(_modalContainerName));
            var modalContainerButtons = modalContainer.FindElements(By.TagName("button")).ToList();
            return modalContainerButtons;
        }

        private IWebElement GetModal()
        {
            var getModalWindow = SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.Id, _modalClassName);
            var getActualModal = getModalWindow.FindElements(By.TagName("div")).FirstOrDefault(a => a.GetAttribute("class").Equals(_modalVisableClass));
            return getActualModal;
        }
    }
}
