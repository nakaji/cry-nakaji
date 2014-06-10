using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReceptionDesk.Models;
using ReceptionDesk.ViewModels;

namespace ReceptionDesk.Controllers
{
    public class ParticipantController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: /Participant/Add/5
        public ActionResult Add(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            StudyMeeting studyMeeting = db.StudyMeetings.Find(id);
            if (studyMeeting == null) return HttpNotFound();

            var model = new ParticipantAddViewModel
            {
                StudyMeeting = studyMeeting,
                Participants = studyMeeting.Participants.ToList(),
            };
            return View(model);
        }

        [HttpPost]
        // POST: /Participant/Add/5
        public ActionResult Add(int id, Participant participant)
        {
            StudyMeeting studyMeeting = db.StudyMeetings.Find(id);

            if (ModelState.IsValid)
            {
                studyMeeting.Participants.Add(participant);
                db.SaveChanges();
                return RedirectToAction("Add", new { Id = id });
            }

            var model = new ParticipantAddViewModel
            {
                StudyMeeting = studyMeeting,
                Participants = studyMeeting.Participants.ToList()
            };
            return View(model);
        }


        // GET: /Participant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }

            var model = new ParticipantEditViewModel();
            model.StudyMeetingId = participant.StudyMeeting.Id;
            model.Participant = participant;

            return View(model);
        }

        // POST: /Participant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Participant participant, int studyMeetingId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Add", new { id = studyMeetingId });
            }
            var model = new ParticipantEditViewModel();
            model.StudyMeetingId = studyMeetingId;
            model.Participant = participant;
            return View(model);
        }

        // GET: /Participant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }

            var model = new ParticipantEditViewModel();
            model.StudyMeetingId = participant.StudyMeeting.Id;
            model.Participant = participant;

            return View(model);
        }

        // POST: /Participant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int studyMeetingId)
        {
            Participant participant = db.Participants.Find(id);
            db.Participants.Remove(participant);
            db.SaveChanges();
            return RedirectToAction("Add", new { id = studyMeetingId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
