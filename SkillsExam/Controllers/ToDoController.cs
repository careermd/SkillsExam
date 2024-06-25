using Microsoft.AspNetCore.Mvc;
using SkillsExam.Models;
using SkillsExam.Services;

namespace SkillsExam.Controllers
{
    [ApiController]
				[Route("[controller]")]
				public class ToDoController : ControllerBase
				{											
								private readonly ToDoService _ToDoService;

								public ToDoController()
								{
												_ToDoService = new ToDoService();
								}

								[HttpGet]
								public IEnumerable<ToDo> GetToDos()
								{
												throw new NotImplementedException();
								}
				}
}
