using DpAuthWebApi.Models;
using DpAuthWebApi.Repository;
using DpAuthWebApi.Enums;
using DpAuthWebApi.Services.Common;
using MassTransit;
using System.Linq;

namespace DpAuthWebApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly IMongoRepository<TodoDocument> _dataContext;
        private readonly IConfiguration _configuration;

        public TodoService(IMongoRepository<TodoDocument> dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> CreateTodoAsync(TodoDocument todo)
        {
            await _dataContext.InsertOneAsync(todo);

            ServiceResponse<string> response = new ServiceResponse<string> { IsSuccess = true, Error = ErrorType.None };
            response.data = todo.Id.ToString();
            return response;
        }

        public async Task<ServiceResponse<TodoDocument>> GetTodoAsync(string Id)
        {
            ServiceResponse<TodoDocument> response = new ServiceResponse<TodoDocument>();

            var todo = await _dataContext.FindByIdAsync(Id);

            if (todo == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Todo details not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = todo;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<TodoDocument>>> GetTodosAsync(string userId)
        {
            ServiceResponse<IEnumerable<TodoDocument>> response = new ServiceResponse<IEnumerable<TodoDocument>>();

            var todos = await Task.Run(() => _dataContext.FilterBy(x => x.UserId == userId).AsQueryable().AsEnumerable<TodoDocument>());

            if (todos == null || !todos.Any())
            {
                response.IsSuccess = false;
                response.ErrorMessage = "todos not found";
                response.Error = ErrorType.NotFoundError;
            }
            else
            {
                response.IsSuccess = true;
                response.Error = ErrorType.None;
                response.data = todos;
            }

            return response;
        }

        public async Task<ServiceResponse<TodoDocument>> UpdateTodoAsync(TodoDocument todo)
        {
            ServiceResponse<TodoDocument> response = new ServiceResponse<TodoDocument>();

            var result = await GetTodoAsync(todo.Id.ToString());

            if (!result.IsSuccess)
            {
                response.IsSuccess = false;
                response.ErrorMessage = result.ErrorMessage;
                response.Error = result.Error;
            }
            else
            {
                await _dataContext.ReplaceOneAsync(todo);

                response.IsSuccess = true;
                response.data = todo;
            }

            return response;
        }
    }
}
