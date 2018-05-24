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
    public class PickupsController : ApiController
    {
        private PGDbContext db = new PGDbContext();

        // GET: api/Pickups
        public List<Pickup> GetPickups()
        {
			Pickup[] arr = db.Pickups.ToArray();
            return db.Pickups.ToList();
        }

        // GET: api/Pickups/5
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult GetPickup(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return NotFound();
            }

            return Ok(pickup);
        }

        // PUT: api/Pickups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPickup(int id, Pickup pickup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pickup.PickupId)
            {
                return BadRequest();
            }

            db.Entry(pickup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PickupExists(id))
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

        // POST: api/Pickups
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult PostPickup(Pickup pickup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pickups.Add(pickup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pickup.PickupId }, pickup);
        }

        // DELETE: api/Pickups/5
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult DeletePickup(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return NotFound();
            }

            db.Pickups.Remove(pickup);
            db.SaveChanges();

            return Ok(pickup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PickupExists(int id)
        {
            return db.Pickups.Count(e => e.PickupId == id) > 0;
        }
    }
}