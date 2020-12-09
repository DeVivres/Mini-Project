using AutoMapper;
using FakeItEasy;
using ProjectManagementApp.BLL.MappingProfiles;
using ProjectManagementApp.BLL.Services;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectManagementApp.BLL.Tests
{
    public class LINQServiceTests
    {
        readonly LINQService _LINQService;
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public LINQServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProjectProfile());
                mc.AddProfile(new ProjectsInfoProfile());
                mc.AddProfile(new TaskProfile());
                mc.AddProfile(new TaskStateProfile());
                mc.AddProfile(new TeamProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new UsersInfoProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _unitOfWork = A.Fake<IUnitOfWork>();

            _LINQService = new LINQService(_unitOfWork, _mapper);
        }

        public void ReceiveData()
        {
            A.CallTo(() => _unitOfWork.Projects.GetAllAsync()).Returns(new List<Project>()
            {
                new Project() { Id = 1, Name = "Project1", AuthorId = 1, Description = "We need to do this project asap", CreatedAt = new System.DateTime(2010, 4, 10),
                    Tasks = new List<Task>()
                {
                    new Task () { Id = 1, Name = "Task 1", ProjectId = 1, PerformerId = 1, Description = "Long task"},
                    new Task () { Id = 2, Name = "Task 2(Special)", ProjectId = 1, PerformerId = 2, Description = "Urgent"},
                    new Task () { Id = 3, Name = "Task Late", ProjectId = 1, PerformerId = 3, Description = "Very big and important task"}
                }},
                new Project() { Id = 2, Name = "Project2", AuthorId = 3, Description = "Eco project",CreatedAt = new System.DateTime(2004, 10, 12),
                    Tasks = new List<Task>()
                {
                    new Task () { Id = 1, Name = "Task Defere", ProjectId = 2, PerformerId = 1, Description = "Long"},
                    new Task () { Id = 2, Name = "Task Tarabarak", ProjectId = 2, PerformerId = 2, Description = "Middle"},
                    new Task () { Id = 3, Name = "Task Deo", ProjectId = 2, PerformerId = 3,  Description = "Task about finances"}
                }},
                new Project() { Id = 3, Name = "Project", AuthorId = 3, Description = "Unique tech project", CreatedAt = new System.DateTime(2001, 09, 19),
                    Tasks = new List<Task>()
                {
                    new Task () { Id = 1, Name = "Task Du", ProjectId = 3, PerformerId = 1, Description = "Task about money"},
                    new Task () { Id = 2, Name = "Task Dekro", ProjectId = 3, PerformerId = 2, Description = "Short task" },
                    new Task () { Id = 3, Name = "Task Taskerutonos", ProjectId = 3, PerformerId = 3, Description = "Later to do" }
                }},
            });
            A.CallTo(() => _unitOfWork.Tasks.GetAllAsync()).Returns(new List<Task>()
            {
                new Task() { Id = 1, Name = "Important Task", FinishedAt = new System.DateTime(2020, 5, 30), PerformerId = 2 },
                new Task() { Id = 2, Name = "Urgent", FinishedAt = new System.DateTime(2015, 2, 25), PerformerId = 3 },
                new Task() { Id = 3, Name = "Now", FinishedAt = new System.DateTime(2018, 4, 11), PerformerId = 5 },
                new Task() { Id = 4, Name = "Later Task", FinishedAt = new System.DateTime(2020, 6, 1), PerformerId = 1 },
                new Task() { Id = 5, Name = "Soon", FinishedAt = new System.DateTime(2020, 7, 20), PerformerId = 4 }
            });
            A.CallTo(() => _unitOfWork.Users.GetAllAsync()).Returns(new List<User>()
            {
                new User() { Id = 1, FirstName = "Tom", TeamId = 1, Birthday = new System.DateTime(1997, 4, 23)},
                new User() { Id = 2, FirstName = "Max", TeamId = 1, Birthday = new System.DateTime(2004, 5, 14)},
                new User() { Id = 3, FirstName = "John", TeamId = 4, Birthday = new System.DateTime(2007, 1, 18)},
                new User() { Id = 4, FirstName = "Ann", TeamId = 5, Birthday = new System.DateTime(2000, 9, 29)},
                new User() { Id = 5, FirstName = "Mark", TeamId = 3, Birthday = new System.DateTime(2015, 7, 11)},
            });
            A.CallTo(() => _unitOfWork.Teams.GetAllAsync()).Returns(new List<Team>()
            {
                new Team() { Id = 1, Name = "Team1" },
                new Team() { Id = 2, Name = "Team2" },
                new Team() { Id = 3, Name = "Team3" },
                new Team() { Id = 4, Name = "Team4" },
                new Team() { Id = 5, Name = "Team5" },
            });
        }

        [Fact]
        public void GetNumberOfTasksOfTheUser()
        {
            ReceiveData();
            
            var result = _LINQService.GetNumberOfTasksOfTheUserInTheProjectAsync(3).Result;

            Assert.Equal(2, result.Count());

        }

        [Fact]
        public void GetNumberOfTaskOfTheUserWhereNameLessThen45Letters_ThenReturn2()
        {
            ReceiveData();

            var result = _LINQService.GetNumberOfTaskOfTheUserWhereNameLessThen45LettersAsync(1).Result;

            Assert.Equal(1, result.ElementAt(0).PerformerId);
        }

        [Fact]
        public void GetTasksFinished2020ForSpecificUser_ThenReturn1TasksFinishedIn2020()
        {
            ReceiveData();

            var result = _LINQService.GetTasksFinished2020ForSpecificUserAsync(4).Result;

            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void GetTeamsWhereAgeIsAtLeast10_ThenReturn3Teams()
        {
            ReceiveData();

            var result = _LINQService.GetTeamsWhereAgeIsAtLeast10Async().Result;

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetUsersByFirstNameWithTasksSortedByNameLength_ThenCorrectListReturns()
        {
            ReceiveData();

            var result = _LINQService.GetUsersByFirstNameWithTasksSortedByNameLengthAsync().Result;
            
            Assert.Equal(5, result.Count());
            Assert.True(result.ElementAt(0).Item1.Id == 4);
            Assert.True(result.ElementAt(0).Item2.ElementAt(0).Name == "Soon");
            Assert.True(result.ElementAt(4).Item1.Id == 1);
            Assert.True(result.ElementAt(4).Item2.ElementAt(0).Name == "Later Task");
        }

        [Fact]
        public void GetUserInfo_ThenReturnCorrectInfo()
        {
            ReceiveData();

            var result = _LINQService.GetUserInfoAsync(1).Result;

            Assert.Equal(0, result.notFinishedTasks);
            Assert.Equal(3, result.tasksAmount);
            Assert.Equal("Tom", result.userInfo.FirstName);
        }

        [Fact]
        public void GetProjectsInfo_ThenReturnCorrectInfo()
        {
            ReceiveData();

            var result = _LINQService.GetProjectsInfoAsync().Result;

            Assert.Equal(3, result.ToList().Count);
            Assert.True(result.ElementAt(0).Project.Name == "Project1");
            Assert.True(result.ElementAt(1).TaskWithLongestDescription.Id == 3);
            Assert.True(result.ElementAt(2).MembersCountByTasksCountAndProjDescLength == 0);
        }
    }
}
