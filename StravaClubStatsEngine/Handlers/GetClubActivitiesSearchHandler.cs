using MediatR;
using StravaClubStatsEngine.Queries;
using StravaClubStatsEngine.Service.Interface;
using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Handlers
{
    public class GetClubActivitiesSearchHandler : BaseHandler, IRequestHandler<GetClubActivitiesSearchQuery, List<Activity>>
    {
        private readonly IStravaClubStatsService _stravaClubStatsService = null;

        public GetClubActivitiesSearchHandler(IStravaClubStatsService stravaClubStatsService)
        {
            _stravaClubStatsService = stravaClubStatsService;
        }

        public async Task<List<Activity>> Handle(GetClubActivitiesSearchQuery request, CancellationToken token)
        {
            var clubActivities = await _stravaClubStatsService.GetClubActivitiesAsync();

            if(clubActivities == null)
            {
                return null;
            }

            return clubActivities
                        .Where(x => x.AthleteFirstName.ToLower().Contains(GetStringForCompare(request.searchText, x.AthleteFirstName))
                            || x.AthleteLastName.ToLower().Contains(GetStringForCompare(request.searchText, x.AthleteLastName))
                            )
                        .ToList();
        }
    }
}
