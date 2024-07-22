using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Service.Interface;

public interface IStravaClubStatsForYearService
{
    Task<List<StravaClubStatsForYear>> GetStravaClubStatsForYearAsync();
}
