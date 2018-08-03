
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMLocationsService : APIActionsBase, IApiPage
    {

        private static readonly string _serviceName = "LG.LGM.LocationsService";

        public LGMLocationsService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }

      

    }
}
