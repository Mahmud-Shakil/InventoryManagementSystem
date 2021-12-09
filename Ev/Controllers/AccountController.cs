using Ev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ev.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]

        public ActionResult PostLogin(TblUser obj)
        {
            using (var dbobj = new MEVEntities())
            {
                var count = dbobj.TblUsers.Where(u => u.UserName == obj.UserName && u.Password == obj.Password).Count();
                if (count <= 0)
                {
                    
                    ModelState.AddModelError("UserName", "Invalid User Name or Password");
                    return View();
                    
                    
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(obj.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            //return View();

        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ActionName("SignUp")]
        public ActionResult PostSignUp(TblUser obj)
        {
            using (var dbobj = new MEVEntities())
            {
                bool isExists = !dbobj.TblUsers.Any(u => u.UserName == obj.UserName);
                if (isExists)
                {
                    dbobj.TblUsers.Add(obj);
                    dbobj.SaveChanges();
                    int count = dbobj.TblUsers.Count();
                    //if (count==1)
                    //{
                    //    return RedirectToAction("CreateRole","SuperAdmin");
                    //}
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("UserName", "User Name Exists");
                    return View();
                }
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}