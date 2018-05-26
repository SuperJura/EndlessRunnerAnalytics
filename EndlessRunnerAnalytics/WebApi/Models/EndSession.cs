using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
	[Table("EndRun", Schema = "public")]
	public class EndSession
	{
		public int EndSessionId { get; set; }
		public int RetriesCount { get; set; }
	}
}