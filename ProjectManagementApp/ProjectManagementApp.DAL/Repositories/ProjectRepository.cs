using System.Collections.Generic;
using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly DatabaseContext _context;
        public ProjectRepository() { }
        public ProjectRepository(DatabaseContext context)
        {
            _context = context;
        }
            
        public async System.Threading.Tasks.Task CreateAsync(Project item)
        {
            await _context.Projects.AddAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Projects.FirstOrDefaultAsync(a => a.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.Projects.Remove(item);
            return true;
        }

        public async Task<Project> GetAsync(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Project item)
        {
            var exists = await _context.Projects.ContainsAsync(item);
            if (!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
