using Newtonsoft.Json;
using System.IO;
using System.Net;
using Dashboard.AOP;

namespace Dashboard.Services
{
	[LogError]
	public static class RESTUtil
	{
		public static T GetFromURL<T>(string url)
		{
			WebRequest request = WebRequest.Create(url);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			if(response.StatusCode == HttpStatusCode.OK)
			{
				Stream dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
				string json = reader.ReadToEnd();

				return JsonConvert.DeserializeObject<T>(json);
			}

			//HttpClient client = new HttpClient();
			//client.BaseAddress = new Uri(url);
			//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			//HttpResponseMessage response = client.GetAsync(url).Result;

			//if(response.IsSuccessStatusCode)
			//{
			//	string json = response.Content.ReadAsStringAsync().Result;
			//	return JsonConvert.DeserializeObject<T>(json);
			//}

			return default(T);
		}
	}
}