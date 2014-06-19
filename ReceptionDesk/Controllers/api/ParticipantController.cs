using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using ReceptionDesk.Models;
using ReceptionDesk.ViewModels;

namespace ReceptionDesk.Controllers.api
{
    public class ParticipantController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        // GET: api/Participant/5
        [ResponseType(typeof(Participant))]
        public IHttpActionResult GetParticipant(int id)
        {
            var participant = db.Participants.Find(1);
            return Json(participant);
        }

        // PUT: api/Participant/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipant(int id, Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participant.Id)
            {
                return BadRequest();
            }

            if (!db.Participants.Any(x => x.Id == id))
            {
                return NotFound();
            }

            db.Entry(participant).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Participant
        [ResponseType(typeof(ParticipantEditViewModel))]
        public IHttpActionResult PostParticipant(ParticipantEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meeting = db.StudyMeetings.Find(model.StudyMeetingId);
            if (meeting == null)
            {
                return BadRequest(string.Format("Not found study meeting.(StudyMeetingId={0})", model.StudyMeetingId));
            }

            meeting.Participants.Add(model.Participant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = model.Participant.Id }, model);
        }

        // DELETE: api/Participant/5
        [ResponseType(typeof(Participant))]
        public IHttpActionResult DeleteParticipant(int id)
        {
            var participant = db.Participants.Find(id);

            if (participant == null)
            {
                return NotFound();
            }

            db.Participants.Remove(participant);
            db.SaveChanges();

            return Ok(participant);
        }
    }
}