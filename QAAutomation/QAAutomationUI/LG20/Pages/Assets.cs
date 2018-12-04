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
    public class Assets : LGBasePage
    {

        #region -- Fields -- 

        private readonly string assetsFuncitonBar = "pmfb-container";
        private readonly string assetsUtilityBar = "";
        private readonly string assetsContentWrapper = "";

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

        private IWebElement GetAssetAddButton()
        {
            var buttons = SeleniumCommon.GetElement(this.Driver, SeleniumCommon.ByType.ClassName, assetsFuncitonBar)
                .FindElements(By.TagName("button"));
            IWebElement button = buttons.FirstOrDefault(b=> b.GetAttribute("title") != null && b.GetAttribute("title").Equals("Add New Asset", StringComparison.OrdinalIgnoreCase));
            return button;
        }

        private IEnumerable<IWebElement> GetAssetModalButtons()
        {
            return new List<IWebElement>();
        }

        private IEnumerable<DisplayOptions> GetAssetDisplayOptionButtons();

        private IEnumerable<DisplayOptions> GetAssetDisplayOptionButtons()
        {
            List<DisplayOptions> displayOptions = new List<DisplayOptions>();

            var displayOptionButtons = Driver.FindElement(By.ClassName("amub-layout-type"))
                .FindElements(By.ClassName("amublt-field")).ToList();

            foreach (IWebElement button in displayOptions)
            {
                IWebElement buttonItem = new IWebElement(Driver) {Name = button.Text, WebElement = button};
                displayOptions.Add(buttonItem);
            }

            return displayOptions;
        }

        #endregion



        #endregion

        #region -- Properties --

        internal IWebElement AddAssetButton => GetAssetAddButton();
        private IEnumerable<IWebElement> AssetModalButtons => GetAssetModalButtons();
        private IWebElement AssetSearchInput => GetAssetSearchInput();
        private IWebElement AssetFilterDropDown => GetAssetFilterDropDown();
        private IEnumerable<IWebElement> AssetDisplayOptionButtons => GetAssetDisplayOptionButtons();
        private IEnumerable<IWebElement> AssetContent => GetAssetContent();



        #endregion


    }
}
