using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    public class SideBar : LGBasePage
    {

        public SideBar(IWebDriver driver, TestSystemConfiguration config) : base(driver, config)
        {

        }

        #region -- Fields -- 
        private static string sideBar = @"interaction-nav-bar-container";

        #endregion

        #region -- Properties ---
        private IEnumerable<SideBarItem> SideBarItems => GetMenuItems();

        private IEnumerable<SideBarItem> GetMenuItems()
        {
            // option to make this a list of string or refactor for list of sidebaritem
            List<SideBarItem> menuList = new List<SideBarItem>();

            var sideBarMenuItems = Driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();

            foreach (IWebElement item in sideBarMenuItems)
            {
                // update this section by setting the correct value to the collection
                SideBarItem menuItem = new SideBarItem(Driver) { Name = item.Text, WebElement = item };
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
        private SideBarItem getItems(string itemName)
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
           // var menuItem = getItems(menuName);

            if (menuName == "My Profile")
            {
                var sideBarMenuItems = Driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();
                foreach (IWebElement item in sideBarMenuItems)
                {         
                    if (item.Text == "MY ACCOUNT")//Expand 'my account' to make 'my profile' visible
                    {
                        item.Click();
                    }                     
                }
            }
            var menuItem = getItems(menuName);
            if (menuItem != null)
            {
                menuItem.WebElement.Click();
            }
        }
        
        public List<string> IsMenuItems
        {
            get
                {
                ExpandMyAccount();
                var sideBarMenuItems = Driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();
                List<string> menuList = new List<string>();
                foreach (IWebElement item in sideBarMenuItems)
                {                   
                    string menuItem = item.Text;
                    menuList.Add(menuItem);
                }
                return menuList;
            }
        } 


        private void ExpandMyAccount()
        {
            var sideBarMenuItems = Driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();
            foreach (IWebElement item in sideBarMenuItems)
            {
                if (item.Text == "MY ACCOUNT")
                {
                    item.Click();
                    break;
                }
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







