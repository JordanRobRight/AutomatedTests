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

namespace QA.Automation.UITests.LG20.Pages
{
    public class SideBar : LGBasePage
    {

        public SideBar(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            
        }

        #region -- Fields -- 
        private static string sideBar = @"interaction-nav-bar-container"; 
        private string playlists = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a";
       
        #endregion

        #region -- Properties ---
        private IEnumerable<SideBarItem> SideBarItems => GetMenuItems();

        // update return value to be ienumerable<T>
        private IEnumerable<SideBarItem> GetMenuItems()
        {
            // option to make this a list of string or refactor for list of sidebaritem
            List<SideBarItem> menuList = new List<SideBarItem>();

            var sideBarMenuItems = Driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();

            foreach (IWebElement item in sideBarMenuItems )
            {
                // update this section by setting the correct value to the collection
                SideBarItem menuItem = new SideBarItem(Driver) { Name = item.Text, WebElement = item};
                menuList.Add(menuItem);
            }
            return menuList;
        }

        private IWebElement menuElement => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, sideBar);
 
        #endregion

        #region -- Methods ---

        public override void Perform()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Driver.Navigate().GoToUrl(url);
        }

        // Add a method call to find a menu item and return the result. This method should be generic. 
        private SideBarItem getItems(string itemName )
        {
            var li = SideBarItems.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            return li;
        }

        public string GetMenuItem(string menuItem)
        {
            return getItems(menuItem).Name;
        }

        public void SelectMenu(string menuName)
        {
            var menuItem = getItems(menuName);

            if (menuItem != null)
            {
                menuItem.WebElement.Click();
            }
        }

        

        public override bool VerifyPage()
        {
            //string url = driver.Url;
            //Assert.IsTrue(url.Contains("dcimliveguide"));
            throw new NotImplementedException();

        }

        #endregion
    }
}







