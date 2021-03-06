﻿using QA.Automation.APITests.Models;
using QA.Automation.Common;
using System.Collections.Generic;

namespace QA.Automation.APITests
{
    public class APITestBase
    {
        private readonly APRIConfigSettings _config = null;
        public IDictionary<string, string> AuthTokens = new Dictionary<string, string>();
        //private LG20.APIActionsBase _apiActionBase = null;
        //private LG20.ApiActionsBase _lgApitAction = null;

        public APITestBase(string userName, string userPassword)
        {
            _config = new APRIConfigSettings { UserName = userName, Password = userPassword };
        }
        public APITestBase()
        {
            _config = ConfigurationSettings.GetSettingsConfiguration<APRIConfigSettings>();
        }

        //public LG20.APIActionsBase LGApitAction
        //{
        //    get
        //    {
        //        if (_apiActionBase == null)
        //        {
        //            _apiActionBase = new LG20.APIActionsBase(new HttpUtilsHelper(), _config);
        //        }

        //        return _apiActionBase;
        //    }

        //}

        public APRIConfigSettings Settings => _config;
    }
}
