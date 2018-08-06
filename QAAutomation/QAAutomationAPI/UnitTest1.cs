using System;
using System.Collections.Generic;
using NUnit.Framework;

using QA.Automation.Common;

namespace QA.Automation.QAAutomationAPI
{
    //[TestFixture("https://lg-lgm-clientprogramsservice-test.azurewebsites.net", "63", "Windows 10")]
    //[TestFixture("https://lg-lgm-clientsservice-test.azurewebsites.net", "63", "Windows 10")]
    //[TestFixture("https://lg-lgm-assetsservice-test.azurewebsites.net", "63", "Windows 10")]
    //[TestFixture("https://lg-lgm-programsservice-test.azurewebsites.net", "63", "Windows 10")]
    //[TestFixture("https://lg-lgm-programsversionservice-test.azurewebsites.net", "63", "Windows 10")]
    public class UnitTest1
    {
        //private static readonly Common.EnvironmentType _env = Common.EnvironmentType.Test;
        private static TestConfiguration _configuration => TestConfiguration.GetTestConfiguration();
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
            "LG.LGM.BuildManifestService"
        });

        //private const string _testUserName = "testrunner@dciartform.com";
        // private const string _testPassword = "LiveGuide#2727";

        private const string _testUserName = "cbam.lgtest1@dciartform.com";
        private const string _testPassword = "Cbam#test1";

        private string url = string.Empty;
        private string userName = string.Empty;
        private string password = string.Empty;

        public UnitTest1(string url, string user, string userPassword)
        {
            this.url = url;
            this.userName = _testUserName;
            this.password = _testPassword;
        }

        public UnitTest1()
        {
            //this.url = url;
            this.userName = _testUserName;
            this.password = _testPassword;
        }

        [SetUp]
        public void Init()
        {
        }

        [TestCaseSource("items")]
        [Category("SmokeTests")]

        //public void PingTokenApi()
        //{

        //}
        public void PingTokenApi(string url)
        {
            string updatedUrl = FormatUrl(url, _configuration.Environment.ToString());
            string result = string.Empty;

            if (!string.IsNullOrEmpty(updatedUrl))
            {
                //Common.WebUtils wu = new Common.WebUtils();
                result =  new Common.WebUtils().GetAuthInfo(updatedUrl, this.userName, this.password);
            }
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Console.WriteLine($"Url: {updatedUrl}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\r\n");
        }

        public static string GetPlayListByName(string playlistName)
        {
            return "Playlist";
        }

        private string FormatUrl(string url, string env)
        {
            return $"https://{url.Replace(".", "-")}" + Common.LGUtils.GetUrlBaseUrl(env.ToString(),"{0}") + ".azurewebsites.net";
        }
    }
}
