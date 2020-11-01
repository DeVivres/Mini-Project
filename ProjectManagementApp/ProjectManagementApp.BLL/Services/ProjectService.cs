using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;

namespace ProjectManagementApp.BLL.Services
{
    public class ProjectService : IService<ProjectDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(ProjectDTO item)
        {
            _unitOfWork.Projects.Create(_mapper.Map<Project>(item));
            _unitOfWork.SaveChanges();
        }

        public bool Delete(int id)
        {
            _unitOfWork.Projects.Delete(id);
            _unitOfWork.SaveChanges();
            return true;
        }

        public ProjectDTO Get(int id)
        {
            var project = _unitOfWork.Projects.Get(id);
            return _mapper.Map<ProjectDTO>(project);
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            var projects = _unitOfWork.Projects.GetAll();
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }

        public bool Update(ProjectDTO item)
        {
            _unitOfWork.Projects.Update(_mapper.Map<Project>(item));
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
