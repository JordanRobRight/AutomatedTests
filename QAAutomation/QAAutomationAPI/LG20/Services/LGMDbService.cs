
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMDbService : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.DbService";

        public LGMDbService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
