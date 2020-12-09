using Newtonsoft.Json;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ProjectManagementApp.WebAPI.Tests.Tests
{
    public class ControllersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly string _base;
        private readonly HttpClient _client;

        public ControllersTests(CustomWebApplicationFactory<Startup> factory)
        {
            _base = "https://localhost:44361/api";
            _client = factory.CreateClient();
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateProject_WhenModelCorrect_ThenSuccess()
        {
            var project = new Project()
            {
                Name = "Test project",
                Description = "Desctiption desctiption desctiption",
                AuthorId = 1,
                TeamId = 1,
                CreatedAt = new DateTime(2010, 4, 10),
                Deadline = new DateTime(2020, 4, 10)
            };

            var json = JsonConvert.SerializeObject(project);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_base}/project", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateProject_WhenWrongArguments_ThenBadRequest()
        {
            var project = new Project()
            {
                Name = "Test project",
            };

            var json = JsonConvert.SerializeObject(project);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_base}/project", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteUser_ThenSuccess()
        {
            var user = new User()
            {
                FirstName = "Tom",
                LastName = "Varko",
                Email = "tomVarko@gmail.com",
                RegisteredAt = new DateTime(2010, 4, 10),
                TeamId = 1,
                Birthday = new DateTime(1997, 4, 19),
            };

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responsePost = await _client.PostAsync($"{_base}/user", content);

            Assert.Equal(HttpStatusCode.Created, responsePost.StatusCode);

            var responseDelete = await _client.DeleteAsync($"{_base}/user/{1}");

            Assert.Equal(HttpStatusCode.NoContent, responseDelete.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetProjectsInfo()
        {
            var result = _client.GetAsync($"{_base}/linq/GetProjectsInfo").Result.Content;
            var json = await result.ReadAsStringAsync();
            var finalResult = JsonConvert.DeserializeObject<List<ProjectsInfoDTO>>(json);

            Assert.Equal(100, finalResult.Count);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTeam_ThenSuccess()
        {
            var team = new Team()
            {
                Name = "Fellons",   
            };

            var json = JsonConvert.SerializeObject(team);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_base}/team", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTasksFinished2020ForSpecificUser()
        {
            var result = _client.GetAsync($"{_base}/linq/GetTasksFinished2020ForSpecificUser/{2}").Result.Content;
            var json = await result.ReadAsStringAsync();
            var finalResult = JsonConvert.DeserializeObject<List<(int, string)>>(json);
            
            Assert.Equal(4, finalResult.Count);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTask_ThenSuccess()
        {
            var task = new ProjectManagementApp.Entities.Task()
            {
                Name = "Task to do",
                Description = "Fix logging",
                CreatedAt = new DateTime(2020, 1, 20),
                FinishedAt = new DateTime(2020, 3, 10),
                PerformerId = 1,
                ProjectId = 1, 
                State = 0,
            };

            var json = JsonConvert.SerializeObject(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_base}/team", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteTask_WhenIdNotProvided_ThenBadRequest()
        {
            var response = await _client.DeleteAsync($"{_base}/team/{1}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
