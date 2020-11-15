using MediatR;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations : DisputenAppBaseType
    {
        public DisputenAppMutations(IMediator mediator)
        {
            AddGroupMutations(mediator);
            AddAppEventMutations(mediator);
            AddMemberMutations(mediator);
        }
    }
}