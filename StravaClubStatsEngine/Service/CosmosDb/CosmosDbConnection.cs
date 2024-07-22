using Microsoft.Azure.Cosmos;
using StravaClubStatsEngine.Service.CosmosDb.Interface;
using StravaClubStatsShared.Models;
using StravaClubStatsShared.Models.FromAzure;
using Container = Microsoft.Azure.Cosmos.Container;

namespace StravaClubStatsEngine.Service.CosmosDb;

public class CosmosDbConnection : ICosmosDbConnection
{
    private readonly StravaClubStatsEngineInput _stravaClubStatsEngineInput;

    public CosmosDbConnection(StravaClubStatsEngineInput stravaClubStatsEngineInput)
    {
        _stravaClubStatsEngineInput = stravaClubStatsEngineInput;
    }

    public async Task<List<ClubStatsForYear>> QueryAsync()
    {
        var container = await SetUpAsync();

        var sqlQueryText = $"SELECT * FROM c";

        var queryDefinition = new QueryDefinition(sqlQueryText);
        var queryResultSetIterator = container.GetItemQueryIterator<ClubStatsForYear>(queryDefinition);

        var clubStatsForYear = new List<ClubStatsForYear>();

        while (queryResultSetIterator.HasMoreResults)
        {
            var response = await queryResultSetIterator.ReadNextAsync();

            foreach (var item in response)
            {
                clubStatsForYear.Add(item);
            }
        }

        return clubStatsForYear;
    }

    private async Task<Container> SetUpAsync()
    {
        var cosmosClient = new CosmosClient(_stravaClubStatsEngineInput.CosmosDbEndointUrl, _stravaClubStatsEngineInput.CosmosDbPrimaryKey);

        Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(_stravaClubStatsEngineInput.CosmosDatabase);

        return await database.CreateContainerIfNotExistsAsync(_stravaClubStatsEngineInput.CosmosPartitionKey, "/id");
    }
}
