using DisputenPWA.DAL.Helpers;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.GroupAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisputenPWA.DAL.Models
{
    public class SeedData
    {
        public List<DalGroup> DalGroups { get; private set; }
        public List<DalAppEvent> DalAppEvents { get; private set; }
        public List<DalMember> DalMembers { get; set; }
        public List<DalAttendee> DalAttendees { get; set; }

        private List<Guid> GroupIds { get; set; }

        public SeedData(
            int nrOfGroups, 
            int maxEventsPerGroup, 
            int maxMembersPerGroup, 
            int maxAttendeesPerEvent,
            List<string> userIds)
        {
            AddDalGroups(nrOfGroups);
            AddDalEvents(maxEventsPerGroup);
            AddMembers(maxMembersPerGroup, userIds);
            AddAttendees(maxAttendeesPerEvent);
        }

        private void AddDalGroups(int nrOfGroups)
        {
            DalGroups = new List<DalGroup>();
            GroupIds = new List<Guid>();
            for (var i = 0; i < nrOfGroups; i++)
            {
                var groupId = Guid.NewGuid();
                DalGroups.Add(new DalGroup
                {
                    Id = groupId,
                    Name = RandomString.GetRandomString(10),
                    Description = RandomString.GetRandomString(50)
                });
                GroupIds.Add(groupId);
            }
        }

        private void AddDalEvents(int maxEventsPerGroup)
        {
            DalAppEvents = new List<DalAppEvent>();
            var random = new Random();
            for (var i = 0; i < GroupIds.Count; i++)
            {
                var groupId = GroupIds[i];
                var nrOfEvents = random.Next(maxEventsPerGroup);
                for (var j = 0; j < nrOfEvents; j++)
                {
                    var startDaysFromNow = random.Next(100) - 50;
                    var hoursDuration = random.Next(24);
                    var startDateTime = DateTime.Now.AddDays(startDaysFromNow);
                    var endDateTime = startDateTime.AddHours(hoursDuration);
                    DalAppEvents.Add(new DalAppEvent
                    {
                        Name = RandomString.GetRandomString(10),
                        Description = RandomString.GetRandomString(50),
                        StartTime = startDateTime,
                        EndTime = endDateTime,
                        GroupId = groupId
                    });
                }
            }
        }

        private void AddMembers(int maxMembersPerGroup, List<string> userIds)
        {
            DalMembers = new List<DalMember>();
            var random = new Random();
            for (var i = 0; i < GroupIds.Count; i++)
            {
                var usedUserIds = new List<string>();
                var groupId = GroupIds[i];
                var nrOfMembers = random.Next(maxMembersPerGroup);
                for (var j = 0; j < nrOfMembers; j++)
                {
                    var randomUserId = GetRandomUserId(usedUserIds, userIds, random);
                    usedUserIds.Add(randomUserId);
                    DalMembers.Add(new DalMember
                    {
                        UserId = randomUserId,
                        GroupId = groupId
                    });
                }
            }
        }

        private void AddAttendees(int maxAttendeesPerEvent)
        {
            DalAttendees = new List<DalAttendee>();
            var random = new Random();
            foreach (var appEvent in DalAppEvents)
            {
                var usedUserIds = new List<string>();
                var userIdsOfGroup = DalMembers.Where(x => x.GroupId == appEvent.GroupId).Select(x => x.UserId).ToList();
                var nrOfAttendees = random.Next(maxAttendeesPerEvent);
                for(var i = 0; i < nrOfAttendees && i < userIdsOfGroup.Count; i++)
                {
                    var randomUserId = GetRandomUserId(usedUserIds, userIdsOfGroup, random);
                    usedUserIds.Add(randomUserId);
                    DalAttendees.Add(new DalAttendee
                    {
                        AppEventId = appEvent.Id,
                        UserId = randomUserId
                    });
                }
            }
        }

        private string GetRandomUserId(List<string> alreadyUsed, List<string> userIds, Random random)
        {
            var idsToPickFrom = userIds.Except(alreadyUsed).ToList();
            return idsToPickFrom[random.Next(idsToPickFrom.Count)];
        }
    }
}
