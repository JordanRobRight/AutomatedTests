
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMSubaruTrimAccessoriesService : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.Subaru.TrimAccessoriesService";

        public LGMSubaruTrimAccessoriesService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
