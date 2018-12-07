
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMSubaruVehicleComparisonServicee : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.Subaru.VehicleComparisonService";

        public LGMSubaruVehicleComparisonServicee(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
