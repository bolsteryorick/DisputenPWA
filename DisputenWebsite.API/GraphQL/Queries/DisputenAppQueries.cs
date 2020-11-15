using MediatR;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries : DisputenAppBaseType
    {
        public DisputenAppQueries(IMediator mediator)
        {
            AddGroupQueries(mediator);
            AddAppEventQueries(mediator);
            AddMemberQueries(mediator);
        }
    }
}
