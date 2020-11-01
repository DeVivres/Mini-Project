using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class TaskStateRepository : IRepository<TaskState>
    {
        private readonly DatabaseContext _context;
        public TaskStateRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Create(TaskState item)
        {
            _context.TaskStates.Add(item);
        }

        public bool Delete(int id)
        {
            var item = _context.TaskStates.FirstOrDefault(a => a.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.TaskStates.Remove(item);
            return true;
        }

        public TaskState Get(int id)
        {
            return _context.TaskStates.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<TaskState> GetAll()
        {
            return _context.TaskStates;
        }

        public bool Update(TaskState item)
        {
            var exists = _context.TaskStates.Contains(item);
            if(!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
