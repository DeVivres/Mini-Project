using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;

namespace ProjectManagementApp.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
