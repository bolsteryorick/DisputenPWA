using DisputenPWA.Domain.Aggregates.GroupAggregate;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Group>> GetItemsAsync(string query);
        Task<Group> GetItemAsync(string id);
        Task AddItemAsync(Group group);
        Task UpdateItemAsync(string id, Group group);
        Task DeleteItemAsync(string id);
    }
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Group group)
        {
            await this._container.CreateItemAsync(group, new PartitionKey(group.Id.ToString()));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Group>(id, new PartitionKey(id));
        }

        public async Task<Group> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Group> response = await this._container.ReadItemAsync<Group>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Group>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Group>(new QueryDefinition(queryString));
            List<Group> results = new List<Group>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Group group)
        {
            await this._container.UpsertItemAsync(group, new PartitionKey(id));
        }
    }
}
