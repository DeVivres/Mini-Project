using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.BLL.Services
{
    public class TaskService : IService<TaskDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(TaskDTO item)
        {
            await _unitOfWork.Tasks.CreateAsync(_mapper.Map<ProjectManagementApp.Entities.Task>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _unitOfWork.Tasks.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<TaskDTO> GetAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetAsync(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<IEnumerable<TaskDTO>> GetAllAsync()
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public async Task<bool> UpdateAsync(TaskDTO item)
        {
            await _unitOfWork.Tasks.UpdateAsync(_mapper.Map<ProjectManagementApp.Entities.Task>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
