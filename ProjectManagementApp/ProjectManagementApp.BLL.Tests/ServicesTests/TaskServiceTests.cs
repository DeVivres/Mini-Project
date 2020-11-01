using AutoMapper;
using FakeItEasy;
using ProjectManagementApp.BLL.MappingProfiles;
using ProjectManagementApp.BLL.Services;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System;
using Xunit;

namespace ProjectManagementApp.BLL.Tests
{
    public class TaskServiceTests
    {
        readonly IService<TaskDTO> _taskService;
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public TaskServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new TaskProfile()));
            _mapper = mappingConfig.CreateMapper();

            _unitOfWork = A.Fake<IUnitOfWork>();

            _taskService = new TaskService(_unitOfWork, _mapper);
        }

        [Fact]
        public void DeleteTask_WhenCorrectArgument_ThenSuccess()
        {
            // Arrange
            var task = new TaskDTO
            {
                Id = 1,
                Name = "Test",
                Description = "Test project",
                CreatedAt = DateTime.Today,
                FinishedAt = DateTime.Now,
                State = 1,
                ProjectId = 1,
                PerformerId = 1,
            };

            // Act
            _taskService.Create(task);
            _taskService.Delete(task.Id);

            // Assert
            A.CallTo(() => _unitOfWork.Tasks.Delete(1)).MustHaveHappened();
        }

        [Fact]
        public void GetTaskById_WhenWrongArguments_ThenFail()
        {
            // Arrange
            A.CallTo(() => _unitOfWork.Tasks.Get(1))
                .Returns(new Task() { Id = 1, Name = "Task1", Description = "Special Task" });

            // Act
            var result = _taskService.Get(2);

            // Assert
            Assert.NotEqual(1, result.Id);
        }

        [Fact]
        public void UpdateExistingTask_WhenCorrectArguments_ThenSuccess()
        {
            // Arrange
            var initialTask = new TaskDTO
            {
                Id = 1,
                Name = "Task to update",
                Description = "First task",
                CreatedAt = DateTime.Today,
                FinishedAt = DateTime.Now,
                State = 1,
                ProjectId = 1,
                PerformerId = 1,
            };
            var updatedTask = new TaskDTO
            {
                Id = 1,
                Name = "Updated Task",
                Description = "Second task",
                CreatedAt = DateTime.Today,
                FinishedAt = DateTime.Now,
                State = 2,
                ProjectId = 1,
                PerformerId = 1,
            };

            // Act
            _taskService.Create(initialTask);
            _taskService.Update(updatedTask);

            // Assert
            A.CallTo(() => _unitOfWork.Tasks.Create(A<Task>.That.Matches(a => a.Id == initialTask.Id))).MustHaveHappened();
            A.CallTo(() => _unitOfWork.Tasks.Update(A<Task>.That.Matches(a => a.Id == updatedTask.Id))).MustHaveHappened();
        }
    }
}
