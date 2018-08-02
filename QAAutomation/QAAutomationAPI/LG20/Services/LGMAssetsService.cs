
using QA.Automation.APITests.Models;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMAssetsService : APIActionsBase, IApiPage
    {
        private static readonly string AssetsUpdatePath = $"{BaseService}/UploadAsset";
        private static readonly string AssetsUpdateUserPath = $"{BaseService}/UploadUserAsset";
        private static readonly string AssetsRemovePath = $"{BaseService}/RemoveAsset/{0}";

        public LGMAssetsService(Common.HttpUtilsHelper httpUtilsHelper, APRIConfigSettings config) : base (httpUtilsHelper, config)
        {

        }
    }
}
