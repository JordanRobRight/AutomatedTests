using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Automation.APITests.LG20
{
    public class ApiActions
    {
        public string GetAuthInfo(string url, string userName, string password)
        {
            string result = string.Empty;
            userName = System.Web.HttpUtility.UrlEncode(userName);
            password = System.Web.HttpUtility.UrlEncode(password);
            result = GetApiInfo(url, $"api/AuthToken?username={userName}&password={password}").GetAwaiter().GetResult();
            return result;
        }

        public string GetDocumentInfo(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            //userName = System.Web.HttpUtility.UrlEncode(userName);
            //password = System.Web.HttpUtility.UrlEncode(password);
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", "/service", headers).GetAwaiter().GetResult();
            return result;
        }

        private async Task<string> GetApiInfo(string url, string urlQuery, IReadOnlyDictionary<string, string> headers = null)
        {
            string resultInfo = string.Empty;
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new System.Uri(url);
                foreach (var header in headers)
                {
                    hc.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
               // hc.DefaultRequestHeaders.AcceptEncoding = 
                HttpResponseMessage response = await hc.GetAsync(urlQuery);
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
