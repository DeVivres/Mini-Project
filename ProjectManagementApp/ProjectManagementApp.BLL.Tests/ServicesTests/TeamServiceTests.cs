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
    public class TeamServiceTests
    {
        readonly IService<TeamDTO> _teamService;
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public TeamServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new TeamProfile()));
            _mapper = mappingConfig.CreateMapper();

            _unitOfWork = A.Fake<IUnitOfWork>();

            _teamService = new TeamService(_unitOfWork, _mapper);
        }

        [Fact]
        public void DeleteTeam_WhenCorrectArguments_ThenSuccess()
        {
            // Arrange
            var team = new TeamDTO
            {
                Id = 1,
                Name = "Special Team",
                CreatedAt = DateTime.Today,
            };

            // Act
            _teamService.CreateAsync(team);
            _teamService.DeleteAsync(team.Id);

            // Assert
            A.CallTo(() => _unitOfWork.Teams.DeleteAsync(1)).MustHaveHappened();
        }

        [Fact]
        public void GetAllTeams_Return2InsteadOf5()
        {
            // Arrange
            A.CallTo(() => _unitOfWork.Teams.GetAllAsync()).Returns(new List<Team>()
            {
                new Team() { Id = 1, Name = "Team1", CreatedAt = DateTime.Today },
                new Team() { Id = 2, Name = "Team2", CreatedAt = DateTime.Today },
            });

            // Act
            var result = _teamService.GetAllAsync().Result;

            // Assert
            Assert.NotEqual(5, result.Count());
        }

        [Fact]
        public void UpdateExistingTeam_WhenCorrectArguments_ThenSuccess()
        {
            // Arrange
            var initialTeam = new TeamDTO
            {
                Id = 1,
                Name = "Team to update",
                CreatedAt = DateTime.Today,

            };
            var updatedTeam = new TeamDTO
            {
                Id = 1,
                Name = "Updated Team",
                CreatedAt = DateTime.Today,
            };

            // Act
            _teamService.CreateAsync(initialTeam);
            _teamService.UpdateAsync(updatedTeam);

            // Assert
            A.CallTo(() => _unitOfWork.Teams.CreateAsync(A<Team>.That.Matches(a => a.Id == initialTeam.Id))).MustHaveHappened();
            A.CallTo(() => _unitOfWork.Teams.UpdateAsync(A<Team>.That.Matches(a => a.Id == updatedTeam.Id))).MustHaveHappened();

        }
    }
}
