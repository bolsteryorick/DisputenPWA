using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate.Commands.Results
{
    public class DeleteMemberCommandResult : CommandResult<Member>
    {
        public DeleteMemberCommandResult(Member member)
            : base(member)
        {

        }
    }
}
