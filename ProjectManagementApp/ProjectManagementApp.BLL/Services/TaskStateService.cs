using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;

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

        public void Create(TaskStateDTO item)
        {
            _unitOfWork.TaskStates.Create(_mapper.Map<TaskState>(item));
        }

        public bool Delete(int id)
        {
            return _unitOfWork.TaskStates.Delete(id);
        }

        public TaskStateDTO Get(int id)
        {
            var taskstate = _unitOfWork.TaskStates.Get(id);
            return _mapper.Map<TaskStateDTO>(taskstate);
        }

        public IEnumerable<TaskStateDTO> GetAll()
        {
            var taskstates = _unitOfWork.TaskStates.GetAll();
            return _mapper.Map<IEnumerable<TaskStateDTO>>(taskstates);
        }

        public bool Update(TaskStateDTO item)
        {
            return _unitOfWork.TaskStates.Update(_mapper.Map<TaskState>(item));
        }
    }
}
