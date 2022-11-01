using StravaClubStatsShared.Models;
using StravaClubStatsEngine.Service;
using StravaClubStatsEngine.Service.API;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
                               .SetBasePath($"{Directory.GetCurrentDirectory()}/../../..")
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var config = builder.Build();

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
};

var httpClient = new HttpClient();

httpClient.BaseAddress = new Uri(config["StravaClubAPIUrl"]);

var httpAPIClient = new HttpAPIClient(httpClient, stravaClubStatsEngineInput);

var stravaClubStatsService = new StravaClubStatsService(httpAPIClient, stravaClubStatsEngineInput);

var clubActivitiesSummaries = await stravaClubStatsService.GetClubActivitiesSummary();

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
