using AutoMapper;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Entities;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using System.Collections.Generic;

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

        public void Create(TeamDTO item)
        {
            _unitOfWork.Teams.Create(_mapper.Map<Team>(item));
        }

        public bool Delete(int id)
        {
            return _unitOfWork.Teams.Delete(id);
        }

        public TeamDTO Get(int id)
        {
            var team = _unitOfWork.Teams.Get(id);
            return _mapper.Map<TeamDTO>(team);
        }

        public IEnumerable<TeamDTO> GetAll()
        {
            var teams = _unitOfWork.Teams.GetAll();
            return _mapper.Map<IEnumerable<TeamDTO>>(teams);
        }

        public bool Update(TeamDTO item)
        {
            return _unitOfWork.Teams.Update(_mapper.Map<Team>(item));
        }
    }
}
