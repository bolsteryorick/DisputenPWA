using DisputenPWA.DAL.Models;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure
{
    public interface ISeedingService
    {
        Task Seed(int nrOfGroups, int maxEventsPerGroup);
    }
    public class SeedingService : ISeedingService
    {
        private readonly IGroupConnector _groupConnector;
        private readonly IAppEventConnector _appEventConnector;

        public SeedingService(
            IGroupConnector groupConnector,
            IAppEventConnector appEventConnector
            )
        {
            _groupConnector = groupConnector;
            _appEventConnector = appEventConnector;
        }

        public async Task Seed(int nrOfGroups, int maxEventsPerGroup)
        {
            var seedData = new SeedData(nrOfGroups, maxEventsPerGroup);
            await _groupConnector.Create(seedData.DALGroups);
            await _appEventConnector.Create(seedData.DALAppEvents);
        }
    }
}
