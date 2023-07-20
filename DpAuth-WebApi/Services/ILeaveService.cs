using DpAuthWebApi.Enums;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;

namespace DpAuthWebApi.Services
{
    public interface ILeaveService
    {
        Task<ServiceResponse<IEnumerable<LeaveDocument>>> GetApproverLeavesAsync(string approverId);

        Task<ServiceResponse<LeaveDocument>> GetLeaveAsync(string Id);

        Task<ServiceResponse<string>> CreateLeaveAsync(LeaveDocument leave);

        Task<ServiceResponse<LeaveDocument>> UpdateLeaveStatusAsync(LeaveStatus status, LeaveDocument leave);

        Task<ServiceResponse<IEnumerable<LeaveDocument>>> GetUserLeavesAsync(string userId);
    }
}

