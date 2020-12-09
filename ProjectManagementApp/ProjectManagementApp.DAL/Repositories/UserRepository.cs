using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task CreateAsync(User item)
        {
            await _context.Users.AddAsync(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Users.FirstOrDefaultAsync(a => a.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.Users.Remove(item);
            return true;
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> UpdateAsync(User item)
        {
            var exists = await _context.Users.ContainsAsync(item);
            if(!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
