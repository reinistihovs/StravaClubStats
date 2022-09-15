using StravaClubStatsShared.Models;

namespace StravaClubStatsBlazorServerApp.Pages
{
    public partial class ClubActivitiesSummary
    {
        private List<ActvitiesSummary> clubActivitiesSummaries = null;

        private bool IsSortedAscending;

        private string CurrentSortColumn;

        private string TidyStravaMetric(decimal stravaMetric) => stravaMetric.ToString("0.##");

        protected override async Task OnInitializedAsync()
        {
            clubActivitiesSummaries = await StravaClubStatsService.GetClubActivitiesSummary();
        }

        private string GetSortStyle(string columnName)
        {
            if (CurrentSortColumn != columnName)
            {
                return string.Empty;
            }

            return IsSortedAscending ? "▲" : "▼";
        }

        private void SortTable(string columnName)
        {
            if (columnName != CurrentSortColumn)
            {
                clubActivitiesSummaries = clubActivitiesSummaries
                                            .OrderBy(x =>
                                                x.GetType()
                                                .GetProperty(columnName)
                                                .GetValue(x, null))
                                            .ToList();

                CurrentSortColumn = columnName;
                IsSortedAscending = true;

            }
            else
            {
                if (IsSortedAscending)
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

                IsSortedAscending = !IsSortedAscending;
            }
        }
    }
}
