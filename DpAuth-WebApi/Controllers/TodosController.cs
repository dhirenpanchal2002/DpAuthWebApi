using DpAuthWebApi.Contracts;
using DpAuthWebApi.Enums;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;
using DpAuthWebApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DpAuthWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IUserService _userService;
        private readonly ILogger<LeavesController> _logger;

        public TodosController(ITodoService todoService,
            IUserService userService,
            ILogger<LeavesController> logger)
        {
            _todoService = todoService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{todoId}/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetTodoDetails(string todoId)
        {
            if (string.IsNullOrWhiteSpace(todoId))
            {
                _logger.LogError("todoId is empty or invalid");
                return BadRequest("todoId is empty or invalid");
            }

            ServiceResponse<TodoDocument> response = await _todoService.GetTodoAsync(todoId); ;

            if (!response.IsSuccess)
            {
                _logger.LogError($"{response.ErrorMessage} todoId: {todoId}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {                
                TodoDetail details = new TodoDetail()
                {
                    Id = response.data.Id.ToString(),
                    UserId = response.data.UserId.ToString(),
                    CompletedOn = response.data.CompletedOn,
                    CreatedAt = response.data.CreatedAt,
                    Status = response.data.Status,                    
                    Description = response.data.Description,
                    Summary = response.data.Summary
                };

                return Ok(details);
            }
            else
            {
                _logger.LogError($"Unexpected error occured for getting details of todoId: {todoId}");
                return BadRequest($"Unexpected error occured for getting details of todoId: {todoId}");
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        public async Task<IActionResult> AddTodo(TodoDetail todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Id))
            {
                _logger.LogError("todoId is empty or invalid");
                return BadRequest("todoId is empty or invalid");
            }

            ServiceResponse<TodoDocument> response = await _todoService.GetTodoAsync(todo.Id); ;

            if (response.IsSuccess && response.data != null)
            {
                _logger.LogError("todo with given Id is already exist");
                return  Conflict("todo with given Id is already exist");
            }

            //todo not exist so Add 
            if (response.data == null)
            {
                TodoDocument todoDocument = new TodoDocument()
                {
                    UserId = todo.UserId.ToString(),
                    CompletedOn = todo.CompletedOn,                    
                    Status = todo.Status,
                    Description = todo.Description,
                    Summary = todo.Summary
                };

                var result = await _todoService.CreateTodoAsync(todoDocument);

                return Ok(result.data.ToString());
            }
            else
            {
                _logger.LogError($"Unexpected error occured while adding new todo");
                return BadRequest($"Unexpected error occured while adding new todo");
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        public async Task<IActionResult> UpdateTodo(TodoDetail todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Id))
            {
                _logger.LogError("todoId is empty or invalid");
                return BadRequest("todoId is empty or invalid");
            }

            ServiceResponse<TodoDocument> response = await _todoService.GetTodoAsync(todo.Id); ;

            if (response.IsSuccess && response.data == null)
            {
                _logger.LogError("todo with given Id does not exist");
                return Conflict("todo with given Id does not exist");
            }

            //todo not exist so Add 
            if (response.data != null)
            {
                TodoDocument todoDocument = response.data;

                todoDocument.CompletedOn = todo.CompletedOn;
                todoDocument.Status = todo.Status;
                todoDocument.Description = todo.Description;
                todoDocument.Summary = todo.Summary;

                var result = await _todoService.UpdateTodoAsync(todoDocument);

                return Ok(result.data.Id);
            }
            else
            {
                _logger.LogError($"Unexpected error occured while updating todo with Id: {todo.Id}");
                return BadRequest($"Unexpected error occured while updating todo with Id: {todo.Id}");
            }
        }

        [HttpGet("user/{userId}/todos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetUserTodos(string userId)
        {
            ServiceResponse<IEnumerable<TodoDocument>> response = await _todoService.GetTodosAsync(userId);

            if (!response.IsSuccess)
            {
                _logger.LogError($"Error occured while getting leaves for userId. {response.ErrorMessage}");

                if (response.Error == ErrorType.NotFoundError)
                    return NotFound(response.ErrorMessage);

                if (response.Error == ErrorType.GeneralError)
                    return BadRequest(response.ErrorMessage);
            }


            if (response.data != null)
            {
                var resultTeams = response.data.Select(x => new TodoDetail
                {
                    Id = x.Id.ToString(),
                    UserId = x.UserId.ToString(),
                    CompletedOn = x.CompletedOn,
                    CreatedAt = x.CreatedAt,
                    Status = x.Status,
                    Description = x.Description,
                    Summary = x.Summary
                });

                return Ok(resultTeams);
            }
            else
            {
                _logger.LogError($"Unexpeced Error occured while getting todos for userId {userId}.");
                return BadRequest($"Unexpeced Error occured while getting todos for userId {userId}");
            }
        }

        
        [HttpGet("todoStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetTodoStatuses()
        {
            var result = Enum.GetNames(typeof(TodoStatus)).ToList();
            return Ok(result);
        }

    }
}
