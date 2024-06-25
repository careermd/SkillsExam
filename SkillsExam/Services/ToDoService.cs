using Microsoft.AspNetCore.Mvc;
using SkillsExam.Models;

namespace SkillsExam.Services
{
    public class ToDoService
				{
								private readonly string _endpoint = "https://jsonplaceholder.typicode.com";

								public ToDoService() 
								{			
												
								}	
        
        [HttpGet]
        public async Task<IEnumerable<ToDo>> GetTodoList()
        {
												throw new NotImplementedException();
        }
    }
}
