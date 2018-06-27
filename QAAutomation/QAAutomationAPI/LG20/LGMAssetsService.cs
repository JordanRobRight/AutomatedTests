
namespace QA.Automation.APITests.LG20
{
    public class LGMAssetsService : ApiActionsBase, IApiPage
    {
        private static readonly string _assetsUpdatePath = $"{BaseService}/UploadAsset";
        private static readonly string _assetsUpdateUserPath = $"{BaseService}/UploadUserAsset";
        private static readonly string _assetsRemovePath = $"{BaseService}/RemoveAsset/{0}";

        public LGMAssetsService(Common.HttpUtilsHelper httpUtilsHelper) : base (httpUtilsHelper)
        {

        }

    }
}
