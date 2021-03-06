using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Hierarchy;
using System;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate
{
    public class Attendee : IdModelBase
    {
        public string UserId { get; set; }
        public Guid AppEventId { get; set; }
        public bool Paid { get; set; }
        public AppEvent AppEvent { get; set; }
        public User User { get; set; }

        private Attendee(Guid id, string userId, Guid appEventId, bool paid)
        {
            Id = id;
            UserId = userId;
            AppEventId = appEventId;
            Paid = paid;
        }

        public static Attendee ForJoiningEvent(string userId, Guid appEventId) => new Attendee(Guid.NewGuid() ,userId, appEventId, false);

        public static Attendee FromSqlQuery(
            DalAttendee dalAttendee,
            AttendeePropertyHelper helper)
        {
            return new Attendee(
                dalAttendee.Id, 
                dalAttendee.UserId, 
                dalAttendee.AppEventId,
                helper.GetPaid ? dalAttendee.Paid : false);
        }

        public static Attendee FromDalAttendee(
            DalAttendee dalAttendee)
        {
            return new Attendee(
                dalAttendee.Id,
                dalAttendee.UserId,
                dalAttendee.AppEventId,
                dalAttendee.Paid);
        }

        public DalAttendee CreateDalAttendee()
        {
            return new DalAttendee
            {
                Id = Id,
                UserId = UserId,
                AppEventId = AppEventId,
                Paid = Paid,
            };
        }
    }
}
