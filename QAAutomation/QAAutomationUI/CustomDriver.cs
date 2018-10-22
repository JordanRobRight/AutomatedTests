using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Remote;

namespace QA.Automation.UITests
{
    class CustomDriver : RemoteWebDriver
    {
        public CustomDriver(Uri uri, DesiredCapabilities capabilities, TimeSpan commandTimeout) : base(uri, capabilities, commandTimeout) { }

        public SessionId getSessionId()
        {
            return this.SessionId;
        }
    }
}
