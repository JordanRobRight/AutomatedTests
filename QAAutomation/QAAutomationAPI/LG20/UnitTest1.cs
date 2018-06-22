using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20
{
    public class UnitTest1 : APRITestBase
    {
        public IDictionary<string, string> AuthTokens = new Dictionary<string, string>();

        public static List<string> items = new List<string>(new string[]
        {
            "LG.LGM.PlayersService",
            "LG.LGM.FiltersService",
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
            //"LG.LGM.DbService",

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
            
        }

        [TestCaseSource("items")]
        [Category("SmokeTests")]
        public void GetAuthToken(string url)
        {
            string updatedUrl = FormatUrl(url);
            string result = string.Empty;

            if (!string.IsNullOrEmpty(updatedUrl))
            {
                result = new ApiActions().GetAuthInfo(updatedUrl, Settings.UserName, Settings.Password);
                AuthTokens.Add(url, result);
            }

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));

            Console.WriteLine($"Url: {updatedUrl}");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("\r\n");
        }

        [TestCase]
        public void GetAllDocuments()
        {

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

        private string FormatUrl(string url)
        {
            return $"https://{url.Replace(".", "-")}" + Common.LGUtils.GetUrlBaseUrl(Settings.Environment.ToString(),"{0}") + ".azurewebsites.net";
        }

    }
    
}
