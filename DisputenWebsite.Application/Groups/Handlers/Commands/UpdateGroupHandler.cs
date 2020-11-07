﻿using DisputenPWA.Application.Base;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Commands;
using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Commands
{
    public class UpdateGroupHandler : UpdateHandlerBase, IRequestHandler<UpdateGroupCommand, UpdateGroupCommandResult>
    {
        private readonly IGroupConnector _groupConnector;

        public UpdateGroupHandler(
            IGroupConnector groupConnector
            )
        {
            _groupConnector = groupConnector;
        }

        public async Task<UpdateGroupCommandResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var properties = GetUpdateProperties(request);
            var group = await _groupConnector.UpdateProperties(properties, request.Id);
            return new UpdateGroupCommandResult(group);
        }
    }
}
