namespace StravaClubStatsBlazorServerApp.Helpers
{
    public static class Helper
    {
        public static string TidyStravaMetric(decimal stravaMetric) => stravaMetric.ToString("0.##");
    }
}
