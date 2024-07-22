namespace StravaClubStatsShared.Models;

public class ActivitiesSummary
{
    public string AthleteFirstName { get; set; } = string.Empty;
    public string AthleteLastName { get; set; } = string.Empty;
    public int TotalNumberOfRides { get; set; }
    public decimal TotalDistanceInKilometers { get; set; }
    public decimal LongestRideInKilometers { get; set; }
    public decimal TotalMovingTimeInHours { get; set; }
    public decimal TotalElapsedTimeInHours { get; set; }
    public decimal TotalElevationGainInKilometers { get; set; }
    public decimal AverageDistancePerRideInKilometers { get; set; }
    public decimal AverageMovingTimeInHours { get; set; }
    public decimal AverageElapsedTimeInHours { get; set; }
    public decimal AverageElevationGainInKilometers { get; set; }
}
