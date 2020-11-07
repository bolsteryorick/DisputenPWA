using DisputenPWA.Domain.SeedWorks.Cqrs;

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
