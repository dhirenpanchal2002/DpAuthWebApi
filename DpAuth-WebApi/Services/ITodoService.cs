using DpAuthWebApi.Enums;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;

namespace DpAuthWebApi.Services
{
    public interface ITodoService
    {
        Task<ServiceResponse<IEnumerable<TodoDocument>>> GetTodosAsync(string userId);

        Task<ServiceResponse<TodoDocument>> GetTodoAsync(string Id);

        Task<ServiceResponse<string>> CreateTodoAsync(TodoDocument todo);

        Task<ServiceResponse<TodoDocument>> UpdateTodoAsync(TodoDocument todo);

    }
}
