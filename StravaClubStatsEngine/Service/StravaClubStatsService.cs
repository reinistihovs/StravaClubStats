using Newtonsoft.Json;
using StravaClubStatsShared.Models.FromAPI;
using StravaClubStatsEngine.Service.API.Interface;
using StravaClubStatsEngine.Service.Interface;
using StravaClubStatsShared.Models;

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
            var clubActvities = new List<Activity>();

            for (int i = 0; i < _stravaClubStatsEngineInput.NumberOfPages; i++)
            {
                var stravaClubActivitiesFromAPI = await GetStravaClubActivitiesFromAPIAsync(i + 1);

                if ((stravaClubActivitiesFromAPI?.Count ?? 0) == 0)
                {
                    break;
                }

                var clubActvitiesPage = ConvertFromClubActvitiesFromAPIToClubActvities(stravaClubActivitiesFromAPI);

                clubActvities = clubActvities.Union(clubActvitiesPage).ToList();
            }

            var clubActvitiesSummary = GetActivitiesSummary(clubActvities);

            return clubActvitiesSummary;
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

        private async Task<List<StravaClubActivities>> GetStravaClubActivitiesFromAPIAsync(int pageNumber)
        {
            string json = await _httpAPIClient.GetAsync($"clubs/{_stravaClubStatsEngineInput.ClubID}/activities?page={pageNumber}&per_page=200&access_token={_stravaClubStatsEngineInput.APIToken}");
            return JsonConvert.DeserializeObject<List<StravaClubActivities>>(json);
        }
    }
}