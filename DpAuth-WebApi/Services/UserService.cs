using DpAuthWebApi.Models;
using DpAuthWebApi.Repository;
using DpAuthWebApi.Services.Common;

namespace DpAuthWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<UserDocument> _dataContext;
        private readonly IConfiguration _configuration;
        public UserService(IMongoRepository<UserDocument> dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public async Task<bool> IsUsernameExist(string username)
        {
            var user = await _dataContext.FindOneAsync(filter => filter.UserName == username);

            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<UserDocument>> GetUser(string id)
        {
            ServiceResponse<UserDocument> response = new ServiceResponse<UserDocument>();

            var user = await _dataContext.FindByIdAsync(id);

            if (user == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "User not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = user;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<UserDocument>>> GetAllUsers()
        {
            ServiceResponse<IEnumerable<UserDocument>> response = new ServiceResponse<IEnumerable<UserDocument>>();

            var users = await Task.Run(() => _dataContext.AsQueryable().AsEnumerable<UserDocument>());

            if (users == null || !users.Any())
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Users not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = users;
            }

            return response;
        }
    }
}
