using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.DatabaseConfig
{
	internal sealed class PGConfiguration : DbMigrationsConfiguration<PGDbContext>
	{
		public PGConfiguration()
		{
			AutomaticMigrationsEnabled = false;
		}
	}
}