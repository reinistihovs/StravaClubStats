using MediatR;
using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Queries
{
    public record GetClubActivitiesSummariesSearchQuery(string searchText) : IRequest<List<ActivitiesSummary>>;

    public class GetClubActivitiesSummariesSearchQueryClass
    {
        public string SearchText { get; set; }

        public GetClubActivitiesSummariesSearchQueryClass(string searchText)
        {
            SearchText = searchText;
        }
    }
}
