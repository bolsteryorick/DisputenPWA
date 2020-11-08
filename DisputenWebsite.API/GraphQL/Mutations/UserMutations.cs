using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.UserAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations
    {
        protected void AddUserMutations(IMediator mediator)
        {
            Field<UserResultType>(
                "RegisterUser",
                description: "Registers a user.",
                arguments: CreateUserArguments(),
                resolve: context => mediator.Send(RegisterUserCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }

        private QueryArguments CreateUserArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "email" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }
            );
        }

        private RegisterUserCommand RegisterUserCommand(ResolveFieldContext<object> context)
        {
            return new RegisterUserCommand(
                context.GetArgument<string>("email"),
                context.GetArgument<string>("password")
            );
        }
    }
}
