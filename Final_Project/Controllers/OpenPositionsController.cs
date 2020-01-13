using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Project_Data;
using Microsoft.AspNet.Identity;

namespace Final_Project.Controllers
{
    [Authorize]
    public class OpenPositionsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: OpenPositions
        public ActionResult Index()
        {
            var openPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);
            return View(openPositions.ToList());
        }

        // GET: OpenPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // GET: OpenPositions/Create
        [Authorize(Roles = "Admin,Corporate,Manager")]
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber");
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title");
            return View();
        }

        // POST: OpenPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Corporate,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenPositionId,PositionId,LocationId")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.OpenPositions.Add(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        // GET: OpenPositions/Edit/5
        [Authorize(Roles = "Admin,Corporate,Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        // POST: OpenPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Corporate,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpenPositionId,PositionId,LocationId")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        // GET: OpenPositions/Delete/5
        [Authorize(Roles = "Admin,Corporate,Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // POST: OpenPositions/Delete/5
        [Authorize(Roles = "Admin,Corporate,Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenPosition openPosition = db.OpenPositions.Find(id);
            db.OpenPositions.Remove(openPosition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult Apply(int id)
        {
            var user = User.Identity.GetUserId();
            Application application = new Application();
            application.OpenPositionId = id;
            application.UserId = user;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatusId = 1;
            application.ResumeFilename = db.UserDetails.Where(x => x.UserId == user).Select(x => x.ResumeFilename).FirstOrDefault();
            if (string.IsNullOrEmpty(application.ResumeFilename) || application.ResumeFilename == "noresume")
            {
                application.ResumeFilename = "noresume";
                return RedirectToAction("ResumeUpload", application);
            }
            db.Applications.Add(application);
            db.SaveChanges();
            return RedirectToAction("Index", "Applications");

        }
        public ActionResult ResumeUpload(Application app)
        {
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResumeUpload([Bind(Include = "ApplicationId,OpenPositionId,UserId,ApplicationDate,ManagerNotes,ApplicationStatus,ResumeFilename")] Application application,
            HttpPostedFileBase resume)
        {
            //fix
            application.ApplicationStatusId = 1; //forcing this before validation

            if (ModelState.IsValid)
            {
                //resume upload
                UserDetail ud = db.UserDetails.Where(x => x.UserId == application.UserId).FirstOrDefault();

                if (ud != null)
                {
                    //file upload
                    string file = resume.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));

                    //assign that resume file name to the UserDetail and save
                    //ud.ResumeFilename = whatever the filename ends up being
                    string[] goodExts = { ".pdf" };

                    if (goodExts.Contains(ext))
                    {
                        if (resume.ContentLength <= 10000000) //10mb
                        {

                            file = Guid.NewGuid() + ext;
                            resume.SaveAs(Server.MapPath("~/Content/resumes/" + file));
                            application.ResumeFilename = file;
                            ud.ResumeFilename = file;
                        }

                        db.Entry(ud).State = EntityState.Modified;
                        db.Applications.Add(application);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Applications");

                    }
                }
                else
                {
                    ViewBag.Error = "something went wrong, debug";
                    return View(application);
                }
            }
            return View(application);
        }
    }
}
