﻿using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Commands.Results
{
    public class DeleteAppEventCommandResult : CommandResult<AppEvent>
    {
        public DeleteAppEventCommandResult(AppEvent appEvent)
            : base(appEvent)
        {

        }
    }
}
