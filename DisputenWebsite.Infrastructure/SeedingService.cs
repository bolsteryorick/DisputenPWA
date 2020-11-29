using DisputenPWA.DAL.Helpers;
using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure
{
    public interface ISeedingService
    {
        Task Seed(int nrOfGroups, int maxEventsPerGroup, int maxMembersPerGroup, int maxAttendeesPerEvent);
    }
    public class SeedingService : ISeedingService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGroupConnector _groupConnector;
        private readonly IAppEventConnector _appEventConnector;
        private readonly IUserRepository _userRepository;
        private readonly IMemberConnector _memberConnector;
        private readonly IAttendeeConnector _attendeeConnector;

        public SeedingService(
            UserManager<ApplicationUser> userManager,
            IGroupConnector groupConnector,
            IAppEventConnector appEventConnector,
            IUserRepository userRepository,
            IMemberConnector memberConnector,
            IAttendeeConnector attendeeConnector
            )
        {
            _userManager = userManager;
            _groupConnector = groupConnector;
            _appEventConnector = appEventConnector;
            _userRepository = userRepository;
            _memberConnector = memberConnector;
            _attendeeConnector = attendeeConnector;
        }

        public async Task Seed(int nrOfGroups, int maxEventsPerGroup, int maxMembersPerGroup, int maxAttendeesPerEvent)
        {
            await CreateUsers(maxMembersPerGroup * 10);
            var userIds = await GetUserIds();

            var seedData = new SeedData(
                nrOfGroups, 
                maxEventsPerGroup, 
                maxMembersPerGroup, 
                maxAttendeesPerEvent, 
                userIds
                );
            await _groupConnector.Create(seedData.DalGroups);
            await _appEventConnector.Create(seedData.DalAppEvents);
            await _memberConnector.Create(seedData.DalMembers);
            await _attendeeConnector.Create(seedData.DalAttendees);
        }

        private async Task CreateUsers(int maxMembersPerGroup)
        {
            var userCount = await _userRepository.GetCount();
            
            for (var i = 0; i < (maxMembersPerGroup - userCount); i++)
            {
                var emailString = $"{RandomString.GetRandomString(12)}@gmail.com";
                var passwordString = RandomString.GetRandomString(12);
                var user = new ApplicationUser { UserName = emailString, Email = emailString };
                await _userManager.CreateAsync(user, passwordString);
            }
        }

        private async Task<List<string>> GetUserIds()
        {
            var users = await _userRepository.GetAll(_userRepository.GetQueryable(), new UserPropertyHelper());
            return users.Select(x => x.Id).ToList();
        }
    }
}
