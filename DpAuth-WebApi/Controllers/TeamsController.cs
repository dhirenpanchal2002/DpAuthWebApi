using DpAuthWebApi.Contracts;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services;
using DpAuthWebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace DpAuthWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        private readonly ILogger<TeamsController> _logger;

        public TeamsController(ITeamService teamService, 
            IUserService userService,
            ILogger<TeamsController> logger)
        {
            _teamService = teamService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{teamId}/Details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetTeamDetails(string teamId)
        {
            if (string.IsNullOrWhiteSpace(teamId))
            {
                _logger.LogError("TeamId is empty or invalid");
                return BadRequest("TeamId is empty or invalid");
            }

            ServiceResponse<TeamDocument> response = await _teamService.GetTeam(teamId);

            if (!response.IsSuccess)
            {
                _logger.LogError($"{response.ErrorMessage} teamId: {teamId}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {
                var teamLead = await _userService.GetUser(response.data.LeadUserId);

                if(!teamLead.IsSuccess)
                {
                    _logger.LogError($"Error occured while getting team Lead details. {response.ErrorMessage} teamId: {teamId}");

                    return BadRequest(response.ErrorMessage);
                }

                TeamDetails details = new TeamDetails()
                {
                    Id = response.data.Id.ToString(),
                    TeamName = response.data.TeamName,
                    TeamEmailId = response.data.TeamEmailId,                    
                    CreatedAt = response.data.CreatedAt,
                    IsDeleted = response.data.IsDeleted,
                    TeamLead = new UserDetails()
                    {
                        Id = teamLead.data.Id.ToString(),
                        FirstName = teamLead.data.FirstName,
                        LastName = teamLead.data.LastName,
                        EmailId = teamLead.data.EmailId,
                        PhotoUrl = teamLead.data.PhotoUrl
                    }
                };

                return Ok(details);
            }
            else
            {
                _logger.LogError($"Unexpected error occured for getting details of teamId: {teamId}");
                return BadRequest($"Unexpected error occured for getting details of teamId: {teamId}");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAllTeams()
        {
            ServiceResponse<IEnumerable<TeamDocument>> response = await _teamService.GetAllTeams();

            if (!response.IsSuccess)
            {
                _logger.LogError($"Error occured while getting all teams. {response.ErrorMessage}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {
                var resultTeams = response.data.Select(x => new TeamDetails
                {
                    Id = x.Id.ToString(),
                    TeamName = x.TeamName,
                    TeamEmailId = x.TeamEmailId,
                    CreatedAt = x.CreatedAt,
                    IsDeleted = x.IsDeleted
                });

                return Ok(resultTeams);
            }
            else
            {
                _logger.LogError("Unexpeced Error occured while getting all teams.");
                return BadRequest("Unexpeced Error occured while getting all teams");
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Create(TeamDetails team)
        {
            ServiceResponse<string> response = await _teamService.CreateTeam (
                new Models.TeamDocument
                {
                    TeamName = team.TeamName,
                    TeamEmailId = string.IsNullOrWhiteSpace(team.TeamEmailId) ? string.Empty : team.TeamEmailId,
                    LeadUserId = string.IsNullOrWhiteSpace(team.TeamLead.Id) ? string.Empty : team.TeamLead.Id,
                    MemberIds = team.TeamMembers?.Select(x => x.Id).ToList()
                });

            if (!response.IsSuccess)
            {
                _logger.LogError($"Error occured while creatin team. {response.ErrorMessage}");
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.data);
        }


        [HttpPut("Update/{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Update(TeamDetails team)
        {
            ServiceResponse<string> response = await _teamService.UpdateTeam(
                new Models.TeamDocument
                {
                    Id = ObjectId.Parse(team.Id),
                    TeamName = team.TeamName,
                    TeamEmailId = string.IsNullOrWhiteSpace(team.TeamEmailId) ? string.Empty : team.TeamEmailId,
                    LeadUserId = string.IsNullOrWhiteSpace(team.TeamLead.Id) ? string.Empty : team.TeamLead.Id,
                    MemberIds = team.TeamMembers?.Select(x => x.Id).ToList()
                });

            if (!response.IsSuccess)
            {
                _logger.LogError($"Error occured while updating the team. {response.ErrorMessage}");
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.data);
        }
    }
}

