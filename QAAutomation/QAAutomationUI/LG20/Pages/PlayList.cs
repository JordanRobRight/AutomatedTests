using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.LG20.Pages.SubCards;

namespace QA.Automation.UITests.LG20.Pages
{
    public class PlayList : LGBasePage
    {

        #region -- Fields --

        private string _playListTitle = "phc-title";
        private string _playListNoContents = "playlist-no-content";
        private string _playListContents = "playlist-content";
        private string _playListWidgetContainer = "pmfb-container";
        private string _playListWidgetList = "lgfe-card-matrix js-drag-drop-playlist lgfe-card-matrix--layout-row";
        private string _widgetListContainer = "lgfe-cm-card-container js-drag-drop-playlist-container";
        private string _playListScrollArea = "";
        #endregion

        #region -- Constructors --
        public PlayList(IWebDriver driver, TestConfiguration config) : base(driver, config)
        {
        }

        private void Test()
        {
            //PlayListItem pl =  PlayListItems.First(a => a.Name.Contains("test"));
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
        #endregion

        #region -- Properties --

        private IWebElement playListTitle => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _playListTitle);
        
        private IWebElement playListNoContent => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _playListNoContents);
        private IWebElement playListContent => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _playListContents);
        private IWebElement playListWidgetContainer => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _playListWidgetContainer);
        private IWebElement playListWidgetList => SeleniumCommon.GetElement(Driver, SeleniumCommon.ByType.ClassName, _playListWidgetList);


        public IEnumerable<IWebElement> PlayListWidets => GetWidgets(Driver);

        public IEnumerable<WidgetListItem> Widgets => GetWidgetList(Driver);

        #endregion

        private IEnumerable<IWebElement> GetWidgets(IWebDriver driver)
        {

            IEnumerable<IWebElement> p = playListWidgetContainer.FindElements(By.TagName("Button"));
           
            return p;
        }
        //public override void WaitFor(string itemToWaitFor = "")
        //{
        //    if(itemToWaitFor == "saveModal")
        //    {
        //        IWebElement savemodal = GetElement();
        //        if(savemodal.Displayed)
        //        {
                    
        //        }
        //    }
            
        //    //base.WaitFor(itemToWaitFor);
        //}

        private IEnumerable<WidgetListItem> GetWidgetList(IWebDriver driver)
        {

            IEnumerable<IWebElement> p = playListWidgetList.FindElements(By.ClassName(_widgetListContainer));

            List<WidgetListItem> widgetsItemList = new List<WidgetListItem>();

            foreach (var item in p)
            {
                widgetsItemList.Add(new WidgetListItem(driver));
            }

            return widgetsItemList;
        }
    }

}
