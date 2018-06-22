using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Automation.APITests.LG20
{
    internal class ApiActions
    {
        public string GetAuthInfo(string url, string userName, string password)
        {
            string result = string.Empty;
            userName = System.Web.HttpUtility.UrlEncode(userName);
            password = System.Web.HttpUtility.UrlEncode(password);
            result = GetAuthToken(url, $"api/AuthToken?username={userName}&password={password}").GetAwaiter().GetResult();
            return result;
        }

        private async Task<string> GetAuthToken(string url, string urlQuery)
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


        //private async Task<string> GetAuthToken(HttpClient hc, string userName, string password)
        //{
        //    string resultInfo = string.Empty;
        //    userName = System.Web.HttpUtility.UrlEncode(userName);
        //    password = System.Web.HttpUtility.UrlEncode(password);

        //    HttpResponseMessage response = await hc.GetAsync($"api/AuthToken?username={userName}&password={password}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        resultInfo = await response.Content.ReadAsStringAsync();
        //    }
        //    return resultInfo;
        //}
    }
}
