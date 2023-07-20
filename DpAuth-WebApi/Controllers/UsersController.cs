using DpAuthWebApi.Contracts;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services;
using DpAuthWebApi.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DpAuthWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("UserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            if(string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogError("userId is empty or invalid");
                return BadRequest("userId is empty or invalid");
            }

            ServiceResponse<UserDocument> response = await _userService.GetUser(userId);

            if (!response.IsSuccess)
            {
                _logger.LogError($"{response.ErrorMessage} userId: {userId}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {
                UserDetails details = new UserDetails()
                {
                    Id = response.data.Id.ToString(),
                    FirstName = response.data.FirstName,
                    LastName = response.data.LastName,
                    EmailId = response.data.EmailId,
                    PhotoUrl = response.data.PhotoUrl,
                    CreatedAt = response.data.CreatedAt,
                    IsDeleted = response.data.IsDeleted
                };

                return Ok(details);
            }
            else
            {
                _logger.LogError($"Unexpected error occured for getting details of userId: {userId}");
                return BadRequest($"Unexpected error occured for getting details of userId: {userId}");
            }
        }

        [HttpGet("Users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAllUsers()
        {
            ServiceResponse<IEnumerable<UserDocument>> response = await _userService.GetAllUsers();

            if (!response.IsSuccess)
            {
                _logger.LogError($"{response.ErrorMessage}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {
                var resultUsers = response.data.Select(x => new UserDetails
                                                            {
                                                                Id = x.Id.ToString(),
                                                                UserName = x.UserName,
                                                                FirstName = x.FirstName,
                                                                LastName = x.LastName,
                                                                EmailId = x.EmailId,
                                                                PhotoUrl = x.PhotoUrl,
                                                                CreatedAt = x.CreatedAt,
                                                                IsDeleted = x.IsDeleted
                                                            });

                return Ok(resultUsers);
            }
            else
            {
                _logger.LogError("Unexpeced Error occured while getting all users");
                return BadRequest("Unexpeced Error occured.");
            }
        }
    }
}

