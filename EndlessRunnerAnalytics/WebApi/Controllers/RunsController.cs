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
    public class RunsController : ApiController
    {
        private PGDbContext db = new PGDbContext();

        // GET: api/Runs
        public List<Run> GetRuns()
        {
            return db.Runs.ToList();
        }

        // GET: api/Runs/5
        [ResponseType(typeof(Run))]
        public IHttpActionResult GetRun(int id)
        {
            Run run = db.Runs.Find(id);
            if (run == null)
            {
                return NotFound();
            }

            return Ok(run);
        }

        // PUT: api/Runs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRun(int id, Run run)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != run.RunId)
            {
                return BadRequest();
            }

            db.Entry(run).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RunExists(id))
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

        // POST: api/Runs
        [ResponseType(typeof(Run))]
        public IHttpActionResult PostRun(Run run)
        {
            if (!ModelState.IsValid)
            {
				return BadRequest(ModelState);
            }

            db.Runs.Add(run);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = run.RunId }, run);
        }

        // DELETE: api/Runs/5
        [ResponseType(typeof(Run))]
        public IHttpActionResult DeleteRun(int id)
        {
            Run run = db.Runs.Find(id);
            if (run == null)
            {
                return NotFound();
            }

            db.Runs.Remove(run);
            db.SaveChanges();

            return Ok(run);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RunExists(int id)
        {
            return db.Runs.Count(e => e.RunId == id) > 0;
        }
	}

}