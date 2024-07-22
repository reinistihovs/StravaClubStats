using MediatR;
using StravaClubStatsShared.Models;

namespace StravaClubStatsEngine.Queries;

public record GetClubActivitiesQuery() : IRequest<List<Activity>>;
