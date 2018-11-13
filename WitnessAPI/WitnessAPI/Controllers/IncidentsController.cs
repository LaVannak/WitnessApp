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
        public IQueryable<Incident> GetIncidents()
        {
            return db.Incidents;
        }

        // GET: api/Incidents/5
        [ResponseType(typeof(Incident))]
        public async Task<IHttpActionResult> GetIncident(int id)
        {
            Incident incident = await db.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            return Ok(incident);
        }

        // PUT: api/Incidents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIncident(int id, Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incident.Id)
            {
                return BadRequest();
            }

            db.Entry(incident).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(id))
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
        [ResponseType(typeof(Incident))]
        public async Task<IHttpActionResult> PostIncident(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stream = new MemoryStream(incident.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = String.Format("{0}.jpg", guid);
            var folder = "~/Content/Images/Incidents";
            var fullPath = String.Format("{0}/{1}", folder, file);

            var response = FileHelper.UploadPhoto(stream, folder, file);

            if (response)
            {
                incident.ImagePath = fullPath;
            }

            var newIncident = new Incident()
            {

                Titile = incident.Titile,
                Description = incident.Description,
                Location = incident.Location,
                Longitude = incident.Longitude,
                Latitude = incident.Latitude,
                Date = incident.Date,
                ImagePath = incident.ImagePath,
                WitnesID = incident.WitnesID
            };


            db.Incidents.Add(newIncident);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = incident.Id }, incident);
        }

        // DELETE: api/Incidents/5
        [ResponseType(typeof(Incident))]
        public async Task<IHttpActionResult> DeleteIncident(int id)
        {
            Incident incident = await db.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            db.Incidents.Remove(incident);
            await db.SaveChangesAsync();

            return Ok(incident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IncidentExists(int id)
        {
            return db.Incidents.Count(e => e.Id == id) > 0;
        }
    }
}