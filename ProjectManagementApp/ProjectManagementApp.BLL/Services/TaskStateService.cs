using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.BLL.Services
{
    public class TaskStateService : IService<TaskStateDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskStateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(TaskStateDTO item)
        {
            await _unitOfWork.TaskStates.CreateAsync(_mapper.Map<TaskState>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _unitOfWork.TaskStates.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<TaskStateDTO> GetAsync(int id)
        {
            var taskstate = await _unitOfWork.TaskStates.GetAsync(id);
            return _mapper.Map<TaskStateDTO>(taskstate);
        }

        public async Task<IEnumerable<TaskStateDTO>> GetAllAsync()
        {
            var taskstates = await _unitOfWork.TaskStates.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskStateDTO>>(taskstates);
        }

        public async Task<bool> UpdateAsync(TaskStateDTO item)
        {
            await _unitOfWork.TaskStates.UpdateAsync(_mapper.Map<TaskState>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
