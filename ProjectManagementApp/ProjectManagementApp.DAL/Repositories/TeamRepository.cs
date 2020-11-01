using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly DatabaseContext _context;
        public TeamRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Create(Team item)
        {
            _context.Teams.Add(item);
        }

        public bool Delete(int id)
        {
            var item = _context.Teams.FirstOrDefault(a => a.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.Teams.Remove(item);
            return true;
        }

        public Team Get(int id)
        {
            return _context.Teams.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Team> GetAll()
        {
            return _context.Teams;
        }

        public bool Update(Team item)
        {
            var exists = _context.Teams.Contains(item);
            if(!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
