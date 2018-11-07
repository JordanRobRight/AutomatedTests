
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMSubaruSDSAppSettingsService : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.Subaru.SDSAppSettingsService";

        public LGMSubaruSDSAppSettingsService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
