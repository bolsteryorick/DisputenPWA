using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.MemberAggregate.DalObject;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Members
{
    public interface IMemberConnector
    {
        Task<Member> GetMember(Guid id, MemberPropertyHelper helper);
        Task<IReadOnlyCollection<Member>> GetMembers(Guid groupId, MemberPropertyHelper helper);
        Task Create(Member member);
        Task Delete(Guid id);
        Task DeleteGroupMembers(Guid groupId);
        Task Create(IEnumerable<DalMember> dalMembers);
        Task<Member> UpdateProperties(Dictionary<string, object> properties, Guid id);
    }

    public class MemberConnector : IMemberConnector
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMediator _mediator;

        public MemberConnector(
            IMemberRepository memberRepository,
            IMediator mediator
            )
        {
            _memberRepository = memberRepository;
            _mediator = mediator;
        }

        public async Task<Member> GetMember(Guid id, MemberPropertyHelper helper)
        {
            return (await _mediator.Send(new MemberByIdRequest(id, helper))).Result;
        }

        public async Task<IReadOnlyCollection<Member>> GetMembers(Guid groupId, MemberPropertyHelper helper)
        {
            return (await _mediator.Send(new MembersByGroupIdsRequest(new List<Guid> { groupId }, helper))).Result;
        }

        public async Task Create(Member member)
        {
            await _memberRepository.Add(member.CreateDalMember());
        }

        public async Task Delete(Guid id)
        {
            await _memberRepository.DeleteByObject(new DalMember { Id = id});
        }

        public async Task DeleteGroupMembers(Guid groupId)
        {
            var deleteQuery = _memberRepository.GetQueryable().Where(x => x.GroupId == groupId);
            await _memberRepository.DeleteByQuery(deleteQuery);
        }

        public async Task Create(IEnumerable<DalMember> dalMembers)
        {
            await _memberRepository.Add(dalMembers);
        }

        public async Task<Member> UpdateProperties(Dictionary<string, object> properties, Guid id)
        {
            var dalMember = await _memberRepository
                .UpdateProperties(new DalMember { Id = id }, properties);
            return dalMember.CreateMember();
        }
    }
}