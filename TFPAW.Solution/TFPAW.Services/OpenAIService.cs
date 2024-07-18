using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Completions;

namespace TFPAW.Services
{
    public class OpenAIService
    {
        private readonly string _apiKey;

        public OpenAIService(IConfiguration configuration)
        {
            _apiKey = configuration["sk-proj-3IkXtq6wY0jWZN1iKzaFT3BlbkFJiHlg7yk5B0NZfUNU7I2l"];
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            var openAiClient = new OpenAIAPI(_apiKey);
            var completionRequest = new CompletionRequest
            {
                Prompt = prompt,
                MaxTokens = 100,
                Temperature = 0.7
            };
            var completionResult = await openAiClient.Completions.CreateCompletionAsync(completionRequest);

            return completionResult.Completions.First().Text.Trim();
        }
    }
}
