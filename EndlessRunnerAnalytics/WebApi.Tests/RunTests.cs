using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using System.Data.Entity;
using WebApi.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Tests
{
	[TestClass]
	public class RunTests
	{
		[TestMethod]
		public void Post_run_adds_it_to_database()
		{
			Mock<DbSet<Run>> runSet = new Mock<DbSet<Run>>();
			Mock<PGDbContext> context = new Mock<PGDbContext>();
			context.Setup(m => m.Runs).Returns(runSet.Object);

			var runsController = new RunsController(context.Object);

			runsController.PostRun(GetTestRun());

			runSet.Verify(m => m.Add(It.IsAny<Run>()), Times.Once());
			context.Verify(m => m.SaveChanges(), Times.Once());
		}

		[TestMethod]
		public void Get_runs_returns_all_runs_in_database()
		{
			var data = new List<Run>
			{
				GetTestRun(),
				GetTestRun(),
				GetTestRun()
			}.AsQueryable();

			Mock<DbSet<Run>> runSet = new Mock<DbSet<Run>>();
			runSet.As<IQueryable<Run>>().Setup(m => m.Provider).Returns(data.Provider);
			runSet.As<IQueryable<Run>>().Setup(m => m.Expression).Returns(data.Expression);
			runSet.As<IQueryable<Run>>().Setup(m => m.ElementType).Returns(data.ElementType);
			runSet.As<IQueryable<Run>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			Mock<PGDbContext> context = new Mock<PGDbContext>();
			context.Setup(c => c.Runs).Returns(runSet.Object);

			var runsController = new RunsController(context.Object);
			List<Run> runs = runsController.GetRuns();

			Assert.AreEqual(3, runs.Count);
			Assert.AreEqual(3, runs[0].Pickups.Count);
		}

		private Run GetTestRun()
		{
			return new Run()
			{
				Distance = 4.4f,
				LeftCount = 5,
				RightCount = 6,
				Score = 100,
				Pickups = new List<Pickup>()
				{
					new Pickup()
					{
						PickupCount = 5,
						PickupName = "Coin"
					},
					new Pickup()
					{
						PickupCount = 2,
						PickupName = "Slow"
					},
					new Pickup()
					{
						PickupCount = 4,
						PickupName = "Easy"
					}
				}
			};
		}
	}
}
