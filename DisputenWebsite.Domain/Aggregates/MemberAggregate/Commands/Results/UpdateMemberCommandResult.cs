using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate.Commands.Results
{
    public class UpdateMemberCommandResult : CommandResult<Member>
    {
        public UpdateMemberCommandResult(Member member)
            : base(member)
        {

        }
    }
}
