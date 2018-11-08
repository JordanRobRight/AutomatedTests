﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages;
using QA.Automation.UITests.Models;

namespace QA.Automation.UITests.LG20.Pages
{
    public class SideBarItem : LGBasePage
    {
        #region -- Fields -- 
        private static string sideBar = @"interaction-nav-bar-container";
       

        #endregion

        #region -- Properties ---

        //private IWebElement PlayLists => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, playlists);

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
            
            IEnumerable<IWebElement> menuItems = menu.FindElements(By.TagName("a")).ToList();

            List<SideBarItem> sidebarItems = new List<SideBarItem>();

            foreach (IWebElement menuLink in menuItems)
            {
                //SideBarItem pli = new SideBarItem(driver) { Name = menuLink.Text };
                //sidebarItems.Add(pli);
            }

            return sidebarItems;
        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }

    }
}







