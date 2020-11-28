using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using GraphQL.Types;

namespace DisputenPWA.API.GraphQL.ResultTypes
{
    public class AttendeeResultType : ObjectGraphType<Attendee>
    {
        public AttendeeResultType()
        {
            Field(f => f.Id, type: typeof(IdGraphType)).Description("The attendee id, unique identifier in the database.");
            Field(f => f.UserId, nullable: true).Description("The id of the user.");
            Field(f => f.AppEventId, type: typeof(IdGraphType), nullable: true).Description("The id of the app event.");
            Field(f => f.Paid, type: typeof(bool), nullable: true).Description("True if the user has paid for the event.");
            Field(f => f.AppEvent, nullable: true, type: typeof(AppEventResultType)).Description("The group this member has membership in.");
            Field(f => f.User, nullable: true, type: typeof(NestedUserResultType)).Description("The user data for this member.");
        }
    }
}
