using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Commands.Results
{
    public class UpdateAppEventCommandResult : CommandResult<AppEvent>
    {
        public UpdateAppEventCommandResult(AppEvent appEvent)
            : base(appEvent)
        {

        }
    }
}
