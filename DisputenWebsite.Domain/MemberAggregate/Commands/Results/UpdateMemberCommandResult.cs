using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class UpdateMemberCommandResult : CommandResult<Member>
    {
        public UpdateMemberCommandResult(Member member)
            : base(member)
        {

        }
    }
}
