using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.BLL.Services
{
    public class LINQService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LINQService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }           

        public async Task<IDictionary<ProjectDTO, int>> GetNumberOfTasksOfTheUserInTheProjectAsync(int authorId)
        {
            await _unitOfWork.InitializeAsync();                                                                        
            var projects = await _unitOfWork.Projects.GetAllAsync();
            var tasks = await _unitOfWork.Tasks.GetAllAsync();
            return projects.Where(p => p.AuthorId == authorId)
                           .Select(p => new { project = p, tasksAmount = 
                                tasks.Count(task => task.ProjectId == p.Id)})
                           .ToDictionary(x => _mapper.Map<ProjectDTO>(x.project), x => x.tasksAmount);
        }

        public async Task<IEnumerable<TaskDTO>> GetNumberOfTaskOfTheUserWhereNameLessThen45LettersAsync(int performerId)
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();
            var result = tasks.Where(a => a.PerformerId == performerId && a.Name.Length < 45)
                              .ToList();
            return _mapper.Map<IEnumerable<TaskDTO>>(result);
        }

        public async Task<IEnumerable<(int, string)>> GetTasksFinished2020ForSpecificUserAsync(int performerId)
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();
            var result = tasks.Where(a => a.PerformerId == performerId && a.FinishedAt.Year == 2020)
                              .Select(a => (a.Id, a.Name))
                              .ToList();
            return result;
        }

        public async Task<IEnumerable<(int, string, IEnumerable<UserDTO>)>> GetTeamsWhereAgeIsAtLeast10Async()
        {
            var teams = await _unitOfWork.Teams.GetAllAsync();
            var users = await _unitOfWork.Users.GetAllAsync();
            return teams.Join(users.Where(a => a.Birthday.Year < 2010 && a.TeamId != null)
                        .OrderByDescending(a => a.RegisteredAt)
                        .GroupBy(a => a.TeamId)
                        .ToDictionary(b => b.Key, b => b.ToList()),
                            c => c.Id,
                            b => b.Key,
                            (c, b) => (c.Id, c.Name, _mapper.Map<IEnumerable<UserDTO>>(b.Value))).ToList();
        }

        public async Task<IEnumerable<(UserDTO, IEnumerable<TaskDTO>)>> GetUsersByFirstNameWithTasksSortedByNameLengthAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            var tasks = await _unitOfWork.Tasks.GetAllAsync();
            var result = users.OrderBy(u => u.FirstName)
                              .GroupJoin(tasks, u => u.Id, t => t.PerformerId,
                                (user, usersTasks) => (_mapper.Map<UserDTO>(user), _mapper.Map<IEnumerable<TaskDTO>>(usersTasks.OrderByDescending(t => t.Name.Length).ToList())))
                              .ToList();
            return result;
        }

        public async Task<UsersInfoDTO> GetUserInfoAsync(int userId)
        {
            await _unitOfWork.InitializeAsync();
            var projects = await _unitOfWork.Projects.GetAllAsync();
            var last = projects.Where(a => a.Id == userId)
                               .OrderByDescending(b => b.CreatedAt)
                               .FirstOrDefault();

            var users = await _unitOfWork.Users.GetAllAsync();
            var tasks = await _unitOfWork.Tasks.GetAllAsync();
            var result = new UsersInfoDTO()
            {
                userInfo = _mapper.Map<UserDTO>(users.Where(a => a.Id == userId)
                                                                          .FirstOrDefault()),
                lastProject = _mapper.Map<ProjectDTO>(last),
                tasksAmount = last.Tasks.Count(),
                notFinishedTasks = tasks.Where(a => a.Id == userId)
                                             .Count(b => b.State == 1 || b.State == 3),
                theLongestTask = _mapper.Map<TaskDTO>(tasks.Where(a => a.Id == userId)
                                                                                .OrderBy(b => b.FinishedAt - b.CreatedAt)
                                                                                .LastOrDefault()),
            };
            return _mapper.Map<UsersInfoDTO>(result);
        }

        public async Task<IEnumerable<ProjectsInfoDTO>> GetProjectsInfoAsync()
        {
            await _unitOfWork.InitializeAsync();
            var projects = await _unitOfWork.Projects.GetAllAsync();
            var result = projects
                .Select(a => new ProjectsInfoDTO()
                {
                    Project = _mapper.Map<ProjectDTO>(a),
                    TaskWithLongestDescription = _mapper.Map<TaskDTO>(a.Tasks.OrderByDescending(b => b.Description).FirstOrDefault()),
                    TaskWithShortestName = _mapper.Map<TaskDTO>(a.Tasks.OrderBy(b => b.Name).FirstOrDefault()),
                    MembersCountByTasksCountAndProjDescLength = a.Description.Length > 20 || a.Tasks.Count > 3 ? a.Tasks.Select(b => b.Performer).Distinct().Count() : 0
                }).ToList();
            return _mapper.Map<IEnumerable<ProjectsInfoDTO>>(result);
        }
    }
}
