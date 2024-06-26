using Moq;
using SkillsExam.Controllers;
using SkillsExam.Interfaces;
using SkillsExam.Models;

namespace SkillsExam.Tests.Controllers
{
    
    public class ToDoControllerTests
    {
        private readonly Mock<IToDoService> _mockToDoService;
        private readonly ToDoController _controller;

        public ToDoControllerTests()
        {
            _mockToDoService = new Mock<IToDoService>();
            _controller = new ToDoController(_mockToDoService.Object);
        }

        [Fact]
        public async Task GetToDos_ReturnsExpectedResult()
        {
            // Arrange
            var mockToDos = new List<ToDo>
            {
                new ToDo { UserId = 1, Id = 1, Title = "Test ToDo 1", Completed = false },
                new ToDo { UserId = 2, Id = 2, Title = "Test ToDo 2", Completed = true }
            };

            _mockToDoService.Setup(service => service.GetTodoList())
                .ReturnsAsync(mockToDos);

            // Act
            var result = await _controller.GetToDos();

            // Assert
            Assert.NotNull(result);
            var toDos = Assert.IsAssignableFrom<IEnumerable<ToDo>>(result);
            Assert.Equal(2, toDos.Count());
        }

        [Fact]
        public async Task GetToDos_ReturnsEmptyResult()
        {
            // Arrange
            _mockToDoService.Setup(service => service.GetTodoList())
                .ReturnsAsync(new List<ToDo>());

            // Act
            var result = await _controller.GetToDos();

            // Assert
            Assert.NotNull(result);
            var toDos = Assert.IsAssignableFrom<IEnumerable<ToDo>>(result);
            Assert.Empty(toDos);
        }

        [Fact]
        public async Task GetToDos_ReturnsNull()
        {
            // Arrange
            _mockToDoService.Setup(service => service.GetTodoList())
                .ReturnsAsync((IEnumerable<ToDo>?)null);

            // Act
            var result = await _controller.GetToDos();

            // Assert
            Assert.Null(result);
        }

    }
}
