using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using ProjectManagementApp.ProjectManagementApp.Repositories;
using System;
using System.Linq;

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

        public void Initialize()
        {
            Projects.GetAll()
            .Join(Teams.GetAll(), a => a.TeamId, b => b.Id, (a, b) =>
            {
                a.Team = b; return a;
            })
            .Join(Users.GetAll(), a => a.AuthorId, b => b.Id, (a, b) =>
            {
                a.Author = b; return a;
            })
            .GroupJoin(Tasks.GetAll(), a => a.Id, b => b.ProjectId, (project, taskList) =>
            {
                project.Tasks = taskList.Join(Users.GetAll(), a => a.PerformerId, b => b.Id, (a, b) =>
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

        public IRepository<Task> Tasks
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

        public int SaveChanges()
        {
            return _context.SaveChanges();
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
