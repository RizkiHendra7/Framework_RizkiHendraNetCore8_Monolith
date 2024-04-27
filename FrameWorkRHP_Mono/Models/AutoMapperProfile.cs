using AutoMapper;
using FrameWorkRHP_Mono.Core.Models.DTO;
using FrameWorkRHP_Mono.Core.Models.EF;

namespace FrameWorkRHP_Mono.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Muser, DTOMusers>();
            CreateMap<DTOMusers, Muser>();

            CreateMap<MMenu, DTOMMenus>();
            CreateMap<DTOMMenus, MMenu>();

            CreateMap<MRoleXMenu, DTOMroleXMenu>();
            CreateMap<DTOMroleXMenu, MRoleXMenu>();

            CreateMap<Mrole, DTOMRoles>();
            CreateMap<DTOMRoles, Mrole>();
        }
    }
}
