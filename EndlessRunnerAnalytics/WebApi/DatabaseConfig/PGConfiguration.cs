using System.Data.Entity.Migrations;

namespace WebApi.DB
{
	internal sealed class PGConfiguration : DbMigrationsConfiguration<PGDbContext>
	{
		public PGConfiguration()
		{
			AutomaticMigrationsEnabled = false;
		}
	}
}