using Microsoft.Extensions.Options;
using Moq.Protected;
using Moq;
using SkillsExam.Models;
using SkillsExam.Services;
using SkillsExam.Util;
using System.Net;
using System.Text.Json;

namespace SkillsExam.Tests.Services
{
    public class ToDoServiceTests
    {
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
        private readonly Mock<IOptions<GeneralSetting>> _mockOptions;
        private readonly ToDoService _service;

        public ToDoServiceTests()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockOptions = new Mock<IOptions<GeneralSetting>>();

            _mockOptions.Setup(opt => opt.Value).Returns(new GeneralSetting { Endpoint = "https://jsonplaceholder.typicode.com/todos" });

            _service = new ToDoService(_mockHttpClientFactory.Object, _mockOptions.Object);
        }

        [Fact]
        public async Task GetTodoList_ReturnsExpectedResult()
        {
            // Arrange
            var mockToDos = new List<ToDo>
        {
            new ToDo { UserId = 1, Id = 1, Title = "Test ToDo 1", Completed = false },
            new ToDo { UserId = 2, Id = 2, Title = "Test ToDo 2", Completed = true }
        };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(mockToDos)),
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await _service.GetTodoList();

            // Assert
            Assert.NotNull(result);
            var toDos = Assert.IsAssignableFrom<IEnumerable<ToDo>>(result);
            Assert.Equal(2, toDos.Count());
        }

        [Fact]
        public async Task GetTodoList_ReturnsEmptyResult()
        {
            // Arrange
            var mockToDos = new List<ToDo>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(mockToDos)),
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await _service.GetTodoList();

            // Assert
            Assert.NotNull(result);
            var toDos = Assert.IsAssignableFrom<IEnumerable<ToDo>>(result);
            Assert.Empty(toDos);
        }

        [Fact]
        public async Task GetTodoList_HandlesNullResult()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("null"),
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await _service.GetTodoList();

            // Assert
            Assert.Null(result);
        }
    }
}
