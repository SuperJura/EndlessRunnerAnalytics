using System.ComponentModel.DataAnnotations.Schema;

namespace EndlessRunner.Models
{
	[Table("EndRun", Schema = "public")]
	public class EndGameSession
	{
		public int EndGameSessionId { get; set; }
		public int RetriesCount { get; set; }
		public float GameTime { get; set; }
	}
}