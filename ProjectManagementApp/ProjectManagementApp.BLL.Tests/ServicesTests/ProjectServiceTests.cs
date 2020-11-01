using AutoMapper;
using FakeItEasy;
using ProjectManagementApp.BLL.MappingProfiles;
using ProjectManagementApp.BLL.Services;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectManagementApp.BLL.Tests
{
    public class ProjectServiceTests
    {
        readonly IService<ProjectDTO> _projectService;
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public ProjectServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new ProjectProfile()));
            _mapper = mappingConfig.CreateMapper();

            _unitOfWork = A.Fake<IUnitOfWork>();

            _projectService = new ProjectService(_unitOfWork, _mapper);
        }

        [Fact]
        public void AddProject_WhenCorrectArguments_ThenSuccess()
        {
            // Arrange
            var project = new ProjectDTO
            {
                Id = 1,
                Name = "Desuro",
                Description = "Financial project",
                CreatedAt = DateTime.Today,
                Deadline = DateTime.Now,
                AuthorId = 1,
                TeamId = 1,
            };

            // Act
            _projectService.Create(project);

            // Assert
            A.CallTo(() => _unitOfWork.Projects.Create(A<Project>.That.Matches(a => a.Id == project.Id))).MustHaveHappened();
        }

        [Fact]
        public void GetProjectById_WhenCorrectArguments_ThenSuccess()
        {
            // Arrange
            A.CallTo(() => _unitOfWork.Projects.Get(1))
                .Returns(new Project() { Id = 1, Name = "Project1", AuthorId = 1 });

            // Act
            var result = _projectService.Get(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetAllProjects_ThenTotalCountIs5()
        {
            // Arrange
            A.CallTo(() => _unitOfWork.Projects.GetAll()).Returns(new List<Project>()
            {
                new Project() { Id = 1, Name = "Project1", AuthorId = 1 },
                new Project() { Id = 2, Name = "Project2", AuthorId = 3 },
                new Project() { Id = 3, Name = "Project3", AuthorId = 5 },
                new Project() { Id = 4, Name = "Project4", AuthorId = 4 },
                new Project() { Id = 5, Name = "Project5", AuthorId = 1 },
            });

            // Act
            var result = _projectService.GetAll();

            // Assert
            Assert.Equal(5, result.Count());
        }
    }
}
