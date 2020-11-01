using ProjectManagementApp.ProjectManagementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace EntityFrameworkCore.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(a => a.Tasks)
                .WithOne(b => b.Project)
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Project>()
                .HasOne(a => a.Team)
                .WithMany(b => b.Projects)
                .HasForeignKey(c => c.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Project>()
                .HasOne(a => a.Author)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Team>()
                .HasMany(a => a.Users)
                .WithOne(b => b.Team)
                .HasForeignKey(c => c.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(a => a.UserTasks)
                .WithOne(b => b.Performer)
                .HasForeignKey(c => c.PerformerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var projects = Get<Project>("Projects");
            var tasks = Get<Task>("Tasks");
            var states = Get<TaskState>("TaskStates");
            var users = Get<User>("Users");
            var teams = Get<Team>("Teams");

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Team>().HasData(teams);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<Task>().HasData(tasks);
            modelBuilder.Entity<TaskState>().HasData(states);
        }

        private static List<T> Get<T>(string model)
        {
            List<T> output = null;
            using (StreamReader reader = new StreamReader($"C:/Users/User/Desktop/Academy/UnitTests-IntergrationTests/ProjectManagementApp/{model}.json"))
            {
                string json = reader.ReadToEnd();

                output = JsonConvert.DeserializeObject<List<T>>(json);
            }
            return output;
        }
    }
}
