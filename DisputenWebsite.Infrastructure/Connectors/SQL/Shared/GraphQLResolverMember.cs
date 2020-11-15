using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.MemberAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared
{
    public partial class GraphQLResolver
    {
        public async Task<Member> ResolveMember(Guid id, MemberPropertyHelper helper)
        {
            var member = await GetMember(id, helper);
            if (helper.CanGetGroup())
            {
                member.Group = await ResolveGroup(member.GroupId, helper.GroupPropertyHelper);
            }
            if (helper.CanGetUser())
            {
                member.User = await ResolveUser(member.UserId, helper.UserPropertyHelper);
            }
            return member;
        }
           
        public async Task<IReadOnlyCollection<Member>> ResolveMembersForGroups(IEnumerable<Guid> groupIds, MemberPropertyHelper helper)
        {
            var members = await GetMembersForGroups(groupIds, helper);
            if (helper.CanGetGroup())
            {
                members = await AddGroupsToMembers(members, groupIds, helper);
            }
            if (helper.CanGetUser())
            {
                var userIds = members.Select(x => x.UserId);
                members = await AddUsersToMembers(members, userIds, helper);
            }
            return members.AsReadOnly();
        }

        public async Task<IReadOnlyCollection<Member>> ResolveMembershipsForUsers(IEnumerable<string> userIds, MemberPropertyHelper helper)
        {
            var members = await GetMembershipsForUsers(userIds, helper);
            if (helper.CanGetGroup())
            {
                var groupIds = members.Select(x => x.GroupId);
                members = await AddGroupsToMembers(members, groupIds, helper);
            }
            if (helper.CanGetUser())
            {
                members = await AddUsersToMembers(members, userIds, helper);
            }
            return members.AsReadOnly();
        }

        private async Task<Member> GetMember(Guid id, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => x.Id == id);
            return await _memberRepository.GetFirstOrDefault(queryable, helper);
        }

        private async Task<List<Member>> GetMembersForGroups(IEnumerable<Guid> groupIds, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => groupIds.Contains(x.GroupId));
            return await _memberRepository.GetAll(queryable, helper);
        }

        private async Task<List<Member>> AddGroupsToMembers(List<Member> members, IEnumerable<Guid> groupIds, MemberPropertyHelper helper)
        {
            var groups = await ResolveGroups(groupIds, helper.GroupPropertyHelper);
            var groupsDictionary = groups.ToDictionary(x => x.Id);
            foreach (var member in members)
            {
                if (groupsDictionary.TryGetValue(member.GroupId, out var group)) member.Group = group;
            }
            return members;
        }

        private async Task<List<Member>> AddUsersToMembers(List<Member> members, IEnumerable<string> userIds, MemberPropertyHelper helper)
        {
            var users = await ResolveUsers(userIds, helper.UserPropertyHelper);
            var usersDictionary = users.ToDictionary(x => x.Id);
            foreach (var member in members)
            {
                if (usersDictionary.TryGetValue(member.UserId, out var user)) member.User = user;
            }
            return members;
        }

        private async Task<List<Member>> GetMembershipsForUsers(IEnumerable<string> userIds, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => userIds.Contains(x.UserId));
            return await _memberRepository.GetAll(queryable, helper);
        }
    }
}
