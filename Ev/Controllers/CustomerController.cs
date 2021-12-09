using CrystalDecisions.CrystalReports.Engine;
using Ev.Models;
using Ev.Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ev.Controllers
{
    public class CustomerController : Controller
    {
        MEVEntities dbobj = new MEVEntities();
        CustomerRepository repo = new CustomerRepository();
        // GET: Customer
        public JsonResult IsCustomerAvailable(string CustomerName)
        {
            return Json(!repo.GetAllCustomer().Any(e => e.CustomerName == CustomerName), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            System.Threading.Thread.Sleep(3000);
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = SearchString;
            IEnumerable<CustomerViewModel> list = repo.GetAllCustomer();

            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(e => e.CustomerName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(e => e.CustomerName).ToList();
                    break;
                default:
                    list = list.OrderBy(e => e.CustomerName).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ViewResult Create()
        {
            List<CustomerCategory> list = repo.GetCategories();
            ViewBag.Categories = list;
            List<Supplier> suppliers = repo.GetSupplier();
            ViewBag.Suppliers = suppliers;
            List<Product> products = repo.GetProduct();
            ViewBag.Products = products;
            return View();
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            List<CustomerCategory> list = repo.GetCategories();
            ViewBag.Categories = list;
            List<Supplier> suppliers = repo.GetSupplier();
            ViewBag.Suppliers = suppliers;
            List<Product> products = repo.GetProduct();
            ViewBag.Products = products;
            Customer cus = repo.GetCustomer(id);
            CustomerViewModel obj = new CustomerViewModel();
            obj.CustomerId = cus.CustomerId;
            obj.CustomerName = cus.CustomerName;
            obj.Email = cus.Email;
            obj.Address = cus.Address;
            obj.MobileNo = cus.MobileNo;
            obj.Price = cus.Price;
            obj.Quantity = cus.Quantity;
            obj.ImageName = cus.ImageName;
            obj.ImageUrl = cus.ImageUrl;
            obj.OrderDate = cus.OrderDate;
            obj.CategoryId = cus.CategoryId;
            obj.CategoryType = cus.CustomerCategory.CategoryType;
            obj.ProductId = cus.ProductId;
            obj.ProductName = cus.Product.ProductName;
            obj.SupplierName = cus.Product.Supplier.SupplierName;
            ViewBag.Edit = "CustomerEdit";
            return PartialView("_EditCustomer",obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(CustomerViewModel obj)
        {
            var result = false;
            Customer cus;
            if (obj.CustomerId == 0)
            {
                if (ModelState.IsValid)
                {
                    cus = new Customer();
                    cus.CustomerName = obj.CustomerName;
                    cus.Email = obj.Email;
                    cus.Address = obj.Address;
                    cus.MobileNo = obj.MobileNo;
                    cus.OrderDate = obj.OrderDate;
                    cus.CategoryId = obj.CategoryId;
                    cus.ProductId = obj.ProductId;
                    cus.Price = obj.Price;
                    cus.Quantity = obj.Quantity;
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    cus.ImageName = fileName + extension;
                    cus.ImageUrl = "~/Images/" + cus.ImageName;
                    fileName = Path.Combine(Server.MapPath(cus.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                    repo.SaveCustomer(cus);
                    result = true;

                }

            }
            else
            {
                cus = repo.GetCustomer(obj.CustomerId);
                cus.CustomerName = obj.CustomerName;
                cus.Email = obj.Email;
                cus.Address = obj.Address;
                cus.MobileNo = obj.MobileNo;
                cus.OrderDate = obj.OrderDate;
                cus.CategoryId = obj.CategoryId;
                cus.ProductId = obj.ProductId;
                cus.Price = obj.Price;
                cus.Quantity = obj.Quantity;
                if (obj.ImageFile != null)
                {
                    DeleteExistingImage(cus.ImageName);
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    cus.ImageName = fileName + extension;
                    cus.ImageUrl = "~/Images/" + cus.ImageName;
                    fileName = Path.Combine(Server.MapPath(cus.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                }
                else
                {
                    obj.ImageName = cus.ImageName;
                    obj.ImageUrl = cus.ImageUrl;
                }
                repo.UpdateCustomer(cus);
                result = true;
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<CustomerCategory> list = repo.GetCategories();
                ViewBag.Categories = list;
                return View();
            }
        }

        private void DeleteExistingImage(string imageName)
        {
            string root = Server.MapPath("~");
            string parent = Path.GetDirectoryName(root);
            string path = parent + "/Images/" + imageName;
            FileInfo fileobj = new FileInfo(path);
            if (fileobj.Exists)
            {
                fileobj.Delete();
            }
        }
      [HttpGet]
        public PartialViewResult Delete(int id)
        {
            List<CustomerCategory> list = repo.GetCategories();
            ViewBag.Categories = list;
            Customer cus = repo.GetCustomer(id);
            CustomerViewModel obj = new CustomerViewModel();
            obj.CustomerId = cus.CustomerId;
            obj.CustomerName = cus.CustomerName;
            obj.Email = cus.Email;
            obj.Address = cus.Address;
            obj.MobileNo = cus.MobileNo;
            obj.ImageName = cus.ImageName;
            obj.ImageUrl = cus.ImageUrl;
            obj.OrderDate = cus.OrderDate;
            obj.CategoryId = cus.CategoryId;
            obj.Quantity = cus.Quantity;
            obj.Price = cus.Price;
            obj.CategoryType = cus.CustomerCategory.CategoryType;

            ViewBag.Delete = "CustomerDelete";
            return PartialView("_DeletePartial",obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(CustomerViewModel obj)
        {
            Customer cus = repo.GetCustomer(obj.CustomerId);
            string imageName = obj.ImageName;
            DeleteExistingImage(imageName);
            repo.DeleteCusotmer(cus.CustomerId);
           
            return RedirectToAction("Index");
        }
        public PartialViewResult Details(int id)
        {
            List<CustomerCategory> list = repo.GetCategories();
            ViewBag.Categories = list;
            List<Supplier> suppliers = repo.GetSupplier();
            ViewBag.Suppliers = suppliers;
            List<Product> products = repo.GetProduct();
            ViewBag.Products = products;
            Customer cus = repo.GetCustomer(id);
            CustomerViewModel obj = new CustomerViewModel();
            obj.CustomerId = cus.CustomerId;
            obj.CustomerName = cus.CustomerName;
            obj.Email = cus.Email;
            obj.Address = cus.Address;
            obj.MobileNo = cus.MobileNo;
            obj.Price = cus.Price;
            obj.Quantity = cus.Quantity;
            obj.ImageName = cus.ImageName;
            obj.ImageUrl = cus.ImageUrl;
            obj.OrderDate = cus.OrderDate;
            obj.CategoryId = cus.CategoryId;
            obj.CategoryType = cus.CustomerCategory.CategoryType;
            obj.ProductId = cus.ProductId;
            obj.ProductName = cus.Product.ProductName;
            obj.SupplierName = cus.Product.Supplier.SupplierName;
            ViewBag.Details = "Show";
            return PartialView("_DetailsRecord", obj);
        }
        public JsonResult GetProduct()
        {

            return Json(dbobj.Products.Select(x => new
            {
                ProductId = x.ProductId,
               ProductName = x.ProductName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductBySupplierId(string ProductId)
        {
            List<Supplier> list = new List<Supplier>();
            int id = Convert.ToInt32(ProductId);
            return Json(dbobj.Products.Where(c => c.ProductId == id).Select(x => new
            {
                SupplierId = x.SupplierId,
                SupplierName = x.Supplier.SupplierName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            string path = Server.MapPath("~") + "Reports/" + "CrystalReport.rpt";
            rd.Load(path);
            IEnumerable<CustomerViewModel> list = repo.GetAllCustomer();
            string Imagepath = Server.MapPath("~");
            rd.SetDataSource(list.Select(c => new
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                MobileNo=c.MobileNo,
                Email = c.Email,
                OrderDate=c.OrderDate,
                ProductName=c.ProductName,
                Quantity = c.Quantity,
                ImageName = Imagepath + "/Images/" + c.ImageName,
                Price = c.Price,
            }).ToList()); ;
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerInfo.pdf");
        }
    }
}