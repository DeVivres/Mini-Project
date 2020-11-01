using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;

namespace ProjectManagementApp.BLL.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
        }
    }
}
