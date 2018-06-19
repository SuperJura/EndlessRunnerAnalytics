using Dashboard.AOP;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
	[LogMethod]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}