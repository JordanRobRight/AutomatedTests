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
            // Remove this property and use the base class driver property.
            // Replace all other instances of driver in this class.
            this.driver = driver;
        }

        #region -- Fields -- 

        /// <summary>
        /// 
        /// </summary>
        private static string sideBar = @"interaction-nav-bar-container"; 
        private string playlists = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a";
        private IWebDriver driver;
       

        #endregion

        #region -- Properties ---
        private List<SideBarItem> SideBarItems => GetMenuItems(Driver);

        // update return value to be ienumerable<T>
        private List<SideBarItem> GetMenuItems(IWebDriver driver)
        {
            // option to make this a list of string or refactor for list of sidebaritem
            List<SideBarItem> menuList = new List<SideBarItem>();

            var sideBarMenuItems = driver.FindElement(By.Id("interaction-nav-bar-container")).FindElements(By.TagName("a")).ToList();

            foreach (IWebElement item in sideBarMenuItems )
            {
                // update this section by setting the correct value to the collection
                //SideBarItem menuItem = new SideBarItem(driver) { Name = item };
                //menuList.Add(menuItem);
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

        public void PlaylistClick()
        {
            var playlist = SideBarItems.FirstOrDefault(x => x.Name.Contains("playlist"));//.Select(x => x.Name.Contains("playlist"));
            SideBarItem match = SideBarItems.Find(x => x.Name == "playlist");
            
        }

        // Add a method call to find a menu item and return the result. This method should be generic. 
        public void AssetsClick()
        {
            var asset = SideBarItems.Select(x => x.Name.Contains("assets")); 
        }

        public void PlayersClick()
        {
           var players = SideBarItems.Select(x => x.Name.Contains("players"));
        }

        public void LocationsClick()
        {
           var locations = SideBarItems.Select(x => x.Name.Contains("location"));
        }

        public void MyAccountClick()
        {
           var myAccount = SideBarItems.Select(x => x.Name.Contains("account"));
        }

        public void ContactUsClick()
        {
            var contactUs = SideBarItems.Select(x => x.Name.Contains("contact"));
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







