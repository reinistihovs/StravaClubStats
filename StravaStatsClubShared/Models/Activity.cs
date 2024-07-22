namespace StravaClubStatsShared.Models;

public class Activity
{
    public string AthleteFirstName { get; set; } = string.Empty;
    public string AthleteLastName { get; set; } = string.Empty;
    public string ActivityName { get; set; } = string.Empty;
    public string SportType { get; set; } = string.Empty;
    public decimal DistanceInKilometers { get; set; }
    public decimal MovingTimeInHours { get; set; }
    public decimal ElapsedTimeInHours { get; set; }
    public decimal TotalElevationGainInKilometers { get; set; }
}
