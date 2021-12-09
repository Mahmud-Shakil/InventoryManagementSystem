using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ev.Models;

namespace Ev.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TbleRolesController : Controller
    {
        

        private MEVEntities db = new MEVEntities();

        // GET: TbleRoles
        public ActionResult Index()
        {
            return View(db.TbleRoles.ToList());
        }

        // GET: TbleRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbleRole tbleRole = db.TbleRoles.Find(id);
            if (tbleRole == null)
            {
                return HttpNotFound();
            }
            return View(tbleRole);
        }

        // GET: TbleRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TbleRoles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleName")] TbleRole tbleRole)
        {
            if (ModelState.IsValid)
            {
                db.TbleRoles.Add(tbleRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbleRole);
        }

        // GET: TbleRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbleRole tbleRole = db.TbleRoles.Find(id);
            if (tbleRole == null)
            {
                return HttpNotFound();
            }
            return View(tbleRole);
        }

        // POST: TbleRoles/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleName")] TbleRole tbleRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbleRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbleRole);
        }

        // GET: TbleRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TbleRole tbleRole = db.TbleRoles.Find(id);
            if (tbleRole == null)
            {
                return HttpNotFound();
            }
            return View(tbleRole);
        }

        // POST: TbleRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TbleRole tbleRole = db.TbleRoles.Find(id);
            db.TbleRoles.Remove(tbleRole);
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
    }
}
