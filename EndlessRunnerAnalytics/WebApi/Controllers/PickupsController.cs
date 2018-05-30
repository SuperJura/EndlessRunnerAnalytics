using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using EndlessRunner.Models;
using WebApi.DB;

namespace WebApi.Controllers
{
    public class PickupsController : ApiController
    {
		private PGDbContext context;

		public PickupsController(PGDbContext context)
		{
			this.context = context;
		}

		public PickupsController()
		{
			context = new PGDbContext();
		}

        // GET: api/Pickups
        public List<Pickup> GetPickups()
        {
            return context.Pickups.ToList();
        }

        // GET: api/Pickups/5
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult GetPickup(int id)
        {
            Pickup pickup = context.Pickups.FirstOrDefault(x => x.PickupId == id);
            if (pickup == null)
            {
                return NotFound();
            }

            return Ok(pickup);
        }

        // DELETE: api/Pickups/5
        [ResponseType(typeof(Pickup))]
        public IHttpActionResult DeletePickup(int id)
        {
            Pickup pickup = context.Pickups.Find(id);
            if (pickup == null)
            {
                return NotFound();
            }

            context.Pickups.Remove(pickup);
            context.SaveChanges();

            return Ok(pickup);
        }
    }
}