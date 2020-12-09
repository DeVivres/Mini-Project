using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.BLL.Services
{
    public class TeamService : IService<TeamDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(TeamDTO item)
        {
            await _unitOfWork.Teams.CreateAsync(_mapper.Map<Team>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _unitOfWork.Teams.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<TeamDTO> GetAsync(int id)
        {
            var team = await _unitOfWork.Teams.GetAsync(id);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task<IEnumerable<TeamDTO>> GetAllAsync()
        {
            var teams = await _unitOfWork.Teams.GetAllAsync();
            return _mapper.Map<IEnumerable<TeamDTO>>(teams);
        }

        public async Task<bool> UpdateAsync(TeamDTO item)
        {
            await _unitOfWork.Teams.UpdateAsync(_mapper.Map<Team>(item));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
