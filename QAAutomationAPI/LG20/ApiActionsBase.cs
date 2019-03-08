using System;
using System.Collections.Generic;
using System.Dynamic;
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20
{
    public class APIActionsBase : IApiPage, IDisposable
    {
        public static readonly string BaseService = "/service";
        public static readonly string DocumentExists = $"{BaseService}/exists/{0}"; //{DocumentId}
        public static readonly string DocumentLastModified = $"{BaseService}/timestamp/{0}"; //{DocumentId}
        public static readonly string DocumentData = $"{BaseService}/{0}"; //{DocumentId}
        public static readonly string DocumentBatchReplace = $"{BaseService}/utilities/batch";
        public static readonly string DocumentSqlQuery = $"{BaseService}/utilities/query";
        public static readonly string DocumentSqlQueryEx = $"{BaseService}/utilities/queryex";

        private string _authKey = string.Empty;

        private APRIConfigSettings _config = null;

        private HttpUtilsHelper _httpUtilsHelper = new HttpUtilsHelper();

        public APIActionsBase(HttpUtilsHelper httpHelper, APRIConfigSettings config)
        {
            _httpUtilsHelper = httpHelper;
            _config = config;
           
        }
        public APIActionsBase(APRIConfigSettings config)
        {
            _config = config;
           
        }

        public APIActionsBase(APRIConfigSettings config, string authKey) //: this(httpHelper, config)
        {
            _config = config;
            _authKey = authKey;
        }


        public APIActionsBase()
        {

        }

        public virtual void Dispose()
        {
            _httpUtilsHelper = null;

        }

        //public virtual string GetAuthInfo(string url, string userName, string password)
        //{
        //    userName = System.Web.HttpUtility.UrlEncode(userName);
        //    password = System.Web.HttpUtility.UrlEncode(password);
        //    var result = _httpUtilsHelper.ApiRequest(url, $"api/AuthToken?username={userName}&password={password}");
        //    return result ?? string.Empty;
        //}

//        public virtual string GetAuthInfo(IDictionary<string, string> parms)
        public virtual string GetAuthInfo(string serviceUrl, string userName, string passWord)
        {
            var usernameEncode = System.Web.HttpUtility.UrlEncode(userName);
            var passwordEncode = System.Web.HttpUtility.UrlEncode(passWord);
            var result = _httpUtilsHelper.ApiRequest(serviceUrl, $"api/AuthToken?username={usernameEncode}&password={passwordEncode}");
            return result ?? string.Empty;
        }

        public virtual string GetAuthInfo(string serviceUrl)
        {
            var usernameEncode = System.Web.HttpUtility.UrlEncode(_config.UserName);
            var passwordEncode = System.Web.HttpUtility.UrlEncode(_config.Password);
            var result = _httpUtilsHelper.ApiRequest(serviceUrl, $"api/AuthToken?username={usernameEncode}&password={passwordEncode}");
            return result ?? string.Empty;
        }


        public string GetAllDocuments(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers);
            return result ?? string.Empty;
        }
        public string DeleteDocumentInfo(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.DELETE);
            return result ?? string.Empty;
        }

        public string GetDoesDocumentExists(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.GET);
            return result ?? string.Empty;
        }

        public string GetDocumentInfoModifiedById(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.GET);
            return result ?? string.Empty;
        }
        public string GetDocumentInfoById(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.GET);
            return result ?? string.Empty;
        }

        public string PutDocumentInfoById(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.PUT);
            return result ?? string.Empty;
        }

        public string PostDocumentInfoById(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.POST);
            return result ?? string.Empty;
        }

        public string DeleteDocumentInfoById(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.DELETE);
            return result ?? string.Empty;
        }

        public string PutBatchReplaceDocuments(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.PUT);
            return result ?? string.Empty;
        }

        public string PostBatchUpdatesDocuments(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.POST);
            return result ?? string.Empty;
        }

        public string GetPerformSql(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.GET);
            return result ?? string.Empty;
        }

        public string GetPerformSqlExToken(IDictionary<string, string> parms)
        {
            IReadOnlyDictionary<string, string> headers = new Dictionary<string, string> {
                {   "AuthToken",  parms["authtoken"] }
            };
            var result = _httpUtilsHelper.ApiRequest($"{parms["url"]}", BaseService, headers, RequestCommandType.GET);
            return result ?? string.Empty;
        }

        public virtual void DeleteItemsFromApi()
        {
            throw new NotImplementedException();
        }

        public HttpUtilsHelper HttpHelper => _httpUtilsHelper;

        public string AuthToken
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_authKey))
                {
                    _authKey = GetAuthInfo(string.Empty, _config.UserName, _config.Password);
                }
               // else
               // {
                    return _authKey;
               // }
            }
        }

        public string ServiceName => _config.ServiceName;

        //public string Get

        //private async Task<string> GetApiInfo(string url, string urlQuery, IReadOnlyDictionary<string, string> headers = null, ApiRequestCommandType apiMethod = ApiRequestCommandType.GET)
        //{
        //    string resultInfo = string.Empty;
        //    using (HttpClient hc = new HttpClient())
        //    {
        //        hc.BaseAddress = new System.Uri(url);
        //        foreach (var header in headers)
        //        {
        //            hc.DefaultRequestHeaders.Add(header.Key, header.Value);
        //        }

        //        HttpResponseMessage response = null;
        //        HttpContent httpContent = null; // new HttpContent();

        //        switch (apiMethod)
        //        {
        //            case ApiRequestCommandType.GET:
        //                response = await hc.GetAsync(urlQuery);
        //                break;
        //            case ApiRequestCommandType.POST:
        //                response = await hc.PostAsync(urlQuery, httpContent);
        //                break;
        //            case ApiRequestCommandType.PUT:
        //                response = await hc.PutAsync(urlQuery, httpContent);
        //                break;
        //            case ApiRequestCommandType.PATCH:
        //                //response = await hc.PostAsync(urlQuery);
        //                break;
        //            case ApiRequestCommandType.DELETE:
        //                response = await hc.DeleteAsync(urlQuery);
        //                break;
        //            default:
        //                break;
        //        }

        //        if (response.IsSuccessStatusCode)
        //        {
        //            resultInfo = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    return resultInfo;
        //}

        //private async Task<string> GetApiInfo(string url, string urlQuery)
        //{
        //    string resultInfo = string.Empty;
        //    using (HttpClient hc = new HttpClient())
        //    {
        //        hc.BaseAddress = new System.Uri(url);
        //        HttpResponseMessage response = await hc.GetAsync(urlQuery);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            resultInfo = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    return resultInfo;
        //}

    }
}
