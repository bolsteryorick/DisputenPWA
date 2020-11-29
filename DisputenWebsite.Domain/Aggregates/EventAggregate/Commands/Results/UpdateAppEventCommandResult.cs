using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results
{
    public class UpdateAppEventCommandResult : CommandResult<AppEvent>
    {
        public UpdateAppEventCommandResult(AppEvent appEvent)
            : base(appEvent)
        {

        }
    }
}
