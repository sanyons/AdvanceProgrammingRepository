using Microsoft.AspNetCore.Mvc;

namespace TFPAW.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
