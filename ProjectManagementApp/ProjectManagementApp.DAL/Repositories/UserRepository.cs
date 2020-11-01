using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        public bool Delete(int id)
        {
            var item = _context.Users.FirstOrDefault(a => a.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.Users.Remove(item);
            return true;
        }

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public bool Update(User item)
        {
            var exists = _context.Users.Contains(item);

            if(!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
