using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Automation.UITests
{
    public class Base
    {
        public static string addPlaylistsButtonClass = "#playlists-container > div.pm-function-bar.js-playlists-function-bar > div > button";
        public static string saveButtonCSSSelector = "#playlist-info-form > div.lg-modal__actions > button";
        public static string playlistOpenButtonCSSSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(2)";
        public static string weatherWidgetCSSSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-favorite-widgets.js-drag-drop-favorite-widgets > button:nth-child(1)";
        public static string weatherZipCodeInputID = "weather-widget-zip";
        public static string weatherWidgetSaveButtonCSSSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
        public static string playlistSaveCSSSelector = "#playlist-container > div.playlist-content-wrapper.js-playlist-content > div > div:nth-child(3) > button:nth-child(1)";
        public static string financeWidgetCSSSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-favorite-widgets.js-drag-drop-favorite-widgets > button:nth-child(2)";
        public static string saveFinanceButtonCSSSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
        public static string trafficWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-favorite-widgets.js-drag-drop-favorite-widgets > button:nth-child(3)";
        public static string trafficWidgetSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
        public static string triviaWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-favorite-widgets.js-drag-drop-favorite-widgets > button:nth-child(4)";
        public static string triviaSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
        public static string imageWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-image-widget-button";
        public static string assestCssSelector = "#asset-image-select-form > div.aisf-image-matrix > div:nth-child(2) > label";
        public static string assestLibraryDoneButtonCssSelector = "#asset-image-select-form > div.aisf-action-bar > button";
        public static string videoWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-video-widget-button";
        public static string videoAssestSelectionCssSelector = "#asset-video-select-form > div.avsf-image-matrix > div:nth-child(1) > label";
        public static string videoWidgetDoneButtonCssSelector = "#asset-video-select-form > div.avsf-action-bar > button";
        public static string screenFeedWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-screenfeedvideo-widget-button";
        public static string screedFeedSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
        public static string brandWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-brand-widget-button";
        public static string brandSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
        public static string schedulePlaylistCssSelector = "#playlist-container > div.playlist-content-wrapper.js-playlist-content > div > div.pm-action-bar.pm-action-bar-upper > button:nth-child(2)";
        public static string submitScheduleCssSelector = "#asset-info-form > div.lg-modal__actions.schedule-modal-button-wrapper > button:nth-child(1)";

        public static string playlistPublishButtonCssSelector = "#playlist-container > div.playlist-content-wrapper.js-playlist-content > div > div.pm-action-bar.pm-action-bar-upper > button:nth-child(3)";
        public static string publishDoneButtonCssSelector = "#notifications-form > div > button";
        public static string uploadButtonCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button:nth-child(5)";
        public static string uploadFromPCCssSelector = "#asset-upload-form > div > div > div > p > button";
        public static string uploadDialogCloseButtonCssSelector = "#asset-upload-modal > div > div.lg-modal__wrapper > div > button";
        public static string logOutButtonCssSelector = "#interaction-nav-bar-container > div.inbc-help-menu-wrapper > ul > li:nth-child(2) > a > span";
        public static string logoutConfirmCssSelector = "#logout-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div > button:nth-child(2)";
        public static string playlistSideBarMenuCssSelector = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a > span";
        public static string deletePlaylistButtonCssSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(1) > button:nth-child(3)";
        public static string newPlaylistDeleteButtonCSSSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(1) > button:nth-child(3)";
        public static string assetLinkCssSelector = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li:nth-child(2) > a";
        public static string assetUploadButtonCssSelector = "#assets-container > div.pm-function-bar.js-playlists-function-bar > div > button";
        public static string assetBrowseComputerCssSelector = "#asset-upload-form > div > div > div > p > button";//weatherEditButtonCssSelector
        //public static string weatherEditButtonCssSelector = "#playlist-container > div.playlist-content-wrapper.js-playlist-content > div > div.lgfe-card-matrix.js-drag-drop-playlist.lgfe-card-matrix--layout-row > div:nth-child(3) > div > div.\\5b.lgfe-cm-utilities.lgfe-cm-utilities--column.\\5d.lgfe-cm-description > div > button.lgfe-cm-utility-button.button-unstyled.js-playlist-edit";
        //string playlistSideBarMenuCssSelector = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a > span";
    }
}
