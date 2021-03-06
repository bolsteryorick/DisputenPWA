using DisputenPWA.Application.Services;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Commands
{
    public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, CreateGroupCommandResult>
    {
        private readonly IGroupConnector _groupConnector;
        private readonly IMemberConnector _memberConnector;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        private readonly IContactRepository _contactRepository;

        public CreateGroupHandler(
            IGroupConnector groupConnector,
            IMemberConnector memberConnector,
            IUserService userService,
            IMediator mediator,
            IContactRepository contactRepository
            )
        {
            _groupConnector = groupConnector;
            _memberConnector = memberConnector;
            _userService = userService;
            _mediator = mediator;
            _contactRepository = contactRepository;
        }

        public async Task<CreateGroupCommandResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await CreateGroup(request);
            await AddSelfAsAdmin(group);
            await AddOtherMembers(request, group);

            return new CreateGroupCommandResult(group);
        }

        private async Task<Group> CreateGroup(CreateGroupCommand request)
        {
            var group = new Group
            {
                Name = request.Name,
                Description = request.Description
            };
            await _groupConnector.Create(group);
            return group;
        }


        private async Task AddSelfAsAdmin(Group group)
        {
            var member = new Member
            {
                UserId = _userService.GetUserId(),
                GroupId = group.Id,
                IsAdmin = true
            };
            await _memberConnector.Create(member);
        }

        private async Task AddOtherMembers(CreateGroupCommand request, Group group)
        {
            await _mediator.Send(new CreateMembersCommand(request.UserIds, group.Id));
        }
    }
}
