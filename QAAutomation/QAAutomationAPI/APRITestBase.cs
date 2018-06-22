using QA.Automation.APITests.Models;
using QA.Automation.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Automation.APITests
{
    public class APRITestBase
    {
        private APRIConfigSettings _config = null;

        public APRITestBase(string userName, string userPassword)
        {
            _config = new APRIConfigSettings { UserName = userName, Password = userPassword };
        }
        public APRITestBase()
        {
            _config = ConfigurationSettings.GetSettingsConfiguration<APRIConfigSettings>();
        }

        public APRIConfigSettings Settings => _config;
    }
}
