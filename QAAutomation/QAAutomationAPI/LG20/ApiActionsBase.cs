using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QA.Automation.APITests.LG20
{
    public class ApiActionsBase : IApiPage
    {
        public static readonly string BaseService = "/service";
        public static readonly string DocumentExists = $"{BaseService}/exists/{0}"; //{DocumentId}
        public static readonly string DocumentLastModified = $"{BaseService}/timestamp/{0}"; //{DocumentId}
        public static readonly string DocumentData = $"{BaseService}/{0}"; //{DocumentId}
        public static readonly string DocumentBatchReplace = $"{BaseService}/utilities/batch";
        public static readonly string DocumentSqlQuery = $"{BaseService}/utilities/query";
        public static readonly string DocumentSqlQueryEx = $"{BaseService}/utilities/queryex";

        public virtual string GetAuthInfo(string url, string userName, string password)
        {
            string result = string.Empty;
            userName = System.Web.HttpUtility.UrlEncode(userName);
            password = System.Web.HttpUtility.UrlEncode(password);
            result = GetApiInfo(url, $"api/AuthToken?username={userName}&password={password}").GetAwaiter().GetResult();
            return result;
        }

        public string GetAllDocuments(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers).GetAwaiter().GetResult();
            return result;
        }
        public string DeleteDocumentInfo(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.DELETE).GetAwaiter().GetResult();
            return result;
        }

        public string GetDoesDocumentExists(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.GET).GetAwaiter().GetResult();
            return result;
        }

        public string GetDocumentInfoModifiedById(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.GET).GetAwaiter().GetResult();
            return result;
        }
        public string GetDocumentInfoById(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.GET).GetAwaiter().GetResult();
            return result;
        }

        public string PutDocumentInfoById(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.PUT).GetAwaiter().GetResult();
            return result;
        }

        public string PostDocumentInfoById(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.POST).GetAwaiter().GetResult();
            return result;
        }

        public string DeleteDocumentInfoById(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.DELETE).GetAwaiter().GetResult();
            return result;
        }

        public string PutBatchReplaceDocuments(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.PUT).GetAwaiter().GetResult();
            return result;
        }

        public string PostBatchUpdatesDocuments(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.POST).GetAwaiter().GetResult();
            return result;
        }

        public string GetPerformSql(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.GET).GetAwaiter().GetResult();
            return result;
        }

        public string GetPerformSqlExToken(IDictionary<string, string> parms)
        {
            string result = string.Empty;
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            result = GetApiInfo($"{parms["url"]}", BaseService, headers, ApiRequestCommandType.GET).GetAwaiter().GetResult();
            return result;
        }

        //public string Get

        private async Task<string> GetApiInfo(string url, string urlQuery, IReadOnlyDictionary<string, string> headers = null, ApiRequestCommandType apiMethod = ApiRequestCommandType.GET)
        {
            string resultInfo = string.Empty;
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new System.Uri(url);
                foreach (var header in headers)
                {
                    hc.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                HttpResponseMessage response = null;
                HttpContent httpContent = null; // new HttpContent();

                switch (apiMethod)
                {
                    case ApiRequestCommandType.GET:
                        response = await hc.GetAsync(urlQuery);
                        break;
                    case ApiRequestCommandType.POST:
                        response = await hc.PostAsync(urlQuery, httpContent);
                        break;
                    case ApiRequestCommandType.PUT:
                        response = await hc.PutAsync(urlQuery, httpContent);
                        break;
                    case ApiRequestCommandType.PATCH:
                        //response = await hc.PostAsync(urlQuery);
                        break;
                    case ApiRequestCommandType.DELETE:
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
