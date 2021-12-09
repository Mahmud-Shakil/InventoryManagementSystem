using Ev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ev
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            CreateInitialUserAndRole();
        }

        private void CreateInitialUserAndRole()
        {
            using (var dboj = new MEVEntities())
            {
                int count = dboj.TblUsers.Count();
                if (count == 0)
                {
                    TblUser user = new TblUser();
                    user.UserName = "admin";
                    user.Password = "123456";
                    dboj.TblUsers.Add(user);
                    dboj.SaveChanges();
                    int userId = (from record in dboj.TblUsers orderby record.Id descending select record.Id).First();
                    TbleRole role = new TbleRole();
                    role.RoleName = "Admin";
                    dboj.TbleRoles.Add(role);
                    dboj.SaveChanges();
                    int roleId = (from record in dboj.TbleRoles orderby record.Id descending select record.Id).First();
                    UserWiseRole ur = new UserWiseRole();
                    ur.RoleId = roleId;
                    ur.UserId = userId;
                    dboj.UserWiseRoles.Add(ur);
                    dboj.SaveChanges();
                }
            }
        }

        public class FilterConfig
        {
            public static void RegisterGlobalFilters(GlobalFilterCollection filters)
            {
                //filters.Add(new HandleErrorAttribute());
            }
        }
    }
}
