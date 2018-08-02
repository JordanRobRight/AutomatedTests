
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20.Services
{
    public class LGMPlayListsService : APIActionsBase, IApiPage
    {
        private static readonly string AssetsUpdatePath = $"{BaseService}/UploadAsset";
        private static readonly string AssetsUpdateUserPath = $"{BaseService}/UploadUserAsset";
        private static readonly string AssetsRemovePath = $"{BaseService}/RemoveAsset/{0}";
        private static readonly string PlaylistById = $"{BaseService}/{0}";

        public LGMPlayListsService(HttpUtilsHelper httpUtilsHelper, APRIConfigSettings config) : base (httpUtilsHelper, config)
        {

        }

        public LGMPlayListsService()
        {

        }

        public int LGMPlayListServiceCreatePlayList(string id)
        {
            string path = string.Format(PlaylistById, id);

            HttpHelper.ApiRequest(path, null, null, RequestCommandType.POST);
            return 0;
        }

    }
}
