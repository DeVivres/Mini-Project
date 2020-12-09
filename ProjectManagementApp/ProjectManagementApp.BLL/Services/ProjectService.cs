using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<bool> CreateAsync(ProjectDTO item)
        {
            await _unitOfWork.Projects.CreateAsync(_mapper.Map<Project>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _unitOfWork.Projects.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<ProjectDTO> GetAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetAsync(id);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllAsync()
        {
            var projects = await _unitOfWork.Projects.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }

        public async Task<bool> UpdateAsync(ProjectDTO item)
        {
            await _unitOfWork.Projects.UpdateAsync(_mapper.Map<Project>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
