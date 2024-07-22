using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Service.Interface;

public interface IStravaClubStatsService
{
    Task<List<ActivitiesSummary>> GetClubActivitiesSummariesAsync();
    Task<List<Activity>> GetClubActivitiesAsync();
}
