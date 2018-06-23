using EndlessRunner.Models;
using System.Collections.Generic;

namespace Dashboard.Services
{
	public interface IEndlessRunnerDAL
	{
		List<Run> GetAllRuns();
		Run GetRunById(int id);

		List<Pickup> GetAllPickups();
		List<Pickup> GetAllPickupsForRunId(int runId);

		List<EndGameSession> GetAllEndGameSessions();
	}
}