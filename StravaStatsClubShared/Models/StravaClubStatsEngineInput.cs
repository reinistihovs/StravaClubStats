namespace StravaClubStatsShared.Models
{
    public class StravaClubStatsEngineInput
    {
        public string StravaClubAPIUrl { get; set; }
        public string APIToken { get; set; }
        public int ClubID { get; set; }
        public int NumberOfPages { get; set; }
    }
}
