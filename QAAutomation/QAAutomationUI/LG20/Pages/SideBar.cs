﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages
{
    public class SideBarItem : LGBasePage
    {
        public SideBarItem(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            this.driver = driver;
        }

        #region -- Fields -- 

        private static string sideBar = @"interaction-nav-bar-container";
        private string playlists = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a";
        private IWebDriver driver;

        //private void Test()
        //{
        //    PlayListItem pl = PlayListItems.First(a => a.Name.Contains("test"));
        //}

        #endregion

        #region -- Properties ---
        private List<SideBarItems> SideBarItems => GetMenuItems(Driver);

        private List<SideBarItems> GetMenuItems(IWebDriver driver)
        {
            List<SideBarItems> menuList = new List<SideBarItems>();

            var sideBarMenuItems = driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();

            foreach (IWebElement item in sideBarMenuItems )
            {
                SideBarItems menuItem = new SideBarItems(driver) { Name = item.Text };
                menuList.Add(menuItem);
            }
            return menuList;
        }

        //IEnumerable<IWebElement> menuItems = new List<IWebElement>();

        private IWebElement menuElement => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, sideBar);

        //private IWebElement Assets => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Css, assets);

        #endregion

        #region -- Methods ---

        public override void Perform()
        {
            string url = Common.LgUtils.GetUrlBaseUrl(Config.Environment.ToString(), Config.BaseUrl, true);
            Driver.Navigate().GoToUrl(url);

        }

        public override bool VerifyPage()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}






