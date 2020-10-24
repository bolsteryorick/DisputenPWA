using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Commands.Results
{
    public class CreateAppEventCommandResult : CommandResult<AppEvent>
    {
        public CreateAppEventCommandResult(AppEvent appEvent)
            : base(appEvent)
        {

        }
    }
}
