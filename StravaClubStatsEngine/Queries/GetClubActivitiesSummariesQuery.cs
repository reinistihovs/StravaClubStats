using MediatR;
using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Queries
{
    public record GetClubActivitiesSummariesQuery() : IRequest<List<ActivitiesSummary>>;
}
