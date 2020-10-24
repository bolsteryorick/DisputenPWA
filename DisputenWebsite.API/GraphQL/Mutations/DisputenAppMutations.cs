using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.AppEvents;
using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Domain.EventAggregate.Commands;
using DisputenPWA.Domain.GroupAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations : DisputenAppBaseType
    {
        public DisputenAppMutations(IMediator mediator)
        {
            AddGroupMutations(mediator);
            AddAppEventMutations(mediator);
        }
    }
}