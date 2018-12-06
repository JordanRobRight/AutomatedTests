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

        public static IWebElement GetElementFromCompoundClass(this List<IWebElement> webElement, string locator)
        {
            return webElement.FirstOrDefault(a => a.GetAttribute("class") != null && a.GetAttribute("class").Equals(locator));
        }
    }

  
}
