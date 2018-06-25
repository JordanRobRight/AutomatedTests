using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20
{
    [TestFixture]
    public class UnitTest1 : APRITestBase
    {
        public static List<string> items = new List<string>(new string[]
        {
            "LG.LGM.PlayersService",
            "LG.LGM.FiltersService",
            "LG.LGM.LocationsService",
            "LG.LGM.ScreenFeedVideoService",
            "LG.LGM.SSOAuthService",
            "LG.LGM.ProgramVersionsService",
            "LG.LGM.AssetsService",
            "LG.LGM.SSOService",
            "LG.LGM.PlaylistsService",
            "LG.LGM.WeatherService",
            "LG.LGM.WidgetsService",
            "LG.LGM.UsersService",
            "LG.LGM.TriviaService",
            "LG.LGM.TrafficService",
            "LG.LGM.SubscriptionsService",
            "LG.LGM.StorageService",
            "LG.LGM.SocialService",
            "LG.LGM.ClientsService",
            "LG.LGM.HealthService",
            "LG.LGM.LicensesService",
            "LG.LGM.ChannelsService",
            "LG.LGM.ClientProgramsService",
            "LG.LGM.ProgramsService",
            "LG.LGM.FrontEndService",
            "LG.LGM.FinanceService",
            "LG.LGM.DbService",

           // "LG.LGM.ProfileService"
        });

        private readonly string url = string.Empty;

        public UnitTest1(string userUrl, string userName, string userPassword) : base(userName, userPassword)
        {
            this.url = userUrl;
        }

        public UnitTest1()
        {
        }

        [SetUp]
        public void Init()
        {
            //if (TestContext.Parameters["AuthKeys"] != null)
            //{
            //    TestContext.Parameters["AuthKeys"] = AuthTokens;
            //}
        }

        [TestCaseSource("items")]
        [Category("SmokeTests")]
        public void GetAuthToken(string url)
        {
            string updatedUrl = FormatUrl(url);
            string result = string.Empty;

            if (!string.IsNullOrEmpty(updatedUrl))
            {
                result = LGApitAction.GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
                AuthTokens.Add(url, result.Trim('"'));
            }

            //NUnit.Framework.Internal.TestExecutionContext t = PropertyHelper.GetPrivateFieldValue<NUnit.Framework.Internal.TestExecutionContext>(TestContext.CurrentContext, "_testExecuteContext");

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Console.WriteLine($"Url: {updatedUrl}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\r\n");
        }

        [TestCaseSource("items")]
        [Category("SmokeTests")]
        public void GetZAllDocuments(string url)
        {
            //var previous = AuthTokens;

            //AuthTokens = GetTestToken();

            var authKey = AuthTokens.FirstOrDefault(a => a.Key.Contains(url));
            Dictionary<string, string> parms = new Dictionary<string, string>()
            {
                { "url", FormatUrl(authKey.Key) },
                { "authtoken", authKey.Value },
            };

            string result = LGApitAction.GetDocumentInfo(parms);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TearDown]
        public void WriteOut()
        {
            //AuthTokens.Select(a => 
            //    {
            //        Console.WriteLine($"Url: {a.Key.ToString()} | AuthKey: {a.Value.ToString()} ");
            //        return true;
            //    });

        }   

        private Dictionary<string, string> GetTestToken()
        {
            return new Dictionary<string, string> {
                 {"LG.LGM.FiltersService", "9R_4I_9-RWoVsT2gG1T0RwREIh9FHcJH6_AZI6xhWK8IgZEksu_kSOaloGMltYNpEmtD8I0cYp0uqXdDaMyfP8MX5SbnkKIt3Z5I24KnPdSobRmxPEH94y8dR_OjlVQ2m5C8dKeXa9pq0kKy3YtrMf1c-MWLZp2IpMMBIsArJBoWVTjgQO7pOs8gbdkt9Jj8WRvq5z2QrypXfcxFacLr88pc2SAICmoPZMj2s43ensl76t4LxU0kIUw2RqRnvepH8ZaBcgYfJuigfYza5iCkz0VtMyJDKqv7pL3KXdCFuCHs0S_zs0QeXdh2oIzvSueKv-nVHnUbcPcJhpf_HlwhQvHxW0L1VDiE_VfxwB4crBZTQrpPjsN8wZMshBi_sHa5TI6Hu9KZZ_orc6G0bHjis3kyDpRgRcU-k7nunAip6gloywm2xR3Ji-fST9LGvctCTz6QsXAaUs7Abihpp31mUQ4qZdNCkwY7vB_T4PtAMo3zBZAHbKxQJ28_04z7pHktzt3SjdiaARddNJTMaoTRuf6PDd8TdriiwcKgPmWJY_81UExMw51YggE8uNjmMXcPHuKZVGqZ-xkZylvEILyCvgwtNm_p0wbXUZ31ybc1fuA" }

                 };
   
        }

        private string FormatUrl(string url)
        {
            return $"https://{url.Replace(".", "-")}" + Common.LGUtils.GetUrlBaseUrl(Settings.Environment.ToString(),"{0}") + ".azurewebsites.net";
        }

    }
    
}
