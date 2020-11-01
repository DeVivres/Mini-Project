using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;

namespace ProjectManagementApp.BLL.MappingProfiles
{
    public class UsersInfoProfile : Profile
    {
        public UsersInfoProfile()
        {
            CreateMap<UsersInfo, UsersInfoDTO>().ReverseMap();
        }
    }
}
