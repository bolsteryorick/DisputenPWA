using AutoMapper;
using DisputenPWA.Domain.GroupAggregate.DALObject;
using DisputenPWA.Domain.Helpers;
using System.Linq;

namespace DisputenPWA.Domain.GroupAggregate.Mappers
{
    public class GroupMapper : Profile
    {
        public GroupMapper()
        {
            CreateMap<DALGroup, Group>()
                .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, cfg => cfg.MapFrom(src => src.Description))
                .ForMember(dest => dest.AppEvents, cfg => cfg.MapFrom(
                    src => src.AppEvents.Where(
                        e => e.EndTime > EventRange.LowestEndDate &&
                        e.StartTime < EventRange.HighestStartDate
                        )
                    .Select(x => x.CreateAppEvent())))
                ;
        }
    }
}