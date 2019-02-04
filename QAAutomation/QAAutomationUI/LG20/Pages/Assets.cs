using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FluentAssertions;
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
                var assetsContainer = Driver.FindElement(By.Id("page-container"));
                assetsContainer.Should().NotBeNull();
                var assetsListContentWrapper = assetsContainer.GetElementFromCompoundClass(By.TagName("div"),
                        "assets-content-wrapper js-assets-content");
                assetsListContentWrapper.Should().NotBeNull();
                var assetContents = assetsListContentWrapper.FindElement(By.ClassName("assets-content"));
                assetContents.Should().NotBeNull();
                var assetItems = assetContents.FindElements(By.TagName("div"))
                    .Where(a => a.GetAttribute("data-guid") != null).Select(a => a).ToList();
                assetItems.Should().NotBeNull();
                assetItems.Should().HaveCountGreaterThan(1);

                return true;
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
            IWebElement button = buttons.FirstOrDefault(b => b.GetAttribute("title") != null && b.GetAttribute("title").Equals("Add New Asset", StringComparison.OrdinalIgnoreCase));
            return button;
        }

        private IEnumerable<AssetItem> AssetItems => GetAssetDisplayOptionButtons();

        private IEnumerable<AssetItem> GetAssetDisplayOptionButtons()
        {
            List<AssetItem> displayOptions = new List<AssetItem>();

            var displayOptionButtons = Driver.FindElement(By.ClassName("amub-layout-type")).FindElements(By.ClassName("amublt-field")).ToList();


            foreach (IWebElement button in displayOptionButtons)
            {
                AssetItem buttonItem = new AssetItem(Driver) { Name = button.Text, WebElement = button };
                displayOptions.Add(buttonItem);
            }

            return displayOptions;
        }

        private AssetItem getItems(string itemName)
        {
            var li = AssetItems.FirstOrDefault(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            return li;
        }

        private AssetItem getDisplayItems(string itemName)
        {
            var li = AssetItems.FirstOrDefault(x => x.Name.Contains(itemName));

            return li;
        }

        public string GetDisplayOptionItem(string headerButton)
        {
            return getDisplayItems(headerButton).Name;
        }

        public void SelectDisplayOption(string option)
        {
            var item = getItems(option);

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
