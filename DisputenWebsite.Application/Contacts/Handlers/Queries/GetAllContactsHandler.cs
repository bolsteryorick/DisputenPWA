using DisputenPWA.Application.Services;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Queries;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Queries.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Contacts.Handlers.Queries
{
    public class GetAllContactsHandler : IRequestHandler<AllContactsQuery, AllContactsQueryResult>
    {
        private readonly IUserService _userService;
        private readonly IContactRepository _contactRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IUserRepository _userRepository;

        public GetAllContactsHandler(
            IUserService userService,
            IContactRepository contactRepository,
            IMemberRepository memberRepository,
            IUserRepository userRepository
            )
        {
            _userService = userService;
            _contactRepository = contactRepository;
            _memberRepository = memberRepository;
            _userRepository = userRepository;
        }

        public async Task<AllContactsQueryResult> Handle(AllContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = (IEnumerable<Contact>) new List<Contact>();
            var userId = _userService.GetUserId();

            // get all direct contacts
            var directContacts = (await _contactRepository.GetQueryable().Where(c => c.UserId == userId).Select(x => new Contact { UserId = x.UserId, ContactUserId = x.ContactUserId, EmailAddress = x.ContactUser.Email }).ToListAsync());
            contacts = contacts.Concat(directContacts);

            // get all associated contacts
            var groupIds = await _memberRepository.GetQueryable().Where(m => m.UserId == userId).Select(x => x.GroupId).ToListAsync();
            var allMembersUserIds = (await _memberRepository.GetQueryable().Where(m => groupIds.Contains(m.GroupId)).Select(x => x.UserId).ToListAsync()).Distinct();
            allMembersUserIds = allMembersUserIds.Where(x => x != userId);
            var userContactInfo = await _userRepository.GetQueryable().Where(u => allMembersUserIds.Contains(u.Id)).Select(x => new Contact { UserId = userId, ContactUserId = x.Id, EmailAddress = x.Email }).ToListAsync();
            contacts = contacts.Concat(userContactInfo);

            return new AllContactsQueryResult(contacts);
        }
    }
}