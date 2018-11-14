
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMVehicleTrimsService : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.Subaru.VehicleTrimsService";

        public LGMVehicleTrimsService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
