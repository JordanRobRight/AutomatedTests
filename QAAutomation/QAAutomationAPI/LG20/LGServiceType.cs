using System;
using System.Collections.Generic;
using System.Text;

namespace QA.Automation.APITests.LG20
{
    public enum LGMServiceType
    {
        PlayersService,
        FiltersService,
        LocationsService,
        ScreenFeedVideoService,
        SSOAuthService,
        ProgramVersionsService,
        AssetsService,
        SSOService,
        PlaylistsService,
        WeatherService,
        WidgetsService,
        UsersService,
        TriviaService,
        TrafficService,
        SubscriptionsService,
        StorageService,
        SocialService,
        ClientsService,
        HealthService,
        LicensesService,
        ChannelsService,
        ClientProgramsService,
        ProgramsService,
        FrontEndService,
        FinanceService,
        DbService,
        AmenitiesService,
    }
}

/*

        {LGMServiceType.PlayersService, a => new LGMPlayersService(a)},
        {LGMServiceType.FiltersService, a => new LGMFiltersService(a)},
        {LGMServiceType.LocationsService, a => new LGMLocationsService(a)},
        {LGMServiceType.ScreenFeedVideoService, a => new LGMScreenFeedVideoService(a)},
        {LGMServiceType.SSOAuthServic, a => new LGMSSOAuthService(a)},
        {LGMServiceType.ProgramVersionsService, a => new LGMProgramVersionsService(a)},
        {LGMServiceType.AssetsService, a => new LGMAssetsService(a)},
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
 
        {LGMServiceType.PlayersService      ,, a => new 
        {LGMServiceType.FiltersService  ,, a => new 
        {LGMServiceType.LocationsService,, a => new 
        {LGMServiceType.ScreenFeedVideoService,, a => new 
        {LGMServiceType.SSOAuthService,, a => new 
        {LGMServiceType.ProgramVersionsService,, a => new 
        {LGMServiceType.AssetsService,, a => new 
        {LGMServiceType.SSOService,, a => new 
        {LGMServiceType.PlaylistsService,, a => new 
        {LGMServiceType.WeatherService,, a => new 
        {LGMServiceType.WidgetsService,, a => new 
        {LGMServiceType.UsersService,, a => new 
        {LGMServiceType.TriviaService,, a => new 
        {LGMServiceType.TrafficService,, a => new 
        {LGMServiceType.SubscriptionsService,, a => new 
        {LGMServiceType.StorageService,, a => new 
        {LGMServiceType.SocialService,, a => new 
        {LGMServiceType.ClientsService,, a => new 
        {LGMServiceType.HealthService,, a => new 
        {LGMServiceType.LicensesService,, a => new 
        {LGMServiceType.ChannelsService,, a => new 
        {LGMServiceType.ClientProgramsService,, a => new 
        {LGMServiceType.ProgramsService,, a => new 
        {LGMServiceType.FrontEndService,, a => new 
        {LGMServiceType.FinanceService,, a => new 
        {LGMServiceType.DbService,, a => new 
        {LGMServiceType.AmenitiesService,, a => new 
        
    */