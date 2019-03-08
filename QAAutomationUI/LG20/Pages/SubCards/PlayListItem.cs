using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards
{
    public sealed class PlayListItem
    {

        private string _favoriteButton = "#assets-container > div.assets-content-wrapper.js-assets-content > div > div > div:nth-child(1) > div > div.\\5b.lgfe-cm-utilities.lgfe-cm-utilities--column.\\5d > div > button:nth-child(1)";
        //private string _editButton = "";
        //private string _trashButton = "";
        //private string _copyButton = "";
        //private string _openButton = "";
        //private string _timeSection = "";
        //private string _playlistInfo = "";

        //private string _playListActionArea = "Div where the buttons are";

        private IWebDriver _driver;
        public PlayListItem(IWebDriver driver)
        {
            _driver = driver;
        }

        public string Name { get; set; }
        /*public string Title { get; set; }
         use this to grab variables without names and only titles*/

    }
}
