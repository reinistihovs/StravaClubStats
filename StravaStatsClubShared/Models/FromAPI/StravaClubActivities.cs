namespace StravaClubStatsEngine.Models.FromAPI
{
    public class StravaClubActivities
    {
        public int resource_state { get; set; }
        public Athlete athlete { get; set; }
        public string name { get; set; }
        public float distance { get; set; }
        public int moving_time { get; set; }
        public int elapsed_time { get; set; }
        public float total_elevation_gain { get; set; }
        public string type { get; set; }
        public string sport_type { get; set; }
        public int? workout_type { get; set; }
    }

    public class Athlete
    {
        public int resource_state { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

}
