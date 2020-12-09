using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly DatabaseContext _context;
        public TeamRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task CreateAsync(Team item)
        {
            await _context.Teams.AddAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Teams.FirstOrDefaultAsync(a => a.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.Teams.Remove(item);
            return true;
        }

        public async Task<Team> GetAsync(int id)
        {
            return await _context.Teams.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Team item)
        {
            var exists = await _context.Teams.ContainsAsync(item);
            if(!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
