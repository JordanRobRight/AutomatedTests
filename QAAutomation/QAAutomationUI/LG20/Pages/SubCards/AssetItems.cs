using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QA.Automation.UITests.LG20.Pages.SubCards;
using QA.Automation.UITests.Models;
using QA.Automation.UITests.Selenium;


namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    class AssetItems
    {
        //    internal IWebElement AssetModalClose => GetAssetModalClose();
        //    internal IWebElement AssetModalSave => GetAssetModalSave();
        //    internal IWebElement AssetModalNameEdit => GetAssetModalNameEdit();
        //    internal IWebElement AssetModalDescriptionEdit => GetAssetModalDescriptionEdit();
        //    internal IWebElement AssetModalTagsEdit => GetAssetModalTagsEdit();
        //    private IEnumerable<IWebElement> AssetTags => GetAssetTags();
        //    internal IWebElement AssetModalExpirationStart => GetAssetModalExpirationStart();
        //    internal IWebElement AssetModalExpirationEnd => GetAssetModalExpirationEnd();
        //    internal IWebElement AssetThumbnail => GetAssetThumbnail();

        public DisplayOptions(IWebDriver driver)
        {

        }

        public string Name { get; set; }
        public IWebElement WebElement { get; set; }
    }

}
