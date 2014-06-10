using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ReceptionDesk.Hub;
using ReceptionDesk.Models;

namespace ReceptionDesk.Controllers
{
    public class ReceptionController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Reception/List/1
        public ActionResult List(int? id)
        {
            if (id == null) return Redirect("~/StudyMeeting");
            var studyMeeting = db.StudyMeetings.Find(id);
            return View(studyMeeting);
        }

        // GET: Reception/CheckIn/1
        public ActionResult CheckIn(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var participant = db.Participants.Find(id);
            return View(participant);
        }

        [HttpPost]
        // POST: Reception/CheckIn/1
        public ActionResult CheckIn(int? id, [Bind(Include = "Id,SocialGathering,IsMinors,IsStudent")]Participant participant, int? studyMeetingId)
        {
            var part = db.Participants.Find(id);
            part.SocialGathering = participant.SocialGathering;
            part.IsMinors = participant.IsMinors;
            part.IsStudent = participant.IsStudent;
            part.CheckedIn = true;
            db.Entry(part).State = EntityState.Modified;

            var hist = new CheckInHistory
            {
                Participant = part
            };
            db.CheckinHistories.Add(hist);

            db.SaveChanges();

            // SignalRで通知
            var context = GlobalHost.ConnectionManager.GetHubContext<CheckInNotifyHub>();
            context.Clients.All.ReceiveMessage();

            return RedirectToAction("List", new { id = studyMeetingId });
        }

        public ActionResult CheckInHistory(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var histries = db.CheckinHistories.Where(x => x.Participant.StudyMeeting.Id == id);
            return View(histries.OrderByDescending(x=>x.CheckInTime).ToList());
        }
    }
}