using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared
{
    public interface IGraphQLResolver
    {
        Task<Group> ResolveGroup(Guid id, GroupPropertyHelper helper);
        Task<IReadOnlyCollection<Group>> ResolveGroups(IEnumerable<Guid> groupIds, GroupPropertyHelper helper);
        Task<IReadOnlyCollection<AppEvent>> ResolveAppEvents(
            IEnumerable<Guid> groupIds,
            AppEventPropertyHelper helper);
        Task<AppEvent> ResolveAppEvent(
            Guid appEventId,
            AppEventPropertyHelper helper);
    }

    public partial class GraphQLResolver : IGraphQLResolver
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAppEventRepository _eventRepository;

        public GraphQLResolver(
            IGroupRepository groupRepository,
            IAppEventRepository eventRepository
            )
        {
            _groupRepository = groupRepository;
            _eventRepository = eventRepository;
        }
    }
}
