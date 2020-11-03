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

        //private IList<string> GetArgumentNames(ResolveFieldContext<object> context)
        //{
        //    return context.SubFields.Select(x => x.Key).ToImmutableList();
        //}

        //// add a first char of string to lower extension to make this easier
        //// make it generic somehow, recreate the entire thing
        //private IList<string> GetSubFieldChildren(ResolveFieldContext<object> context, string subFieldName)
        //{
        //    var hasSubField = context.SubFields.ContainsKey(subFieldName);
        //    if(!hasSubField)
        //    {
        //        return new List<string>();
        //    }
        //    return context.SubFields[subFieldName].SelectionSet.Children.Select(x => (Field) x).Select(x => x.Name).ToList();
        //}
    }
}
