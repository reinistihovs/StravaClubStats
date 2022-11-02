namespace StravaClubStatsShared.Models
{
    public class Activity
    {
        public string AthleteFirstName { get; set; }
        public string AthleteLastName { get; set; }
        public string ActivityName { get; set; }
        public string SportType { get; set; }
        public decimal DistanceInKilometers { get; set; }
        public decimal MovingTimeInHours { get; set; }
        public decimal ElapsedTimeInHours { get; set; }
        public decimal TotalElevationGainInKilometers { get; set; }
    }
}
