using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        protected void AddContactQueries(IMediator mediator)
        {
            Field<ListGraphType<ContactResultType>>(
                "GetAllContacts",
                description: "Gets all direct and associated contacts.",
                resolve: context => mediator.Send(new AllContactsQuery()).Map(r => ProcessResult(context, r))
                );
        }

    }
}