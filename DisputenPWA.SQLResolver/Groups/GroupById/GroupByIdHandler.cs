using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.SQLResolver.AppEvents.AppEventsFromGroupsIds;
using DisputenPWA.SQLResolver.Members.MembersByGroupIds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Groups.GroupById
{
    public class GroupByIdHandler : IRequestHandler<GroupByIdRequest, Group>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMediator _mediator;

        public GroupByIdHandler(
            IGroupRepository groupRepository,
            IMediator mediator
            )
        {
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        public async Task<Group> Handle(GroupByIdRequest request, CancellationToken cancellationToken)
        {
            return await ResolveGroupById(request.Id, request.Helper, cancellationToken);
        }

        private async Task<Group> ResolveGroupById(Guid id, GroupPropertyHelper helper, CancellationToken cancellationToken)
        {
            var group = await GetGroupById(id, helper);
            if (helper.CanGetAppEvents())
            {
                group.AppEvents = await _mediator.Send(new AppEventsFromGroupIdsRequest(new List<Guid> { id }, helper.AppEventPropertyHelper), cancellationToken);
            }
            if (helper.CanGetMembers())
            {
                group.Members = await _mediator.Send(new MembersByGroupIdsRequest(new List<Guid> { id }, helper.MemberPropertyHelper), cancellationToken);
            }
            return group;
        }

        private async Task<Group> GetGroupById(Guid id, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => x.Id == id);
            return await _groupRepository.GetFirstOrDefault(queryable, helper);
        }
    }
}
