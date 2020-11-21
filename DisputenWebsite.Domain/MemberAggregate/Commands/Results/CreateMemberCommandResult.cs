using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class CreateMemberCommandResult : CommandResult<Member>
    {
        public CreateMemberCommandResult(Member member)
            : base(member)
        {

        }
    }
}
