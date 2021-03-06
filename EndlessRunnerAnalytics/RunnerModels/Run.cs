﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndlessRunner.Models
{
	[Table("Run", Schema = "public")]
	public class Run
	{
		public int RunId { get; set; }
		public int LeftCount { get; set; }
		public int RightCount { get; set; }
		public int Score { get; set; }
		public float Distance { get; set; }
		public virtual ICollection<Pickup> Pickups { get; set; }

		public Run() { }
	}
}