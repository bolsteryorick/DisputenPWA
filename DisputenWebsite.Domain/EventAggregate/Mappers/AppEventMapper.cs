using AutoMapper;
using DisputenPWA.Domain.EventAggregate.DALObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Mappers
{
    public class AppEventMapper : Profile
    {
        public AppEventMapper()
        {
            //CreateMap<DALAppEvent, AppEvent>()
            //    .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Description, cfg => cfg.MapFrom(src => src.Description))
            //    .ForMember(dest => dest.GroupId, cfg => cfg.MapFrom(src => src.GroupId))
            //    .ForMember(dest => dest.Group, cfg => cfg.MapFrom(src => src.Group.CreateGroup()))
            //    ;
        }
    }
}
