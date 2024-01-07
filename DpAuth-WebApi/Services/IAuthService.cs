using DpAuthWebApi.Contracts;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;

namespace DpAuthWebApi.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(UserDocument user,string password);

        Task<ServiceResponse<UserDetails>> Login(string username, string password);

        Task<bool> IsUserExist(string username);

        //Task<UserDocument> GetExistingUserExistWithEmail(string emailId);

        Task<ServiceResponse<bool>> ChangePassword(string username, string password,string newpassword);

        Task<ServiceResponse<bool>> UpdatePassword(string emailId, string verificationCode, string newpassword);

        Task<ServiceResponse<UserDocument>> GetUser(string Id);

        Task<ServiceResponse<string>> GenerateVerificationCode(string emailId);

        
    }
}
