using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using ProjectManagementApp.ProjectManagementApp.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.ProjectManagementApp.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _context;
        private ProjectRepository projectRepository;
        private TaskRepository taskRepository;
        private TaskStateRepository taskStateRepository;
        private TeamRepository teamRepository;
        private UserRepository userRepository;
        bool disposed;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            disposed = false;
        }

        public async System.Threading.Tasks.Task InitializeAsync()
        {
            var projects = await Projects.GetAllAsync();
            var teams = await Teams.GetAllAsync();
            var users = await Users.GetAllAsync();
            var tasks = await Tasks.GetAllAsync();
            projects
            .Join(teams, a => a.TeamId, b => b.Id, (a, b) =>
            {
                a.Team = b; return a;
            })
            .Join(users, a => a.AuthorId, b => b.Id, (a, b) =>
            {
                a.Author = b; return a;
            })
            .GroupJoin(tasks, a => a.Id, b => b.ProjectId, (project, taskList) =>
            {
                project.Tasks = taskList.Join(users, a => a.PerformerId, b => b.Id, (a, b) =>
                {
                    a.Performer = b;
                    return a;
                }).ToList();
                return project;
            }).ToList();
        }

        public IRepository<Project> Projects
        {
            get
            {
                if(projectRepository == null)
                {
                    projectRepository = new ProjectRepository(_context);
                }
                return projectRepository;
            }
        }

        public IRepository<Entities.Task> Tasks
        {
            get
            {
                if (taskRepository == null)
                {
                    taskRepository = new TaskRepository(_context);
                }
                return taskRepository;
            }
        }

        public IRepository<TaskState> TaskStates
        {
            get
            {
                if (taskStateRepository == null)
                {
                    taskStateRepository = new TaskStateRepository(_context);
                }
                return taskStateRepository;
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                if (teamRepository == null)
                {
                    teamRepository = new TeamRepository(_context);
                }
                return teamRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_context);
                }
                return userRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
