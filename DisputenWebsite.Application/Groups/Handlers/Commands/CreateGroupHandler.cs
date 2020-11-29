using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Commands
{
    public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, CreateGroupCommandResult>
    {
        private readonly IGroupConnector _groupConnector;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public CreateGroupHandler(
            IGroupConnector groupConnector,
            IUserService userService,
            IMediator mediator
            )
        {
            _groupConnector = groupConnector;
            _userService = userService;
            _mediator = mediator;
        }

        public async Task<CreateGroupCommandResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new Group { 
                Name = request.Name, 
                Description = request.Description
            };
            await _groupConnector.Create(group);
            await _mediator.Send(new CreateMemberCommand(_userService.GetUserId(), true, group.Id));
            return new CreateGroupCommandResult(group);
        }
    }
}
