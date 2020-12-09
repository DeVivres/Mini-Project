using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<bool> CreateAsync(UserDTO item)
        {
            await _unitOfWork.Users.CreateAsync(_mapper.Map<User>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _unitOfWork.Users.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<bool> UpdateAsync(UserDTO item)
        {
            await _unitOfWork.Users.UpdateAsync(_mapper.Map<User>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
