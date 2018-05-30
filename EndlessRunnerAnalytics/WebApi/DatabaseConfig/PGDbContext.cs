using System.Data.Entity;
using EndlessRunner.Models;

namespace WebApi.DB
{
	public class PGDbContext : DbContext
	{
		public PGDbContext(string schema) : this() { }

		public PGDbContext() : base("PGConnectionString") { }

		public virtual DbSet<Run> Runs { get; set; }
		public virtual DbSet<Pickup> Pickups { get; set; }
		public virtual DbSet<EndGameSession> EndGameSessions { get; set; }
	}
}