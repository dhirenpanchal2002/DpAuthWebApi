using DpAuthWebApi.Models;
using DpAuthWebApi.Repository;
using DpAuthWebApi.Services.Common;

namespace DpAuthWebApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly IMongoRepository<TeamDocument> _dataContext;
        private readonly IConfiguration _configuration;
        public TeamService(IMongoRepository<TeamDocument> dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public async Task<bool> IsTeamNameExist(string teamName)
        {
            var team = await _dataContext.FindOneAsync(x => x.TeamName == teamName);

            if (team != null && team.IsDeleted == false)
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<TeamDocument>> GetTeam(string id)
        {
            ServiceResponse<TeamDocument> response = new ServiceResponse<TeamDocument>();

            var team = await _dataContext.FindByIdAsync(id);

            if (team == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Team not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = team;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<TeamDocument>>> GetAllTeams()
        {
            ServiceResponse<IEnumerable<TeamDocument>> response = new ServiceResponse<IEnumerable<TeamDocument>>();

            var teams = await Task.Run(() => _dataContext.AsQueryable().AsEnumerable<TeamDocument>());

            if (teams == null || !teams.Any())
            {
                response.IsSuccess = false;
                response.ErrorMessage = "teams not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = teams;
            }

            return response;
        }


        public async Task<ServiceResponse<string>> CreateTeam(TeamDocument teamDocument)
        {
            if (await IsTeamNameExist(teamDocument.TeamName))
            {
                return new ServiceResponse<string>
                {
                    IsSuccess = false,
                    Error = ErrorType.ValidationError,
                    ErrorMessage = $"Team already exist with name {teamDocument.TeamName}",
                    data = teamDocument.Id.ToString()
                };
            }

            await _dataContext.InsertOneAsync(teamDocument);

            ServiceResponse<string> response = new ServiceResponse<string> { IsSuccess = true, Error = ErrorType.None };
            response.data = teamDocument.Id.ToString();
            return response;

        }

        public async Task<ServiceResponse<string>> UpdateTeam(TeamDocument teamDocument)
        {
            var team = await _dataContext.FindOneAsync(x => x.Id == teamDocument.Id);

            if (team == null || team.IsDeleted)
            {
                return new ServiceResponse<string>
                {
                    IsSuccess = false,
                    Error = ErrorType.ValidationError,
                    ErrorMessage = $"Team does not exist with Id: {teamDocument.Id}",
                    data = teamDocument.Id.ToString()
                };
            }

            await _dataContext.ReplaceOneAsync(teamDocument);

            return new ServiceResponse<string>
            {
                IsSuccess = true,
                Error = ErrorType.None,
                data = teamDocument.Id.ToString()
            };
        }
    }
}
