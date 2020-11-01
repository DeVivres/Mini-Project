using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;

namespace ProjectManagementApp.BLL.MappingProfiles
{
    public class ProjectsInfoProfile : Profile
    {
        public ProjectsInfoProfile()
        {
            CreateMap<ProjectsInfo, ProjectsInfoDTO>().ReverseMap();
        }
    }
}
