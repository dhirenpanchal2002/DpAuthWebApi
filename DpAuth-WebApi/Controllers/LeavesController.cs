using DpAuthWebApi.Contracts;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;
using DpAuthWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DpAuthWebApi.Enums;

namespace DpAuthWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly ILeaveService _leaveService;
        private readonly IUserService _userService;
        private readonly ILogger<LeavesController> _logger;

        public LeavesController(ILeaveService leaveService,
            IUserService userService,
            ILogger<LeavesController> logger)
        {
            _leaveService = leaveService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{leaveId}/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetLeaveDetails(string leaveId)
        {
            if (string.IsNullOrWhiteSpace(leaveId))
            {
                _logger.LogError("leaveId is empty or invalid");
                return BadRequest("leaveId is empty or invalid");
            }

            ServiceResponse<LeaveDocument> response = await _leaveService.GetLeaveAsync(leaveId); ;

            if (!response.IsSuccess)
            {
                _logger.LogError($"{response.ErrorMessage} leaveId: {leaveId}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {
                var ApproverResponse = await _userService.GetUser(response.data.ApproverId);

                if (!ApproverResponse.IsSuccess)
                {
                    _logger.LogError($"Error occured while getting Approver details. {response.ErrorMessage} leaveId: {leaveId}");

                    return BadRequest(response.ErrorMessage);
                }

                LeaveDetail details = new LeaveDetail()
                {
                    Id = response.data.Id.ToString(),
                    UserId = response.data.UserId.ToString(),
                    StartDate = response.data.StartDate,
                    EndDate = response.data.EndDate,
                    CreatedAt = response.data.CreatedAt,
                    Status = response.data.Status,
                    Type = response.data.Type,
                    leaveDay = response.data.LeaveDay,
                    Description = response.data.Description,

                    Approver = new UserDetails()
                    {
                        Id = ApproverResponse.data.Id.ToString(),
                        FirstName = ApproverResponse.data.FirstName,
                        LastName = ApproverResponse.data.LastName,
                        EmailId = ApproverResponse.data.EmailId
                    },
                    ApprovedAt = response.data.ApprovedAt
                };

                return Ok(details);
            }
            else
            {
                _logger.LogError($"Unexpected error occured for getting details of leaveId: {leaveId}");
                return BadRequest($"Unexpected error occured for getting details of leaveId: {leaveId}");
            }
        }

        [HttpGet("user/{userId}/leaves")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetUserLeaves(string userId)
        {
            ServiceResponse<IEnumerable<LeaveDocument>> response = await _leaveService.GetUserLeavesAsync(userId);

            if (!response.IsSuccess)
            {
                _logger.LogError($"Error occured while getting leaves for userId. {response.ErrorMessage}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {
                var resultTeams = response.data.Select(x => new LeaveDetail
                {
                    Id = x.Id.ToString(),
                    UserId = x.UserId.ToString(),
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                    Type = x.Type,
                    leaveDay = x.LeaveDay,
                    Description = x.Description,

                    Approver = new UserDetails()
                    {
                        Id = x.ApproverId.ToString()
                    },
                    ApprovedAt = x.ApprovedAt
                });

                return Ok(resultTeams);
            }
            else
            {
                _logger.LogError($"Unexpeced Error occured while getting leaves for userId {userId}.");
                return BadRequest($"Unexpeced Error occured while getting leaves for userId {userId}");
            }
        }

        [HttpGet("approver/{approverId}/leaves")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetApproverLeaves(string approverId)
        {
            ServiceResponse<IEnumerable<LeaveDocument>> response = await _leaveService.GetApproverLeavesAsync(approverId);

            if (!response.IsSuccess)
            {
                _logger.LogError($"Error occured while getting leaves for approverId. {response.ErrorMessage}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {
                var resultTeams = response.data.Select(x => new LeaveDetail
                {
                    Id = x.Id.ToString(),
                    UserId = x.UserId.ToString(),
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                    Type = x.Type,
                    leaveDay = x.LeaveDay,
                    Description = x.Description,

                    Approver = new UserDetails()
                    {
                        Id = x.ApproverId.ToString()
                    },
                    ApprovedAt = x.ApprovedAt
                });

                return Ok(resultTeams);
            }
            else
            {
                _logger.LogError($"Unexpeced Error occured while getting leaves for approverId {approverId}.");
                return BadRequest($"Unexpeced Error occured while getting leaves for approverId {approverId}");
            }
        }

        [HttpGet("/leaveTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetLeaveTypes()
        {
            var result = Enum.GetNames(typeof(LeaveType)).ToList();
            return Ok(result);
        }

        [HttpGet("/leaveStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetLeaveStatuses()
        {
            var result = Enum.GetNames(typeof(LeaveStatus)).ToList();
            return Ok(result);
        }

        [HttpGet("/leaveCategories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetLeaveCategories()
        {
            var result = Enum.GetNames(typeof(LeaveCategory)).ToList();
            return Ok(result);
        }
    }
}
