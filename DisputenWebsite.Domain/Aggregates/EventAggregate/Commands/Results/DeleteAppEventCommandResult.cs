﻿using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results
{
    public class DeleteAppEventCommandResult : CommandResult<AppEvent>
    {
        public DeleteAppEventCommandResult(AppEvent appEvent)
            : base(appEvent)
        {

        }
    }
}
