using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TFPAW.Services
{
	public class ChatGPTService : IChatGPTService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public ChatGPTService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_apiKey = "sk-proj-3IkXtq6wY0jWZN1iKzaFT3BlbkFJiHlg7yk5B0NZfUNU7I2l"; // Reemplaza con tu API key real
		}

		public async Task<string> GetNextQuestion(string previousAnswer)
		{
			var requestContent = new
			{
				model = "text-davinci-003",
				prompt = $"Generate the next question based on the previous answer: {previousAnswer}",
				max_tokens = 50
			};

			var response = await _httpClient.PostAsync(
				"https://api.openai.com/v1/completions",
				new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json")
			);

			response.EnsureSuccessStatusCode();

			var responseContent = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<dynamic>(responseContent);

			return result.choices[0].text.Trim();
		}
	}
}
