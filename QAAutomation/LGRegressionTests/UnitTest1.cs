
using ServiceStack;
using System.Net;
using System.Net.Http;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LGRegressionTests
{
    [TestClass]
    public class UnitTest1
    {
        private const string TestUserName = "testrunner%40dciartform.com";
        private const string TestPassword = "LiveGuide%232727";

        [TestMethod]
        public void TestMethod1Async()
        {
            HttpClient hc = new HttpClient();

            hc.BaseAddress = new System.Uri("https://lg-qa2-appservice-deploytest.azurewebsites.net");

            string item = GetInfo(hc).GetAwaiter().GetResult();

            //IServiceClient client = new JsonHttpClient
            //{
            //    BaseUri = "https://lg-qa2-appservice-deploytest.azurewebsites.net"
            //}.WithCache();

            ////var httpResponseMessage = await _client.GetAsync($"api/AuthToken?username={TestUserName}&password={TestPassword}");
            ////_authToken = await httpResponseMessage.Content.ReadAsJsonAsync<string>();
            ////httpResponseMessage.EnsureSuccessStatusCode();

            //var httpResponseMessage = client.Get(new IReturnVoid($"api/AuthToken?username={TestUserName}&password={TestPassword}"));
            ////_authToken = await httpResponseMessage.Content.ReadAsJsonAsync<string>();
            //// httpResponseMessage.EnsureSuccessStatusCode();

            hc.Dispose();
        }

        private async Task<string> GetInfo(HttpClient hc)
        {
            string product = string.Empty;
            HttpResponseMessage response = await hc.GetAsync($"api/AuthToken?username={TestUserName}&password={TestPassword}");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsStringAsync();
                    //response.Content.ReadAsStringAsync<string>();
            }
            return product;
        }
    }
}
