using StravaClubStatsEngine.Service.CosmosDb.Interface;
using StravaClubStatsEngine.Service.Interface;
using StravaClubStatsShared.Models;
using System.Globalization;

namespace StravaClubStatsEngine.Service;

public class StravaClubStatsForYearService : IStravaClubStatsForYearService
{
    public readonly ICosmosDbConnection _connection;
    
    public StravaClubStatsForYearService(ICosmosDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<StravaClubStatsForYear>> GetStravaClubStatsForYearAsync()
    {
        var clubStatsForYear = await _connection.QueryAsync();

        var stravaClubStatsForYear = clubStatsForYear.Select(record => new StravaClubStatsForYear
        {
            Id = new Guid(record.id),
            Cyclist = record.cyclist,
            Rides = Convert.ToInt32(record.rides),
            Time = record.time,
            Distance = Convert.ToDecimal(TidyDistance(record.distance)),
            ElevationGain = Convert.ToDecimal(TidyDistance(record.elevationgain)),
            DistanceTarget = Convert.ToDecimal(TidyDistance(record.distancetarget)),
        })
        .ToList();

        stravaClubStatsForYear.ForEach(record => UpdateDistances(record));

        return stravaClubStatsForYear;
    }

    private void UpdateDistances(StravaClubStatsForYear record)
    {
        const int NumberOfWeeksInYear = 52;

        int currentWeekNumber = GetCurrentWeekNumber();

        int weeksLeftInYear = NumberOfWeeksInYear - currentWeekNumber;

        record.DistanceLeftToDo = record.DistanceTarget - record.Distance;
        record.AverageDistanceToDoPerWeek = record.DistanceTarget / NumberOfWeeksInYear;
        record.AverageDistanceDonePerWeek = record.Distance / currentWeekNumber;
        record.AverageDistanceLeftToDoPerWeek = record.DistanceLeftToDo / weeksLeftInYear;
        record.DistanceTargetForCurrentWeek = record.AverageDistanceToDoPerWeek * currentWeekNumber;
    }

    private int GetCurrentWeekNumber()
    {
        var dateFormatInfo = DateTimeFormatInfo.CurrentInfo;
        var calendar = dateFormatInfo.Calendar;

        return calendar.GetWeekOfYear(DateTime.UtcNow, dateFormatInfo.CalendarWeekRule,
                                            dateFormatInfo.FirstDayOfWeek);
    }

    private string TidyDistance(string distance) =>
        distance.Replace(" km", string.Empty).Replace(" m", string.Empty).Replace(",", string.Empty);
}
