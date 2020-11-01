using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;

namespace ProjectManagementApp.BLL.MappingProfiles
{
    public class TaskStateProfile : Profile
    {
        public TaskStateProfile()
        {
            CreateMap<TaskState, TaskStateDTO>().ReverseMap();
        }
    }
}
