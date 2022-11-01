using Newtonsoft.Json;
using StravaClubStatsShared.Models.FromAPI;
using StravaClubStatsEngine.Service.API.Interface;
using StravaClubStatsEngine.Service.Interface;
using StravaClubStatsShared.Models;
using System.Net.Http.Json;

namespace StravaClubStatsEngine.Service
{
    public class StravaClubStatsService : IStravaClubStatsService
    {
        private readonly IHttpAPIClient _httpAPIClient;
        private readonly StravaClubStatsEngineInput _stravaClubStatsEngineInput;

        public StravaClubStatsService(IHttpAPIClient httpAPIClient, StravaClubStatsEngineInput stravaClubStatsEngineInput)
        {
            _httpAPIClient = httpAPIClient;
            _stravaClubStatsEngineInput = stravaClubStatsEngineInput;
        }

        public async Task<List<ActvitiesSummary>> GetClubActivitiesSummary()
        {
            var refreshAPIToken = await GetRefreshAPIToken();

            var stravaClubActivitiesFromAPI = await GetStravaClubActivitiesFromAPIAsync(refreshAPIToken);

            var clubActvities = ConvertFromClubActvitiesFromAPIToClubActvities(stravaClubActivitiesFromAPI);

            return GetActivitiesSummary(clubActvities);
        }

        private List<Activity> ConvertFromClubActvitiesFromAPIToClubActvities(List<StravaClubActivities> stravaClubActivitiesFromAPI)
        {
            return stravaClubActivitiesFromAPI
                                .Select(x =>
                                            new Activity()
                                            {
                                                FirstName = x.athlete.firstname,
                                                LastName = x.athlete.lastname,
                                                DistanceInKilometers = ((decimal)x.distance) / 1000.00M,
                                                MovingTimeInHours = ((decimal)x.moving_time) / 60.00M / 60.00M,
                                                ElapsedTimeInHours = ((decimal)x.elapsed_time) / 60.00M / 60.00M,
                                                TotalElevationGainInKilometers = ((decimal)x.total_elevation_gain) / 1000.00M,
                                            })
                                    .ToList();
        }

        private List<ActvitiesSummary> GetActivitiesSummary(List<Activity> clubActvities)
        {
            return clubActvities
                        .GroupBy(x =>
                                    new
                                    {
                                        x.FirstName,
                                        x.LastName
                                    })
                        .Select(x => new ActvitiesSummary()
                        {
                            FirstName = x.Key.FirstName,
                            LastName = x.Key.LastName,
                            TotalDistanceInKilometers = x.Sum(x => x.DistanceInKilometers),
                            TotalMovingTimeInHours = x.Sum(x => x.MovingTimeInHours),
                            TotalElapsedTimeInHours = x.Sum(x => x.ElapsedTimeInHours),
                            TotalElevationGainInKilometers = x.Sum(x => x.TotalElevationGainInKilometers),
                        })
                        .ToList();
        }

        private async Task<RefreshAPIToken> GetRefreshAPIToken()
        {
            var response = await _httpAPIClient.PostAsync($"oauth/token?client_id={_stravaClubStatsEngineInput.ClientID}&client_secret={_stravaClubStatsEngineInput.ClientSecret}&grant_type=refresh_token&refresh_token={_stravaClubStatsEngineInput.RefreshToken}");
            return await response.Content.ReadFromJsonAsync<RefreshAPIToken>();
        }

        private async Task<List<StravaClubActivities>> GetStravaClubActivitiesFromAPIAsync(RefreshAPIToken refreshAPIToken)
        {
            string json = await _httpAPIClient.GetAsync($"clubs/{_stravaClubStatsEngineInput.ClubID}/activities?per_page=200&access_token={refreshAPIToken.access_token}");
            return JsonConvert.DeserializeObject<List<StravaClubActivities>>(json);
        }
    }
}