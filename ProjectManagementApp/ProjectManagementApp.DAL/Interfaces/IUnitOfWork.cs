using ProjectManagementApp.ProjectManagementApp.Entities;
using System;

namespace ProjectManagementApp.ProjectManagementApp.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<Task> Tasks { get; }
        IRepository<TaskState> TaskStates { get; }
        IRepository<Team> Teams { get; }
        IRepository<User> Users { get; }
        int SaveChanges();
        void Initialize();
    }
}
