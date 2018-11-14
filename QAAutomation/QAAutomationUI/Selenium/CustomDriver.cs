using System;
using OpenQA.Selenium.Remote;

namespace QA.Automation.UITests.Selenium
{
    public class CustomDriver : RemoteWebDriver
    {
        public CustomDriver(Uri uri, DesiredCapabilities capabilities, TimeSpan commandTimeout) : base(uri, capabilities, commandTimeout) { }

        public SessionId GetSessionId()
        {
            return this.SessionId;
        }
    }
}
