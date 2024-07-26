using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TFPAW.Services;

namespace TFPAW.Web.Controllers
{
	public class AkinatorController : Controller
	{
		private readonly IChatGPTService _chatGptService;

		public AkinatorController(IChatGPTService chatGptService)
		{
			_chatGptService = chatGptService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> GetQuestion([FromBody] string previousAnswer)
		{
			var question = await _chatGptService.GetNextQuestion(previousAnswer);
			return Json(new { question });
		}
	}
}
