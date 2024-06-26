using Microsoft.AspNetCore.Mvc;
using SkillsExam.Models;
using SkillsExam.Interfaces;

namespace SkillsExam.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ToDoController : ControllerBase
	{
		private readonly IToDoService _ToDoService;

        public ToDoController(IToDoService toDoService)
        {
            _ToDoService = toDoService;
        }

        [HttpGet]
		public async Task<IEnumerable<ToDo>?> GetToDos()
		{
			return await _ToDoService.GetTodoList();
		}
	}
}
