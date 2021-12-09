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
    public class UserWiseRolesController : Controller
    {
        private MEVEntities db = new MEVEntities();

        // GET: UserWiseRoles
        public ActionResult Index()
        {
            var userWiseRoles = db.UserWiseRoles.Include(u => u.TbleRole).Include(u => u.TblUser);
            return View(userWiseRoles.ToList());
        }

        // GET: UserWiseRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWiseRole userWiseRole = db.UserWiseRoles.Find(id);
            if (userWiseRole == null)
            {
                return HttpNotFound();
            }
            return View(userWiseRole);
        }

        // GET: UserWiseRoles/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.TbleRoles, "Id", "RoleName");
            ViewBag.UserId = new SelectList(db.TblUsers, "Id", "UserName");
            return View();
        }

        // POST: UserWiseRoles/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RoleId")] UserWiseRole userWiseRole)
        {
            if (ModelState.IsValid)
            {
                db.UserWiseRoles.Add(userWiseRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.TbleRoles, "Id", "RoleName", userWiseRole.RoleId);
            ViewBag.UserId = new SelectList(db.TblUsers, "Id", "UserName", userWiseRole.UserId);
            return View(userWiseRole);
        }

        // GET: UserWiseRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWiseRole userWiseRole = db.UserWiseRoles.Find(id);
            if (userWiseRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.TbleRoles, "Id", "RoleName", userWiseRole.RoleId);
            ViewBag.UserId = new SelectList(db.TblUsers, "Id", "UserName", userWiseRole.UserId);
            return View(userWiseRole);
        }

        // POST: UserWiseRoles/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RoleId")] UserWiseRole userWiseRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userWiseRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.TbleRoles, "Id", "RoleName", userWiseRole.RoleId);
            ViewBag.UserId = new SelectList(db.TblUsers, "Id", "UserName", userWiseRole.UserId);
            return View(userWiseRole);
        }

        // GET: UserWiseRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWiseRole userWiseRole = db.UserWiseRoles.Find(id);
            if (userWiseRole == null)
            {
                return HttpNotFound();
            }
            return View(userWiseRole);
        }

        // POST: UserWiseRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserWiseRole userWiseRole = db.UserWiseRoles.Find(id);
            db.UserWiseRoles.Remove(userWiseRole);
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
