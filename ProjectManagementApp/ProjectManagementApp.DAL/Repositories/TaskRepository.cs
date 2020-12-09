using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class TaskRepository : IRepository<Entities.Task>
    {
        private readonly DatabaseContext _context;
        public TaskRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task CreateAsync(Entities.Task item)
        {
            await _context.Tasks.AddAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Tasks.FirstOrDefaultAsync(a => a.Id == id);
            if (item == null)
            {
                return false;
            }
            _context.Tasks.Remove(item);
            return true;
        }

        public async Task<Entities.Task> GetAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Entities.Task>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Entities.Task item)
        {
            var exists = await _context.Tasks.ContainsAsync(item);
            if(!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified; 
            return true;
        }
    }
}
