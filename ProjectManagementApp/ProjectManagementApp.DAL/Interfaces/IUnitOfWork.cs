using ProjectManagementApp.ProjectManagementApp.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectManagementApp.ProjectManagementApp.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<Entities.Task> Tasks { get; }
        IRepository<TaskState> TaskStates { get; }
        IRepository<Team> Teams { get; }
        IRepository<User> Users { get; }
        Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task InitializeAsync();
    }
}
