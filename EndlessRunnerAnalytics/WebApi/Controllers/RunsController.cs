using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class RunsController : ApiController
    {
		private PGDbContext context;

		public RunsController(PGDbContext context)
		{
			this.context = context;
		}

        // GET: api/Runs
        public List<Run> GetRuns()
        {
            return context.Runs.ToList();
        }

        // GET: api/Runs/5
        [ResponseType(typeof(Run))]
        public IHttpActionResult GetRun(int id)
        {
            Run run = context.Runs.Find(id);
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

            context.Entry(run).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
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

			context.Runs.Add(run);
			context.SaveChanges();

			//db.Pickups.AddRange(run.Pickups);
			//db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = run.RunId }, run);
        }

        // DELETE: api/Runs/5
        [ResponseType(typeof(Run))]
        public IHttpActionResult DeleteRun(int id)
        {
            Run run = context.Runs.Find(id);
            if (run == null)
            {
                return NotFound();
            }

            context.Runs.Remove(run);
            context.SaveChanges();

            return Ok(run);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RunExists(int id)
        {
            return context.Runs.Count(e => e.RunId == id) > 0;
        }
	}
}