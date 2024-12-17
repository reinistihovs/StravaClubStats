using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using StravaClubStatsEngine;
using StravaClubStatsEngine.Service;
using StravaClubStatsEngine.Service.CosmosDb;
using StravaClubStatsEngine.Service.CosmosDb.Interface;
using StravaClubStatsEngine.Service.Interface;
using StravaClubStatsShared.Models;
using System.Reflection;

namespace StravaClubsStatsMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("StravaClubsStatsMauiApp.appsettings.json");

        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        Int32.TryParse(config["ClientID"], out int clientID);

        Int32.TryParse(config["ClubID"], out int clubID);

        Int32.TryParse(config["NumberOfPages"], out int numberOfPages);

        var stravaClubStatsEngineInput = new StravaClubStatsEngineInput()
        {
            StravaClubAPIUrl = config["StravaClubAPIUrl"],
            ClientID = clientID,
            ClientSecret = config["ClientSecret"],
            RefreshToken = config["RefreshToken"],
            ClubID = clubID,
            NumberOfPages = numberOfPages,
            CosmosDbEndointUrl = config["CosmosDbEndpointUrl"],
            CosmosDbPrimaryKey = config["CosmosDbPrimaryKey"],
            CosmosDatabase = config["CosmosDatabase"],
            CosmosPartitionKey = config["CosmosPartitionKey"],
        };

        builder.Services.AddSingleton(stravaClubStatsEngineInput);
        builder.Services.AddSingleton<IStravaClubStatsService, StravaClubStatsService>();
        builder.Services.AddSingleton<ICosmosDbConnection, CosmosDbConnection>();
        builder.Services.AddSingleton<IStravaClubStatsForYearService, StravaClubStatsForYearService>();

        builder.Services.AddMudServices();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StravaClubStatsEngineMediatREntryPoint).Assembly));

        return builder.Build();
    }
}
