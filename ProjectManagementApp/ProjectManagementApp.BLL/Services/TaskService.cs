using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;

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

        public void Create(TaskDTO item)
        {
            _unitOfWork.Tasks.Create(_mapper.Map<Task>(item));
        }

        public bool Delete(int id)
        {
            return _unitOfWork.Tasks.Delete(id);
        }

        public TaskDTO Get(int id)
        {
            var task = _unitOfWork.Tasks.Get(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            var tasks = _unitOfWork.Tasks.GetAll();
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public bool Update(TaskDTO item)
        {
            return _unitOfWork.Tasks.Update(_mapper.Map<Task>(item));
        }
    }
}
