using System;
using System.Collections.Generic;
using System.Linq;
using Dashboard.AOP;
using EndlessRunner.Models;

namespace Dashboard.Services
{
	[TimeMethod]
	public class EndlessRunnerDAL : IEndlessRunnerDAL
	{
		public const string RUNS_PATH = "http://localhost:29418/api/Runs";
		public const string PICKUPS_PATH = "http://localhost:29418/api/Pickups";
		public const string ENDGAMESESSION_PATH = "http://localhost:29418/api/EndGameSessions";

		public List<EndGameSession> GetAllEndGameSessions()
		{
			List<EndGameSession> endGames = RESTUtil.GetFromURL<List<EndGameSession>>(ENDGAMESESSION_PATH);
			if(endGames == null) endGames = new List<EndGameSession>();
			return endGames;
		}

		public List<Pickup> GetAllPickups()
		{
			List<Pickup> pickups = RESTUtil.GetFromURL<List<Pickup>>(PICKUPS_PATH);
			if(pickups == null) pickups = new List<Pickup>();
			return pickups;
		}

		public List<Pickup> GetAllPickupsForRunId(int runId)
		{
			throw new NotImplementedException();
		}

		public List<Run> GetAllRuns()
		{
			List<Run> runs = RESTUtil.GetFromURL<List<Run>>(RUNS_PATH);
			if(runs == null) runs = new List<Run>();
			return runs;
		}

		public EndGameSession GetEndGameSessionById(int id)
		{
			throw new NotImplementedException();
		}

		public Pickup GetPickupById(int pickupId)
		{
			throw new NotImplementedException();
		}

		public Run GetRunById(int id)
		{
			Run run = RESTUtil.GetFromURL<Run>(RUNS_PATH + "/" + id);
			if(run == null) run = new Run() { RunId = -1 };
			return run;
		}
	}
}