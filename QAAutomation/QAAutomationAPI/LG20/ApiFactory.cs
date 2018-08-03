using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using QA.Automation.APITests.LG20.Services;
using QA.Automation.APITests.Models;
using QA.Automation.Common;

namespace QA.Automation.APITests.LG20
{
    public class APIFactory
    {
        private static Dictionary<LGMServiceType, Func<Models.APRIConfigSettings, IApiPage>> apiPage = new Dictionary<LGMServiceType, Func<Models.APRIConfigSettings, IApiPage>>
        {
            {LGMServiceType.AssetsService, a => new LGMAssetsService(a)},
            {LGMServiceType.PlayersService, a => new LGMPlayersService(a)},
            {LGMServiceType.FiltersService, a => new LGMFiltersService(a)},
            {LGMServiceType.LocationsService, a => new LGMLocationsService(a)},
            {LGMServiceType.ScreenFeedVideoService, a => new LGMScreenFeedVideoService(a)},
            {LGMServiceType.SSOAuthService, a => new LGMSSOAuthService(a)},
            {LGMServiceType.ProgramVersionsService, a => new LGMProgramVersionsService(a)},
            {LGMServiceType.SSOService, a => new LGMSSOService(a)},
            {LGMServiceType.PlaylistsService, a => new LGMPlaylistsService(a)},
            {LGMServiceType.WeatherService, a => new LGMWeatherService(a)},
            {LGMServiceType.WidgetsService, a => new LGMWidgetsService(a)},
            {LGMServiceType.UsersService, a => new LGMUsersService(a)},
            {LGMServiceType.TriviaService, a => new LGMTriviaService(a)},
            {LGMServiceType.TrafficService, a => new LGMTrafficService(a)},
            {LGMServiceType.SubscriptionsService, a => new LGMSubscriptionsService(a)},
            {LGMServiceType.StorageService, a => new LGMStorageService(a)},
            {LGMServiceType.SocialService, a => new LGMSocialService(a)},
            {LGMServiceType.ClientsService, a => new LGMClientsService(a)},
            {LGMServiceType.HealthService, a => new LGMHealthService(a)},
            {LGMServiceType.LicensesService, a => new LGMLicensesService(a)},
            {LGMServiceType.ChannelsService, a => new LGMChannelsService(a)},
            {LGMServiceType.ClientProgramsService, a => new LGMClientProgramsService(a)},
            {LGMServiceType.ProgramsService, a => new LGMProgramsService(a)},
            {LGMServiceType.FrontEndService, a => new LGMFrontEndService(a)},
            {LGMServiceType.FinanceService, a => new LGMFinanceService(a)},
            {LGMServiceType.DbService, a => new LGMDbService(a)},
            {LGMServiceType.AmenitiesService, a => new LGMAmenitiesService(a)},
        };

        private static IApiPage GetPage(LGMServiceType service, string userName, string passWord)
        {
           // Common.HttpUtilsHelper htppHelper = new HttpUtilsHelper();
            Models.APRIConfigSettings config = new APRIConfigSettings
            {
                UserName = userName,
                Password = passWord
            };
            IApiPage page = null;

            switch (service)
            {
                  case LGMServiceType.AssetsService:
                      page = new LGMAssetsService(config);
                      break;
               
            }

            return page;
        }

        public static IApiPage ApiFactory(LGMServiceType service, string userUser, string password)
        {
            Models.APRIConfigSettings config = new APRIConfigSettings
            {
                UserName = userUser,
                Password = password
            };

            //IApiPage page = null;

            Func<APRIConfigSettings, IApiPage> page;
            
            apiPage.TryGetValue(service, out page);

            IApiPage returnPage = page(config);

            //IApiPage page = GetPage(service, userUser, password);

            return returnPage;
        }
    }
}
