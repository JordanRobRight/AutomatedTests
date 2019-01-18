using NUnit.Framework;
using OpenQA.Selenium;
using QA.Automation.UITests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
// RK - 1/18/19 - Please remove the FluentAssertions using statement. POM classes should not have this using statement since they do not perform assertions. 
using FluentAssertions;
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
        #endregion

        #region --- Constructor ---
        internal ContactUs(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            PageContainerName = "contact-us-container"; // This is property is in the base class which is the string to the id attribute for the content container. 
        }

        protected IWebElement GetField(string tagName, string attribute, string fieldName)
        {
            return GetPageInputFields(tagName).FirstOrDefault(a => a.GetAttribute(attribute) != null && a.GetAttribute(attribute)
                                                                        .Equals(fieldName, StringComparison.OrdinalIgnoreCase));
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
                return getField != null ? getField.Text : string.Empty;
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
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", contactTitleId);
                getField?.SendKeys(value);
            }
        }

        public string ContactCompanyTextField
        {
            get
            {
                var getField = GetField("input", "id", contactCompanyId);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", contactCompanyId);
                getField?.SendKeys(value);
            }
        }

        public string ContactPhoneTextField
        {
            get
            {
                var getField = GetField("input", "id", contactPhoneId);
                return getField != null ? getField.Text : string.Empty;
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
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("input", "id", contactEmailId);
                getField?.SendKeys(value);
            }
        }

        public string ContactCommentsTextField
        {
            get
            {
                var getField = GetField("textarea", "id", contactCommentsId);
                return getField != null ? getField.Text : string.Empty;
            }

            set
            {

                var getField = GetField("textarea", "id", contactCommentsId);
                getField?.SendKeys(value);
            }
        }

        //RK - 1/18/19 - I fixed the SendKeysOrClear method so these methods are probably not needed. :)

        public void ClearFullNameTextbox()
        {
            var textbox = GetField("input", "id", contactFullNameId);
            textbox.Clear();
        }

        public void ClearPhoneTextbox()
        {
            var textbox = GetField("input", "id", contactPhoneId);
            textbox.Clear();
        }

        public void ClearEmailTextbox()
        {
            var textbox = GetField("input", "id", contactEmailId);
            textbox.Clear();
        }

        public void ClickSendButton()
        {
            //click send button
            /*var sendButton = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, contactSendClass)
                .FindElements(By.TagName("button"));
            IWebElement button = sendButton.FirstOrDefault(b => b.GetAttribute("type") != null && b.GetAttribute("type").Equals("submit", StringComparison.OrdinalIgnoreCase));
            button.Click(); */
            IWebElement send = Driver.FindElement(By.CssSelector(contactSendCss));
            send.Click();// click send
            Wait(5);

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
        // RK - Please move the .Should() statement to the actual test method in UnitTest.cs. It is returning the text anyway.
        // The reason is that the test method should do the asserts not this class. There could be an instance
        // depending on user roles that the message might be slightly different which is where we do the test in the Test method. 
        // RK - 1/18/19 - Not sure why you commented this method out. My suggestion was to remove (or actually move) to UnitTest1.cs class. 

        /* public string ContactNotificationMessage()
         {
                 Wait(2);

                 IWebElement contactNotificationMessage = Driver.FindElement(By.CssSelector(contactNotificationMessageCss));
                 string successNotificationMessage = contactNotificationMessage.Text;
                 // RK - 1/18/19 - Just move this statement to UnitTest1.cs since that is where we should do the validation. This method is returning the value anyway it should be pretty easy.
                 successNotificationMessage.Should().Be("Mail sent and will be processed in the order it was received.");

                 return successNotificationMessage;
         }*/

        // RK - 1/18/19 - I suggest that you change the method from VerifyNotificationPopup to IsNotificationPopupDisplayed. You are already returning a bool so it should be easy to move the check to UnitTest1.cs
        #region -- Rob Example --
        public bool IsNotificationPopupDisplayed
        {
            get
            {
                IWebElement contactNotificationPopup = Driver.FindElement(By.CssSelector(contactNotificationCss));
                return contactNotificationPopup.Displayed;
            }
           
        }
        #endregion
        public bool VerifyNotificationPopup()
        {
            IWebElement contactNotificationPopup = Driver.FindElement(By.CssSelector(contactNotificationCss));
            //contactNotificationPopup.Should().Be(contactNotificationPopup.Displayed != false);
            // RK - 1/18/19 - Please move the statement below to the test method in UnitTest1.cs. 
             contactNotificationPopup.Displayed.Should().BeTrue();
        
            return contactNotificationPopup.Displayed;
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

        // RK - 1/18/19 - I suggest that you change the method to something like IsEmailFieldError and return a bool. Follow the example I did for VerifyNotificationPopup
        public bool VerifyEmailFieldError()
        {
            IWebElement invalidEmail = Driver.FindElement(By.XPath("//input[@id='email' and @class='lgfe-input-text expanded contactErrorInput']"));// using xpath to check class "lgfe-input-text expanded contactErrorInput" is enabled
            bool emailErrorCheck = invalidEmail.Enabled;
            // RK - 1/18/19 - Just move this statement to UnitTest1.cs since that is where we should do the validation. This method is returning the value anyway it should be pretty easy.
            emailErrorCheck.Should().BeTrue("Error not shown");
            return emailErrorCheck;
        }
        // RK - 1/18/19 - I suggest that you change the method to something like IsFullNameFieldError and return a bool. Follow the example I did for VerifyNotificationPopup
        public bool VerifyFullNameFieldError()
        {
            IWebElement invalidFullName = Driver.FindElement(By.XPath("//input[@id='full-name' and @class='lgfe-input-text expanded contactErrorInput']"));
            bool fullNameErrorCheck = invalidFullName.Enabled;
            // bool required = Boolean.Parse(fullName.GetAttribute("required"));
            // RK - 1/18/19 - Just move this statement to UnitTest1.cs since that is where we should do the validation. This method is returning the value anyway it should be pretty easy. 
            fullNameErrorCheck.Should().BeTrue("Error not shown");
            return fullNameErrorCheck;
        }

        #endregion

        #region --- Private Methods ---


        #endregion
    }
    
}




