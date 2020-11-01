using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;

namespace ProjectManagementApp.BLL.Services
{
    public class UserService : IService<UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(UserDTO item)
        {
            _unitOfWork.Users.Create(_mapper.Map<User>(item));
        }

        public bool Delete(int id)
        {
            return _unitOfWork.Users.Delete(id);
        }

        public UserDTO Get(int id)
        {
            var user = _unitOfWork.Users.Get(id);
            return _mapper.Map<UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public bool Update(UserDTO item)
        {
            return _unitOfWork.Users.Update(_mapper.Map<User>(item));
        }
    }
}
