﻿using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.Domain.UserAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System.Linq;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        protected void AddUserQueries(IMediator mediator)
        {
            Field<UserResultType>(
                   "GetUser",
                   description: "Gets the current user.",
                   resolve: context => mediator.Send(GetUserQuery(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                   );
        }

        private UserQuery GetUserQuery(ResolveFieldContext<object> context)
        {
            return new UserQuery(GetUserPropertyHelper(context));
        }

        private UserPropertyHelper GetUserPropertyHelper(ResolveFieldContext<object> context)
        {
            return new UserPropertyHelper(
                context.SubFields.Select(x => x.Value)
            );
        }
    }
}