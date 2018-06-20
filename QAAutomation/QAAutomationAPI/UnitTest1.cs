using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace QA.Automation.QAAutomationAPI
{
    public class UnitTest1
    {
        private static readonly Common.EnvironmentType _env = Common.EnvironmentType.Test;
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
            "LG.LGM.DbService"
        });

        private const string _testUserName = "testrunner@dciartform.com";
        private const string _testPassword = "LiveGuide#2727";

        private readonly string url = string.Empty;
        private readonly string userName = string.Empty;
        private readonly string password = string.Empty;

        public UnitTest1(string userUrl, string userName, string userPassword)
        {
            this.url = userUrl;
            this.userName = userName;
            this.password = userPassword;
        }

        public UnitTest1()
        {
            this.userName = _testUserName;
            this.password = _testPassword;
        }

        [SetUp]
        public void Init()
        {
        }

        [TestCaseSource("items")]
        [Category("SmokeTests")]

        public void PingTokenApi(string url)
        {
            string updatedUrl = FormatUrl(url);
            string result = string.Empty;

            if (!string.IsNullOrEmpty(updatedUrl))
            {
                result =  new Common.WebUtils().GetAuthInfo(updatedUrl, this.userName, this.password);
            }
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Console.WriteLine($"Url: {updatedUrl}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\r\n");
        }

        private string FormatUrl(string url)
        {
            return $"https://{url.Replace(".", "-")}" + Common.LGUtils.GetUrlBaseUrl(_env.ToString(),"{0}") + ".azurewebsites.net";
        }
    }
}
