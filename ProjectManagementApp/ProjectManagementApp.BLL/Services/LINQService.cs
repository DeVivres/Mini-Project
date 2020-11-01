using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            _unitOfWork.Initialize();
        }           

        public IDictionary<ProjectDTO, int> GetNumberOfTasksOfTheUserInTheProject(int authorId)
        {
            _unitOfWork.Initialize();
            return _unitOfWork.Projects.GetAll().Where(p => p.AuthorId == authorId)
                                                .Select(p => new { project = p, tasksAmount = _unitOfWork.Tasks.GetAll()
                                                .Count(task => task.ProjectId == p.Id)})
                                                .ToDictionary(x => _mapper.Map<ProjectDTO>(x.project), x => x.tasksAmount);
        }

        public IEnumerable<TaskDTO> GetNumberOfTaskOfTheUserWhereNameLessThen45Letters(int performerId)
        {
            var result = _unitOfWork.Tasks.GetAll()
                .Where(a => a.PerformerId == performerId && a.Name.Length < 45)
                .ToList();
            return _mapper.Map<IEnumerable<TaskDTO>>(result);
        }

        public IEnumerable<(int, string)> GetTasksFinished2020ForSpecificUser(int performerId)
        {
            var result =  _unitOfWork.Tasks.GetAll()
                .Where(a => a.PerformerId == performerId && a.FinishedAt.Year == 2020)
                .Select(a => (a.Id, a.Name))
                .ToList();
            return result;
        }

        public IEnumerable<(int, string, IEnumerable<UserDTO>)> GetTeamsWhereAgeIsAtLeast10()
        {
            return _unitOfWork.Teams.GetAll()
                .Join(_unitOfWork.Users.GetAll()
                    .Where(a => a.Birthday.Year < 2010 && a.TeamId != null)
                    .OrderByDescending(a => a.RegisteredAt)
                    .GroupBy(a => a.TeamId)
                    .ToDictionary(b => b.Key, b => b.ToList()),
                c => c.Id,
                b => b.Key,
                (c, b) => (c.Id, c.Name, _mapper.Map<IEnumerable<UserDTO>>(b.Value))).ToList();
        }

        public IEnumerable<(UserDTO, IEnumerable<TaskDTO>)> GetUsersByFirstNameWithTasksSortedByNameLength()
        {
            var result = _unitOfWork.Users.GetAll()
                .OrderBy(u => u.FirstName)
                .GroupJoin(_unitOfWork.Tasks.GetAll(), u => u.Id, t => t.PerformerId,
                    (user, usersTasks) => (_mapper.Map<UserDTO>(user), _mapper.Map<IEnumerable<TaskDTO>>(usersTasks.OrderByDescending(t => t.Name.Length).ToList())))
                .ToList();
            return result;
        }

        public UsersInfoDTO GetUserInfo(int userId)
        {
            var last = _unitOfWork.Projects.GetAll()
                .Where(a => a.Id == userId)
                .OrderByDescending(b => b.CreatedAt)
                .FirstOrDefault();

            var result = new UsersInfoDTO()
            {
                userInfo = _mapper.Map<UserDTO>(_unitOfWork.Users.GetAll().Where(a => a.Id == userId)
                                                                          .FirstOrDefault()),
                lastProject = _mapper.Map<ProjectDTO>(last),
                tasksAmount = last.Tasks.Count(),
                notFinishedTasks = _unitOfWork.Tasks.GetAll().Where(a => a.Id == userId)
                                             .Count(b => b.State == 1 || b.State == 3),
                theLongestTask = _mapper.Map<TaskDTO>(_unitOfWork.Tasks.GetAll().Where(a => a.Id == userId)
                                                                                .OrderBy(b => b.FinishedAt - b.CreatedAt)
                                                                                .LastOrDefault()),
            };
            return _mapper.Map<UsersInfoDTO>(result);
        }

        public IEnumerable<ProjectsInfoDTO> GetProjectsInfo()
        {
            var result = _unitOfWork.Projects.GetAll()
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
