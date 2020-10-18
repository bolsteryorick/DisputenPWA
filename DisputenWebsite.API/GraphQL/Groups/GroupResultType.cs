﻿using DisputenPWA.Domain.GroupAggregate;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.Groups
{
    public class GroupResultType : ObjectGraphType<Group>
    {
        public GroupResultType()
        {
            Field(f => f.Id, type: typeof(IdGraphType)).Description("The group id, unique identifier in the database.");
            Field(f => f.Name).Description("The name of the group.");
            Field(f => f.Description).Description("The description of the group.");
        }
    }
}
