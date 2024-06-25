namespace SkillsExam.Tests
{
				public class UnitTest1
				{
								[Theory]
								[InlineData(1, "delectus aut autem", false)]
								[InlineData(4, "et porro tempora", true)]
								public void ToDoValuesShouldMatch(int id, string title, bool completed)
								{
												// write code here which hits the SkillsExam WebAPI, and loads the to-do list into this result variable
												SkillsExam.Models.ToDo result = null;
											
												Assert.Equal(id, result.Id);
												Assert.Equal(title, result.Title);
												Assert.Equal(completed, result.Completed);
								}
				}
}
