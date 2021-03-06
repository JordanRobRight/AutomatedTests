﻿
using QA.Automation.APITests.Models;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMFiltersService : APIActionsBase, IApiPage
    {
        public static readonly string FilterPublish = $"{BaseService}/actions/publish/{0}"; // {filterId}
        public static readonly string FilterUnPublish = $"{BaseService}/actions/unpublish/{0}"; // {filterId}

        public static readonly string FilterAddDeviceNameToFilters = $"{BaseService}/actions/addDeviceNamesToFilters";
        public static readonly string FilterAddDeviceNameToFilter = $"{BaseService}/actions/addDeviceNamesToFilter"; 
        public static readonly string FilterRemoveDeviceNameFromFilters = $"{BaseService}/actions/removeDeviceNamesFromFilters"; 
        public static readonly string FilterRemoveDeviceNameFromFilter = $"{BaseService}/actions/removeDeviceNamesFromFilter"; 
        public static readonly string FilterMoveDeviceNameToFilter = $"{BaseService}/actions/moveDeviceNamesToFilter"; 

        private static readonly string _serviceName = "LG.LGM.FiltersService";

        public LGMFiltersService(Common.HttpUtilsHelper httpUtilsHelper, APRIConfigSettings config) : base(httpUtilsHelper, config)
        {

        }

        public LGMFiltersService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }

    }
}
