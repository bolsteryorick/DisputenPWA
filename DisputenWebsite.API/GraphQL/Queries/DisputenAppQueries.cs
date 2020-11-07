using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.AppEvents;
using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Domain.EventAggregate.Queries;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate.Queries;
using GraphQL.Language.AST;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries : DisputenAppBaseType
    {
        public DisputenAppQueries(IMediator mediator)
        {
            AddGroupQueries(mediator);
            AddAppEventQueries(mediator);
        }
    }
}
