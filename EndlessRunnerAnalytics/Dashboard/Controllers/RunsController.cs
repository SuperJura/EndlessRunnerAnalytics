using Dashboard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class RunsController : Controller
    {
        // GET: Runs
        public ActionResult Index()
        {
			ViewBag.RunURL = EndlessRunnerDAL.RUNS_PATH;
			return View();
        }
    }
}