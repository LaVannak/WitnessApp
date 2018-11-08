using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WitnessAPI.Helpers;
using WitnessAPI.Models;

namespace WitnessAPI.Controllers
{
    public class IncidentsController : ApiController
    {
        private WitnessAPIContext db = new WitnessAPIContext();

        // GET: api/Incidents
        public IQueryable<Incidents> GetIncidents()
        {
            return db.Incidents;
        }

        // GET: api/Incidents/5
        [ResponseType(typeof(Incidents))]
        public async Task<IHttpActionResult> GetIncidents(int id)
        {
            Incidents incidents = await db.Incidents.FindAsync(id);
            if (incidents == null)
            {
                return NotFound();
            }

            return Ok(incidents);
        }

        // PUT: api/Incidents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIncidents(int id, Incidents incidents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incidents.Id)
            {
                return BadRequest();
            }

            db.Entry(incidents).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentsExists(id))
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

        // POST: api/Incidents
        [ResponseType(typeof(Incidents))]
        public async Task<IHttpActionResult> PostIncidents(Incidents incidents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stream = new MemoryStream(incidents.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = String.Format("{0}.jpp",guid);
            var folder = "~/Images/Incidents";
            var fullPath = String.Format("{0}/{1}",folder,file);

            var response = FileHelper.UploadPhoto(stream, folder,file);

            if (response)
            {
                incidents.ImagePath = fullPath;
            }

            var newIncident = new Incidents()
            {
            
                Titile = incidents.Titile,
                Description = incidents.Description,
                Location =incidents.Location,
                Longitude = incidents.Longitude,
                Latitude =incidents.Latitude,
                Date = incidents.Date,
                ImagePath = incidents.ImagePath
            };


            db.Incidents.Add(newIncident);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = incidents.Id }, incidents);
        }




        private bool IncidentsExists(int id)
        {
            return db.Incidents.Count(e => e.Id == id) > 0;
        }
    }
}