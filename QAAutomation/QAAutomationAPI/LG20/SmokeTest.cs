using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using QA.Automation.APITests.LG20.Services;
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20
{
    //[TestFixture(Category ="TheSmokeTest")]
    [TestFixture]
    public class SmokeTest : APITestBase
    {
        public static Dictionary<LGMServiceType, string> items = new Dictionary<LGMServiceType, string>
          {
                {LGMServiceType.PlayersService, "" },
                {LGMServiceType.FiltersService,"" },
                {LGMServiceType.LocationsService,"" },
                {LGMServiceType.ScreenFeedVideoService,"" },
                //{LGMServiceType.SSOAuthService,"" },
                {LGMServiceType.ProgramVersionsService,"" },
                {LGMServiceType.AssetsService,"" },
                //{LGMServiceType.SOService,"" },
                {LGMServiceType.PlaylistsService,"" },
                {LGMServiceType.WeatherService,"" },
                {LGMServiceType.WidgetsService,"" },
                {LGMServiceType.UsersService,"" },
                //{LGMServiceType.TriviaService,"" },
                {LGMServiceType.TrafficService,"" },
                {LGMServiceType.SubscriptionsService,"" },
                {LGMServiceType.StorageService,"" },
                //{LGMServiceType.SocialService,"" },
                {LGMServiceType.ClientsService,"" },
                {LGMServiceType.HealthService,"" },
                {LGMServiceType.LicensesService,"" },
                {LGMServiceType.ChannelsService,"" },
                {LGMServiceType.ClientProgramsService,"" },
                {LGMServiceType.ProgramsService,"" },
                {LGMServiceType.FrontEndService,"" },
                {LGMServiceType.FinanceService,"" },
                {LGMServiceType.DbService,"" },
                {LGMServiceType.AmenitiesService,"" },

          // "LG.LGM.ProfileService"
      };
        private readonly string _url = string.Empty;

        public SmokeTest(string userUrl, string userName, string userPassword) : base(userName, userPassword)
        {
            _url = userUrl;
        }

        public SmokeTest()
        {
        }

        [SetUp]
        public void Init()
        {
        }

        [TestCaseSource("items")]
        [NUnit.Framework.Category("SmokeTests")]
        //public void GetEnv(KeyValuePair<string, string> item)
        public void GetEnv(KeyValuePair<LGMServiceType,string> item)
        {
            var apiUrl = $@"{FormatUrl("LG-LGM-" + item.Key.ToString(), Settings)}/swagger/v1/swagger.json";
            var helper = new Common.HttpUtilsHelper();

            var data = helper.ApiRequest(apiUrl,string.Empty);

            var m =  Common.JsonHelper.GetMatchFromRegEx(Common.JsonHelper.GetTokenByPath(Common.JsonHelper.GetJsonJObjectFromString(data), "info.title"), @".*\((?<Test>\w+)\)");
            var envData = m != null && m.Success ? m.Groups["Test"].Value : string.Empty;

            Assert.AreEqual(envData.ToLower(), Settings.Environment.ToString().ToLower());

            /*
           // IApiPage i = new LGMFiltersService();

           if (!string.IsNullOrEmpty(updatedUrl))
           {
               Dictionary<string, string> parms = new Dictionary<string, string>()
               {
                   { "url", updatedUrl },
                   { "username", Settings.UserName },
                   { "password", Settings.Password },
               };
               result = LGApitAction.GetAuthInfo(parms);
               //result = LGApitAction.GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
               AuthTokens.Add(data.Key, result.Trim('"'));
           }

           //NUnit.Framework.Internal.TestExecutionContext t = PropertyHelper.GetPrivateFieldValue<NUnit.Framework.Internal.TestExecutionContext>(TestContext.CurrentContext, "_testExecuteContext");

           Assert.IsFalse(string.IsNullOrWhiteSpace(result));

           Console.WriteLine($"Url: {updatedUrl}");
           Console.WriteLine($"Result: {result}");
           Console.WriteLine("\r\n");
           */
        }

        [TestCaseSource("items")]
        [NUnit.Framework.Category("SmokeTests")]
        public void GetAuthTokenByPage(KeyValuePair<LGMServiceType,string> item)
        {
            //string updatedUrl = FormatUrl(item.Key);
            string result = string.Empty;
            string updatedUrl = string.Empty;

            using (IApiPage page = APIFactory.ApiFactory<IApiPage>(item.Key, Settings))
            {

                updatedUrl = FormatUrl(page.ServiceName, Settings);

                // IApiPage i = new LGMFiltersService();

                if (!string.IsNullOrEmpty(updatedUrl))
                {
                    //Dictionary<string, string> parms = new Dictionary<string, string>()
                    //{
                    //    { "url", updatedUrl },
                    //    { "username", Settings.UserName },
                    //    { "password", Settings.Password },
                    //};
                    result = page.GetAuthInfo(updatedUrl);

                    //result = LGApitAction.GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
                    //result = LGApitAction.GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
                    //AuthTokens.Add(item.Key.ToString(), result.Trim('"'));
                }
            }
            //NUnit.Framework.Internal.TestExecutionContext t = PropertyHelper.GetPrivateFieldValue<NUnit.Framework.Internal.TestExecutionContext>(TestContext.CurrentContext, "_testExecuteContext");

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Console.WriteLine($"Url: {updatedUrl}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\r\n");
        }

        /*
        [TestCaseSource("items")]
        [Category("SmokeTests")]
        public void GetAuthToken(KeyValuePair<string,string> item)
        {
            string updatedUrl = FormatUrl(item.Key);
            string result = string.Empty;

           // IApiPage i = new LGMFiltersService();

            if (!string.IsNullOrEmpty(updatedUrl))
            {
                //Dictionary<string, string> parms = new Dictionary<string, string>()
                //{
                //    { "url", updatedUrl },
                //    { "username", Settings.UserName },
                //    { "password", Settings.Password },
                //};
                result = LGApitAction.GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
                //result = LGApitAction.GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
                AuthTokens.Add(item.Key, result.Trim('"'));
            }

            //NUnit.Framework.Internal.TestExecutionContext t = PropertyHelper.GetPrivateFieldValue<NUnit.Framework.Internal.TestExecutionContext>(TestContext.CurrentContext, "_testExecuteContext");

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Console.WriteLine($"Url: {updatedUrl}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\r\n");
        }
        */
        /*
        [TestCaseSource("Items")]
        [NUnit.Framework.Category("SmokeTests")]
        [NUnit.Framework.Category("SmokeTests1")]
        public void GetZAllDocuments(KeyValuePair<string, string> item)
        {
            //var previous = AuthTokens;
            bool isdebug = false;
            //AuthTokens = GetTestToken();
            if (isdebug)
            {
                var authKey = AuthTokens.FirstOrDefault(a => a.Key.Contains(item.Key));
                Dictionary<string, string> parms = new Dictionary<string, string>()
                { 
                    { "url", FormatUrl(authKey.Key, Settings) },
                    { "authtoken", authKey.Value },
                };

                //string result = LGApitAction.GetAllDocuments(parms);
                //Assert.IsFalse(string.IsNullOrWhiteSpace(result));
            }
        }
        */

        //[TestCaseSource("items")]
        //[Category("SmokeTests")]
        //public void GetZAllDocuments(string url)
        //{
        //    //var previous = AuthTokens;

        //    //AuthTokens = GetTestToken();

        //    var authKey = AuthTokens.FirstOrDefault(a => a.Key.Contains(url));
        //    Dictionary<string, string> parms = new Dictionary<string, string>()
        //    {
        //        { "url", FormatUrl(authKey.Key) },
        //        { "authtoken", authKey.Value },
        //    };

        //    string result = LGApitAction.GetAllDocuments(parms);
        //    Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        //}

        [TearDown]
        public void WriteOut()
        {

        }

        public static string GetPlayListByName(string playlistName, string userName, string passWord, EnvironmentType env)
        {

            
            APRIConfigSettings config = new APRIConfigSettings
            {
                UserName = userName,
                Password = passWord,
                Environment =  env
            };

            string result = string.Empty;
            string updatedUrl = string.Empty;

            using (LGMPlaylistsService page = APIFactory.ApiFactory<LGMPlaylistsService>(LGMServiceType.PlaylistsService, config))
            {
                updatedUrl = FormatUrl(page.ServiceName, config);

                // IApiPage i = new LGMFiltersService();

                if (!string.IsNullOrEmpty(updatedUrl))
                {
                    //Dictionary<string, string> parms = new Dictionary<string, string>()
                    //{
                    //    { "url", updatedUrl },
                    //    { "username", Settings.UserName },
                    //    { "password", Settings.Password },
                    //};
                    result = ((LGMPlaylistsService) page).LGMPlaylistServiceByName(playlistName);

                    //result = LGApitAction.GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
                    //result = LGApitAction.GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
                    //AuthTokens.Add(item.Key.ToString(), result.Trim('"'));
                }
            }
            //NUnit.Framework.Internal.TestExecutionContext t = PropertyHelper.GetPrivateFieldValue<NUnit.Framework.Internal.TestExecutionContext>(TestContext.CurrentContext, "_testExecuteContext");

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Console.WriteLine($"Url: {updatedUrl}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\r\n");

            return "Playlist";
        }

        private Dictionary<string, string> GetTestToken()
        {
            return new Dictionary<string, string> {
                 {"LG.LGM.FiltersService", "9R_4I_9-RWoVsT2gG1T0RwREIh9FHcJH6_AZI6xhWK8IgZEksu_kSOaloGMltYNpEmtD8I0cYp0uqXdDaMyfP8MX5SbnkKIt3Z5I24KnPdSobRmxPEH94y8dR_OjlVQ2m5C8dKeXa9pq0kKy3YtrMf1c-MWLZp2IpMMBIsArJBoWVTjgQO7pOs8gbdkt9Jj8WRvq5z2QrypXfcxFacLr88pc2SAICmoPZMj2s43ensl76t4LxU0kIUw2RqRnvepH8ZaBcgYfJuigfYza5iCkz0VtMyJDKqv7pL3KXdCFuCHs0S_zs0QeXdh2oIzvSueKv-nVHnUbcPcJhpf_HlwhQvHxW0L1VDiE_VfxwB4crBZTQrpPjsN8wZMshBi_sHa5TI6Hu9KZZ_orc6G0bHjis3kyDpRgRcU-k7nunAip6gloywm2xR3Ji-fST9LGvctCTz6QsXAaUs7Abihpp31mUQ4qZdNCkwY7vB_T4PtAMo3zBZAHbKxQJ28_04z7pHktzt3SjdiaARddNJTMaoTRuf6PDd8TdriiwcKgPmWJY_81UExMw51YggE8uNjmMXcPHuKZVGqZ-xkZylvEILyCvgwtNm_p0wbXUZ31ybc1fuA" }

                 };
   
        }

        private static string FormatUrl(string url, APRIConfigSettings settings)
        {
            return $"https://{url.Replace(".", "-")}" + Common.LgUtils.GetUrlBaseUrl(settings.Environment.ToString(),"{0}") + ".azurewebsites.net";
        }

    }
    
}
