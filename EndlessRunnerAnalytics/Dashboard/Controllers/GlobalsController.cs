using Dashboard.Services;
using EndlessRunner.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class GlobalsController : Controller
	{
		IEndlessRunnerDAL dal = new EndlessRunnerDAL();
		// GET: Globals
		public ActionResult Index()
		{
			float timePlayedInSeconds = 0;
			List<EndGameSession> sessions = dal.GetAllEndGameSessions();
			for (int i = 0; i < sessions.Count; i++)
			{
				timePlayedInSeconds += sessions[i].GameTime;
			}
			TimeSpan timePlayed = TimeSpan.FromSeconds(timePlayedInSeconds);

			int numOfPickups = 0;
			List<Pickup> pickups = dal.GetAllPickups();
			for (int i = 0; i < pickups.Count; i++)
			{
				numOfPickups += pickups[i].PickupCount;
			}

			ViewBag.NumOfRuns = dal.GetAllRuns().Count;
			ViewBag.NumOfPickups = numOfPickups;
			ViewBag.TimePlayed = timePlayed.ToString(@"hh\:mm\:ss");

			return View();
        }
    }
}