
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMPlayListsService : ApiActionsBase, IApiPage
    {
        private static readonly string _assetsUpdatePath = $"{BaseService}/UploadAsset";
        private static readonly string _assetsUpdateUserPath = $"{BaseService}/UploadUserAsset";
        private static readonly string _assetsRemovePath = $"{BaseService}/RemoveAsset/{0}";
        private static readonly string _playlistById = $"{BaseService}/{0}";

        public LGMPlayListsService(Common.HttpUtilsHelper httpUtilsHelper) : base (httpUtilsHelper)
        {

        }

        public LGMPlayListsService()
        {

        }

        public int LgmPlayListServiceCreatePlayList(string id)
        {
            string path = string.Format(_playlistById, id);

            HttpHelper.ApiRequest(path, null, null, RequestCommandType.POST);
            return 0;
        }

    }
}
