using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using EndlessRunner.Models;
using WebApi.DB;

namespace WebApi.Controllers
{
    public class EndGameSessionsController : ApiController
    {
		private PGDbContext context;

		public EndGameSessionsController(PGDbContext context)
		{
			this.context = context;
		}

		public EndGameSessionsController()
		{
			context = new PGDbContext();
		}

        // GET: api/EndGameSessions
        public List<EndGameSession> GetEndGameSessions()
        {
            return context.EndGameSessions.ToList();
        }

        // GET: api/EndGameSessions/5
        [ResponseType(typeof(EndGameSession))]
        public IHttpActionResult GetEndGameSession(int id)
        {
            EndGameSession endGameSession = context.EndGameSessions.FirstOrDefault(x => x.EndGameSessionId == id);
            if (endGameSession == null)
            {
                return NotFound();
            }

            return Ok(endGameSession);
        }

        // POST: api/EndGameSessions
        [ResponseType(typeof(EndGameSession))]
        public IHttpActionResult PostEndGameSession(EndGameSession endGameSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.EndGameSessions.Add(endGameSession);
            context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = endGameSession.EndGameSessionId }, endGameSession);
        }

        // DELETE: api/EndGameSessions/5
        [ResponseType(typeof(EndGameSession))]
        public IHttpActionResult DeleteEndGameSession(int id)
        {
            EndGameSession endGameSession = context.EndGameSessions.Find(id);
            if (endGameSession == null)
            {
                return NotFound();
            }

            context.EndGameSessions.Remove(endGameSession);
            context.SaveChanges();

            return Ok(endGameSession);
        }

        private bool EndGameSessionExists(int id)
        {
            return context.EndGameSessions.Count(e => e.EndGameSessionId == id) > 0;
        }
    }
}