using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using GraphQL.Types;

namespace DisputenPWA.API.GraphQL.ResultTypes
{
    public class AppEventResultType : ObjectGraphType<AppEvent>
    {
        public AppEventResultType()
        {
            Field(f => f.Id, type: typeof(IdGraphType)).Description("The app event id, unique identifier in the database.");
            Field(f => f.Name, nullable: true).Description("The name of the app event.");
            Field(f => f.Description, nullable: true).Description("The description of the app event.");
            Field(f => f.StartTime, nullable: true, type: typeof(DateTimeGraphType)).Description("The start time of the app event.");
            Field(f => f.EndTime, nullable: true, type: typeof(DateTimeGraphType)).Description("The end time of the app event.");
            Field(f => f.GroupId, nullable: true, type: typeof(IdGraphType)).Description("The id of the group this event is assigned to.");
            Field(f => f.MaxAttendees, nullable: true).Description("The max attendees this event can hold.");
            Field(f => f.Group, nullable: true, type: typeof(GroupResultType)).Description("The group this event is assigned to.");
            Field(f => f.Attendees, nullable: true, type: typeof(ListGraphType<AttendeeResultType>)).Description("The attendences to this event.");
        }
    }
}
