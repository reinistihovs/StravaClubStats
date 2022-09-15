namespace StravaClubStatsShared.Models
{
    public class Activity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal DistanceInKilometers { get; set; }
        public decimal MovingTimeInHours { get; set; }
        public decimal ElapsedTimeInHours { get; set; }
        public decimal TotalElevationGainInKilometers { get; set; }
    }
}
