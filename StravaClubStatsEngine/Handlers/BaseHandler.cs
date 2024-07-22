namespace StravaClubStatsEngine.Handlers;

public class BaseHandler
{
    protected string GetStringForCompare(string compareFrom, string compareTo) =>
      !string.IsNullOrEmpty(compareFrom) ? compareFrom.ToLower() : compareTo.ToLower();
}
