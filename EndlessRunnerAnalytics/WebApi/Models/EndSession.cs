using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
	[Table("EndRun", Schema = "public")]
	public class EndGameSession
	{
		public int EndGameSessionId { get; set; }
		public int RetriesCount { get; set; }
		public float GameTime { get; set; }
	}
}