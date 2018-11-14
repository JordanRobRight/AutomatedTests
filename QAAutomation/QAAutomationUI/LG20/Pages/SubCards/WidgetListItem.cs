using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using QA.Automation.UITests.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    public sealed class WidgetListItem
    {

        private string _cardClass = "lgfe-cm-card";
        private string _informationSection = "[ lgfe-cm-information lgfe-cm-information--order ]";
        private string _widgetNameSection = "lgfe-cm-name line-clamp";
        private string _widgetDurationSection = "[ lgfe-cm-duration [ lgfe-cm-duration--corner lgfe-cm-duration--corner-br ] ]";
        private string _descriptionSection = "lgfe-cm-description line-clamp"; // two lines of descriptions. 
        private string _dragHandleSection = "lgfe-cm-drag-handle button-unstyled js-drag-drop-playlist-widget-handle";
        private string _utilsRowSection = "lgfe-cm-utility-row";

        private string _playListActionArea = "Div where the buttons are";

        private IWebDriver _driver;
        public WidgetListItem(IWebDriver driver)
        {
            _driver = driver;
        }

        public IEnumerable<IWebElement> ActionButtons => UtilButtons();


        public string Name { get; set; }

        private IEnumerable<IWebElement> UtilButtons()
        {
            return SeleniumCommon.GetElement(_driver, SeleniumCommon.ByType.ClassName, _utilsRowSection).FindElements(By.TagName("button"));
        }
        /*public string Title { get; set; }
         use this to grab variables without names and only titles*/

    }
}
