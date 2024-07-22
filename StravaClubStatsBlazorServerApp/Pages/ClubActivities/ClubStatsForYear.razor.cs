using MediatR;
using StravaClubStatsEngine.Queries;
using StravaClubStatsShared.Models;

namespace StravaClubStatsBlazorServerApp.Pages.ClubActivities;

public partial class ClubStatsForYear
{
    private List<StravaClubStatsForYear> clubStatsForYear = null;

    private bool isInvalidClubStatsForYear = false;

    private string errorMessage { get; set; }

    private string searchText;

    private bool filterColumn(string columnName) =>
                            columnName.Contains(searchText, StringComparison.OrdinalIgnoreCase);

    private string ConvertTo2DecimalPlaces(decimal decimalValue) => decimalValue.ToString("0.##");

    private Func<StravaClubStatsForYear, bool> quickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return true;

        if (filterColumn(x.Cyclist))
            return true;

        if (filterColumn(x.Rides.ToString()))
            return true;

        if (filterColumn(x.Time))
            return true;

        if (filterColumn(x.Distance.ToString()))
            return true;

        if (filterColumn(x.ElevationGain.ToString()))
            return true;

        if (filterColumn(x.DistanceTarget.ToString()))
            return true;

        if (filterColumn(x.DistanceLeftToDo.ToString()))
            return true;

        if (filterColumn(x.AverageDistanceToDoPerWeek.ToString()))
            return true;

        if (filterColumn(x.AverageDistanceDonePerWeek.ToString()))
            return true;

        if (filterColumn(x.AverageDistanceLeftToDoPerWeek.ToString()))
            return true;

        return false;
    };

    protected override async Task OnInitializedAsync()
    {
        await GetStravaClubStatsForYearAsync();
    }

    private async Task GetStravaClubStatsForYearAsync()
    {
        try
        {
            clubStatsForYear = await Mediator.Send(new GetClubStatsForYearQuery());
        }
        catch (Exception ex)
        {
            isInvalidClubStatsForYear = true;
            errorMessage = $"Could not retrieve the club stats for the year";
        }
    }
}
