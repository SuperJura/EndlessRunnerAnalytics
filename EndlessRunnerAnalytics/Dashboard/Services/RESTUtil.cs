using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Dashboard.Services
{
	public static class RESTUtil
	{
		public static T GetFromURL<T>(string url)
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage response = client.GetAsync(url).Result;

			if(response.IsSuccessStatusCode)
			{
				string json = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<T>(json);
			}

			return default(T);
		}
	}
}