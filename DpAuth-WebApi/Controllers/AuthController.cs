using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DpAuthWebApi.Contracts;
using DpAuthWebApi.Services.Common;
using DpAuthWebApi.Services;
using DpAuthWebApi.Models;
using Microsoft.AspNetCore.Cors;
using MassTransit;
using DpAuthWebApi.Events;
using DPAuth.Messages.Abstractions.Enums;
using DPAuth.Messages.Abstractions.AuthEvents;

namespace DpAuthWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {   
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthController(IAuthService authService, ILogger<AuthController> logger, IPublishEndpoint publishEndpoint)
        {
            _authService = authService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Register(RegisterUser registerUserDto)
        {
            ServiceResponse<string> response = await _authService.Register(
                new Models.UserDocument 
                { 

                    FirstName = registerUserDto.FirstName,
                    LastName = registerUserDto.LastName,
                    UserName = registerUserDto.UserName,
                    EmailId = registerUserDto.EmailId
                }, registerUserDto.Password);

            if (!response.IsSuccess)
            {
                _logger.LogError($"Error occured while registering user. {response.ErrorMessage}");
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.data);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Login(UserLogin loginUserDto)
        {
            ServiceResponse<UserDetails> response = await _authService.Login(
                loginUserDto.UserName, loginUserDto.Password);

            if (!response.IsSuccess)
            {
                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }

            var useLoggedinEvent = new UserLoggedInEvent()
            {
                UserName = loginUserDto.UserName,
                LoggedInDevice = LoginDevice.Web
            };

            //Publish User Logged in event
            await _publishEndpoint.Publish<IUserLoggedInEvent>(useLoggedinEvent); 

            return Ok(response.data);
        }

        [HttpPost("ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> ChangePassword(UserLogin loginUser)
        {
            ServiceResponse<bool> response = await _authService.ChangePassword(
                loginUser.UserName, loginUser.Password, loginUser.NewPassword);

            if (!response.IsSuccess)
            {
                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }

            var passwordChangedEvent = new PasswordChangedEvent()
            {
                UserName = loginUser.UserName,
                NewPassword = loginUser.NewPassword
            };

            //Publish User Logged in event
            await _publishEndpoint.Publish<IPasswordChangedEvent>(passwordChangedEvent);

            return Ok(response.data);
        }

        [HttpPost("SendVerificationCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> SendVerificationCode(string emailId)
        {
            ServiceResponse<string> response = await _authService.GenerateVerificationCode(emailId);

            if (!response.IsSuccess)
            {
                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }

            var sendVerificationCodeEvent = new SendVerificationCodeEvent()
            {
                EmailId = emailId,
                VerificationCode = response.data
            };

            //Publish User Logged in event
            await _publishEndpoint.Publish<ISendVerificationCodeEvent>(sendVerificationCodeEvent);

            return Ok("Successfully sent the verification code");
        }
    }
}

