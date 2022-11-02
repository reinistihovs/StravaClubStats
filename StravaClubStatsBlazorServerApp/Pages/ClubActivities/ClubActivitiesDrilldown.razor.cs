using StravaClubStatsBlazorServerApp.Helpers;
using StravaClubStatsShared.Models;

namespace StravaClubStatsBlazorServerApp.Pages.ClubActivities
{
    public partial class ClubActivitiesDrilldown
    {
        private List<Activity> clubActivities = null;

        private TableSortHelper<Activity> tableSortHelper;

        private bool isInvalidClubActivities = false;

        private string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                clubActivities = await StravaClubStatsService.GetClubActivitiesAsync();

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
