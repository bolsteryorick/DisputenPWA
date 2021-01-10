using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands;
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
        protected void AddContactMutations(IMediator mediator)
        {
            Field<ContactResultType>(
                "CreateContact",
                description: "Creates a contact for the user.",
                arguments: CreateContactArguments(),
                resolve: context => mediator.Send(CreateContactCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
            );

            Field<ContactResultType>(
                "DeleteContact",
                description: "Deletes a contact by id.",
                arguments: DeleteContactsArguments(),
                resolve: context => mediator.Send(DeleteContactCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
            );

            Field<ContactResultType>(
                "CreateOutsideContacts",
                description: "Creates contacts for the user.",
                arguments: CreateOutsideContactsArguments(),
                resolve: context => mediator.Send(CreateContactsCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
            );
        }
        
        private QueryArguments CreateContactArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "contactId" }
            );
        }

        private CreateContactCommand CreateContactCommand(ResolveFieldContext<object> context)
        {
            return new CreateContactCommand(
                context.GetArgument<string>("contactId")
            );
        }
        
        private QueryArguments DeleteContactsArguments()
        {
            return new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" }
            );
        }

        private DeleteContactCommand DeleteContactCommand(ResolveFieldContext<object> context)
        {
            return new DeleteContactCommand(
                context.GetArgument<Guid>("id")
            );
        }

        private QueryArguments CreateOutsideContactsArguments()
        {
            return new QueryArguments(
                new QueryArgument<ListGraphType<StringGraphType>> { Name = "emailAddresses" }
            );
        }

        private CreateOutsideContactsCommand CreateContactsCommand(ResolveFieldContext<object> context)
        {
            return new CreateOutsideContactsCommand(
                context.GetArgument<List<string>>("emailAddresses")
            );
        }
    }
}