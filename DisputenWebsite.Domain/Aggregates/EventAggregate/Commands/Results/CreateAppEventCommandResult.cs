using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results
{
    public class CreateAppEventCommandResult : CommandResult<AppEvent>
    {
        public CreateAppEventCommandResult(AppEvent appEvent)
            : base(appEvent)
        {

        }
    }
}
