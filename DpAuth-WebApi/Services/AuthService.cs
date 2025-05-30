﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using DpAuthWebApi.Repository;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;
using DpAuthWebApi.Contracts;
using System.Text.Unicode;
using MongoDB.Bson;
using MongoDB.Libmongocrypt;
using System.Buffers.Text;

namespace DpAuthWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMongoRepository<UserDocument> _dataContext;
        private readonly IConfiguration _configuration;
        public AuthService(IMongoRepository<UserDocument> dataContext,IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public async Task<bool> IsUserExist(string username)
        {
            var user = await _dataContext.FindOneAsync(filter => filter.UserName == username);

            if (user != null)
            { 
                return true;
            }
            return false;
        }

        private async Task<UserDocument> GetExistingUserExistWithEmail(string emailId)
        {
            var user = await _dataContext.FindOneAsync(filter => filter.EmailId == emailId);

            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<ServiceResponse<bool>> UpdatePassword(string emailId, string verificationCode, string newpassword)
        {
            var user = await GetExistingUserExistWithEmail(emailId);

            if (user == null)
            {
                return new ServiceResponse<bool> { data = false, IsSuccess = false, Error = ErrorType.ValidationError ,  ErrorMessage = "Missing or invalid email" };
            }

            if (!VerifyPasswordHash(verificationCode, Convert.FromBase64String(user.PwdHash), Convert.FromBase64String(user.PwdSalt)))
            {
                return new ServiceResponse<bool> { data = false, IsSuccess = false, Error = ErrorType.GeneralError, ErrorMessage = "Missing or invalid verification code" };
            }
            else
            {
                CreatePasswordHash(newpassword, out byte[] passwordHash, out byte[] passwordSalt);

                user.PwdHash = Convert.ToBase64String(passwordHash, 0, passwordHash.Length);
                user.PwdSalt = Convert.ToBase64String(passwordSalt, 0, passwordSalt.Length);
                user.IsVerificationCodeSet = true;

                await _dataContext.ReplaceOneAsync(user);

                return new ServiceResponse<bool> { data = true, IsSuccess = true, Error = ErrorType.None, ErrorMessage = null };
            }
        }

        public async Task<ServiceResponse<UserDetails>> Login(string username, string password)
        {
            ServiceResponse<UserDetails> response = new ServiceResponse<UserDetails>();

            if(!await IsUserExist(username))
            {
                response.IsSuccess = false;
                response.ErrorMessage = "User not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                //User user = await _dataContext.tbl_Users.FirstOrDefaultAsync(c => c.UserName.ToLower() == username.ToLower());
                var user = await _dataContext.FindOneAsync(filter => filter.UserName == username);

                if (user == null)
                {
                    return new ServiceResponse<UserDetails>("User not found", ErrorType.NotFoundError);
                }

                if (!VerifyPasswordHash(password, Convert.FromBase64String(user.PwdHash), Convert.FromBase64String(user.PwdSalt)))
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Missing or invalid login details";
                    response.Error = ErrorType.GeneralError;
                }
                else
                {
                    response.IsSuccess = true;
                    response.data = new UserDetails() 
                    { 
                        Id = user.Id.ToString(),
                        UserName = user.UserName,
                        AuthToken = CreateToken(user),
                        EmailId = user.EmailId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        IsVerificationCodeSet = user.IsVerificationCodeSet,
                        IsDeleted = user.IsDeleted
                    };
                    response.ErrorMessage = "Successfully logged in";
                }
            }

            return response;
        }
        public async Task<ServiceResponse<string>> Register(UserDocument user, string password)
        {
            if(await IsUserExist(user.UserName))
            {
                return new ServiceResponse<string> { data = null, IsSuccess = false, ErrorMessage = "User already exists" };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PwdHash = Convert.ToBase64String(passwordHash, 0, passwordHash.Length);
            user.PwdSalt = Convert.ToBase64String(passwordSalt, 0, passwordSalt.Length);
            user.IsVerificationCodeSet = false;

            await _dataContext.InsertOneAsync(user);

            ServiceResponse<string> response = new ServiceResponse<string> { IsSuccess = true, Error = ErrorType.None };
            response.data = user.Id.ToString();
            return response;
        }
        public async Task<ServiceResponse<bool>> ChangePassword(string username, string password, string newpassword)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            if (!await IsUserExist(username))
            {
                response.IsSuccess = false;
                response.ErrorMessage = "User not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                var user = await _dataContext.FindOneAsync(filter => filter.UserName == username);

                if (!VerifyPasswordHash(password, Convert.FromBase64String(user.PwdHash), Convert.FromBase64String(user.PwdSalt)))
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Wrong Password, authentication failed.";
                    response.Error = ErrorType.GeneralError;
                }
                else
                {
                    CreatePasswordHash(newpassword, out byte[] passwordHash, out byte[] passwordSalt);

                    user.PwdHash = Convert.ToBase64String(passwordHash, 0, passwordHash.Length);
                    user.PwdSalt = Convert.ToBase64String(passwordSalt, 0, passwordSalt.Length);
                    user.IsVerificationCodeSet = false;

                    await _dataContext.ReplaceOneAsync(user);

                    response.IsSuccess = true;
                    response.data = true;
                }
            }

            return response;
        }
        public async Task<ServiceResponse<string>> GenerateVerificationCode(string emailId)
        {
            var user = await GetExistingUserExistWithEmail(emailId);

            if (user == null)
            {
                return new ServiceResponse<string> { data = null, IsSuccess = false, ErrorMessage = "User does not exists" };
            }

            var VerificationCode = RandomNumberGenerator.GetInt32(100001, 999999);
            
            CreatePasswordHash(VerificationCode.ToString(), out byte[] passwordHash, out byte[] passwordSalt);

            user.PwdHash = Convert.ToBase64String(passwordHash, 0, passwordHash.Length);
            user.PwdSalt = Convert.ToBase64String(passwordSalt, 0, passwordSalt.Length);
            user.IsVerificationCodeSet = true;

            await _dataContext.ReplaceOneAsync(user);

            ServiceResponse<string> response = new ServiceResponse<string> { IsSuccess = true, Error = ErrorType.None };
            response.data = VerificationCode.ToString();
            return response;
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
            //Private method to generate the random Salt and then generate the hash based on the salt.
        private void CreatePasswordHash(string password,out  byte[] pwdHash,out  byte[] pwdSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                pwdSalt = hmac.Key;
                pwdHash = hmac.ComputeHash(System.Text.Encoding.Unicode.GetBytes(password));
            }
        }

        //Private method to verify the user pssword using db stored Salt and db stored password hash.
        private bool VerifyPasswordHash(string password, byte[] pwdHash, byte[] pwdSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(pwdSalt))
            {
                byte[] UserpwdHash = hmac.ComputeHash(System.Text.Encoding.Unicode.GetBytes(password));
                if (UserpwdHash.Length != pwdHash.Length)
                    return false;

                for (int i = 0; i < UserpwdHash.Length; i++)
                    if (UserpwdHash[i] != pwdHash[i])
                        return false;

                return true;
            }
        }

        private string CreateToken(UserDocument user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.EmailId),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            SymmetricSecurityKey key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials cred = new SigningCredentials(key1, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor desc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = cred
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(desc);

            return tokenHandler.WriteToken(token);
        }
    }
}
