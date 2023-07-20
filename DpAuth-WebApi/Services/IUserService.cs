using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;

namespace DpAuthWebApi.Services
{
    public interface IUserService
    {
        Task<bool> IsUsernameExist(string username);

        Task<ServiceResponse<UserDocument>> GetUser(string Id);

        Task<ServiceResponse<IEnumerable<UserDocument>>> GetAllUsers();
    }
}
