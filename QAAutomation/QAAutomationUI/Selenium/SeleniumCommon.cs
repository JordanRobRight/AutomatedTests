using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace QA.Automation.UITests.Selenium
{
    public class SeleniumCommon
    {

        #region -- Private Methods ---
       
        #endregion


        #region -- Public Methods -- 
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

        public static void ClickOffScreen(IWebDriver driver, SeleniumCommon.ByType byType, string locator, int secondsToWait = 2)
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            // IWebElement offClick = driver.FindElement(By.CssSelector(BaseStrings.offClickCssSelector));
            IWebElement element = GetElement(driver, byType, locator);
            Actions a = new Actions(driver);
            // MoveByOffset(-100, -100)
            a.MoveToElement(element).MoveByOffset(-10, -10).Click().Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(secondsToWait));
            
        }

        //public static void OffClick(IWebDriver driver, By byType, string locator )
        //{
        //    IWebElement offClick = driver.FindElement(By.CssSelector(BaseStrings.offClickCssSelector));
        //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
        //    var element = driver.FindElement(By.CssSelector(BaseStrings.playlistSideBarMenuCssSelector));
        //    new Actions(driver).MoveToElement(element).Click().Perform();
        //}

        public static void AcceptAlert(IWebDriver driver, int secondsToWait = 2)
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(secondsToWait));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

        public static IEnumerable<IWebElement> GetWebElements(IWebElement element, By selector, string NameElement = "")
        {
            IList<IWebElement> query = null;

            try
            {
                query = element.FindElements(selector).ToList();
            }
            catch (NoSuchElementException nsex)
            {
                Console.WriteLine("Element couldn't be found: " + nsex);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: GetElement() {e}");
            }

            return query ?? throw new Exception("GetElements returned a null value.");
        }



        public static void WaitForElementExists(ThreadLocal<IWebDriver> _driver, string element)
        {
            WaitUntilElementExists(_driver.Value, By.Id(element));
        }

       
        //private static WaitForElement(IWebDriver _driver )
        //{
        //    WebDriverWait waitForElement = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        //    waitForElement.Until(ExpectedConditions.ElementIsVisible(By.Id("yourIDHere")));
        //}

        //WaitUntilElementExists(_driver, By.Id("page-header-container"));
        // New way of doing things.
        //var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));

        //this will search for the element until a timeout is reached

        public static Func<IWebDriver, bool> IsElementVisible(IWebElement iwe)
        {
            return (d) =>
            {
                try
                {
                    return iwe.Displayed;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                    //throw;
                }
            };
        }
        public static IWebElement WaitUntilElementExists(IWebDriver driver, By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(c =>
                    {
                        var i = c.FindElement(elementLocator);
                        return i;
                    }
                );
                //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                //return wait.Until(ExpectedConditions.ElementExists(elementLocator));
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

        public static IEnumerable<T> GetListItems<T>(IWebDriver driver, IWebElement elementLocator, By secondLocator)
        {
            IEnumerable<IWebElement> p = elementLocator.FindElements(secondLocator);

            List<T> itemList = new List<T>();

            foreach (var item in p)
            {
                itemList.Add((T)Activator.CreateInstance(typeof(T), new object[] { driver }));
            }

            return itemList;
        }
        #endregion

        public enum ByType
        {
            Css = 1,
            Xml = 2,
            Id = 3,
            ClassName = 4,

        }

        internal static bool IsElementVisible()
        {
            throw new NotImplementedException();
        }
    }
}
