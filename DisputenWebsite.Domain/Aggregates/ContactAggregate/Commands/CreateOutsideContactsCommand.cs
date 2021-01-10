using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.Commands
{
    public class CreateOutsideContactsCommand : IRequest<CreateContactsResult>
    {
        public CreateOutsideContactsCommand(
            IEnumerable<string> emailAddresses
            )
        {
            EmailAddresses = emailAddresses;
        }
        public IEnumerable<string> EmailAddresses { get; }
    }
}
