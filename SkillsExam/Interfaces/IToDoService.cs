using SkillsExam.Models;

namespace SkillsExam.Interfaces
{
    public interface IToDoService
    {
        public Task<IEnumerable<ToDo>?> GetTodoList();
    }
}
