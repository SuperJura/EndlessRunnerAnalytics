using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApi.Models
{
	[Table("Pickup", Schema = "public")]
	public class Pickup
	{
		public int PickupId { get; set; }
		public int PickupCount { get; set; }
		public string PickupName { get; set; }
		public int RunId { get; set; }
		public virtual Run Run { get; set; }

		public Pickup() { }
	}
}