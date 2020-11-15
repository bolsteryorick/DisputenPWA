using DisputenPWA.DAL.Helpers;
using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.Domain.GroupAggregate.DalObject;
using DisputenPWA.Domain.MemberAggregate.DalObject;
using DisputenPWA.Domain.UserAggregate;
using System;
using System.Collections.Generic;

namespace DisputenPWA.DAL.Models
{
    public class SeedData
    {
        public List<DalGroup> DalGroups { get; private set; }
        public List<DalAppEvent> DalAppEvents { get; private set; }
        public List<DalMember> DalMembers { get; set; }

        private List<Guid> GroupIds { get; set; }

        public SeedData(int nrOfGroups, int maxEventsPerGroup, int maxMembersPerGroup, List<string> userIds)
        {
            AddDalGroups(nrOfGroups);
            AddDalEvents(maxEventsPerGroup);
            AddMembers(maxMembersPerGroup, userIds);
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
                var groupId = GroupIds[i];
                var nrOfMembers = random.Next(maxMembersPerGroup);
                for (var j = 0; j < nrOfMembers; j++)
                {
                    DalMembers.Add(new DalMember
                    {
                        UserId = userIds[j],
                        GroupId = groupId
                    });
                }
            }
        }
    }
}
