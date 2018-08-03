
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMStorageService : APIActionsBase, IApiPage
    {

        private static readonly string _serviceName = "LG.LGM.StorageService";

        public LGMStorageService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
