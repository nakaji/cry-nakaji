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
using WebGrease.Css.Extensions;

namespace ReceptionDesk.Controllers
{
    public class StudyMeetingController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: /StudyMeeting/
        public ActionResult Index()
        {
            return View(db.StudyMeetings.ToList());
        }

        // GET: /StudyMeeting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StudyMeeting/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Date,SeminarFee,SocialGatheringFee,SocialGatheringFeeForStudent")] StudyMeeting studyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.StudyMeetings.Add(studyMeeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studyMeeting);
        }

        // GET: /StudyMeeting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyMeeting studyMeeting = db.StudyMeetings.Find(id);
            if (studyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(studyMeeting);
        }

        // POST: /StudyMeeting/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Date,SeminarFee,SocialGatheringFee,SocialGatheringFeeForStudent")] StudyMeeting studyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studyMeeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studyMeeting);
        }

        // GET: /StudyMeeting/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyMeeting studyMeeting = db.StudyMeetings.Find(id);
            if (studyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(studyMeeting);
        }

        // POST: /StudyMeeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudyMeeting studyMeeting = db.StudyMeetings.Find(id);
            db.StudyMeetings.Remove(studyMeeting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TotalFee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StudyMeeting studyMeeting = db.StudyMeetings.Find(id);
            
            var model = new StudyMeetingTotalFeeViewModels();
            model.Participants = studyMeeting.Participants;
            studyMeeting.Participants.ForEach(x =>
            {
                var fee = studyMeeting.CalcFee(x);
                model.EstSeminarFee += fee.SeminarFee;
                model.EstSocialGatheringFee += fee.SocialGatheringFee;
                if (x.CheckedIn)
                {
                    model.ResSeminarFee += fee.SeminarFee;
                    model.ResSocialGatheringFee += fee.SocialGatheringFee;
                }
            });

            return View(model);
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
