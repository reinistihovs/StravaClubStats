using MediatR;
using StravaClubStatsEngine.Queries;
using StravaClubStatsEngine.Service.Interface;
using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Handlers
{
    public class GetClubActivitiesSummariesSearchHandler : BaseHandler, IRequestHandler<GetClubActivitiesSummariesSearchQuery, List<ActivitiesSummary>>
    {
        private readonly IStravaClubStatsService _stravaClubStatsService = null;

        public GetClubActivitiesSummariesSearchHandler(IStravaClubStatsService stravaClubStatsService)
        {
            _stravaClubStatsService = stravaClubStatsService;
        }

        public async Task<List<ActivitiesSummary>> Handle(GetClubActivitiesSummariesSearchQuery request, CancellationToken token)
        {
            var clubActivitiesSummaries = await _stravaClubStatsService.GetClubActivitiesSummariesAsync();

            if(clubActivitiesSummaries == null)
            {
                return null;
            }

            return clubActivitiesSummaries
                        .Where(x => x.AthleteFirstName.ToLower().Contains(GetStringForCompare(request.searchText, x.AthleteFirstName))
                            || x.AthleteLastName.ToLower().Contains(GetStringForCompare(request.searchText, x.AthleteLastName))
                            )
                        .ToList();
        }
    }
}
