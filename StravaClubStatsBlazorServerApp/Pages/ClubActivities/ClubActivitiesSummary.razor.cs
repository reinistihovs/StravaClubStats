using StravaClubStatsBlazorServerApp.Helpers;
using StravaClubStatsEngine.Queries;
using StravaClubStatsShared.Models;

namespace StravaClubStatsBlazorServerApp.Pages.ClubActivities
{
    public partial class ClubActivitiesSummary
    {
        private List<ActivitiesSummary> clubActivitiesSummaries = null;

        public TableSortHelper<ActivitiesSummary> tableSortHelper;

        private bool isInvalidClubActivities = false;

        private string errorMessage { get; set; }

        private string searchText;

        protected override async Task OnInitializedAsync()
        {
            await GetAllClubActivitiesSummaries();
        }

        private async Task SearchAsync()
        {
            clubActivitiesSummaries = await Mediator.Send(new GetClubActivitiesSummariesSearchQuery(searchText));
            tableSortHelper = new TableSortHelper<ActivitiesSummary>(clubActivitiesSummaries);
        }

        private async Task ClearAsync()
        {
            clubActivitiesSummaries = await Mediator.Send(new GetClubActivitiesSummariesQuery());
            tableSortHelper = new TableSortHelper<ActivitiesSummary>(clubActivitiesSummaries);
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

        private async Task GetAllClubActivitiesSummaries()
        {
            try
            {
                clubActivitiesSummaries = await Mediator.Send(new GetClubActivitiesSummariesQuery());

                tableSortHelper = new TableSortHelper<ActivitiesSummary>(clubActivitiesSummaries);
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

            clubActivitiesSummaries = tableSortHelper.ListToSort;
        }
    }
}
