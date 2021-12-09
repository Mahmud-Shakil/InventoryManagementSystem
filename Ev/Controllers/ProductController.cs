using Ev.Models;
using Ev.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ev.Controllers
{
    public class ProductController : Controller
    {
        CustomerRepository repo = new CustomerRepository();
        MEVEntities dbobj = new MEVEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateProduct()
        {
            List<Supplier> list = repo.GetSupplier();
            ViewBag.Suppliers = list;
            var Model = dbobj.Products.ToList();
          
            return View(Model);
        }
        public ActionResult Product(ProductViewModel obj)
        {
            List<Supplier> list = repo.GetSupplier();
            ViewBag.Suppliers = list;
            Product Pro;
            if (obj.ProductId==0)
            {
                Pro = new Product();
                Pro.ProductId = obj.ProductId;
                Pro.ProductName = obj.ProductName;
                Pro.SupplierId = obj.SupplierId;
               
                repo.SaveProduct(Pro);
            }
         IEnumerable<Product> products = dbobj.Products.ToList();
         return PartialView("_ProductListView",products);
        }
    }
}