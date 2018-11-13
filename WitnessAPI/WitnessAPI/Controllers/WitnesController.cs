using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WitnessAPI.Models;

namespace WitnessAPI.Controllers
{
    public class WitnesController : ApiController
    {
        private WitnessAPIContext db = new WitnessAPIContext();

        // GET: api/Witnes
        public IQueryable<Witnes> GetWitnes()
        {
            return db.Witnes;
        }

        // GET: api/Witnes/5
        [ResponseType(typeof(Witnes))]
        public async Task<IHttpActionResult> GetWitnes(int id)
        {
            Witnes witnes = await db.Witnes.FindAsync(id);
            if (witnes == null)
            {
                return NotFound();
            }

            return Ok(witnes);
        }

        // GET: api/Witnes?email=email..
        [ResponseType(typeof(Witnes))]
        public async Task<IHttpActionResult> GetWitnes(string email)
        {
            Witnes witnes = await db.Witnes.FirstOrDefaultAsync(a => a.Email == email);
            if (witnes == null)
            {
                return NotFound();
            }

            return Ok(witnes);
        }

        // PUT: api/Witnes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWitnes(int id, Witnes witnes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != witnes.Id)
            {
                return BadRequest();
            }

            db.Entry(witnes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WitnesExists(id))
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

        // POST: api/Witnes
        [ResponseType(typeof(Witnes))]
        public async Task<IHttpActionResult> PostWitnes(Witnes witnes)
        {
            if (!WitnesExists(witnes.Email))
            {
                db.Witnes.Add(witnes);
                await db.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { id = witnes.Id }, witnes);
            }
            else
            {
                return StatusCode(HttpStatusCode.Found);
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WitnesExists(int id)
        {
            return db.Witnes.Count(e => e.Id == id) > 0;
        }
        private bool WitnesExists(string email)
        {
            return db.Witnes.Count(e => e.Email == email) > 0;
        }
    }
}