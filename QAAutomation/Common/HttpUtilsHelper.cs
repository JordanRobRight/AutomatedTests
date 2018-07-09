using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Automation.Common
{
    public class HttpUtilsHelper
    {
        public string ApiRequest(string url, string urlQuery, IReadOnlyDictionary<string, string> headers = null, RequestCommandType apiMethod = RequestCommandType.GET)
        {
            return GetApiInfo(url, urlQuery, headers, apiMethod).GetAwaiter().GetResult();
        }
        private async Task<string> GetApiInfo(string url, string urlQuery, IReadOnlyDictionary<string, string> headers = null, RequestCommandType apiMethod = RequestCommandType.GET)
        {
            string resultInfo = string.Empty;
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new System.Uri(url);
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        hc.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = null;
                HttpContent httpContent = null; // new HttpContent();

                switch (apiMethod)
                {
                    case RequestCommandType.GET:
                        response = await hc.GetAsync(urlQuery);
                        break;
                    case RequestCommandType.POST:
                        response = await hc.PostAsync(urlQuery, httpContent);
                        break;
                    case RequestCommandType.PUT:
                        response = await hc.PutAsync(urlQuery, httpContent);
                        break;
                    case RequestCommandType.PATCH:
                        //response = await hc.PostAsync(urlQuery);
                        break;
                    case RequestCommandType.DELETE:
                        response = await hc.DeleteAsync(urlQuery);
                        break;
                    default:
                        break;
                }

                if (response.IsSuccessStatusCode)
                {
                    resultInfo = await response.Content.ReadAsStringAsync();
                }
            }
            return resultInfo;
        }

        private async Task<string> GetApiInfo(string url, string urlQuery)
        {
            string resultInfo = string.Empty;
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new System.Uri(url);
                HttpResponseMessage response = await hc.GetAsync(urlQuery);
                if (response.IsSuccessStatusCode)
                {
                    resultInfo = await response.Content.ReadAsStringAsync();
                }
            }
            return resultInfo;
        }

    }
}
