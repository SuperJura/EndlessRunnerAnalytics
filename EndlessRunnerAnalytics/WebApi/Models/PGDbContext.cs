using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
	public class PGDbContext : DbContext
	{
		public PGDbContext(string schema) : this()
		{

		}

		public PGDbContext() : base("PGConnectionString")
		{
			Configuration.ProxyCreationEnabled = false;
			Configuration.LazyLoadingEnabled = true;
		}

		public DbSet<Run> Runs { get; set; }

		public DbSet<Pickup> Pickups { get; set; }
	}
}