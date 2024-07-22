using MediatR;
using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Queries;

public record GetClubStatsForYearQuery() : IRequest<List<StravaClubStatsForYear>>;
