using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class DeleteMemberCommandResult : CommandResult<Member>
    {
        public DeleteMemberCommandResult(Member member)
            : base(member)
        {

        }
    }
}
