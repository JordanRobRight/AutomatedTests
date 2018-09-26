using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Automation.UITests
{

    public class BaseStrings
    {
        public static string newPlaylistDiv = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div";
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
        public static string videoWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-video-widget-button > span.\\5b.fa.fa-play-circle.\\5d";
        public static string videoAssestSelectionXPath = "//*[@id='asset-video-select-form']/div[2]/div[1]/label/span[2]/div";
        public static string videoWidgetDoneButtonXpath = "//*[@id='asset-video-select-form']/div[3]/button";
        public static string videoPlayIconXpath = "//*[@id='asset-video-select-modal']/div/div[1]/div/div[1]/div";
        public static string screenFeedWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-screenfeedvideo-widget-button";
        public static string screedFeedSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
        public static string brandWidgetCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button.pmfb-function-button.lgfe-cm-new-card.button-unstyled.lgfe-elem-state.js-drag-drop-pinned-widget-item.lgfe-add-brand-widget-button";
        public static string brandSaveButtonCssSelector = "#widget-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div.lg-modal__actions > button";
        public static string schedulePlaylistCssSelector = "#playlist-container > div.playlist-content-wrapper.js-playlist-content > div > div.pm-action-bar.pm-action-bar-upper > button:nth-child(2)";
        public static string submitScheduleCssSelector = "//*[@id='asset-info-form']/div[3]/button[1]";
        public static string gearIconXpath = "//*[@id='schedule-modal']/div/div/div[1]/div[1]/div";
        public static string playlistPublishButtonCssSelector = "#playlist-container > div.playlist-content-wrapper.js-playlist-content > div > div.pm-action-bar.pm-action-bar-upper > button:nth-child(3)";
        public static string publishDoneButtonCssSelector = "#notifications-form > div > button";
        public static string uploadButtonCssSelector = "#playlist-container > div.pm-function-bar.js-playlist-function-bar > div > div.pmfbc-pinned-widgets.js-drag-drop-pinned-widgets > button:nth-child(5)";
        public static string uploadFromPCCssSelector = "#asset-upload-form > div > div > div > p > button";
        public static string uploadDialogCloseButtonCssSelector = "#asset-upload-modal > div > div.lg-modal__wrapper > div > button";
        public static string logOutButtonCssSelector = "#interaction-nav-bar-container > div.inbc-help-menu-wrapper > ul > li:nth-child(2) > a > span";
        public static string logOutCancelButtonCssSelector = "#logout-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div > button:nth-child(1)";
        public static string logOutCancelButtonCssSelector2 = "#logout-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div > button:nth-child(1)";
        public static string logoutConfirmCssSelector = "#logout-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div > button:nth-child(2)";
        public static string logoutConfirmCssSelector2 = "#logout-modal > div > div > div.lg-modal__container > div.lg-modal__content > form > div > button:nth-child(2)";
        public static string playlistSideBarMenuCssSelector = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li.active > a";
        public static string deletePlaylistButtonCssSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(1) > button:nth-child(3)";
        public static string newPlaylistDeleteButtonCSSSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(1) > button:nth-child(3)";
        public static string assetLinkCssSelector = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li:nth-child(2) > a";
        public static string assetUploadButtonCssSelector = "#assets-container > div.pm-function-bar.js-playlists-function-bar > div > button";
        public static string assetBrowseComputerCssSelector = "#asset-upload-form > div > div > div > p > button";//weatherEditButtonCssSelector
        public static string playerChannelDropdownCssSelector = "#interaction-info-bar-container > div > div > div > div.iibcu-name > span:nth-child(2) > span.iibcun-client";
        public static string gmChannelSelectionXPath = "//*[@id='interaction-info-bar-container']/div/div/div/div[3]/ul/li[3]/a/span";
        public static string logOutChannelSelectionXPath = "//*[@id='interaction-info-bar-container']/div/div/div/div[3]/ul/li[2]/a/span";
        public static string logOutChannelSelectionXPath2 = "//*[@id='interaction-info-bar-container']/div/div/div/div[3]/ul/li[2]/a/span";
        public static string contactUsLinkCssSelector = "#interaction-nav-bar-container > div.inbc-help-menu-wrapper > ul > li:nth-child(1) > a";
        public static string sendButtonCssSelector = "#contact-us-container > form > div.lg-modal__actions > button";
        public static string playersTabCssSelector = "#interaction-nav-bar-container > div.inbc-menu-wrapper > ul > li:nth-child(3) > a > span";
        public static string playerInfoDownArrowCssSelectors = "#player-container > div > div.lgfe-tile-grid-wrapper > div.lgfe-tile.lgfe-tile-grid-item.lgfe-tile-grid-top-item > div.lgfe-tile-button-wrapper > div:nth-child(2) > i";
        public static string playerInfoUpArrowCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div.lgfe-tile.lgfe-tile-grid-item.lgfe-tile-grid-top-item > div.lgfe-tile-button-wrapper > div:nth-child(3) > i";
        public static string playerInfoXCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div.lgfe-tile.lgfe-tile-grid-item.lgfe-tile-grid-top-item > div.lgfe-tile-button-wrapper > div:nth-child(4) > i";
        public static string deviceDownArrowCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) > div.lgfe-tile-button-wrapper > div:nth-child(1) > i";
        public static string deviceUpArrowCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) > div.lgfe-tile-button-wrapper > div:nth-child(2) > i";
        public static string xOnDeviceCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) > div.lgfe-tile-button-wrapper > div:nth-child(3) > i";
        public static string locationDownArrowCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div.lgfe-tile-button-wrapper > div:nth-child(1) > i";
        public static string locationUpArrowCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div.lgfe-tile-button-wrapper > div:nth-child(2) > i";
        public static string locationXButtonCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div.lgfe-tile-button-wrapper > div:nth-child(3) > i";
        public static string playlistsButtonXPath = "//*[@id='player-container']/div/div[1]/div[2]/button[2]/div";
        public static string whatsPlayingDownArrowCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(3) > div:nth-child(1) > div > div.lgfe-tile-button-wrapper > div:nth-child(1) > i";
        public static string whatsPlayingUpArrowCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(3) > div:nth-child(1) > div > div.lgfe-tile-button-wrapper > div:nth-child(2) > i";
        public static string whatsPlayingXButtonCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(3) > div:nth-child(1) > div > div.lgfe-tile-button-wrapper > div:nth-child(3) > i";
        public static string playlistInfoDownArrowCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(3) > div:nth-child(2) > div > div.lgfe-tile-button-wrapper > div:nth-child(1) > i";
        public static string playlistInfoUpArrowXpath = "//*[@id='player-container']/div/div[2]/div[3]/div[2]/div/div[1]/div[2]/i";
        public static string playlistInfoXButtonCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(3) > div:nth-child(2) > div > div.lgfe-tile-button-wrapper > div:nth-child(3) > i";
        public static string devicePingDeviceButtonXPath = "//*[@id='player-container']/div/div[2]/div[2]/div[2]/div[1]/div[2]/div[1]/div[2]/div[1]";
        public static string deviceRefreshAppButtonCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) > div.lgfe-tile-grid-item-content-wrapper.lgfe-tile-max > div:nth-child(1) > div.device-button-wrapper > div:nth-child(2)";
        public static string pageRefreshButtonCssSelector = "#player-container > div > div.pdw-utility-row > div.pdw-utility-row-bottom > div > div > i";
        public static string deviceRestartAppButtonCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) > div.lgfe-tile-grid-item-content-wrapper.lgfe-tile-max > div:nth-child(1) > div.device-button-wrapper > div:nth-child(2)";
        public static string deviceRestartDeviceButtonCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) > div.lgfe-tile-grid-item-content-wrapper.lgfe-tile-max > div:nth-child(1) > div.device-button-wrapper > div:nth-child(4)";
        public static string channelJoinButtonCssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(1) > div:nth-child(2) > div.lgfe-tile-grid-item-content-wrapper.lgfe-tile-max > div.lgfe-tile-grid-item-label-text-wrapper.player-filter-entry-wrapper > button";
        public static string xOutButton1XPath = "//*[@id='player-container']/div/div[2]/div[2]/div[1]/div[2]/div[2]/div[1]/div[2]/div/div[2]/button/span";
        public static string xOutButton1CssSelector = "#player-container > div > div.lgfe-tile-grid-wrapper > div:nth-child(2) > div:nth-child(1) > div:nth-child(2) > div.lgfe-tile-grid-item-content-wrapper.lgfe-tile-max > div:nth-child(1) > div.lg-modal__field > div > div:nth-child(2) > button > span";
        public static string screenConnectCssSelector = "#player-container > div > div.pdw-utility-row > div.pdw-utility-row-top > div.pdwurt-status-wrapper > div > a > i";
        public static string playListXoutCssSelector = "#playlist-info-modal > div > div > div.lg-modal__container > button > span";
        public static string offClickCssSelector = "#playlist-info-modal > div > div > div.lg-modal__overlay";
        public static string createCustomPlaylistCssSelector = "#playlist-info-form > div.lg-modal__section > div.lg-modal__field.lgfe-input-checkbox > label > span.lgfe-input-checkbox__custom-input";
        public static string playlistEditButtonCssSelector = "#playlists-container > div.playlists-content-wrapper.js-playlists-content > div > div > a:nth-child(1) > div > div.lgfe-cm-utilities > div:nth-child(1) > button.lgfe-cm-utility-button.button-unstyled.js-playlist-edit > span.\\5b.fa.fa-pencil.\\5d";
        public static string playlistTitleInputCssSelector = "#form-name";
        public static string playlistEditSaveButtonCssSelector = "#playlist-info-form > div.lg-modal__actions > button";
        public static string playlistTagSectionCssSelector = "#form-tags";
        public static string playlistTagAddButtonCssSelector = "#playlist-info-form > div:nth-child(2) > div:nth-child(4) > div.pim-tags-wrapper > button";
    }
}
