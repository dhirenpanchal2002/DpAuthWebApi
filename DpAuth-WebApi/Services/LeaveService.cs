using DpAuthWebApi.Models;
using DpAuthWebApi.Repository;
using DpAuthWebApi.Enums;
using DpAuthWebApi.Services.Common;
using MassTransit;
using System.Linq;

namespace DpAuthWebApi.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IMongoRepository<LeaveDocument> _dataContext;
        private readonly IConfiguration _configuration;
        public LeaveService(IMongoRepository<LeaveDocument> dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> CreateLeaveAsync(LeaveDocument leave)
        {          
            await _dataContext.InsertOneAsync(leave);

            ServiceResponse<string> response = new ServiceResponse<string> { IsSuccess = true, Error = ErrorType.None };
            response.data = leave.Id.ToString();
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<LeaveDocument>>> GetApproverLeavesAsync(string approverId)
        {
            ServiceResponse<IEnumerable<LeaveDocument>> response = new ServiceResponse<IEnumerable<LeaveDocument>>();

            var leaves = await Task.Run(() => _dataContext.FilterBy(x => x.ApproverId == approverId).AsQueryable().AsEnumerable<LeaveDocument>());

            if (leaves == null || !leaves.Any())
            {
                response.IsSuccess = false;
                response.ErrorMessage = "leaves not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = leaves;
            }

            return response;
        }

        public async Task<ServiceResponse<LeaveDocument>> GetLeaveAsync(string Id)
        {
            ServiceResponse<LeaveDocument> response = new ServiceResponse<LeaveDocument>();

            var leave = await _dataContext.FindByIdAsync(Id);

            if (leave == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Leave details not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = leave;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<LeaveDocument>>> GetUserLeavesAsync(string userId)
        {
            ServiceResponse<IEnumerable<LeaveDocument>> response = new ServiceResponse<IEnumerable<LeaveDocument>>();

            var leaves = await Task.Run(() => _dataContext.FilterBy(x => x.UserId == userId).AsQueryable().AsEnumerable<LeaveDocument>());

            if (leaves == null || !leaves.Any())
            {
                response.IsSuccess = false;
                response.ErrorMessage = "leaves not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = leaves;
            }

            return response;
        }

        public async Task<ServiceResponse<LeaveDocument>> UpdateLeaveStatusAsync(LeaveStatus status, LeaveDocument leave)
        {
            ServiceResponse<LeaveDocument> response = new ServiceResponse<LeaveDocument>();

            var result = await GetLeaveAsync(leave.Id.ToString());

            if (!result.IsSuccess)
            {
                response.IsSuccess = false;
                response.ErrorMessage = result.ErrorMessage;
                response.Error = result.Error;
            }
            else
            {
                result.data.ApprovedAt = leave.ApprovedAt;
                result.data.ApproverId = leave.ApproverId;
                result.data.Status = status;

                await _dataContext.ReplaceOneAsync(result.data);

                response = result;
            }

            return response;
        }
    }
}
