using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver
{
    public interface IGraphQLResolver
    {
        Task<Group> ResolveGroupById(Guid id, GroupPropertyHelper helper);
        Task<IReadOnlyCollection<Group>> ResolveGroupsByIds(IEnumerable<Guid> groupIds, GroupPropertyHelper helper);
        Task<AppEvent> ResolveAppEventById(Guid appEventId, AppEventPropertyHelper helper);
        Task<IReadOnlyCollection<AppEvent>> ResolveAppEventsFromGroupIds(IEnumerable<Guid> groupIds, AppEventPropertyHelper helper);
        Task<Member> ResolveMemberById(Guid id, MemberPropertyHelper helper);
        Task<IReadOnlyCollection<Member>> ResolveMembersByGroupIds(IEnumerable<Guid> groupIds, MemberPropertyHelper helper);
        Task<User> ResolveUserById(string userId, UserPropertyHelper helper);
        Task<IReadOnlyCollection<User>> ResolveUsersByIds(IEnumerable<string> userIds, UserPropertyHelper helper);
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
