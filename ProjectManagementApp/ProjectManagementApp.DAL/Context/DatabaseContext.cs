using ProjectManagementApp.ProjectManagementApp.Entities;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.DAL.Context;

namespace ProjectManagementApp.ProjectManagementApp.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options) { }

        public DbSet<Project> Projects { get; private set; }
        public DbSet<Task> Tasks { get; private set; }
        public DbSet<TaskState> TaskStates { get; private set; }
        public DbSet<Team> Teams { get; private set; } 
        public DbSet<User> Users { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
            modelBuilder.Seed();
        }
    }
}
