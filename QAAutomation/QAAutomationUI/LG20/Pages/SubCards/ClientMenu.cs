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



        #endregion

        #region -- Properties --

        private IEnumerable<ClientMenuItem> ClientMenuItems => GetClientMenuItems();

        private IEnumerable<ClientMenuItem> GetClientMenuItems()
        {
            // option to make this a list of string or refactor for list of sidebaritem
            List<ClientMenuItem> menuList = new List<GetClientMenuItems>();

            var sideBarMenuItems = Driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();

            foreach (IWebElement item in sideBarMenuItems)
            {
                // update this section by setting the correct value to the collection
                ClientMenuItem menuItem = new ClientMenuItem(Driver) { Name = item.Text, WebElement = item };
                menuList.Add(menuItem);
            }
            return menuList;
        }

        #endregion

        #region -- Methods --


        private ClientMenuItem getItems(string itemName)
        {
            var li = ClientMenuItems.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            return li;
        }
        public override void Perform()
        {
            throw new NotImplementedException();
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

            if (!playerChannelDropdown.Text.Equals(menuItemToSelect, StringComparison.OrdinalIgnoreCase))
            {
                playerChannelDropdown.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));

                var wrapper = GetElement(ByType.ClassName, "iibcuinow-menu-wrapper").FindElements(By.TagName("a"));
                IWebElement gmChannelSelection = null;

                foreach (var menuItem in wrapper)
                {
                    if (menuItem.Text.Equals(menuItemToSelect, StringComparison.OrdinalIgnoreCase)) // == menuItemToSelect)
                    {
                        gmChannelSelection = menuItem;
                        break;
                    }
                }

                //gmChannelSelection = wrapper.FirstOrDefault();

                gmChannelSelection = gmChannelSelection ?? wrapper.FirstOrDefault();

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                gmChannelSelection.Click();
            }
        }
        #endregion
    }
}
