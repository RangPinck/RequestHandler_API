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
            CreateMap<Role, RolesDto>();
            CreateMap<Status, StatusDto>();
            CreateMap<UserAppointment, AppointmentGetDto>()
                .ForMember(a => a.Appointment,
                    opt => opt.MapFrom(uaDto => uaDto.Appointment))
                .ForMember(a => a.User,
                    opt => opt.MapFrom(uaDto => uaDto.User))
                .ForMember(a => a.UserName,
                    opt => opt.MapFrom(uaDto => 
                        uaDto.UserNavigation.Surname + ' ' + uaDto.UserNavigation.Name.Substring(0,1) + '.'))
                .ForMember(a => a.Problem,
                    opt => opt.MapFrom(uaDto => uaDto.AppointmentNavigation.Problem))
                .ForMember(a => a.DiscriptionProblem,
                    opt => opt.MapFrom(uaDto => uaDto.AppointmentNavigation.DiscriptionProblem))
                .ForMember(a => a.Place,
                    opt => opt.MapFrom(uaDto => uaDto.AppointmentNavigation.Place))
                .ForMember(a => a.DateCreate,
                    opt => opt.MapFrom(uaDto => uaDto.AppointmentNavigation.DateCreate))
                .ForMember(a => a.DateApprove,
                    opt => opt.MapFrom(uaDto => uaDto.AppointmentNavigation.DateApprove))
                .ForMember(a => a.DateFix,
                    opt => opt.MapFrom(uaDto => uaDto.AppointmentNavigation.DateFix))
                .ForMember(a => a.Status,
                    opt => opt.MapFrom(uaDto => uaDto.AppointmentNavigation.StatusNavigation.Title))
                .ForMember(a => a.DocumentId,
                    opt => opt.MapFrom(uaDto => uaDto.AppointmentNavigation.Documents));
        }
    }
}
