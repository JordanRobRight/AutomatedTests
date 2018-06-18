using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Automation.Common
{
    public class WebUtils
    {
        public string GetAuthInfo(string url, string userName, string password)
        {
            string result = string.Empty;
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new System.Uri(url);

                result = GetAuthToken(hc, userName, password).GetAwaiter().GetResult();
            }
            return result;
        }

        private async Task<string> GetAuthToken(HttpClient hc, string userName, string password)
        {
            string resultInfo = string.Empty;
            userName = System.Web.HttpUtility.UrlEncode(userName);
            password = System.Web.HttpUtility.UrlEncode(password);

            HttpResponseMessage response = await hc.GetAsync($"api/AuthToken?username={userName}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                resultInfo = await response.Content.ReadAsStringAsync();
            }
            return resultInfo;
        }
    }
}
