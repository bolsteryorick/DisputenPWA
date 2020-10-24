using DisputenPWA.Domain.EventAggregate;
using GraphQL.Types;

namespace DisputenPWA.API.GraphQL.AppEvents
{
    public class AppEventResultType : ObjectGraphType<AppEvent>
    {
        public AppEventResultType()
        {
            Field(f => f.Id, type: typeof(IdGraphType)).Description("The app event id, unique identifier in the database.");
            Field(f => f.Name).Description("The name of the app event.");
            Field(f => f.Description).Description("The description of the app event.");
            Field(f => f.StartTime, type: typeof(DateTimeGraphType)).Description("The start time of the app event.");
            Field(f => f.EndTime, type: typeof(DateTimeGraphType)).Description("The end time of the app event.");
        }
    }
}
