
using QA.Automation.APITests.Models;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMAssetsService : APIActionsBase, IApiPage
    {
        private static readonly string AssetsUpdatePath = $"{BaseService}/UploadAsset";
        private static readonly string AssetsUpdateUserPath = $"{BaseService}/UploadUserAsset";
        private static readonly string AssetsRemovePath = $"{BaseService}/RemoveAsset/{0}";

        private static readonly string _serviceName = "LG.LGM.AssetsService";

        public LGMAssetsService(Common.HttpUtilsHelper httpUtilsHelper, APRIConfigSettings config) : base (httpUtilsHelper, config)
        {
            config.UserName = _serviceName;
        }

        public LGMAssetsService(APRIConfigSettings config) : base (config)
        {
            config.ServiceName = _serviceName;
        }
    }
}
