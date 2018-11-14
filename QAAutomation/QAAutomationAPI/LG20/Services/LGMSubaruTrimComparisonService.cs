
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMSubaruTrimComparisonServicee : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.Subaru.TrimComparisonService";

        public LGMSubaruTrimComparisonServicee(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
