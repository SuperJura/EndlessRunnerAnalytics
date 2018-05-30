using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EndlessRunner.Models;
using System.Data.Entity;
using WebApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using WebApi.DB;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApi.Tests
{
	[TestClass]
	public class RunTests
	{
		[TestMethod]
		public void Post_Run_adds_it_to_database()
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
		public void Get_Runs_returns_all_runs_in_database()
		{
			var data = new List<Run>
			{
				GetTestRun(),
				GetTestRun(),
				GetTestRun()
			}.AsQueryable();

			Mock<PGDbContext> context = GetTestRunContext(data);

			var runsController = new RunsController(context.Object);
			List<Run> runs = runsController.GetRuns();

			Assert.AreEqual(3, runs.Count);
			Assert.AreEqual(3, runs[0].Pickups.Count);
			foreach(var item in runs)
			{
				Assert.IsNotNull(item, "All Runs that were fethed by Get shouldnt be null");
			}
		}

		[TestMethod]
		public void Get_Run_with_id_1_returns_Run_object_with_id_1()
		{
			var data = new List<Run>
			{
				GetTestRun(),
			}.AsQueryable();

			Mock<PGDbContext> context = GetTestRunContext(data);

			var runsController = new RunsController(context.Object);
			IHttpActionResult response = runsController.GetRun(1);

			Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<Run>), "GetRun should return OK response with Run data");

			OkNegotiatedContentResult<Run> responseWithData = (OkNegotiatedContentResult<Run>)response;

			Assert.IsNotNull(responseWithData.Content, "Run data that was fetched with Get should not be null");
			Assert.AreEqual(1, responseWithData.Content.RunId, "Run data ID should be 1");
		}

		private Mock<PGDbContext> GetTestRunContext(IQueryable<Run> data)
		{
			Mock<DbSet<Run>> set = new Mock<DbSet<Run>>();
			set.As<IQueryable<Run>>().Setup(m => m.Provider).Returns(data.Provider);
			set.As<IQueryable<Run>>().Setup(m => m.Expression).Returns(data.Expression);
			set.As<IQueryable<Run>>().Setup(m => m.ElementType).Returns(data.ElementType);
			set.As<IQueryable<Run>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			Mock<PGDbContext> context = new Mock<PGDbContext>();
			context.Setup(c => c.Runs).Returns(set.Object);

			return context;
		}

		private Run GetTestRun()
		{
			return new Run()
			{
				RunId = 1,
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
