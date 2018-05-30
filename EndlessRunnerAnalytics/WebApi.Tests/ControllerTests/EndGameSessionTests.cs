using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;
using WebApi.DB;
using EndlessRunner.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApi.Tests
{
	[TestClass]
	public class EndGameSessionTests
	{
		[TestMethod]
		public void Post_EndGameSession_adds_it_to_database()
		{
			Mock<DbSet<EndGameSession>> endGameSessionSet = new Mock<DbSet<EndGameSession>>();
			Mock<PGDbContext> context = new Mock<PGDbContext>();
			context.Setup(m => m.EndGameSessions).Returns(endGameSessionSet.Object);

			var endGameSessionsController = new EndGameSessionsController(context.Object);

			endGameSessionsController.PostEndGameSession(GetTestEndGameSession());

			endGameSessionSet.Verify(m => m.Add(It.IsAny<EndGameSession>()), Times.Once());
			context.Verify(m => m.SaveChanges(), Times.Once());
		}

		[TestMethod]
		public void Get_EndGameSessions_returns_all_in_database()
		{
			var data = new List<EndGameSession>
			{
				GetTestEndGameSession(),
				GetTestEndGameSession(),
				GetTestEndGameSession(),
				GetTestEndGameSession(),
			}.AsQueryable();

			Mock<PGDbContext> context = GetTestEndGameSessionContext(data);

			EndGameSessionsController controller = new EndGameSessionsController(context.Object);
			List<EndGameSession> endGameSessions = controller.GetEndGameSessions();

			Assert.AreEqual(4, endGameSessions.Count);
			foreach(var item in endGameSessions)
			{
				Assert.IsNotNull(item, "All EndGameSessions that were fethed by Get shouldnt be null");
			}
		}

		[TestMethod]
		public void Get_EndGameSession_with_id_4_returns_EndGameSession_object_with_id_4()
		{
			var data = new List<EndGameSession>
			{
				GetTestEndGameSession(),
			}.AsQueryable();

			Mock<PGDbContext> context = GetTestEndGameSessionContext(data);

			var controller = new EndGameSessionsController(context.Object);
			IHttpActionResult response = controller.GetEndGameSession(4);

			Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<EndGameSession>), "GetEndGameSession should return OK with EndGameSession data");

			OkNegotiatedContentResult<EndGameSession> responseWithData = (OkNegotiatedContentResult<EndGameSession>)response;

			Assert.IsNotNull(responseWithData.Content, "EndGameSession data that was fetched with Get should not be null");
			Assert.AreEqual(4, responseWithData.Content.EndGameSessionId, "EndGameSession data ID should be 4");
		}

		private Mock<PGDbContext> GetTestEndGameSessionContext(IQueryable<EndGameSession> data)
		{
			Mock<DbSet<EndGameSession>> set = new Mock<DbSet<EndGameSession>>();
			set.As<IQueryable<EndGameSession>>().Setup(m => m.Provider).Returns(data.Provider);
			set.As<IQueryable<EndGameSession>>().Setup(m => m.Expression).Returns(data.Expression);
			set.As<IQueryable<EndGameSession>>().Setup(m => m.ElementType).Returns(data.ElementType);
			set.As<IQueryable<EndGameSession>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			Mock<PGDbContext> context = new Mock<PGDbContext>();
			context.Setup(c => c.EndGameSessions).Returns(set.Object);

			return context;
		}

		private EndGameSession GetTestEndGameSession()
		{
			return new EndGameSession()
			{
				EndGameSessionId = 4,
				GameTime = 150.5f,
				RetriesCount = 5
			};
		}
	}
}
