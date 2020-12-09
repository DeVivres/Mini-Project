using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class TaskStateRepository : IRepository<TaskState>
    {
        private readonly DatabaseContext _context;
        public TaskStateRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task CreateAsync(TaskState item)
        {
            await _context.TaskStates.AddAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.TaskStates.FirstOrDefaultAsync(a => a.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.TaskStates.Remove(item);
            return true;
        }

        public async Task<TaskState> GetAsync(int id)
        {
            return await _context.TaskStates.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<TaskState>> GetAllAsync()
        {
            return await _context.TaskStates.ToListAsync();
        }

        public async Task<bool> UpdateAsync(TaskState item)
        {
            var exists = await _context.TaskStates.ContainsAsync(item);
            if(!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
