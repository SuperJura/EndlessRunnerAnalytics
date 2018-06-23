using Dashboard.AOP;
using Dashboard.Services;
using EndlessRunner.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
	[LogMethod]
    public class EndGameSessionsController : Controller
    {
		IEndlessRunnerDAL dal = new EndlessRunnerDAL();
        public ActionResult Index()
        {
			return IndexWithCount(0);
        }

		public ActionResult IndexWithCount(int count)
		{
			List<EndGameSession> allSessions = dal.GetAllEndGameSessions();

			ViewBag.totalSessions = allSessions.Count;
			ViewBag.meanRetries = allSessions.Sum(x => x.RetriesCount) / (float)allSessions.Count;
			ViewBag.meanGameTime = allSessions.Sum(x => x.GameTime) / allSessions.Count;

			List<EndGameSession> sessions = count == 0 ? allSessions : allSessions.TakeLastN(count);
			ViewBag.sessions = JsonConvert.SerializeObject(sessions);
			return View("Index");
		}
	}
}