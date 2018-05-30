using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;
using WebApi.DB;
using EndlessRunner.Models;

namespace WebApi.Tests.ControllerTests
{
	[TestClass]
	public class PickupTests
	{
		[TestMethod]
		public void Get_Pickups_returns_all_in_database()
		{
			var data = new List<Pickup>
			{
				GetTestPickup(),
				GetTestPickup(),
			}.AsQueryable();

			Mock<PGDbContext> context = GetTestPickupContext(data);

			PickupsController controller = new PickupsController(context.Object);
			List<Pickup> endGameSessions = controller.GetPickups();

			Assert.AreEqual(2, endGameSessions.Count);
			foreach(var item in endGameSessions)
			{
				Assert.IsNotNull(item, "All Pickups that were fethed by Get shouldnt be null");
			}
		}

		[TestMethod]
		public void Get_EndGameSession_with_id_4_returns_EndGameSession_object_with_id_4()
		{
			var data = new List<Pickup>
			{
				GetTestPickup(),
			}.AsQueryable();

			Mock<PGDbContext> context = GetTestPickupContext(data);

			var controller = new PickupsController(context.Object);
			IHttpActionResult response = controller.GetPickup(1);

			Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<Pickup>), "GetPickup should return OK with Pickup data");

			OkNegotiatedContentResult<Pickup> responseWithData = (OkNegotiatedContentResult<Pickup>)response;

			Assert.IsNotNull(responseWithData.Content, "Pickup data that was fetched with Get should not be null");
			Assert.AreEqual(1, responseWithData.Content.PickupId, "Pickup data ID should be 1");
		}

		private Mock<PGDbContext> GetTestPickupContext(IQueryable<Pickup> data)
		{
			Mock<DbSet<Pickup>> set = new Mock<DbSet<Pickup>>();
			set.As<IQueryable<Pickup>>().Setup(m => m.Provider).Returns(data.Provider);
			set.As<IQueryable<Pickup>>().Setup(m => m.Expression).Returns(data.Expression);
			set.As<IQueryable<Pickup>>().Setup(m => m.ElementType).Returns(data.ElementType);
			set.As<IQueryable<Pickup>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			Mock<PGDbContext> context = new Mock<PGDbContext>();
			context.Setup(c => c.Pickups).Returns(set.Object);

			return context;
		}

		private Pickup GetTestPickup()
		{
			return new Pickup()
			{
				PickupId = 1,
				PickupCount = 10,
				PickupName = "Coin",
				RunId = 1,
			};
		}
	}
}
