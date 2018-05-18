using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
	[Table("Run", Schema = "public")]
	public class Run
	{
		public int RunId { get; set; }
		public int LeftCount { get; set; }
		public int RightCount { get; set; }
		public int Score { get; set; }
		public float Distance { get; set; }

		public Run() { }
	}
}