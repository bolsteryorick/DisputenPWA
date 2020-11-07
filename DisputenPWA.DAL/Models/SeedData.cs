using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate.DALObject;
using System;
using System.Collections.Generic;

namespace DisputenPWA.DAL.Models
{
    public class SeedData
    {
        public List<DALGroup> DALGroups { get; set; }
        public List<DALAppEvent> DALAppEvents { get; set; }

        public SeedData(int nrOfGroups, int maxEventsPerGroup)
        {
            DALGroups = new List<DALGroup>();
            DALAppEvents = new List<DALAppEvent>();
            var groupIdsForEvents = new List<Guid>();
            for (var i = 0; i < nrOfGroups; i++)
            {
                var groupId = Guid.NewGuid();
                DALGroups.Add(new DALGroup
                {
                    Id = groupId,
                    Name = RandomString(10),
                    Description = RandomString(50)
                });
                groupIdsForEvents.Add(groupId);
            }
            var random = new Random();
            for (var i = 0; i < groupIdsForEvents.Count; i++)
            {
                var groupId = groupIdsForEvents[i];
                var nrOfEvents = random.Next(maxEventsPerGroup);
                for (var j = 0; j < nrOfEvents; j++)
                {
                    var startDaysFromNow = random.Next(100) - 50;
                    var hoursDuration = random.Next(24);
                    var startDateTime = DateTime.Now.AddDays(startDaysFromNow);
                    var endDateTime = startDateTime.AddHours(hoursDuration);
                    DALAppEvents.Add(new DALAppEvent
                    {
                        Name = RandomString(10),
                        Description = RandomString(50),
                        StartTime = startDateTime,
                        EndTime = endDateTime,
                        GroupId = groupId
                    });
                }
            }
        }

        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private string RandomString(int length)
        {
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = _chars[random.Next(_chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
