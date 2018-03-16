using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support;

namespace Models.Pages
{
    class Login
    {
       // [FindsBy(How = How.XPath, Using = "//*[@id='search-field-input' or @id='search-input']")]
        public string Email = string.Empty; //"login-email"

        public string password = string.Empty; //login-password

        public Login()
        {

        }
    }
}
