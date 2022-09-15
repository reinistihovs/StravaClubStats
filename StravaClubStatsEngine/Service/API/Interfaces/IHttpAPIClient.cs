namespace StravaClubStatsEngine.Service.API.Interface
{
    public interface IHttpAPIClient
    {
        public Task<string> GetAsync(string url);
    }
}
