using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace QA.Automation.UITests.Selenium
{
    public static class SeleniumExtensionMethods
    {

        /// <summary>
        /// Get the innerHTML text for better debugging
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public static string GetInnerHTML(this IWebElement webElement)
        {
            return webElement != null && webElement.GetAttribute("innerHTML") != null
                ? webElement.GetAttribute("innerHTML")
                : string.Empty;
        }

        /// <summary>
        /// Get the outerHTML text for better debugging
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public static string GetOuterHTML(this IWebElement webElement)
        {
            return webElement != null && webElement.GetAttribute("outerHTML") != null
                ? webElement.GetAttribute("outerHTML")
                : string.Empty;
        }

        /// <summary>
        /// Get the element by Class for a compound class name that Selenium doesn't support fully.
        /// </summary>
        /// <param name="webElement"></param>
        /// <param name="locator"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static IWebElement GetElementFromCompoundClass(this IWebElement webElement, By locator, string className)
        {
            return webElement.FindElements(locator).FirstOrDefault(a => a.GetAttribute("class") != null && a.GetAttribute("class").Equals(className));
        }
    }

  
}
