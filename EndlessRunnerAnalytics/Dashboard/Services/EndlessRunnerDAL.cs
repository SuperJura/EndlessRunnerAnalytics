using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using EndlessRunner.Models;
using Newtonsoft.Json;

namespace Dashboard.Services
{
	public class EndlessRunnerDAL : IEndlessRunnerDAL
	{
		const string RUNS_PATH = "http://localhost:29418/api/Runs";

		const string PICKUPS_PATH = "http://localhost:29418/api/Pickups";

		const string ENDGAMESESSION_PATH = "http://localhost:29418/api/EndGameSessions";

		public List<EndGameSession> GetAllEndGameSessions()
		{
			throw new NotImplementedException();
		}

		public List<Pickup> GetAllPickups()
		{
			throw new NotImplementedException();
		}

		public List<Pickup> GetAllPickupsForRunId(int runId)
		{
			throw new NotImplementedException();
		}

		public List<Run> GetAllRuns()
		{
			List<Run> output = new List<Run>();
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(RUNS_PATH);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage response = client.GetAsync(RUNS_PATH).Result;

			if(response.IsSuccessStatusCode)
			{
				string json = response.Content.ReadAsStringAsync().Result;
				output = JsonConvert.DeserializeObject<List<Run>>(json);
			}

			return output;
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
			throw new NotImplementedException();
		}
	}
}