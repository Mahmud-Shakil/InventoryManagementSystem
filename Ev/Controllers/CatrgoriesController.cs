using Ev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ev.Controllers
{
    public class CatrgoriesController : Controller
    {
        MEVEntities dbobj = new MEVEntities();
        // GET: Catrgories
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateCategories()
        {
            var Model = dbobj.CustomerCategories.ToList();
            return View(Model);
        }
        public ActionResult Categories(CustomerCategory obj)
        {

            dbobj.CustomerCategories.Add(obj);
            dbobj.SaveChanges();
            IEnumerable<CustomerCategory> list = dbobj.CustomerCategories.ToList();
            return PartialView("_CategoryListView", list);
        }
    }
}