using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
	public class PGDbContext : DbContext
	{
		public PGDbContext(string schema) : base("PGConnectionString")
		{

		}

		public PGDbContext() : base("PGConnectionString")
		{

		}

		public DbSet<Run> Runs { get; set; }
	}
}