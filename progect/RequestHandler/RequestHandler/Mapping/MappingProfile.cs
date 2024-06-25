using AutoMapper;
using RequestHandler.DTO;
using RequestHandler.Models;

namespace RequestHandler.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.UserId,
                opt => opt.MapFrom(uDto => uDto.UserId))
                .ForMember(u => u.Login,
                opt => opt.MapFrom(uDto => uDto.Login))
                .ForMember(u => u.Surname,
                opt => opt.MapFrom(uDto => uDto.Surname))
                .ForMember(u => u.Name,
                opt => opt.MapFrom(uDto => uDto.Name))
                .ForMember(u => u.Role,
                opt => opt.MapFrom(uDto => uDto.RoleNavigation.Title));
        }
    }
}
