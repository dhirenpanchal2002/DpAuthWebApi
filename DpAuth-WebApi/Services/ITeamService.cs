using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;

namespace DpAuthWebApi.Services
{
    public interface ITeamService
    {
        Task<bool> IsTeamNameExist(string teamName);

        Task<ServiceResponse<TeamDocument>> GetTeam(string id);

        Task<ServiceResponse<IEnumerable<TeamDocument>>> GetAllTeams();

        Task<ServiceResponse<string>> CreateTeam(TeamDocument teamDocument);

        Task<ServiceResponse<string>> UpdateTeam(TeamDocument teamDocument);
    }
}
