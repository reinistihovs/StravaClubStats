using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Service.Interface
{
    public interface IStravaClubStatsService
    {
        Task<List<ActvitiesSummary>> GetClubActivitiesSummary();
    }
}
