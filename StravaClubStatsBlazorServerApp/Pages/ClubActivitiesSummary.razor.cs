using StravaClubStatsShared.Models;

namespace StravaClubStatsBlazorServerApp.Pages
{
    public partial class ClubActivitiesSummary
    {
        private List<ActvitiesSummary> clubActivitiesSummaries = null;

        private bool isSortedAscending;

        private string currentSortColumn;

        private bool isInvalidClubActivities = false;

        private string errorMessage { get; set; }

        private string TidyStravaMetric(decimal stravaMetric) => stravaMetric.ToString("0.##");

        protected override async Task OnInitializedAsync()
        {
            try
            {
                clubActivitiesSummaries = await StravaClubStatsService.GetClubActivitiesSummary();
            }
            catch (Exception ex)
            {
                isInvalidClubActivities = true;
                errorMessage = $"Could not retrieve the club activities";
            }
        }

        private string GetSortStyle(string columnName)
        {
            if (currentSortColumn != columnName)
            {
                return string.Empty;
            }

            return isSortedAscending ? "▲" : "▼";
        }

        private void SortTable(string columnName)
        {
            if (columnName != currentSortColumn)
            {
                clubActivitiesSummaries = clubActivitiesSummaries
                                            .OrderBy(x =>
                                                x.GetType()
                                                .GetProperty(columnName)
                                                .GetValue(x, null))
                                            .ToList();

                currentSortColumn = columnName;
                isSortedAscending = true;

            }
            else
            {
                if (isSortedAscending)
                {
                    clubActivitiesSummaries = clubActivitiesSummaries
                                                        .OrderByDescending(x =>
                                                                      x.GetType()
                                                                       .GetProperty(columnName)
                                                                       .GetValue(x, null))
                                                        .ToList();
                }
                else
                {
                    clubActivitiesSummaries = clubActivitiesSummaries
                                                .OrderBy(x =>
                                                    x.GetType()
                                                     .GetProperty(columnName)
                                                     .GetValue(x, null))
                                                .ToList();
                }

                isSortedAscending = !isSortedAscending;
            }
        }
    }
}
