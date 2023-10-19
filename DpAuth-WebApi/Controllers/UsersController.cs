using System.Web;
using Azure.Storage.Blobs;
using DpAuthWebApi.Contracts;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services;
using DpAuthWebApi.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static MassTransit.ValidationResultExtensions;

namespace DpAuthWebApi.Controllers
{
    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string ProfileContainerName;
        public UsersController(IUserService userService, ILogger<UsersController> logger, BlobServiceClient blobServiceClient)
        {
            _userService = userService;
            _logger = logger;
            _blobServiceClient = blobServiceClient;
            ProfileContainerName = "dpauth-profiles";
        }

        [HttpGet("UserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetCurrentUserDetails(string userId)
        {
            if(string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogError("CurrentUserId is empty or invalid");
                return BadRequest("CurrentUserId is empty or invalid");
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

        [HttpPost("ProfilePicture")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> UploadUserProfilePicture(IFormFile uploadedFile)
        {
            var request = HttpContext.Request;
            const int maxContentLength = 512 * 512 * 512;

            try
            {
                if (request.ContentLength > maxContentLength || request.Form.Files.Count == 0)
                    return BadRequest("Invalid upload file");

                var pictureFile = request.Form.Files[0];

                //Call upload to blob service
                
                var filePath = Path.Join(Directory.GetCurrentDirectory(),  @"\tempFiles\", DateTime.UtcNow.ToString("yyyyMMddHHmm") + "_" + pictureFile.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await pictureFile.CopyToAsync(stream);
                }

                await UploadFileToContainerAsync(filePath);

                var result = "Successful";

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogError($"Internal server error while uploading profile picture. {e.Message}");

                return  StatusCode(500, $"Internal server error while uploading profile picture");
            }
        }

        private async Task UploadFileToContainerAsync(string localFilePath)
        {
            string fileName = Path.GetFileName(localFilePath);

            var containerClient = _blobServiceClient.GetBlobContainerClient(ProfileContainerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(localFilePath, true);
        }
    }
}

