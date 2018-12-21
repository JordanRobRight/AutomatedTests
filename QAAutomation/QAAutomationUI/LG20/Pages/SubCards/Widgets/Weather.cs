﻿using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.LG20.Pages.SubCards.Widgets
{
    internal class Weather : ModalBasePage
    {
        #region --- Fields ---
        private readonly string _modal = "widget-modal";
        private IWebDriver _driver;
        private static string _modalClassName = @"widget-modal";
        private static string _modalContainerName = @"lg-modal__container";
        private static string _modalVisableClass = @"lg-modal lg-modal--large lg-modal--visible";
        private static string _weatherBrandSelect = @"select-brand";
        private static string _weatherDescription = @"lg-modal-description";
        private static string _weatherDescriptionContent = @"lg-modal-description-content";
        private static string _weatherZipCodeError = @"lg-modal__field__input lgfe-input-text small errorInput";
        #endregion

        #region --- Properties ---

        public string BrandSelectBox
        {
            get
            {
                var selectBoxProgramChannel = GetSelect("select", _weatherBrandSelect);
                return selectBoxProgramChannel != null ? selectBoxProgramChannel.SelectedOption.Text : string.Empty;
                //////////////////////////////////IF     then                                             else 
            }

            set
            {
                var selectBoxProgramChannel = GetSelect("select", _weatherBrandSelect);
                selectBoxProgramChannel.SelectByText(value);
            }
        }

        //public IEnumerable<string> BrandsFromSelectionBox
        //{
        //    get;
        //}

        public string DurationTextBox
        { get; set; }

        public string WeatherDescription
        {
            get
            {
                var textDescription = GetModal().FindElement(By.ClassName(_weatherDescription));
                var textDescriptionContent = textDescription.FindElement(By.ClassName(_weatherDescriptionContent));
                return textDescriptionContent.Text.Trim();
            }
        }

        public string WeatherZipCodeTextBox
        {
            get
            {
                var textbox = GetField("input", "id", "weather-widget-zip");
                return textbox.Text;
            }
            set
            {
                var textbox = GetField("input", "id", "weather-widget-zip");
                textbox.SendKeys(value);
            }
        }

        public bool WeatherZipCodeError
        {
            get
            {
                var textbox = GetField("input", "id", "weather-widget-zip");
                var textError = textbox.GetAttribute("class").ToLower().Contains("small errorinput");
                return textError != null ? true : false;
            }
        }
        #endregion

        #region --- Constructor ---
        internal Weather(IWebDriver driver) : base (driver, _modalClassName, _modalContainerName, _modalVisableClass)
        {
            _driver = driver;
        }
        #endregion

        #region --- Public Methods ---
        public void ClearWeatherZipCodeTextbox()
        {
            var textbox = GetField("input", "id", "weather-widget-zip");
            textbox.Clear();
        }
        #endregion

        #region --- Private Methods ---
        
        #endregion 

    }
}