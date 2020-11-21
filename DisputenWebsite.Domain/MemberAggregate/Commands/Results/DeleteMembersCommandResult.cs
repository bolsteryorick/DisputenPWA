using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class DeleteMembersCommandResult : CommandResult<Member>
    {
        public DeleteMembersCommandResult(Member member)
            : base(member)
        {

        }
    }
}
