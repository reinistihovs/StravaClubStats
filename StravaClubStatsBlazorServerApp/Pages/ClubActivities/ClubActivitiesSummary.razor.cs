using StravaClubStatsBlazorServerApp.Helpers;
using StravaClubStatsShared.Models;

namespace StravaClubStatsBlazorServerApp.Pages.ClubActivities
{
    public partial class ClubActivitiesSummary
    {
        private List<ActivitiesSummary> clubActivitiesSummaries = null;

        private TableSortHelper<ActivitiesSummary> tableSortHelper;

        private bool isInvalidClubActivities = false;

        private string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                clubActivitiesSummaries = await StravaClubStatsService.GetClubActivitiesSummariesAsync();

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
