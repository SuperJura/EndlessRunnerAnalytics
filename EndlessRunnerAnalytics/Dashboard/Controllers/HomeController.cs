using Dashboard.Services;
using EndlessRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
	public class HomeController : Controller
	{
		IEndlessRunnerDAL dal = new EndlessRunnerDAL();
		public ActionResult Index()
		{
			List<Run> runs = dal.GetAllRuns();
			ViewBag.NumOfRuns = runs.Count;
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}