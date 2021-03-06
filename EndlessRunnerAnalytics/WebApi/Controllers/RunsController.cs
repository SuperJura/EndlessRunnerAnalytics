﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using EndlessRunner.Models;
using WebApi.DB;

namespace WebApi.Controllers
{
    public class RunsController : ApiController
    {
		private PGDbContext context;

		public RunsController(PGDbContext context)
		{
			this.context = context;
		}

		public RunsController()
		{
			context = new PGDbContext();
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
            Run run = context.Runs.FirstOrDefault(x => x.RunId == id);
            if (run == null)
            {
                return NotFound();
            }

            return Ok(run);
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

        private bool RunExists(int id)
        {
            return context.Runs.Count(e => e.RunId == id) > 0;
        }
	}
}