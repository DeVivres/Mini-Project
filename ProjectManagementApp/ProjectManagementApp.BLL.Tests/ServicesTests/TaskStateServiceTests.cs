using AutoMapper;
using FakeItEasy;
using ProjectManagementApp.BLL.MappingProfiles;
using ProjectManagementApp.BLL.Services;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Xunit;

namespace ProjectManagementApp.BLL.Tests.ServicesTests
{
    public class TaskStateServiceTests
    {
        readonly IService<TaskStateDTO> _taskStateService;
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public TaskStateServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new TaskStateProfile()));
            _mapper = mappingConfig.CreateMapper();

            _unitOfWork = A.Fake<IUnitOfWork>();

            _taskStateService = new TaskStateService(_unitOfWork, _mapper);
        }

        [Fact]
        public void CreateNewTaskState_WhenCorrectArguments_ThenCreate()
        {
            // Arrange
            var newTaskState = new TaskStateDTO()
            {
                Id = 1,
                Value = "Assistance Required",
            };

            // Act
            _taskStateService.Create(newTaskState);

            // Assert
            A.CallTo(() => _unitOfWork.TaskStates.Create(A<TaskState>.That.Matches(a => a.Id == newTaskState.Id))).MustHaveHappened();

        }
    }   
}
