using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace QA.Automation.UITests
{
    public class SeleniumCommon
    {

        #region -- Private Methods ---

        public static IWebElement GetElement(IWebDriver _driver, ByType byType, string element)
        {
            By selector = null;
            IWebElement query = null;

            switch (byType)
            {
                case ByType.Css:
                    selector = By.CssSelector(element);
                    break;
                case ByType.Id:
                    selector = By.Id(element);
                    break;
                case ByType.Xml:
                    selector = By.XPath(element);
                    break;
                case ByType.ClassName:
                    selector = By.ClassName(element);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(byType), byType, null);
            }

            try
            {
                query = _driver.FindElement(selector);

            }
            catch (NoSuchElementException nsex)
            {
                Console.WriteLine("Element couldn't be found: " + nsex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return query ?? throw new Exception("GetElement returned a null value.");
        }
        public static IWebElement GetElement(ThreadLocal<IWebDriver> _driver, By selector, string element = "")
        {
            IWebElement query = null;

            try
            {
                query = _driver.Value.FindElement(selector);
            }
            catch (NoSuchElementException nsex)
            {
                Console.WriteLine("Element couldn't be found: " + nsex);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: GetElement() {e}");
            }

            return query ?? throw new Exception("GetElement returned a null value.");
        }
        public static void WaitForElementExists(ThreadLocal<IWebDriver> _driver, string element)
        {
            WaitUntilElementExists(_driver.Value, By.Id(element));
        }

        #endregion


        #region -- Public Methods -- 
        //private static WaitForElement(IWebDriver _driver )
        //{
        //    WebDriverWait waitForElement = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        //    waitForElement.Until(ExpectedConditions.ElementIsVisible(By.Id("yourIDHere")));
        //}

        //WaitUntilElementExists(_driver, By.Id("page-header-container"));
        // New way of doing things.
        //var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));

        //this will search for the element until a timeout is reached
        public static IWebElement WaitUntilElementExists(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public static IWebElement WaitUntilElementVisible(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }

        public static IWebElement WaitUntilElementClickable(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }
        #endregion

        public enum ByType
        {
            Css = 1,
            Xml = 2,
            Id = 3,
            ClassName = 4,

        }
    }
}
