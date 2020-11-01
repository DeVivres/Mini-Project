using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly DatabaseContext _context;
        public TaskRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Create(Task item)
        {
            _context.Tasks.Add(item);
        }

        public bool Delete(int id)
        {
            var item = _context.Tasks.FirstOrDefault(a => a.Id == id);
            if (item == null)
            {
                return false;
            }
            _context.Tasks.Remove(item);
            return true;
        }

        public Task Get(int id)
        {
            return _context.Tasks.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Task> GetAll()
        {
            return _context.Tasks;
        }

        public bool Update(Task item)
        {
            var exists = _context.Tasks.Contains(item);
            if(!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified; 
            return true;
        }
    }
}
