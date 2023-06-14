using MediatR;
using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Queries
{
    public record GetClubActivitiesSearchQuery(string searchText) : IRequest<List<Activity>>;

    public class GetClubActivitiesSearchQueryClass
    {
        public string SearchText { get; set; }

        public GetClubActivitiesSearchQueryClass(string searchText)
        {
            SearchText = searchText;
        }
    }
}
