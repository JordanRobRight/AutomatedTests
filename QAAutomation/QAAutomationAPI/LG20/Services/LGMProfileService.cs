
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMProfileService : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.ProfileService";

        public LGMProfileService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
