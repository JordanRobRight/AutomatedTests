using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using System;
using System.Collections.Generic;
using System.Linq;


using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    internal class ContactUs : LGBasePage
    {
        #region --- Fields ---
        #endregion

        #region --- Properties ---
        private static readonly string contactUsLinkId = @"interaction-nav-bar-container";
        private static readonly string contactUsLinkXpath = @"//div[@id='interaction-nav-bar-container']//a[@href='#contact-us']";
        private static readonly string contactFullNameId = @"full-name";
        private static readonly string contactTitleId = @"title";
        private static readonly string contactCompanyId = @"company";
        private static readonly string contactPhoneId = @"phone";
        private static readonly string contactEmailId = @"email";
        private static readonly string contactCommentsId = @"comments";
        private static readonly string contactSendCss = @"#contact-us-container > form > div.lg-modal__actions > button";
        private static readonly string contactSendClass = @"lgfe-button";
        private static readonly string contactDoneCss = @"#notifications-form > div > button";
        private static readonly string contactNotificationCss = @"#notification-modal > div > div.lg-modal__wrapper > div > div";
        private static readonly string contactNotificationMessageCss = @"#notification-modal > div > div.lg-modal__wrapper > div > div > p > strong";
        private static readonly string checkFullNameError = @"//input[@id='full-name' and @class='lgfe-input-text expanded contactErrorInput']";
        private static readonly string checkEmailError = @"//input[@id='email' and @class='lgfe-input-text expanded contactErrorInput']";
        #endregion

        #region --- Constructor ---
        internal ContactUs(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {
            PageContainerName = "contact-us-container"; // This is property is in the base class which is the string to the id attribute for the content container. 
        }

        protected IWebElement GetField(string tagName, string attribute, string fieldName)
        {
            return GetPageInputFields(tagName).FirstOrDefault(a => a.GetAttribute(attribute) != null && a.GetAttribute(attribute)
                                                                        .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }

        private string GetValueFromTextField(IWebElement field)
        {
            return field != null && string.IsNullOrEmpty(field.GetInnerHTML())
                ? field.GetAttribute("value") ?? field.Text
                : string.Empty;
        }

        protected IEnumerable<IWebElement> GetPageInputFields(string tagName)
        {
            var getModalDialog = GetPageContainer();
            var inputFields = getModalDialog.FindElements(By.TagName(tagName)).ToList();

            return inputFields;
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

        public string ContactFullNameTextField
        {
            get
            {
                var getField = GetField("input", "id", contactFullNameId);
                return GetValueFromTextField(getField);
            }

            set
            {
                var getField = GetField("input", "id", contactFullNameId);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ContactTitleTextField
        {
            get
            {
                var getField = GetField("input", "id", contactTitleId);
                return GetValueFromTextField(getField);
                //return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", contactTitleId);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ContactCompanyTextField
        {
            get
            {
                var getField = GetField("input", "id", contactCompanyId);
                return GetValueFromTextField(getField);
                //return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", contactCompanyId);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ContactPhoneTextField
        {
            get
            {
                var getField = GetField("input", "id", contactPhoneId);
                return GetValueFromTextField(getField);
                //return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", contactPhoneId);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ContactEmailTextField
        {
            get
            {
                var getField = GetField("input", "id", contactEmailId);
                return GetValueFromTextField(getField);
                //return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", contactEmailId);
                getField?.SendKeysOrClear(value);
            }
        }

        public string ContactCommentsTextField
        {
            get
            {
                var getField = GetField("textarea", "id", contactCommentsId);
                return GetValueFromTextField(getField);
                //return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("textarea", "id", contactCommentsId);
                getField?.SendKeysOrClear(value);
            }
        }

        public void ClickSendButton()
        {
            Wait(2);
            var button = GetPageContainer().FindElement(By.TagName("button"));
            button.Click();
        }

        public void ClickDone()
        {
            IWebElement done = Driver.FindElement(By.CssSelector(contactDoneCss));
            done.Click();//click done button
            Wait(2);
        }

        /// <summary>
        /// verify popup being displayed and verify the success message
        /// </summary>
        public string ContactNotificationMessage
        {
            get
            {
                Wait(2);
                IWebElement contactNotificationMessage = Driver.FindElement(By.CssSelector(contactNotificationMessageCss));
                string successNotificationMessage = contactNotificationMessage.Text;

                return successNotificationMessage;
            }

        }

        #region -- Rob Example --
        public bool IsNotificationPopupDisplayed2
        {
            get
            {
                IWebElement contactNotificationPopup = Driver.FindElement(By.CssSelector(contactNotificationCss));
                return contactNotificationPopup.Displayed;
            }

        }
        #endregion
        public bool IsNotificationPopupDisplayed
        {
            get
            {
                IWebElement contactNotificationPopup = Driver.FindElement(By.CssSelector(contactNotificationCss));
       

                return contactNotificationPopup.Displayed;
            }
        }

        /*  public bool ClickContactUs()
          {
              try
              {
                  var links = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.Id, contactUsLinkId).FindElements(By.XPath(contactUsLinkXpath));

                  IWebElement link = links.FirstOrDefault(a => a.GetAttribute("title") != null &&
                                                                   a.GetAttribute("title").Equals("Contact Us", StringComparison.OrdinalIgnoreCase));
                  link.Click();
              }
              catch (Exception e)
              {
                  Console.WriteLine(e);
                  return false;
                  //throw;
              }
              return true;
          }*/

        /*public void ContactUsWithAllFields(string contactFullName, string contactTitle, string contactCompany, string contactPhone, string contactEmail, string contactComment)
        {
            IWebElement fullname = Driver.FindElement(By.Id(contactFullNameId));
            //fullname.SendKeys(emailFullName); // from json
            fullname.SendKeys(contactFullName);// valid data to full name

            IWebElement title = Driver.FindElement(By.Id(contactTitleId));
            title.SendKeys(contactTitle); // valid data to title

            IWebElement company = Driver.FindElement(By.Id(contactCompanyId));
            company.SendKeys(contactCompany);// valid data to company

            IWebElement phone = Driver.FindElement(By.Id(contactPhoneId));
            phone.SendKeys(contactPhone);

            IWebElement email = Driver.FindElement(By.Id(contactEmailId));
            email.SendKeys(contactEmail);// valid data to email

            IWebElement comment = Driver.FindElement(By.Id(contactCommentsId));
            comment.SendKeys(contactComment);// valid data to comment/question

            IWebElement send = Driver.FindElement(By.CssSelector(contactSendCss));
            send.Click();// click send
            Wait(5);

            VerifyNotificationPopup();//verify popup being displayed and verify the success message

            IWebElement done = Driver.FindElement(By.CssSelector(contactDoneCss));
            done.Click();//click done button
            Wait(2);
        } */

        /* public void ContactUsWithRequiredFields()
         {
             IWebElement fullName = Driver.FindElement(By.Id(contactFullNameId));
             fullName.SendKeys("LG-AUTOTEST");

             IWebElement send = Driver.FindElement(By.CssSelector(contactSendCss));

             Wait(2);
             send.Click();

             IWebElement phone = Driver.FindElement(By.Id(contactPhoneId));

             phone.SendKeys("Auto Test");// non numeric data in phone number field
             send.Click();
             phone.Clear();// clear the phone number field 

             phone.SendKeys("1234567");// less than 10 digits
             send.Click();
             phone.Clear();// clear the phone number field 

             phone.SendKeys("1234567890");//valid 10 digits phone number
             send.Click();

             IWebElement email = Driver.FindElement(By.Id(contactEmailId));

             email.SendKeys("test"); // invalid email data
             send.Click();
             email.Clear();

             email.SendKeys("test@"); // invalid email data
             send.Click();
             email.Clear();

             email.SendKeys("test@qa");// invalid email data
             send.Click();
             email.Clear();

             email.SendKeys("test@qa.");// invalid email data
             send.Click();
             email.Clear();

             email.SendKeys("test@qa.c");// invalid email data
             send.Click();
             email.Clear();

             email.SendKeys("test@qa.co");// valid email data
             send.Click();

             IWebElement comment = Driver.FindElement(By.Id(contactCommentsId));
             comment.SendKeys("Automated Tester"); // data to comment/questions
             send.Click();

             Wait(5);

             VerifyNotificationPopup();//verify popup being displayed and verify the success message

             IWebElement done = Driver.FindElement(By.CssSelector(contactDoneCss));
             done.Click();//click done button
             Wait(2);

         }      */

       
        public bool IsEmailFieldError
        {
            get
            {
                IWebElement invalidEmail = Driver.FindElement(By.XPath(checkEmailError));// using xpath to check class "lgfe-input-text expanded contactErrorInput" is enabled
                return invalidEmail.Enabled;
            }
        }
        
        public bool IsFullNameFieldError
        {
            get
            {
                IWebElement invalidFullName = Driver.FindElement(By.XPath(checkFullNameError));
                return invalidFullName.Enabled;
            }

        }

        #endregion

        #region --- Private Methods ---


        #endregion
    }
}    