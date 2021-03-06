
using System.Net.Http;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace QA.Automation.LGRegressionTests
{
    public class UnitTest1
    {
        private const string TestUserName = "testrunner@dciartform.com";
        private const string TestPassword = "LiveGuide#2727";
        private const string TestUrl = "https://lg-qa2-appservice-deploytest.azurewebsites.net";

        public void TestMethod1Async()
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.BaseAddress = new System.Uri(TestUrl);

                string item = GetInfo(hc).GetAwaiter().GetResult();
                Assert.IsTrue(string.IsNullOrWhiteSpace(item));
            }
        }

        private async Task<string> GetInfo(HttpClient hc)
        {
            string product = string.Empty;
            HttpResponseMessage response = await hc.GetAsync(HttpUtility.HtmlEncode($"api/AuthToken?username={TestUserName}&password={TestPassword}"));
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsStringAsync();
            }
            return product;
        }
    }
}
