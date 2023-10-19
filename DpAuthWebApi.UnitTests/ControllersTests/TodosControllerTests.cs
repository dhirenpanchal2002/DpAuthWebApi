using Moq;
using DpAuthWebApi.Controllers;
using DpAuthWebApi.Services;
using Microsoft.Extensions.Logging;
using DpAuthWebApi.Models;
using DpAuthWebApi.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NuGet.Frameworks;
using System.Net;

namespace DpAuthWebApi.UnitTests.ControllersTests
{
    public class TodosControllerTests
    {
        private string dummyCurrentUserId = "testUser123";
        private Mock<ITodoService> _mockTodoService;
        private Mock<IUserService>  _mockUserService;
        private Mock<ILogger<LeavesController>> _mockLogger;
        private IEnumerable<TodoDocument> _dummyTodoDocuments;

        private TodosController _sut;

        public TodosControllerTests()
        {
            _mockTodoService = new Mock<ITodoService>();
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<LeavesController>>();
            _dummyTodoDocuments = new List<TodoDocument>() 
                { 
                    new TodoDocument() 
                    { 
                        Id = MongoDB.Bson.ObjectId.GenerateNewId(),
                        Summary="todo1", 
                        Description = "",
                        UserId= dummyCurrentUserId 
                    },
                    new TodoDocument()
                    {
                        Id = MongoDB.Bson.ObjectId.GenerateNewId(),
                        Summary="todo2",
                        Description = "Todo desription 2",
                        UserId= dummyCurrentUserId
                    },
                };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "Test User"),
                new Claim(ClaimTypes.NameIdentifier, dummyCurrentUserId),
                new Claim("custom-claim", "example claim value"),
            }, "mock"));

            _sut = new TodosController(_mockTodoService.Object, _mockUserService.Object, _mockLogger.Object);

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public async Task When_GetCurrentUserTodos_successful_should_return_all_todos_For_LoggedinUser()
        {
            // Arrange
            _mockTodoService.Setup(x => x.GetTodosAsync(dummyCurrentUserId)).ReturnsAsync(new ServiceResponse<IEnumerable<TodoDocument>>() { data = _dummyTodoDocuments, IsSuccess = true});

            // act
            var result = await _sut.GetCurrentUserTodos();            
            var okResult = result as OkObjectResult;

            // assert
            _mockTodoService.Verify(x => x.GetTodosAsync(dummyCurrentUserId), Times.Once);
            
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public async Task When_GetCurrentUserTodos_unsuccessful_should_return_general_error()
        {
            // Arrange
            _mockTodoService.Setup(x => x.GetTodosAsync(dummyCurrentUserId)).ReturnsAsync(new ServiceResponse<IEnumerable<TodoDocument>>() { data = null, IsSuccess = false });

            // act
            var result = await _sut.GetCurrentUserTodos();
            var objectResult = result as ObjectResult;

            // assert
            _mockTodoService.Verify(x => x.GetTodosAsync(dummyCurrentUserId), Times.Once);

            Assert.NotNull(objectResult);
            Assert.Equal(400, objectResult?.StatusCode);
        }

    }
}