using Ev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ev.Controllers
{
    public class SupplierController : Controller
    {
        MEVEntities dbobj = new MEVEntities();
        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateSupplier()
        {
            var Model = dbobj.Suppliers.ToList();
            return View(Model);
        }
        public ActionResult Supplier(Supplier obj)
        {

            dbobj.Suppliers.Add(obj);
            dbobj.SaveChanges();
            IEnumerable<Supplier> list = dbobj.Suppliers.ToList();
            return PartialView("_SupplierListView", list);
        }
    }
}