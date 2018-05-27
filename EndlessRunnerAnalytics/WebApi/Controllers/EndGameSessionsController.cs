using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EndGameSessionsController : ApiController
    {
        private PGDbContext db = new PGDbContext();

        // GET: api/EndGameSessions
        public IQueryable<EndGameSession> GetEndGameSessions()
        {
            return db.EndGameSessions;
        }

        // GET: api/EndGameSessions/5
        [ResponseType(typeof(EndGameSession))]
        public IHttpActionResult GetEndGameSession(int id)
        {
            EndGameSession endGameSession = db.EndGameSessions.Find(id);
            if (endGameSession == null)
            {
                return NotFound();
            }

            return Ok(endGameSession);
        }

        // PUT: api/EndGameSessions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEndGameSession(int id, EndGameSession endGameSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != endGameSession.EndGameSessionId)
            {
                return BadRequest();
            }

            db.Entry(endGameSession).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndGameSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EndGameSessions
        [ResponseType(typeof(EndGameSession))]
        public IHttpActionResult PostEndGameSession(EndGameSession endGameSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EndGameSessions.Add(endGameSession);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = endGameSession.EndGameSessionId }, endGameSession);
        }

        // DELETE: api/EndGameSessions/5
        [ResponseType(typeof(EndGameSession))]
        public IHttpActionResult DeleteEndGameSession(int id)
        {
            EndGameSession endGameSession = db.EndGameSessions.Find(id);
            if (endGameSession == null)
            {
                return NotFound();
            }

            db.EndGameSessions.Remove(endGameSession);
            db.SaveChanges();

            return Ok(endGameSession);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EndGameSessionExists(int id)
        {
            return db.EndGameSessions.Count(e => e.EndGameSessionId == id) > 0;
        }
    }
}