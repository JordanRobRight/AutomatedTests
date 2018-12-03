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
    public class ClientMenu: LGBasePage
    {
        public ClientMenu(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            // Remove this property and use the base class driver property.
            // Replace all other instances of driver in this class.

        }

        #region -- Fields --
        public static string UserDiv = "#interaction-info-bar-container > div > div";
        public static string ClientDropDownDiv = "#interaction-info-bar-container > div > div > div > div.iibcuinow-menu-wrapper";



        #endregion

        #region -- Properties --

        private IEnumerable<ClientMenuItem> ClientMenuItems => GetClientMenuItems();

        private IEnumerable<ClientMenuItem> GetClientMenuItems()
        {
            List<ClientMenuItem> menuList = new List<ClientMenuItem>();

            var ClientMenuItems = Driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();

            foreach (IWebElement item in ClientMenuItems)
            {
                // update this section by setting the correct value to the collection
                ClientMenuItem menuItem = new ClientMenuItem(Driver) { Name = item.Text, WebElement = item };
                menuList.Add(menuItem);
            }
            return menuList;
        }

        #endregion

        #region -- Methods --


        public override void Perform()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Driver.Navigate().GoToUrl(url);
        }


        private ClientMenuItem getItems(string itemName)
        {
            var li = ClientMenuItems.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            return li;
        }

        public string GetClientMenuItem(string ClientItem)
        {
            return getItems(ClientItem).Name;
        }

        public void SelectClient(string clientName)
        {
            var ClientItem = getItems(clientName);

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


        #region Old Code
        private void SelectItemFromCilentMenu(IWebDriver driver, string menuItemToSelect)
        {
 
            //string playlistDivCssSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div";

            //IWebElement playlistDiv = _driver.Value.FindElement(By.CssSelector(playlistDivCssSelector));
            //if playlists is empty find profile dropdown 

            IWebElement playerChannelDropdown = driver.FindElement(By.CssSelector(BaseStrings.playerChannelDropdownCssSelector));
            //string clientName = "GM";

            //if (!playerChannelDropdown.Text.Equals(menuItemToSelect, StringComparison.OrdinalIgnoreCase))
            //{
            //    playerChannelDropdown.Click();
            //    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));

            //    var wrapper = GetElement(ByType.ClassName, "iibcuinow-menu-wrapper").FindElements(By.TagName("a"));
            //    IWebElement gmChannelSelection = null;

            //    foreach (var menuItem in wrapper)
            //    {
            //        if (menuItem.Text.Equals(menuItemToSelect, StringComparison.OrdinalIgnoreCase)) // == menuItemToSelect)
            //        {
            //            gmChannelSelection = menuItem;
            //            break;
            //        }
            //    }

            //    //gmChannelSelection = wrapper.FirstOrDefault();

            //    gmChannelSelection = gmChannelSelection ?? wrapper.FirstOrDefault();

            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //    gmChannelSelection.Click();
            //}
        }
        #endregion
    }
}
