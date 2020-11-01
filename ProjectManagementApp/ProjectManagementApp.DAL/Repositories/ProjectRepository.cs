using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApp.ProjectManagementApp.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly DatabaseContext _context;
        public ProjectRepository(DatabaseContext context)
        {
            _context = context;
        }
            
        public void Create(Project item)
        {
            _context.Projects.Add(item);
        }

        public bool Delete(int id)
        {
            var item = _context.Projects.FirstOrDefault(a => a.Id == id);
            if(item == null)
            {
                return false;
            }
            _context.Projects.Remove(item);
            return true;
        }

        public Project Get(int id)
        {
            return _context.Projects.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects;
        }

        public bool Update(Project item)
        {
            var exists = _context.Projects.Contains(item);
            if (!exists)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
