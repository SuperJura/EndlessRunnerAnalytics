using Dashboard.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EndlessRunner.Models;
using Dashboard.Models;
using Newtonsoft.Json;
using Dashboard.AOP;

namespace Dashboard.Controllers
{
	[LogMethod]
	public class PickupsController : Controller
    {
		IEndlessRunnerDAL dal = new EndlessRunnerDAL();

        public ActionResult Index()
		{
			return IndexWithCount(0);
		}

		public ActionResult IndexWithCount(int count)
		{
			List<Run> allRuns = dal.GetAllRuns();

			ViewBag.meanCoins = GetPickupCount(allRuns, "Coin") / (float)allRuns.Count;
			ViewBag.meanEasy = GetPickupCount(allRuns, "Easy") / (float)allRuns.Count;
			ViewBag.meanSlow = GetPickupCount(allRuns, "Slow") / (float)allRuns.Count;

			List<Run> runs = count == 0 ? allRuns : allRuns.TakeLastN(count);
			ViewBag.pickups = JsonConvert.SerializeObject(getAllPickupData(runs));

			return View("Index");
		}

		private List<PickupData> getAllPickupData(List<Run> allRuns)
		{
			List<PickupData> pickups = new List<PickupData>();
			foreach(var run in allRuns)
			{
				PickupData data = new PickupData();
				data.RunId = run.RunId;
				data.CoinCount = run.Pickups.Where(x => x.PickupName == "Coin").Sum(x => x.PickupCount);
				data.SlowCount = run.Pickups.Where(x => x.PickupName == "Slow").Sum(x => x.PickupCount);
				data.EasyCount = run.Pickups.Where(x => x.PickupName == "Easy").Sum(x => x.PickupCount);
				pickups.Add(data);
			}	
			
			return pickups;
		}

		private int GetPickupCount(List<Run> runs, string pickupName)
		{
			int max = 0;
			for(int i = 0; i < runs.Count; i++)
			{
				max += runs[i].Pickups.Where(x => x.PickupName == pickupName).Sum(x => x.PickupCount);
			}

			return max;
		}
    }
}