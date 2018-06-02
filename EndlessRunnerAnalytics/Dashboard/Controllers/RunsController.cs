using Dashboard.Services;
using EndlessRunner.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dashboard.Models;
using Newtonsoft.Json;

namespace Dashboard.Controllers
{
    public class RunsController : Controller
    {
		IEndlessRunnerDAL dal = new EndlessRunnerDAL();

        public ActionResult Index()
        {
			List<Run> allRuns = dal.GetAllRuns();

			float meanScore = allRuns.Sum(x => x.Score) / (float)allRuns.Count;

			int maxCoins = 0;
			foreach(var item in allRuns)
			{
				maxCoins += item.Pickups.Where(x => x.PickupName == "Coin").Sum(x => x.PickupCount);
			}

			float meanCoins = maxCoins / (float)allRuns.Count;

			ViewBag.runPickups = JsonConvert.SerializeObject(GetAllPickupsInRuns(allRuns));
			ViewBag.meanTime = meanScore;
			ViewBag.meanCoins = meanCoins;
			ViewBag.totalRuns = allRuns.Count;

			return View();
        }

		public ActionResult RunDetails(int id)
		{
			Run run = dal.GetRunById(id);
			return View(run);
		}

		private List<RunPickupsData> GetAllPickupsInRuns(List<Run> allRuns)
		{
			List<RunPickupsData> runPickups = new List<RunPickupsData>();

			for(int i = 0; i < allRuns.Count; i++)
			{
				Run run = allRuns[i];

				RunPickupsData data = new RunPickupsData();
				data.RunId = run.RunId;
				data.Score = run.Score;
				for(int j = 0; j < run.Pickups.Count; j++)
				{
					data.PickupCount += run.Pickups.ElementAt(j).PickupCount;
				}

				runPickups.Add(data);
			}

			return runPickups;
		}
    }
}