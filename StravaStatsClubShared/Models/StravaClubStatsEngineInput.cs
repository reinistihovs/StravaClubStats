namespace StravaClubStatsShared.Models
{
    public class StravaClubStatsEngineInput
    {
        public string StravaClubAPIUrl { get; set; }
        public int ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshToken { get; set; }
        public int ClubID { get; set; }
        public int NumberOfPages { get; set; }
    }
}
