using StravaClubStatsEngine.Queries;
using StravaClubStatsShared.Models;

namespace StravaClubStatsBlazorServerApp.Pages.ClubActivities;

public partial class ClubActivitiesDrilldown
{
    private List<Activity> ClubActivities = new List<Activity>();

    private List<string> Cyclists = new List<string>();

    private bool IsInvalidClubActivities = false;

    private string ErrorMessage { get; set; } = string.Empty;

    private string SearchText = string.Empty;

    private bool IsSmall = false;

    private bool FilterColumn(string columnName) =>
                    columnName.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

    private string ConvertTo2DecimalPlaces(decimal decimalValue) => decimalValue.ToString("0.##");

    private Func<Activity, bool> quickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return true;

        if (FilterColumn(x.ActivityName))
            return true;

        if (FilterColumn($"{x.AthleteFirstName} {x.AthleteLastName}"))
            return true;

        if(FilterColumn(x.SportType))
            return true;

        if (FilterColumn(x.DistanceInKilometers.ToString()))
            return true;

        if (FilterColumn(x.ElapsedTimeInHours.ToString()))
            return true;

        if (FilterColumn(x.MovingTimeInHours.ToString()))
            return true;

        if (FilterColumn(x.TotalElevationGainInKilometers.ToString()))
            return true;

        return false;
    };

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ClubActivities = await Mediator.Send(new GetClubActivitiesQuery());

            Cyclists = ClubActivities
                       .GroupBy(clubActivity => clubActivity.AthleteFirstName)
                       .Select(cyclist => cyclist.Key)
                       .OrderBy(cyclist => cyclist)
                       .ToList();

            if (IsSmall &&
                string.IsNullOrEmpty(SearchText) &&
                Cyclists.Any())
            {
                SearchText = Cyclists.First();
            }
        }
        catch (Exception ex)
        {
            IsInvalidClubActivities = true;
            ErrorMessage = $"Could not retrieve the club activities - {ex.Message}";
        }
    }
}
