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
    public class UserServiceTests
    {
        readonly IService<UserDTO> _userService;
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public UserServiceTests()
        {
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new UserProfile()));
            _mapper = mappingConfig.CreateMapper();

            _unitOfWork = A.Fake<IUnitOfWork>();

            _userService = new UserService(_unitOfWork, _mapper);
        }

        [Fact]
        public void AddUser_WhenCorrectArguments_ThenSuccess()
        {
            // Arrange
            var user = new UserDTO
            {
                Id = 1,
                FirstName = "Tom",
                LastName = "Tonwos",
                Email = "test@gmail.com",
                Birthday = DateTime.Parse("20.09.1997"), 
                RegisteredAt = DateTime.Now,         
                TeamId = 1,
            };

            // Act
            _userService.CreateAsync(user);

            // Assert
            A.CallTo(() => _unitOfWork.Users.CreateAsync(A<User>.That.Matches(a => a.Id == user.Id))).MustHaveHappened();
        }

        [Fact]
        public void DeleteUser_WhenCorrectArgument_ThenSuccess()
        {
            // Arrange
            var user = new UserDTO
            {
                Id = 1,
                FirstName = "Tom",
                LastName = "Tonwos",
                Email = "test@gmail.com",
                Birthday = DateTime.Parse("20.09.1997"),
                RegisteredAt = DateTime.Now,
                TeamId = 1,
            };

            // Act
            _userService.CreateAsync(user);
            _userService.DeleteAsync(user.Id);

            // Assert
            A.CallTo(() => _unitOfWork.Users.DeleteAsync(1)).MustHaveHappened();
        }       
    }
}
