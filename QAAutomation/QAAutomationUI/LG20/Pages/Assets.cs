using System;
using System.Collections.Generic;
using System.Data;
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
    public class Assets : LGBasePage
    {

        #region -- Fields -- 

        private readonly string assetsFuncitonBar = "pmfb-container";
        private readonly string assetsUtilityBar = "";
        private readonly string assetsContentWrapper = "";
        private readonly string assetsSearchInput = "assets-search";

        #endregion

        #region -- Constructors -- 

        public Assets(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
        }

        #endregion

        #region -- Methods --

            #region -- Overrides --
            public override void Perform()
            {
                throw new NotImplementedException();
            }

            public override bool VerifyPage()
            {
                throw new NotImplementedException();
            }
        #endregion

        public IWebElement GetAssetSearchInput()
        {
            IWebElement searchInput = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.Id, assetsSearchInput);
            return searchInput;
        }

        public IWebElement GetAssetAddButton()
        {
            var buttons = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, assetsFuncitonBar)
                .FindElements(By.TagName("button"));
            IWebElement button = buttons.FirstOrDefault(b=> b.GetAttribute("title") != null && b.GetAttribute("title").Equals("Add New Asset", StringComparison.OrdinalIgnoreCase));
            return button;
        }

        //private IEnumerable<IWebElement> GetAssetModalButtons()
        //{
        //    return new List<IWebElement>();
        //}

        private IEnumerable<AssetItem> AssetItems => GetAssetDisplayOptionButtons();

        //private string test = "pm-utility-bar js-playlists-utility-bar";

        private IEnumerable<AssetItem> GetAssetDisplayOptionButtons()
        {
            List<AssetItem> displayOptions = new List<AssetItem>();

            var displayOptionButtons = Driver.FindElement(By.ClassName("amub-layout-type"));


            //var parentDiv = displayOptionButtons.GetElementFromCompoundClass(By.TagName("div"),
                //"pm-utility-bar js-playlists-utility-bar");
            //GetElementFromCompoundClass("pmub-layout-type");

            var newItem = displayOptionButtons.FindElements(By.ClassName("amublt-field")).ToList();//amub-layout-type


            foreach (IWebElement button in newItem)
            {
                AssetItem buttonItem = new AssetItem(Driver) {Name = button.Text, WebElement = button};
                displayOptions.Add(buttonItem);
            }

            return displayOptions;
        }

        private AssetItem getItems(string itemName)
        {
            var li = AssetItems.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            return li;
        }

        public string GetItem(string headerButton)
        {
            //WaitFor(headerButton);

            return getItems(headerButton).Name;
        }

        public string GetDisplayOptionItem(string headerButton)
        {
            //WaitFor(headerButton);

            return getItems(headerButton).WebElement;
        }

        public void SelectDisplayOption(string option)
        {
            var item = getItems(option);

            if (item != null)
            {
                item.WebElement.Click();
            }
        }

        public void SelectAddButton(string button)
        {
            var item = getItems(button);

            if (item != null)
            {
                item.WebElement.Click();
            }
        }



        #endregion

        #region -- Properties --

        internal IWebElement AddAssetButton => GetAssetAddButton();
        //private IEnumerable<IWebElement> AssetModalButtons => GetAssetModalButtons();
        private IWebElement AssetSearchInput => GetAssetSearchInput();
        //private IWebElement AssetFilterDropDown => GetAssetFilterDropDown();
        //private IEnumerable<IWebElement> AssetDisplayOptionButtons => GetAssetDisplayOptionButtons();
        //private IEnumerable<IWebElement> AssetContent => GetAssetContent();



        #endregion


    }
}
