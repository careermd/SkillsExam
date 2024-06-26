using Microsoft.Extensions.Options;
using SkillsExam.Models;
using SkillsExam.Util;
using SkillsExam.Interfaces;
using System.Text.Json;

namespace SkillsExam.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly string _endpoint;

        public ToDoService(IHttpClientFactory httpClientFactory,
                           IOptions<GeneralSetting> generalSetting)
        {
            _httpClientFactory = httpClientFactory;
            _endpoint = generalSetting.Value.Endpoint;
        }

        public async Task<IEnumerable<ToDo>?> GetTodoList()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(_endpoint);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var todos = JsonSerializer.Deserialize<IEnumerable<ToDo>>(responseContent);

            return todos;
        }
    }
}
