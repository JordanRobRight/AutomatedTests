
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMFrontEndService : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.FrontEndService";

        public LGMFrontEndService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
