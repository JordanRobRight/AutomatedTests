using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    public class SideBarItem : LGBasePage
    {
      
        #region -- Fields -- 
        
        private static string sideBar = @"interaction-nav-bar-container";
        private string playlists = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a";


        #endregion

        #region -- Properties ---

        private IWebElement PlayLists => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, playlists);

        //private IWebElement Assets => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, assets);

        #endregion

        public SideBarItem(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            //this.Driver;
        }

        private void Test()
        {
            //SideBarItem pl = GetMenuItems.First(a => a.Name.Contains("test"));
        }

        public override void Perform()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Driver.Navigate().GoToUrl(url);

            //PlayLists.Click();

        }

        public List<SideBarItem> GetMenuItems => GetMenu(Driver);

        public object Name { get; private set; }

        private List<SideBarItem> GetMenu(IWebDriver driver)
        {
            IWebElement menu = driver.FindElement(By.Id("interaction-nav-bar-container"));
            IWebElement sideBarItem = null;
            
            IEnumerable<IWebElement> menuItems = menu.FindElements(By.TagName("a")).ToList();

            List<SideBarItem> sidebarItems = new List<SideBarItem>();

            foreach (IWebElement menuLink in menuItems)
            {
                if(menuLink.Text.Contains("playlist"))
                {
                    sideBarItem = menuLink;
                }
                else if(menuLink.Text.Contains("assets"))
                {
                    sideBarItem = menuLink;
                }
                else if (menuLink.Text.Contains("players"))
                {
                    sideBarItem = menuLink;
                }
                else if (menuLink.Text.Contains("locations"))
                {
                    sideBarItem = menuLink;
                }
            }

            return sidebarItems;
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }

    }
}







