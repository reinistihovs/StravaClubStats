using StravaClubStatsBlazorServerApp.Helpers;
using StravaClubStatsEngine.Queries;
using StravaClubStatsShared.Models;

namespace StravaClubStatsBlazorServerApp.Pages.ClubActivities
{
    public partial class ClubActivitiesDrilldown
    {
        private List<Activity> clubActivities = null;

        private TableSortHelper<Activity> tableSortHelper;

        private bool isInvalidClubActivities = false;

        private string errorMessage { get; set; }

        private string searchText;

        protected override async Task OnInitializedAsync()
        {
            await GetAllClubActivities();
        }

        private async Task SearchAsync()
        {
            clubActivities = await Mediator.Send(new GetClubActivitiesSearchQuery(searchText));
            tableSortHelper = new TableSortHelper<Activity>(clubActivities);
        }

        private async Task ClearAsync()
        {
            await GetAllClubActivities();
            searchText = string.Empty;
        }

        private async Task<IEnumerable<ActivitiesSummary>> SearchForAutoCompleteAsync(string searchForAutoComplete)
        {
            searchText = searchForAutoComplete;

            if (string.IsNullOrEmpty(searchText))
            {
                return await Mediator.Send(new GetClubActivitiesSummariesQuery());
            }

            return await Mediator.Send(new GetClubActivitiesSummariesSearchQuery(searchText));
        }

        private async Task GetAllClubActivities()
        {
            try
            {
                clubActivities = await Mediator.Send(new GetClubActivitiesQuery());

                tableSortHelper = new TableSortHelper<Activity>(clubActivities);
            }
            catch (Exception ex)
            {
                isInvalidClubActivities = true;
                errorMessage = $"Could not retrieve the club activities";
            }
        }

        private void SortTable(string columnName)
        {
            tableSortHelper.SortTable(columnName);

            clubActivities = tableSortHelper.ListToSort;
        }
    }
}
