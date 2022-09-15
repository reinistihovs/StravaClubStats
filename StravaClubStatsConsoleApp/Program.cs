using StravaClubStatsShared.Models;
using StravaClubStatsEngine.Service;
using StravaClubStatsEngine.Service.API;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
                               .SetBasePath($"{Directory.GetCurrentDirectory()}/../../..")
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var config = builder.Build();

var stravaClubStatsEngineInput = new StravaClubStatsEngineInput()
{
    StravaClubAPIUrl = config["StravaClubAPIUrl"],
    APIToken = config["APIToken"],
    ClubID = Convert.ToInt32(config["ClubID"]),
    NumberOfPages = Convert.ToInt32(config["NumberOfPages"]),
};

var httpClient = new HttpClient();

httpClient.BaseAddress = new Uri(config["StravaClubAPIUrl"]);

var httpAPIClient = new HttpAPIClient(httpClient, stravaClubStatsEngineInput);

var stravaClubStatsService = new StravaClubStatsService(httpAPIClient, stravaClubStatsEngineInput);

var clubActivitiesSummaries = await stravaClubStatsService.GetClubActivitiesSummary();

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
