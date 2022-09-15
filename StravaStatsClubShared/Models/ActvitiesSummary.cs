namespace StravaClubStatsShared.Models
{
    public class ActvitiesSummary
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalDistanceInKilometers { get; set; }
        public decimal TotalMovingTimeInHours { get; set; }
        public decimal TotalElapsedTimeInHours { get; set; }
        public decimal TotalElevationGainInKilometers { get; set; }
    }
}
