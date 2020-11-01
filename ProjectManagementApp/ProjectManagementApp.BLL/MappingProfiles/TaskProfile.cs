using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;

namespace ProjectManagementApp.BLL.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDTO>().ReverseMap();
        }
    }
}
