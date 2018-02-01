
using System.Net.Http;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LGRegressionTests
{
    [TestClass]
    public class UnitTest1
    {
        #region Old Stuff
        // private const string TestUserName = "testrunner%40dciartform.com";
        // private const string TestPassword = "LiveGuide%232727";
        #endregion

        private const string _testUserName = "testrunner@dciartform.com";
        private const string _testPassword = "LiveGuide#2727";
        private const string _testUrl = "https://lg-qa2-appservice-deploytest.azurewebsites.net";

        [TestMethod]
        public void TestMethod1Async()
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new System.Uri(_testUrl);

                string item = GetInfo(hc).GetAwaiter().GetResult();
            }
        }

        private async Task<string> GetInfo(HttpClient hc)
        {
            string product = string.Empty;
            HttpResponseMessage response = await hc.GetAsync(HttpUtility.HtmlEncode($"api/AuthToken?username={_testUserName}&password={_testPassword}"));
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsStringAsync();
                    //response.Content.ReadAsStringAsync<string>();
            }
            return product;
        }
    }
}
