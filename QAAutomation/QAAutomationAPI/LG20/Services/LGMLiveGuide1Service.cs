
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMLiveGuide1Service : APIActionsBase, IApiPage
    {
        private static readonly string _serviceName = "LG.LGM.LiveGuide1Service";

        public LGMLiveGuide1Service(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
