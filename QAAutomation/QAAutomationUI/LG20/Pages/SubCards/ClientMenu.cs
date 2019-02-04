using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    public class ClientMenu : LGBasePage
    {
        public ClientMenu(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            // Remove this property and use the base class driver property.
            // Replace all other instances of driver in this class.

        }

        #region -- Fields --
        private static string UserDiv = "#interaction-info-bar-container > div > div";
        private static string ClientDropDownDiv = "#interaction-info-bar-container > div > div > div > div.iibcuinow-menu-wrapper";

        private static string _clientMenuWrapper = "#interaction-info-bar-container > div";
        private static string _clientMenuItem = "iibcun-client";


        #endregion

        #region -- Properties --

        public string CurrentClient
        {
            get
            {
                var wrapper = Driver.FindElement(By.CssSelector(_clientMenuWrapper));
                var clientMenuItem = wrapper.FindElement(By.ClassName(_clientMenuItem));
                return clientMenuItem != null ? clientMenuItem.Text : string.Empty;
            }
        }
        private IEnumerable<ClientMenuItem> ClientMenuItems => GetClientMenuItems();

        private IEnumerable<ClientMenuItem> GetClientMenuItems()
        {
            List<ClientMenuItem> menuList = new List<ClientMenuItem>();

            var ClientMenuItems = Driver.FindElement(By.ClassName("iibcuinow-menu-wrapper")).FindElements(By.TagName("a")).ToList();

            foreach (IWebElement item in ClientMenuItems)
            {
                ClientMenuItem menuItem = new ClientMenuItem(Driver) { Name = item.Text, WebElement = item };
                menuList.Add(menuItem);
            }
            return menuList;

        }

        #endregion

        #region -- Methods --


        public override void Perform()
        {
            //string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            //Driver.Navigate().GoToUrl(url);
        }


        private ClientMenuItem GetItems(string itemName)
        {
            var li = ClientMenuItems.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            return li;
        }

        //        public string GetClientMenuItem(string ClientItem)
        //        {
        ////            WaitFor(ClientItem);
        //            Wait();

        //            return GetItems(ClientItem).Name;
        //        }

        public void SelectClient(string clientName)
        {
            var ClientItem = GetItems(clientName);

            if (ClientItem != null)
            {
                ClientItem.WebElement.Click();
            }
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }
        #endregion


        
    }
}
