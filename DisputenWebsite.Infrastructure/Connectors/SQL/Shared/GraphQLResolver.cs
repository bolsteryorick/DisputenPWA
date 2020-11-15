using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.MemberAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared
{
    public interface IGraphQLResolver
    {
        Task<Group> ResolveGroup(Guid id, GroupPropertyHelper helper);
        Task<IReadOnlyCollection<Group>> ResolveGroups(IEnumerable<Guid> groupIds, GroupPropertyHelper helper);
        Task<AppEvent> ResolveAppEvent(Guid appEventId, AppEventPropertyHelper helper);
        Task<IReadOnlyCollection<AppEvent>> ResolveAppEvents(IEnumerable<Guid> groupIds, AppEventPropertyHelper helper);
        Task<Member> ResolveMember(Guid id, MemberPropertyHelper helper);
        Task<IReadOnlyCollection<Member>> ResolveMembersForGroups(IEnumerable<Guid> groupIds, MemberPropertyHelper helper);
    }

    public partial class GraphQLResolver : IGraphQLResolver
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAppEventRepository _eventRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IUserRepository _userRepository;

        public GraphQLResolver(
            IGroupRepository groupRepository,
            IAppEventRepository eventRepository,
            IMemberRepository memberRepository,
            IUserRepository userRepository
            )
        {
            _groupRepository = groupRepository;
            _eventRepository = eventRepository;
            _memberRepository = memberRepository;
            _userRepository = userRepository;
        }
    }
}
