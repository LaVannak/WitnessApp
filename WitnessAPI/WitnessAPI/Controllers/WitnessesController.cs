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
using System.Web.Http.Results;
using WitnessAPI.Models;

namespace WitnessAPI.Controllers
{
    public class WitnessesController : ApiController
    {
        private WitnessAPIContext db = new WitnessAPIContext();

        // GET: api/Witnesses
        public IHttpActionResult GetWitnesses()
        {
            var Witnesses = db.Witnesses.Include("Incident");
            return Ok(Witnesses);
        }

        // GET: api/Witnesses/5
        [ResponseType(typeof(Witness))]
        public async Task<IHttpActionResult> GetWitness(int id)
        {
            Witness witness = await db.Witnesses.Include("Incident").FirstOrDefaultAsync(a=>a.Id==id);
            if (witness == null)
            {
                return NotFound();
            }

            return Ok(witness);
        }

        public async Task<IHttpActionResult> GetWitness(string email)
        {
            Witness witness = await db.Witnesses.Include("Incident").FirstOrDefaultAsync(a => a.Email == email);
            if (witness == null)
            {
                return NotFound();
            }

            return Ok(witness);
        }

        // PUT: api/Witnesses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWitness(int id, Witness witness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != witness.Id)
            {
                return BadRequest();
            }

            db.Entry(witness).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WitnessExists(id))
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

        // POST: api/Witnesses
        [ResponseType(typeof(Witness))]
        public async Task<IHttpActionResult> PostWitness(Witness witness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!WitnessExists(witness.Email))
            {
                db.Witnesses.Add(witness);
                await db.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { id = witness.Id }, witness);
            }
            else
            {
                return StatusCode(HttpStatusCode.Found);
            }
            

        }



        private bool WitnessExists(int id)
        {
            return db.Witnesses.Count(e => e.Id == id) > 0;
        }

        private bool WitnessExists(string email)
        {
            return db.Witnesses.Count(e => e.Email == email) > 0;
        }
    }
}