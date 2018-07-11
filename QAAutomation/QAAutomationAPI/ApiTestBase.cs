using QA.Automation.APITests.Models;
using QA.Automation.Common;
using System.Collections.Generic;

namespace QA.Automation.APITests
{
    public class ApiTestBase
    {
        private APRIConfigSettings _config = null;
        public IDictionary<string, string> AuthTokens = new Dictionary<string, string>();
        private LG20.ApiActionsBase _lgApitAction = null;

        public ApiTestBase(string userName, string userPassword)
        {
            _config = new APRIConfigSettings { UserName = userName, Password = userPassword };
        }
        public ApiTestBase()
        {
            _config = ConfigurationSettings.GetSettingsConfiguration<APRIConfigSettings>();
        }

        public LG20.ApiActionsBase LGApitAction = new LG20.ApiActionsBase(new HttpUtilsHelper());

        public APRIConfigSettings Settings => _config;
    }
}
