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
    /// <summary>
    /// 参加者編集API
    /// </summary>
    public class ParticipantController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        // GET: api/Participant/5
        /// <summary>
        /// 参加者情報を取得する
        /// </summary>
        /// <param name="id">参加者ID</param>
        /// <returns></returns>
        [ResponseType(typeof(Participant))]
        public IHttpActionResult GetParticipant(int id)
        {
            var participant = db.Participants.Find(1);
            return Json(participant);
        }

        // PUT: api/Participant/5
        /// <summary>
        /// 参加者情報を更新する
        /// </summary>
        /// <param name="id">参加者ID</param>
        /// <param name="participant">参加者情報</param>
        /// <returns></returns>
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
        /// <summary>
        /// 参加者を登録する
        /// </summary>
        /// <param name="model">参加者情報</param>
        /// <returns></returns>
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
        /// <summary>
        /// 参加者を削除する
        /// </summary>
        /// <param name="id">参加者ID</param>
        /// <returns></returns>
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