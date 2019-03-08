
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMScreenFeedVideoService : APIActionsBase, IApiPage
    {

        private static readonly string _serviceName = "LG.LGM.ScreenFeedVideoService";

        public LGMScreenFeedVideoService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }

      

    }
}
