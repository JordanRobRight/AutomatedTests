using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace APITests
{
    //[TestFixture("https://lg-lgm-clientprogramsservice-test.azurewebsites.net", "63", "Windows 10")]
    //[TestFixture("https://lg-lgm-clientsservice-test.azurewebsites.net", "63", "Windows 10")]
    //[TestFixture("https://lg-lgm-assetsservice-test.azurewebsites.net", "63", "Windows 10")]
    //[TestFixture("https://lg-lgm-programsservice-test.azurewebsites.net", "63", "Windows 10")]
    //[TestFixture("https://lg-lgm-programsversionservice-test.azurewebsites.net", "63", "Windows 10")]
    public class UnitTest1
    {
        private static QA.Automation.Common.EnvironmentType _env = QA.Automation.Common.EnvironmentType.Test;
        public static List<string> items = new List<string>(new string[]
        {
            "LG.LGM.PlayersService",
            //"LG.LGM.FiltersService",
            //"LG.LGM.LocationsService",
            //"LG.LGM.ScreenFeedVideoService",
            //"LG.LGM.SSOAuthService",
            //"LG.LGM.ProgramVersionsService",
            //"LG.LGM.AssetsService",
            //"LG.LGM.SSOService",
            //"LG.LGM.PlaylistsService",
            //"LG.LGM.WeatherService",
            //"LG.LGM.WidgetsService",
            //"LG.LGM.UsersService",
            //"LG.LGM.TriviaService",
            //"LG.LGM.TrafficService",
            //"LG.LGM.SubscriptionsService",
            //"LG.LGM.StorageService",
            //"LG.LGM.SocialService",
            //"LG.LGM.ClientsService",
            //"LG.LGM.HealthService",
            //"LG.LGM.LicensesService",
            //"LG.LGM.ChannelsService",
            //"LG.LGM.ClientProgramsService",
            //"LG.LGM.ProgramsService",
            //"LG.LGM.FrontEndService",
            //"LG.LGM.FinanceService",
            //"LG.LGM.DbService"
        });

        private const string _testUserName = "cbam.lgtest1@dciartform.com"; //"testrunner@dciartform.com";
        private const string _testPassword = "Cbam#test1"; // "LiveGuide#2727";
        private static readonly TestConfiguration _config = TestConfiguration.GetTestConfiguration();

        private string url = string.Empty;
        private readonly string userName = string.Empty;
        private readonly string password = string.Empty;

        public UnitTest1(string url, string user, string userPassword)
        {
            this.url = url;
            this.userName = _testUserName;
            this.password = _testPassword;
        }

        public UnitTest1()
        {
            userName = _config.UserName;
            password = _config.Password;
            _env = _config.Environment;

            //this.url = url;
            //this.userName = _testUserName;
            //this.password = _testPassword;
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
            string updatedUrl = FormatUrl(url);
            string result = string.Empty;

            if (!string.IsNullOrEmpty(updatedUrl))
            {
                //Common.WebUtils wu = new Common.WebUtils();
                result =  new QA.Automation.Common.WebUtils().GetAuthInfo(updatedUrl, this.userName, this.password);
            }
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Console.WriteLine($"Url: {updatedUrl}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\r\n");
        }

        private string FormatUrl(string url)
        {
            return $"https://{url.Replace(".", "-")}" + QA.Automation.Common.LGUtils.GetUrlBaseUrl(_env.ToString(),"{0}") + ".azurewebsites.net";
        }
    }
}
