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
    public class PlayLists : LGBasePage
    {

        #region -- Fields --

        private string _addPlayListButtonId = "";
        private string _searchPlayListField = "";
        private string _playListSortDropDown = "";
        private string _playListDisplayByRowButton = "";
        private string _playListDisplayByGridButton = "";
        private string _playListScrollArea = "";
        #endregion

        #region -- Constructors --
        public PlayLists(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
            //this.Driver;
        }

        private void Test()
        {
            PlayListItem pl =  PlayListItems.First(a => a.Name.Contains("test"));
        }
        #endregion

        #region -- Methods -- 

        #region -- Overrides --

        // Example of overriding the WaitFor element method.
        public override void WaitFor(string itemToWaitFor)
        {
            base.WaitFor(itemToWaitFor);
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
        #endregion

        #region -- Properties --

        private IWebElement AddPlayListButton => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.Id, _addPlayListButtonId);

        public List<PlayListItem> PlayListItems => GetPlayList(Driver);

        #endregion

        private List<PlayListItem> GetPlayList(IWebDriver driver)
        {
            List<PlayListItem> pls = new List<PlayListItem>();

            try
            {
                // THis is just an example using the menu are the right so the locators need to change to support Playlist screen

                // Get the main container for the menu.
                IWebElement p = driver.FindElement(By.Id("interaction-nav-bar-container"));
                // Get the menu that is for the various pages like playlist,assets and such.

                IWebElement p1 = p.FindElement(By.ClassName("inbc-menu-wrapper"));

                // Find all elements that have an a tagname.
                IEnumerable<IWebElement> p2 = p1.FindElements(By.TagName("a")).ToList();

                foreach (IWebElement we in p2)
                {
                    PlayListItem pli = new PlayListItem(driver) { Name = we.Text };
                    pls.Add(pli);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return pls;
        }
    }

}
