using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TFPAW.Services;

namespace TFPAW.Web.Controllers
{
    public class AkinatorController : Controller
    {
        private readonly OpenAIService _openAIService;
        private static List<string> conversationHistory = new List<string>();
        private static int questionNumber = 1;
        private static string guessedFruit = null;

        public AkinatorController(OpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public IActionResult Index()
        {
            ViewBag.History = conversationHistory;
            ViewBag.Question = "Piensa en una fruta y yo intentaré adivinar cuál es. Responde a mis preguntas.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(string userInput)
        {
            conversationHistory.Add($"Usuario: {userInput}");

            string prompt = BuildPrompt(conversationHistory);
            string response = await _openAIService.GetResponseAsync(prompt);

            conversationHistory.Add($"AI: {response}");

            ViewBag.History = conversationHistory;
            ViewBag.Question = response;

            questionNumber++;

            if (questionNumber > 10 || response.Contains("Creo que la fruta es"))
            {
                guessedFruit = response;
                return View("Result");
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult Reset()
        {
            conversationHistory.Clear();
            questionNumber = 1;
            guessedFruit = null;
            return RedirectToAction("Index");
        }

        private string BuildPrompt(List<string> conversation)
        {
            StringBuilder promptBuilder = new StringBuilder();
            promptBuilder.AppendLine("Vamos a jugar a un juego. Piensa en una fruta y yo intentaré adivinar cuál es. Responde a mis preguntas.");

            foreach (var entry in conversation)
            {
                promptBuilder.AppendLine(entry);
            }

            if (conversation.Count == 0)
            {
                promptBuilder.AppendLine("Comenzaré con una pregunta simple.");
            }
            else
            {
                promptBuilder.AppendLine("Basándome en tus respuestas anteriores, aquí está mi próxima pregunta:");
            }

            return promptBuilder.ToString();
        }
    }
}
